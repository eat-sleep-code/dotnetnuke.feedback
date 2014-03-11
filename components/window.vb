Imports System.Web

Namespace components

Public Class window

	'// THIS CLASS GENERATES COMMONLY USED JAVASCRIPT USED TO HANDLE BROWSER WINDOWS, PRINTING, ALERTS, ETC.



	'// OPEN A NEW WINDOW (OVERLOADED)
	Public Sub open(url As String)
		window_handler("var window_open=window.open(""" + url.ToString + """, ""popup"", ""toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no"")")
	End Sub

	Public Sub open(url As String, width As Integer, height As Integer)
		window_handler("var window_open=window.open(""" + url.ToString + """, ""popup"", ""width=" + width.ToString + ",height=" + height.ToString + ",toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no"")")
	End Sub
	
	Public Sub open(url As String, width As Integer, height As Integer, toolbar As String, scrollbars As String, resizable As String)
		window_handler("var window_open=window.open(""" + url.ToString + """, ""popup"", ""width=" + width.ToString + ",height=" + height.ToString + ",toolbar=" + toolbar.ToString + ",location=no,status=no,menubar=no,scrollbars=" + scrollbars.ToString + ",resizable=" + resizable.ToString + ",copyhistory=no"")")
	End Sub

	Public Sub open(url As String, width As Integer, height As Integer, toolbar As String, location As String, status As String, menubar As String, scrollbars As String, resizable As String, copyhistory As String)
		window_handler("var window_open=window.open(""" + url.ToString + """, ""popup"", ""width=" + width.ToString + ",height=" + height.ToString + ",toolbar=" + toolbar.ToString + ",location=" + location.ToString + ",status=" + status.ToString + ",menubar=" + menubar.ToString + ",scrollbars=" + scrollbars.ToString + ",resizable=" + resizable.ToString + ",copyhistory=" + copyhistory.ToString + """)")
	End Sub



	'// CLOSE THE CURRENT WINDOW
	Public Sub close()
		window_handler("var window_close=window.close()")
	End Sub



	'// BRING THE CURRENT WINDOW INTO FOCUS
	Public Sub focus()
		window_handler("var window_focus=window.focus()")
	End Sub



	'// PUSH THE CURRENT WINDOW OUT OF FOCUS
	Public Sub blur()
		window_handler("var window_blur=window.blur()")
	End Sub



	'// MOVE THE CURRENT WINDOW TO THE SPECIFIED X AND Y POSITION
	Public Sub moveto(x As Integer,y As Integer)
		window_handler("var window_move_to=window.moveTo(" + x.ToString + ", " + y.ToString + ")")
	End Sub



	'// SCROLL THE CURRENT WINDOW TO THE SPECIFIED X AND Y POSITION (OVERLOADED)
	Public Sub scrollto(y As Integer)
		window_handler("var window_scroll_to=window.scrollTo(0," + y.ToString + ")")
	End Sub

	Public Sub scrollto(x As Integer,y As Integer)
		window_handler("var window_scroll_to=window.scrollTo(" + x.ToString + ", " + y.ToString + ")")
	End Sub



	'// CREATE AN ALERT MESSAGE BOX
	Public Sub alert(message As String)
		window_handler("var window_alert=window.alert(""" + message.ToString + """)")
	End Sub



	'// CREATE AN CONFIRMATION MESSAGE BOX
	Public Sub confirm(message As String)
		window_handler("var window_confirm=window.confirm(""" + message.ToString + """)")
	End Sub

	Public Sub confirm(message As String, failure_url As String)
		window_handler("var success=window.confirm(""" + message.ToString + """)" + vbcrlf + "if (! success)" + vbcrlf + "location.href=""" + failure_url.ToString + """")
	End Sub

	Public Sub confirm(message As String, success_url As String, failure_url As String)
		window_handler("var success=window.confirm(""" + message.ToString + """)" + vbcrlf + "if (success)" + vbcrlf + "location.href=""" + success_url.ToString + """" + vbcrlf + "else" + vbcrlf + "location.href=""" + failure_url.ToString + """")
	End Sub



	'// OPEN THE PRINT DIALOG BOX
	Public Sub print()
		window_handler("window.print()")
	End Sub

	

	'// THIS FUNCTION IS CALLED BY THE ABOVE FUNCTIONS TO GENERATE THE JAVASCRIPT
	Public Sub window_handler(action_string As String)
		HttpContext.Current.Response.Write(vbcrlf)
		HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" + vbcrlf)
		HttpContext.Current.Response.Write("{" + vbcrlf)
		HttpContext.Current.Response.Write(action_string + vbcrlf)
		HttpContext.Current.Response.Write("}" + vbcrlf)
		HttpContext.Current.Response.Write("</SCRIPT>" + vbcrlf)
	End Sub

End Class

End NameSpace