Imports System
Imports System.Net.Mail

Namespace DONEIN_NET.Feedback

	Public Class Mail

		Public Function mail_send(recipient As String, sender As String, body As String, Optional subject As String = "", Optional cc As String = "", Optional bcc As String = "", Optional reply_to As String = "", Optional priority As String = "Normal", Optional smtp_server As String = "") As String
			
			Dim mail_object As New MailMessage
			Dim mail_client As New SmtpClient
			Dim mail_credentials As New System.Net.NetworkCredential


			'// === SET THE SENDER === 
			mail_object.From = New MailAddress(sender.Trim)
			
			
			'// === SET THE REPLY TO ADDRESS ===
			If reply_to.Trim.Length > 0 Then
				Try
					mail_object.ReplyTo = New MailAddress(reply_to)
				Catch ex As Exception
					mail_object.ReplyTo = New MailAddress(sender.Trim)
				End Try
			Else
				mail_object.ReplyTo = New MailAddress(sender.Trim)
			End If
			

			'// === SET THE RECIPIENTS ===
			For Each recipient_address As String In Split(recipient.Trim, ";")
				If recipient_address.Trim.Length >= 5 Then
					mail_object.To.Add(recipient_address.Trim)
				End If
			Next
			
			For Each cc_address As String In Split(cc.Trim, ";")
				If cc_address.Trim.Length >= 5 Then
					mail_object.CC.Add(cc_address.Trim)
				End If
			Next
			
			For Each bcc_address As String In Split(bcc.Trim, ";")
				If bcc_address.Trim.Length >= 5 Then
					mail_object.BCC.Add(bcc_address.Trim)
				End If
			Next

			'// === SET THE SUBJECT ===
			mail_object.Subject  = subject.Trim

			'// === SET THE MESSAGE BODY TEXT ===
			mail_object.Body = body.Trim

			'// === SET PRIORITY OF MESSAGE (DEFAULT IS NORMAL) ===
			If priority.Trim = "High" Then
				mail_object.Priority = MailPriority.High
			ElseIf priority.Trim = "Low" Then
				mail_object.Priority = MailPriority.Low
			Else
				mail_object.Priority = MailPriority.Normal
			End If

			'// === SET FORMAT OF THE MAIL ===
			mail_object.IsBodyHtml = True
		 	
		 	'// === SET THE SMTP SERVER & PORT ===
		 	smtp_server = smtp_server.Trim.ToLower
		 	
		 	If smtp_server.Contains(":") = True Then
				Dim smtp_host As String = ""
				Dim smtp_port As String = ""
				Dim colon_position As Integer = 0
				smtp_port = Right(smtp_server, smtp_server.Length - (smtp_server.IndexOf(":") + 1)).Trim
				smtp_host = smtp_server.Replace(":" + smtp_port, "")
				mail_client.Host = smtp_host
				If smtp_port.Length > 0 Then
					Try
						mail_client.Port = Cint(smtp_port)
					Catch ex As Exception
						Return ("<SPAN STYLE=""font-weight: bold;"">Could Not Send Mail On Port: </SPAN>" + smtp_port)
					End Try
				Else
					mail_client.Port = 25
				End If
			Else
				If smtp_server.Length > 0 Then
					mail_client.Host = smtp_server
				Else
					mail_client.Host = "127.0.0.1"
				End If
			End If
			
			
			'// === SET THE USERNAME AND PASSWORD ===
			Dim host_settings As HashTable =  DotNetNuke.Common.HostSettings
			If host_settings.Item("SMTPUsername").Length > 0 Then
				mail_credentials.UserName = host_settings.Item("SMTPUsername").Length
				mail_credentials.Password = host_settings.Item("SMTPPassword").Length
				mail_client.Credentials = mail_credentials
			End If
			
		
			'// === SEND THE MESSAGE, RETURN ANY ERRORS ===
			Try
				mail_object.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
				mail_client.Send(mail_object)
				Return ""
			Catch mail_exception as Exception
				Return mail_exception.ToString
			End Try

		End Function
		    
	End Class

End NameSpace


