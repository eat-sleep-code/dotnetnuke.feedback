Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Exceptions

Namespace DONEIN_NET.Feedback

	Public Class Base
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        Implements Entities.Modules.ISearchable
		
		
		
		#Region " Declare: Shared Classes "

			Private database As New database(System.Configuration.ConfigurationSettings.AppSettings("SiteSqlServer"), "SqlClient")
			Private mail As New Mail()
			Private module_info As New Module_Info()
			
		#End Region





		#Region " Declare: Local Objects "
			
			Protected WithEvents lbl_message As System.Web.UI.WebControls.Label
			Protected WithEvents tbl_feedback As System.Web.UI.HtmlControls.HtmlTable
		
			Protected WithEvents tr_sender_name As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_name As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_sender_email As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_email As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_sender_account_number As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_account_number As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_sender_address As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents ddl_sender_address_country As System.Web.UI.WebControls.DropDownList
			Protected WithEvents ltl_sender_address_country As System.Web.UI.WebControls.Literal
			Protected WithEvents txt_sender_address_street_01 As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_sender_address_street_02 As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_sender_address_city As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_sender_address_zipcode As System.Web.UI.WebControls.TextBox
			Protected WithEvents ddl_sender_address_state_province As System.Web.UI.WebControls.DropDownList
			Protected WithEvents tr_sender_phone As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_phone As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_sender_fax As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_fax As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_sender_url As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_sender_url As System.Web.UI.WebControls.TextBox
			Protected WithEvents tr_subject As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents txt_subject As System.Web.UI.WebControls.TextBox
			Protected WithEvents ddl_subject As System.Web.UI.WebControls.DropDownList
			Protected WithEvents txt_body As DotNetNuke.UI.UserControls.TextEditor
			Protected WithEvents tr_send_copy As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents chk_send_copy As System.Web.UI.WebControls.CheckBox
			Protected WithEvents tr_captcha As System.Web.UI.HtmlControls.HtmlTableRow
			Protected WithEvents dnn_captcha As DotNetNuke.UI.WebControls.CaptchaControl
			Protected WithEvents btn_send As System.Web.UI.WebControls.LinkButton
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton
			Protected WithEvents lbl_divider As System.Web.UI.WebControls.Label
			Protected WithEvents ltl_script As System.Web.UI.WebControls.Literal
			
			Private obj_user As New UserController
			Private obj_user_info As UserInfo  = obj_user.GetCurrentUserInfo
			
		#End Region





		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				If Request.QueryString.Item("debug") <> "" Then
					module_info.get_info(Request.QueryString.Item("debug").Trim, ModuleID, TabID)
				End If
				
				ltl_script.Text = "<SCRIPT LANGUAGE=""JavaScript"" SRC=""" + ModulePath + "scripts/feedback.js""></SCRIPT>"
				
				Try
					If Not IsPostBack Then
						'//Session.Add("previous_text_editor_default_mode", txt_body.DefaultMode)
						Session.Add("previous_text_editor_mode", txt_body.Mode)
				
						Session.Add("tmp_referrer", Request.ServerVariables.Item("http_referer") + "")
						txt_sender_email.Text = obj_user_info.Membership.Email
						txt_sender_name.Text = obj_user_info.Profile.FullName.Tostring
						
					End If
					
					initialize_form()
					tbl_feedback.Visible = True
					
				Catch ex As Exception  
					ProcessModuleLoadException(Me, ex)
				End Try
				
			End Sub

		#End Region
		
		
		
		
		
		#Region " Page: Unload "

			Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

				Try
					'//txt_body.DefaultMode = Session.Item("previous_text_editor_default_mode")
					'//Session.Remove("previous_text_editor_default_mode")
					txt_body.Mode = Session.Item("previous_text_editor_mode").ToString
					Session.Remove("previous_text_editor_mode")
				Catch ex As Exception
					'// SUPPRESS ERROR MESSAGES
				End Try
						
			End Sub

		#End Region		
	
	



		#Region " Page: PreRender "

			Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
				module_localize() '// LOCALIZE THE MODULE
			End Sub

		#End Region





		#Region " Page: Localization "

 			Private Sub module_localize()
				lbl_divider.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_lbl_divider.Text", LocalResourceFile)
			End Sub 

		#End Region





		#Region " Bind: Subject Dropdown (ddl_subject) "
		
			Private Sub ddl_subject_bind()
				If ddl_subject.Items.Count < 1 Then
					Dim dt_subject As New DataTable
					database.CreateCommand("donein_feedback_subject_R", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", "0")
					database.AddParameter("@int_module", ModuleID.ToString) 
					database.Execute(dt_subject)
					If dt_subject.Rows.Count > 0 Then
						txt_subject.Visible = False
						ddl_subject.Visible = True
						ddl_subject.DataSource = dt_subject
						ddl_subject.DataTextField = "vch_subject"
						ddl_subject.DataValueField = "ID"
						ddl_subject.DataBind()
					Else
						txt_subject.Visible = True
						ddl_subject.Visible = False
					End If				
					dt_subject.Clear
					dt_subject.Dispose		
				End If
			End Sub			
			
		#End Region		
		
		
		
		
		
		#Region " Bind: Initialize Feedback Form "

			Private Sub initialize_form()
			
				'// GET PORTAL VERSION IN INTEGER FORM
				Dim tmp_portal_version As String = (glbAppVersion.ToString + "").Trim
				Dim tmp_portal_version_number As Integer = Convert.ToInt32(tmp_portal_version.Replace(".",""))
			
				
				Dim tmp_feedback_show_sender As Integer = CType(Settings("donein_feedback_show_sender"), Integer)
				Dim tmp_feedback_show_account_number As Integer = CType(Settings("donein_feedback_show_account_number"), Integer)
				Dim tmp_feedback_show_address As Integer = CType(Settings("donein_feedback_show_address"), Integer)
				Dim tmp_feedback_show_phone As Integer = CType(Settings("donein_feedback_show_phone"), Integer)
				Dim tmp_feedback_show_fax As Integer = CType(Settings("donein_feedback_show_fax"), Integer)
				Dim tmp_feedback_show_url As Integer = CType(Settings("donein_feedback_show_url"), Integer)
				Dim tmp_feedback_show_subject As Integer = CType(Settings("donein_feedback_show_subject"), Integer)
				Dim tmp_feedback_show_RTE As Integer = CType(Settings("donein_feedback_show_RTE"), Integer)
				Dim tmp_feedback_show_send_copy As Integer = CType(Settings("donein_feedback_show_send_copy"), Integer)
				Dim tmp_feedback_enable_captcha As String = CType(Settings("donein_feedback_enable_captcha"), String)
				
					
				'// SET WIDTH AND HEIGHT ATTRIBUTES
				set_dimensions(CType(Settings("donein_feedback_textbox_width"), String), CType(Settings("donein_feedback_textbox_height"), String))
				
				'// =======================================================================================================
				
				If tmp_feedback_show_sender >= 0 Then
					tr_sender_name.Visible = True
					tr_sender_email.Visible = True
				Else
					tr_sender_name.Visible = False
					tr_sender_email.Visible = False
				End If	
				
				'// =======================================================================================================
				
				If tmp_feedback_show_account_number >= 0 Then
					tr_sender_account_number.Visible = True
				Else
					tr_sender_account_number.Visible = False
				End If
				
				'// =======================================================================================================
				
				If tmp_feedback_show_address >= 0 Then
					tr_sender_address.Visible = True
					If Not IsPostBack Then
						Dim dt_country As New DataTable
						dt_country = get_list_entries("Country", "")
						If tmp_feedback_show_address = 3 Then
							'// RENDER COUNTRY LIST 
							For Each dr As DataRow In dt_country.Rows()
								ddl_sender_address_country.Items.Add(New ListItem(dr.Item("Text").ToString, dr.Item("EntryID").ToString))
							Next
							'// RENDER STATE / PROVINCE LIST
							get_list_entries_state_province()
						Else If tmp_feedback_show_address = 2 Then
							'// RENDER COUNTRY LIST WITH US AND CANADA ONLY
							For Each dr As DataRow In dt_country.Rows()
								If dr.Item("Value").ToString = "US" Or dr.Item("Value").ToString = "CA" Then
									ddl_sender_address_country.Items.Add(New ListItem(dr.Item("Text").ToString, dr.Item("EntryID").ToString))
								End If
							Next
							'// RENDER STATE / PROVINCE LIST
							get_list_entries_state_province()
						Else
							'// HIDE COUNTRY LIST
							ddl_sender_address_country.Visible = False
							ltl_sender_address_country.Visible = False
							'// RENDER STATE / PROVINCE LIST
							get_list_entries_state_province("Country.US")
						End If
						
						dt_country.Dispose
					End If
					
					If txt_sender_address_street_01.Text.Trim.Length = 0 Then
						txt_sender_address_street_01.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_01.Text", LocalResourceFile)
						txt_sender_address_street_01.Attributes.Add("class", "donein_feedback_form_text_light")
						txt_sender_address_street_01.Attributes.Add("onfocus", "if (this.value == '" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_01.Text", LocalResourceFile) + "') { this.value=''; this.setAttribute('class', 'donein_feedback_form_text_normal'); this.setAttribute('className', 'donein_feedback_form_text_normal'); }")
						txt_sender_address_street_01.Attributes.Add("onblur", "if (this.value == '') { this.value='" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_01.Text", LocalResourceFile) + "';  this.setAttribute('class', 'donein_feedback_form_text_light'); this.setAttribute('className', 'donein_feedback_form_text_light'); }")
					End If
					If txt_sender_address_street_02.Text.Trim.Length = 0 Then
						txt_sender_address_street_02.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_02.Text", LocalResourceFile)
						txt_sender_address_street_02.Attributes.Add("class", "donein_feedback_form_text_light")
						txt_sender_address_street_02.Attributes.Add("onfocus", "if (this.value == '" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_02.Text", LocalResourceFile) + "') { this.value=''; this.setAttribute('class', 'donein_feedback_form_text_normal'); this.setAttribute('className', 'donein_feedback_form_text_normal'); }")
						txt_sender_address_street_02.Attributes.Add("onblur", "if (this.value == '') { this.value='" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_02.Text", LocalResourceFile) + "';  this.setAttribute('class', 'donein_feedback_form_text_light'); this.setAttribute('className', 'donein_feedback_form_text_light');}")
					End If
					If txt_sender_address_city.Text.Trim.Length = 0 Then
						txt_sender_address_city.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_city.Text", LocalResourceFile)
						txt_sender_address_city.Attributes.Add("class", "donein_feedback_form_text_light")
						txt_sender_address_city.Attributes.Add("onfocus", "if (this.value == '" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_city.Text", LocalResourceFile) + "') { this.value=''; this.setAttribute('class', 'donein_feedback_form_text_normal'); this.setAttribute('className', 'donein_feedback_form_text_normal'); }")
						txt_sender_address_city.Attributes.Add("onblur", "if (this.value == '') { this.value='" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_city.Text", LocalResourceFile) + "';  this.setAttribute('class', 'donein_feedback_form_text_light'); this.setAttribute('className', 'donein_feedback_form_text_light');}")
					End If
					If txt_sender_address_zipcode.Text.Trim.Length = 0 Then
						txt_sender_address_zipcode.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_zipcode.Text", LocalResourceFile)
						txt_sender_address_zipcode.Attributes.Add("class", "donein_feedback_form_text_light")
						txt_sender_address_zipcode.Attributes.Add("onfocus", "if (this.value == '" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_zipcode.Text", LocalResourceFile) + "') { this.value=''; this.setAttribute('class', 'donein_feedback_form_text_normal'); this.setAttribute('className', 'donein_feedback_form_text_normal'); }")
						txt_sender_address_zipcode.Attributes.Add("onblur", "if (this.value == '') { this.value='" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_zipcode.Text", LocalResourceFile) + "';  this.setAttribute('class', 'donein_feedback_form_text_light'); this.setAttribute('className', 'donein_feedback_form_text_light');}")
					End If
				Else
					tr_sender_address.Visible = False
				End If
				
				'// =======================================================================================================
				
				If tmp_feedback_show_phone >= 0 Then
					tr_sender_phone.Visible = True
				Else
					tr_sender_phone.Visible = False
				End If
				
				
				'// =======================================================================================================
				
				If tmp_feedback_show_fax >= 0 Then
					tr_sender_fax.Visible = True
				Else
					tr_sender_fax.Visible = False
				End If
				
				'// =======================================================================================================
				
				If tmp_feedback_show_url >= 0 Then
					tr_sender_url.Visible = True
				Else
					tr_sender_url.Visible = False
				End If
				
				'// =======================================================================================================
				
				Dim tmp_subject_querystring As String
				If Request.QueryString.Item("feedback_subject") <> Nothing Then
					tmp_subject_querystring =  Request.QueryString.Item("feedback_subject").Trim
				Else
					tmp_subject_querystring = ""
				End If						
							
				If tmp_feedback_show_subject >= 0 And tmp_subject_querystring <> "hide" Then
					tr_subject.Visible = True
					If tmp_feedback_show_subject = 1 Then
						ddl_subject.Visible = True
						txt_subject.Visible = False
						ddl_subject_bind()
					Else
						ddl_subject.Visible = False
						txt_subject.Visible = True
					End If							
				Else
					tr_subject.Visible = False
				End If
				
				If Not IsPostBack Then
					If Request.QueryString.Item("feedback_SID") <> Nothing Then
						Try	
							ddl_subject.SelectedValue = Request.QueryString.Item("feedback_SID").Trim
						Catch ex As Exception
							'// SUPPRESS ERROR WHEN NO MATCHING RECORD FOUND IN DROPDOWN
						End Try	
					Else
						Dim i As Integer 
						For i = 0 To 100
							Try
								ddl_subject.SelectedValue = i.ToString
								Exit For
							Catch ex As Exception
								'// SUPPRESS ERROR WHEN NO MATCHING RECORD FOUND IN DROPDOWN
							End Try						
						Next											
					End If
				End If
										
				'// =======================================================================================================
	
				If tmp_feedback_show_RTE > 0 Then
					'//txt_body.DefaultMode = "R"
					txt_body.Mode = "R"
				Else
					'//txt_body.DefaultMode = "BASIC"
					txt_body.Mode = "BASIC"
				End If	
				
				If tmp_feedback_show_send_copy > 0 Then
					tr_send_copy.Visible = True
				Else
					tr_send_copy.Visible = False
				End If
				
				txt_body.ChooseMode = False
				txt_body.ChooseRender = False
				
				'// =======================================================================================================
				
				If (tmp_portal_version_number >= 40300) And ((tmp_feedback_enable_captcha = "1") OR (tmp_feedback_enable_captcha = "0" And (obj_user_info.Username + "").Trim = "")) Then
				'	Response.Write("CAPTCHA Enabled: " + tmp_portal_version_number.ToString + "<BR />Username: " + obj_user_info.Username + "<BR />")
					tr_captcha.Visible = True
				Else
				'	Response.Write("CAPTCHA Disabled: " + tmp_portal_version_number.ToString + "<BR />Username: " + obj_user_info.Username + "<BR />")
					tr_captcha.Visible = False
				End If	
				
				'// =======================================================================================================
				
			End Sub	
			
			
			
			Public Function parse_unit(Optional ByVal unit_value As Integer = 512) As Unit
				Return System.Web.UI.WebControls.Unit.Parse(unit_value)	
			End Function	
			
			
			
			Private Sub set_dimensions(ByVal width As String, ByVal height As String)
				
				Dim tmp_feedback_textbox_width As String = ""
				Dim tmp_feedback_textbox_height As String = "" 

				If Not width = Nothing Then
					tmp_feedback_textbox_width = width
				End If
				
				If Not height = Nothing Then
					tmp_feedback_textbox_height = height
				End If
				
				If tmp_feedback_textbox_height <> "" Then
					txt_body.height = parse_unit(tmp_feedback_textbox_height)
				Else
					txt_body.Height = parse_unit("256")
				End If

				If tmp_feedback_textbox_width.Trim = "" Then
					tmp_feedback_textbox_width = "512"
				End If

				lbl_message.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_name.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_email.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_account_number.Width = parse_unit(tmp_feedback_textbox_width)
				ddl_sender_address_country.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_address_street_01.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_address_street_02.Width = parse_unit(tmp_feedback_textbox_width)
				If tmp_feedback_textbox_width >= 400 Then
					txt_sender_address_city.Width = parse_unit(tmp_feedback_textbox_width - 254)
				Else
					txt_sender_address_city.Width = parse_unit(tmp_feedback_textbox_width)
				End If
				'ddl_sender_address_state_province.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_phone.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_fax.Width = parse_unit(tmp_feedback_textbox_width)
				txt_sender_url.Width = parse_unit(tmp_feedback_textbox_width)
				txt_subject.Width = parse_unit(tmp_feedback_textbox_width)
				ddl_subject.Width = parse_unit(tmp_feedback_textbox_width)
				txt_body.Width = parse_unit(tmp_feedback_textbox_width)
			
			End Sub	
			
		#End Region
				
		
		
		
		
		#Region " Handle: Send Button (btn_send) "

			Private Sub btn_send_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_send.Click
				'initialize_form()
				If txt_sender_address_street_01.Text.Trim = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_01.Text", LocalResourceFile) Then
					txt_sender_address_street_01.Text = ""
				Else
					txt_sender_address_street_01.Attributes.Add("class", "donein_feedback_form_text_normal")
				End If
				If txt_sender_address_street_02.Text.Trim = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_street_02.Text", LocalResourceFile) Then
					txt_sender_address_street_02.Text = ""
				Else
					txt_sender_address_street_02.Attributes.Add("class", "donein_feedback_form_text_normal")
				End If
				If txt_sender_address_city.Text.Trim = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_city.Text", LocalResourceFile) Then
					txt_sender_address_city.Text = ""
				Else
					txt_sender_address_city.Attributes.Add("class", "donein_feedback_form_text_normal")
				End If
				If txt_sender_address_zipcode.Text.Trim = DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address_zipcode.Text", LocalResourceFile) Then
					txt_sender_address_zipcode.Text = ""
				Else
					txt_sender_address_zipcode.Attributes.Add("class", "donein_feedback_form_text_normal")
				End If
						
				Dim tmp_feedback_show_account_number As Integer = CType(Settings("donein_feedback_show_account_number"), Integer)
				Dim tmp_feedback_show_address As Integer = CType(Settings("donein_feedback_show_address"), Integer)
				Dim tmp_feedback_show_phone As Integer = CType(Settings("donein_feedback_show_phone"), Integer)
				Dim tmp_feedback_show_fax As Integer = CType(Settings("donein_feedback_show_fax"), Integer)
				Dim tmp_feedback_show_url As Integer = CType(Settings("donein_feedback_show_url"), Integer)
				
				Dim tmp_feedback_require_account_number As Integer = CType(Settings("donein_feedback_require_account_number"), Integer)
				Dim tmp_feedback_require_address As Integer = CType(Settings("donein_feedback_require_address"), Integer)
				Dim tmp_feedback_require_phone As Integer = CType(Settings("donein_feedback_require_phone"), Integer)
				Dim tmp_feedback_require_fax As Integer = CType(Settings("donein_feedback_require_fax"), Integer)
				Dim tmp_feedback_require_url As Integer = CType(Settings("donein_feedback_require_url"), Integer)
				
				'// CHECK REQUIRED FIELDS ============================================================================
				lbl_message.Text = ""
				
				If tr_captcha.Visible = True Then
					If dnn_captcha.IsValid = False Then
							lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_verification_failed", Me.LocalResourceFile) + "<BR />"	
					End If	
				End If
				
				If (txt_sender_name.Text.Trim = "" Or txt_sender_email.Text.Trim = "") Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_sender_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If ((txt_sender_email.Text.Contains("@") = False) Or (txt_sender_email.Text.EndsWith(".") = True) Or (txt_sender_email.Text.Contains("@") = True And Right(txt_sender_email.Text, txt_sender_email.Text.Length - InStr(txt_sender_email.Text, "@")).ToString.ToString.Contains(".") = False)) Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_invalid_email", Me.LocalResourceFile) + "<BR />"
				End If
			
				If tmp_feedback_require_account_number >= 0 And txt_sender_account_number.Text.Trim = "" Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_account_number_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_address >= 0 And ddl_sender_address_country.Visible = True And ddl_sender_address_country.SelectedIndex = -1 Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_address_country_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_address >= 0 And txt_sender_address_street_01.Text.Trim = "" Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_address_street_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_address >= 0 And txt_sender_address_city.Text.Trim = "" Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_address_city_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_address >= 0 And ddl_sender_address_state_province.Visible = True And ddl_sender_address_state_province.SelectedIndex = -1 Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_address_state_province_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_address >= 0 And txt_sender_address_zipcode.Text.Trim = "" Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_address_zipcode_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_phone >= 0 And txt_sender_phone.Text.Trim = "" Then
						lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_phone_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_fax >= 0 And txt_sender_fax.Text.Trim = "" Then
						lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_fax_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If tmp_feedback_require_url >= 0 And (txt_sender_url.Text.Trim = "" Or txt_sender_url.Text.Trim.Contains(".") = False) Then
						lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_url_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If txt_subject.Visible = True And txt_subject.Text = "" Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_subject_required", Me.LocalResourceFile) + "<BR />"
				End If
				
				If txt_body.Text.Trim.Length <= 3 Then
					lbl_message.Text += DotNetNuke.Services.Localization.Localization.Getstring("alert_body_required", Me.LocalResourceFile) + "<BR />"
				End If	
								
				If lbl_message.Text.Trim.Length > 0 Then
					lbl_message.Visible = True
					initialize_form()
					Exit Sub
				Else
					lbl_message.Visible = False
				End If				
				'// ==================================================================================================
										
				'// GET THE RECIPIENT EMAIL ADDRESS ===================================================================
					Dim tmp_feedback_show_subject As Integer = CType(Settings("donein_feedback_show_subject"), Integer)
					Dim tmp_feedback_default_recipient As String = CType(Settings("donein_feedback_default_recipient"), String)
					Dim tmp_feedback_default_sender As String = CType(Settings("donein_feedback_default_sender"), String)
					Dim tmp_feedback_default_sender_email As String = ""
					
				'// GET THE SENDER EMAIL ADDRESS ===================================================================
					If tmp_feedback_default_sender = "1" Then
						tmp_feedback_default_sender_email = txt_sender_email.Text.Trim		
					Else If tmp_feedback_default_sender = "0" Then
						tmp_feedback_default_sender_email = PortalSettings.Email.ToString.Trim
					Else
						tmp_feedback_default_sender_email = HostSettings("HostEmail").ToString.Trim
					End If					
					
					
					
					
					Dim tmp_recipient_email As String
					Dim tmp_subject As String
					Dim tmp_success_message As String
					If tmp_feedback_show_subject = 1 Then
						Dim dt_subject As New DataTable
						database.CreateCommand("donein_feedback_subject_R", CommandType.StoredProcedure)
						database.AddParameter("@int_ID", ddl_subject.SelectedValue) 
						database.AddParameter("@int_module", ModuleID.ToString) 
						database.Execute(dt_subject)
						If dt_subject.Rows.Count > 0 Then
							tmp_recipient_email = dt_subject.Rows(0).Item("vch_recipient").ToString
							tmp_subject = dt_subject.Rows(0).Item("vch_subject").ToString
							If dt_subject.Rows(0).Item("vch_success_message").ToString.Trim.Length > 0 Then
								tmp_success_message = dt_subject.Rows(0).Item("vch_success_message").ToString
							Else
								tmp_success_message = DotNetNuke.Services.Localization.Localization.Getstring("alert_message_sent", Me.LocalResourceFile)
							End If
						Else
							tmp_recipient_email = HostSettings("HostEmail").ToString.Trim
							tmp_subject = txt_subject.Text.Trim
							tmp_success_message = DotNetNuke.Services.Localization.Localization.Getstring("alert_message_sent", Me.LocalResourceFile)
						End If				
						dt_subject.Clear
						dt_subject.Dispose						
					Else
						If tmp_feedback_default_recipient = "" Then
							tmp_recipient_email = HostSettings("HostEmail").ToString.Trim
						Else
							tmp_recipient_email = tmp_feedback_default_recipient.Trim
						End If
						tmp_subject = txt_subject.Text.Trim
						tmp_success_message = DotNetNuke.Services.Localization.Localization.Getstring("alert_message_sent", Me.LocalResourceFile)
					End If
									
				'// ==================================================================================================
				
				
				If txt_sender_email.Text <> "" Then
					Dim mail_status As String = ""
					Dim tmp_referrer As String = Session.Item("tmp_referrer").ToString + ""
					Dim tmp_ip_address As String = Request.ServerVariables.Item("REMOTE_ADDR") + ""
					Dim tmp_body As String = "<DIV STYLE=""font-family: Arial; font-size: 10pt;"">" + txt_body.Text.Trim & "<BR /><HR NOSHADE><SPAN STYLE=""font-weight: bold;""><A HREF=""mailto:" + txt_sender_email.Text.Trim +"?subject=RE:&nbsp;" + tmp_subject.Trim.Replace(" ","&nbsp;") + """>" + txt_sender_name.Text.Trim + " </SPAN> (" + txt_sender_email.Text.Trim + ")</A><BR /><SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_referrer.Text", LocalResourceFile) + " </SPAN><SPAN STYLE=""font-style: italic;"">" + tmp_referrer + "</SPAN><BR /><SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_ip_address.Text", LocalResourceFile) + " </SPAN><SPAN STYLE=""font-style: italic;"">" + tmp_ip_address + "</SPAN></DIV>"
					Dim tmp_body_additional_info As String = "<HR NOSHADE>"
					tmp_body = tmp_body.Replace("&amp;", "&").Replace("&nbsp;", " ").Replace("&lt;","<").Replace("&gt;",">")
					
					'// PREPARE THE ADDITIONAL INFORMATION ==========================================================
					If tmp_feedback_show_account_number >= 0 Then
						tmp_body_additional_info += "<SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_account_number.Text", LocalResourceFile) + "</SPAN><BR />"
						tmp_body_additional_info += txt_sender_account_number.Text.Trim + "<BR />"
						tmp_body_additional_info += "<HR NOSHADE>"
					End If
					Dim tmp_state_province_text As String = ""
					Dim tmp_country_text As String = ""
					If tmp_feedback_show_address >= 0 Then
						tmp_body_additional_info += "<SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_address.Text", LocalResourceFile) + "</SPAN><BR />"
						tmp_body_additional_info += txt_sender_address_street_01.Text.Trim + "<BR />"
						If txt_sender_address_street_02.Text.Trim.Length > 0 Then
							tmp_body_additional_info += txt_sender_address_street_02.Text.Trim + "<BR />"
						End If
						tmp_body_additional_info += txt_sender_address_city.Text.Trim + ", " + ddl_sender_address_state_province.SelectedItem.Text.Trim + " " + txt_sender_address_zipcode.Text.Trim + "<BR />"
						tmp_state_province_text = ddl_sender_address_state_province.SelectedItem.Text.Trim
						If ddl_sender_address_country.SelectedIndex >= 0 Then
							tmp_body_additional_info += ddl_sender_address_country.SelectedItem.Text.Trim + "<BR />"
							tmp_country_text = ddl_sender_address_country.SelectedItem.Text.Trim
						End If
						tmp_body_additional_info += "<HR NOSHADE>"
						
					End If
					If tmp_feedback_show_phone >= 0 Then
						tmp_body_additional_info += "<SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_phone.Text", LocalResourceFile) + "</SPAN><BR />"
						tmp_body_additional_info += txt_sender_phone.Text.Trim + "<BR />"
						tmp_body_additional_info += "<HR NOSHADE>"
					End If
					If tmp_feedback_show_fax >= 0 Then
						tmp_body_additional_info += "<SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_fax.Text", LocalResourceFile) + "</SPAN><BR />"
						tmp_body_additional_info += txt_sender_fax.Text.Trim + "<BR />"
						tmp_body_additional_info += "<HR NOSHADE>"
					End If
					If tmp_feedback_show_url >= 0 Then
						tmp_body_additional_info += "<SPAN STYLE=""font-weight: bold;"">" + DotNetNuke.Services.Localization.Localization.GetString("pl_sender_url.Text", LocalResourceFile) + "</SPAN><BR />"
						tmp_body_additional_info += txt_sender_url.Text.Trim + "<BR />"
						tmp_body_additional_info += "<HR NOSHADE>"
					End If
					'// =============================================================================================
					tmp_body_additional_info = "<DIV STYLE=""font-family: Arial; font-size: 10pt;"">" + tmp_body_additional_info + "</DIV>"
					
				
					If chk_send_copy.Checked Then
						mail_status = mail.mail_send(tmp_recipient_email.Trim.Replace("|",";").Replace(",",";"), tmp_feedback_default_sender_email, tmp_body + tmp_body_additional_info, tmp_subject.Trim, "", txt_sender_email.Text.Trim, txt_sender_email.Text.Trim, "Normal", Common.Globals.HostSettings("SMTPServer").ToString.Trim).ToString
					Else
						mail_status = mail.mail_send(tmp_recipient_email.Trim.Replace("|",";").Replace(",",";"), tmp_feedback_default_sender_email, tmp_body + tmp_body_additional_info, tmp_subject.Trim, "", "", txt_sender_email.Text.Trim, "Normal", Common.Globals.HostSettings("SMTPServer").ToString.Trim).ToString
					End If
					
										
					'// DISPLAY THE STATUS OF THE MAIL SERVER REQUEST =====
						If mail_status.Trim.Length > 0 Then
							lbl_message.Text = DotNetNuke.Services.Localization.Localization.Getstring("alert_message_exception", Me.LocalResourceFile) + "<BR /><BR /><HR NOSHADE><BR />" + mail_status
						Else
							lbl_message.Text = tmp_success_message

							'// ADD FEEDBACK TO DATABASE ====================================================================

								Try
									Dim dt_feedback_new As New DataTable
									database.CreateCommand("donein_feedback_CUD", CommandType.StoredProcedure)
									database.AddParameter("@int_ID", 0)
									database.AddParameter("@vch_sender", txt_sender_email.Text.Trim)
									database.AddParameter("@vch_recipient", tmp_recipient_email.Trim)
									If tmp_feedback_show_subject = 1 And ddl_subject.Items.Count > 0 Then
										database.AddParameter("@int_subject", ddl_subject.SelectedValue.ToString.Trim)
										database.AddParameter("@vch_subject", ddl_subject.SelectedItem.Text.ToString.Trim)
									Else
										database.AddParameter("@int_subject", 0)
										database.AddParameter("@vch_subject", txt_subject.Text.Trim)
									End If	
									database.AddParameter("@vch_message", tmp_body.Trim)
									database.AddParameter("@vch_account_number", txt_sender_account_number.Text.Trim)
									database.AddParameter("@vch_address_street_01", txt_sender_address_street_01.Text.Trim)
									database.AddParameter("@vch_address_street_02", txt_sender_address_street_02.Text.Trim)
									database.AddParameter("@vch_address_city", txt_sender_address_city.Text.Trim)
									If ddl_sender_address_state_province.Items.Count > 0 Then
										database.AddParameter("@int_address_state_province", ddl_sender_address_state_province.SelectedValue.ToString.Trim)
									Else
										database.AddParameter("@int_address_state_province", 0)
									End If
									database.AddParameter("@vch_address_state_province", tmp_state_province_text)
									database.AddParameter("@vch_address_zipcode", txt_sender_address_zipcode.Text.Trim)
									If ddl_sender_address_country.Items.Count > 0 Then
										database.AddParameter("@int_address_country", ddl_sender_address_country.SelectedValue.ToString.Trim)
									Else
										database.AddParameter("@int_address_country", 0)
									End If
									database.AddParameter("@vch_address_country", tmp_country_text)
									database.AddParameter("@vch_phone", txt_sender_phone.Text.Trim)
									database.AddParameter("@vch_fax", txt_sender_fax.Text.Trim)
									database.AddParameter("@vch_url", txt_sender_url.Text.Trim)
									database.AddParameter("@vch_referrer", tmp_referrer)										
									database.AddParameter("@int_module", ModuleID.ToString)
									database.AddParameter("@int_author", obj_user_info.UserID.ToString)
									database.Execute(dt_feedback_new)
									If dt_feedback_new.Rows.Count > 0 Then

									Else

									End If		
									dt_feedback_new.Dispose
								Catch ex As Exception
									ProcessModuleLoadException(Me, ex)
								End Try

							'// =============================================================================================

						End If
						lbl_message.Visible = True
						tbl_feedback.Visible = False

					'// ===================================================
				Else
					lbl_message.Text = DotNetNuke.Services.Localization.Localization.Getstring("alert_invalid_email", Me.LocalResourceFile)
					lbl_message.Visible = True
					initialize_form()
				End If

			End Sub

		#End Region
		
		
		
		

		#Region " Handle: Cancel Button (btn_cancel) "
			
			Private Sub btn_cancel_click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_cancel.Click
					lbl_message.Text = ""
					lbl_message.Visible = False
					
					Response.Redirect(Request.ServerVariables("URL") + "?" + Request.ServerVariables("QUERY_STRING"))
			End Sub

		#End Region
		
		
		
		
		
		#Region " Handle: Country Dropdown List Change (ddl_sender_address_country) "
		
			Private Sub ddl_sender_address_country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_sender_address_country.SelectedIndexChanged
				get_list_entries_state_province()
			End Sub
			
		#End Region
		
		
		
		
		
		#Region " Get: List Entries From DotNetNuke Built-In Lists "
		
			Private Function get_list_entries(ByVal list_name As String, ByVal parent_key As String) As System.Data.DataTable
				'// DNN CORE TEAM KEEPS CHANGING THIS, SO...
				Dim dt_list_entries As New DataTable()
				Try
					'// VERSION 5.x
					database.CreateCommand("GetListEntries", CommandType.StoredProcedure)
					database.AddParameter("@ListName", list_name)
					database.AddParameter("@ParentKey", parent_key)
					database.AddParameter("@PortalID", -1)
					database.Execute(dt_list_entries)
				Catch ex_5 As Exception
					Try
						'// VERSION 4.x
						database.CreateCommand("GetListEntries", CommandType.StoredProcedure)
						database.AddParameter("@ListName", list_name)
						database.AddParameter("@ParentKey", parent_key)
						database.Execute(dt_list_entries)
					Catch ex_4 As Exception
						'// VERSION 3.x 
					End Try
					
				End Try	
				Return dt_list_entries				
			End Function
			
			
			Private Sub get_list_entries_state_province(Optional ByVal current_country As String = "")
				Try
					'// FIND THE ENTRYID IN THE LIST
					Dim dt_current_country As New DataTable() 
					database.CreateCommand("GetListEntry", CommandType.StoredProcedure)
					database.AddParameter("@ListName", "Country")
					database.AddParameter("@Value", "")
					database.AddParameter("@EntryID", ddl_sender_address_country.SelectedValue.ToString)
					database.Execute(dt_current_country)
					If dt_current_country.Rows.Count > 0 Then
						current_country = dt_current_country.Rows(0).Item("ListName").ToString + "." + dt_current_country.Rows(0).Item("Value").ToString
					End If
					
					'// CHANGE THE STATE / REGION DROPDOWN LIST
					ddl_sender_address_state_province.Items.Clear()
					Dim dt_state_province As New DataTable()
					dt_state_province = get_list_entries("Region", current_country)
					ddl_sender_address_state_province.Items.Add(New ListItem("","0"))
					For Each dr As DataRow In dt_state_province.Rows()
						ddl_sender_address_state_province.Items.Add(New ListItem(dr.Item("Text").ToString, dr.Item("EntryID").ToString))
					Next
				Catch ex As Exception
					'// SUPPRESS ERRORS
				End Try
				If ddl_sender_address_state_province.Items.Count > 1 Then
					ddl_sender_address_state_province.Visible = True		
				Else
					ddl_sender_address_state_province.Visible = False
				End If
				
			End Sub
			
		#End Region
		
		
		
		
		
		#Region " Web Form Designer Generated Code "

				'This call is required by the Web Form Designer.
				<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

				End Sub

				Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
					'CODEGEN: This method call is required by the Web Form Designer
					'Do not modify it using the code editor.
					InitializeComponent()
				End Sub

		#End Region
		
		
		
		

		#Region " Optional Interfaces "

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_update.Text", LocalResourceFile), "", "", "", get_update_url("DONEIN.NET Feedback"), False, DotNetNuke.Security.SecurityAccessLevel.Host, True, True) '// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_response.Text", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Response"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_subject.Text", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Subject"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Edit"), False, Security.SecurityAccessLevel.Edit, True, False)
					Return Actions
				End Get
			End Property

			'// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
			'Private Function get_update_url(ByVal module_name As String) As String
			'	Dim obj_tab As DotNetNuke.Entities.Tabs.TabInfo
			'	With New DotNetNuke.Entities.Tabs.TabController 
			'		obj_tab = .GetTabByName("DNN Update", DotNetNuke.Common.Utilities.Null.NullInteger)
			'	End With

			'	If obj_tab Is Nothing Then
			'		Return "http://www.dnnupdate.com/module-intro.content?module=" + module_name
			'	Else
			'		Return obj_tab.Url + "?tabid=" + obj_tab.TabID.ToString + "&module=" + module_name
			'	End If
			'End Function


			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
				' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			End Function

		#End Region
		
		
  		
  End Class

End Namespace
