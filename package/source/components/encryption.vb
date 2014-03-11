Imports System.Security.Cryptography
Imports System.Text

Namespace DONEIN_NET.Feedback

	Public Class Encryption
	
	
	
		#Region " Encrypt: String "
		
			Public Function encrypt_string(ByVal str_data As String, Optional ByVal str_salt As String = "", Optional ByVal str_crypto As String = "MD5") As String
			
				'// CREATES MD5 OR SHA1 ENCRYPTED STRING
				Dim hashed_bytes As Byte()   
				Dim encoder As New UTF8Encoding()
				Dim md5_hasher As New MD5CryptoServiceProvider()
				Dim sha1_hasher As New SHA1CryptoServiceProvider()
				
				If str_crypto.Trim.ToLower = "sha1" Then
					str_salt = Convert.ToBase64String(sha1_hasher.ComputeHash(encoder.GetBytes(str_salt)))
					hashed_bytes = sha1_hasher.ComputeHash(encoder.GetBytes(str_salt + str_data + str_salt))
				Else
					str_salt = Convert.ToBase64String(md5_hasher.ComputeHash(encoder.GetBytes(str_salt)))
					hashed_bytes = md5_hasher.ComputeHash(encoder.GetBytes(str_salt + str_data + str_salt))
				End If				
		
				Return Convert.ToBase64String(hashed_bytes).TrimEnd("=".ToCharArray())
				
			End Function
			
		#End Region
	
	
			    
	End Class
	
End Namespace
	
