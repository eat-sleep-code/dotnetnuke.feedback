Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.ModuleSettingsBase
Imports DotNetNuke.Services.Exceptions

Namespace DONEIN_NET.Feedback

    Public Class Settings
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        'Implements Entities.Modules.ISearchable



		#Region " Declare: Shared Classes "

			Private database As New database(System.Configuration.ConfigurationSettings.AppSettings("SiteSqlServer"), "SqlClient")
			
		#End Region





		#Region " Declare: Local Objects "
			
			Protected WithEvents rad_show_RTE As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents txt_textbox_width As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_textbox_height As System.Web.UI.WebControls.TextBox
			Protected WithEvents rad_show_sender As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_subject As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_send_copy As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_enable_captcha As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_default_sender As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents txt_default_recipient As System.Web.UI.WebControls.TextBox
			Protected WithEvents rad_show_account_number As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_require_account_number As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_address As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_require_address As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_phone As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_require_phone As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_fax As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_require_fax As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_show_url As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents rad_require_url As System.Web.UI.WebControls.RadioButtonList
			
			
			
			Protected WithEvents val_textbox_width As System.Web.UI.WebControls.RangeValidator
			Protected WithEvents val_textbox_height As System.Web.UI.WebControls.RangeValidator
			
			Protected WithEvents btn_update As System.Web.UI.WebControls.LinkButton
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton

		#End Region
		
		
		
		

		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				
				If Not IsPostBack Then
				
					module_localize() '// LOCALIZE THE MODULE
					
				
					'// GET PORTAL VERSION IN INTEGER FORM
					Dim tmp_portal_version As String = (glbAppVersion.ToString + "").Trim
					Dim tmp_portal_version_number As Integer = Convert.ToInt32(tmp_portal_version.Replace(".","").Replace("-",""))
					
					'// GET MODULE SETTINGS
					Dim tmp_feedback_show_RTE As String = CType(Settings("donein_feedback_show_RTE"), String)
					Dim tmp_feedback_textbox_width As String = CType(Settings("donein_feedback_textbox_width"), String)
					Dim tmp_feedback_textbox_height As String = CType(Settings("donein_feedback_textbox_height"), String)
					Dim tmp_feedback_show_sender As String = CType(Settings("donein_feedback_show_sender"), String)
					Dim tmp_feedback_show_subject As String = CType(Settings("donein_feedback_show_subject"), String)
					Dim tmp_feedback_show_send_copy As String = CType(Settings("donein_feedback_show_send_copy"), String)
					Dim tmp_feedback_enable_captcha As String = CType(Settings("donein_feedback_enable_captcha"), String)
					Dim tmp_feedback_default_sender As String = CType(Settings("donein_feedback_default_sender"), String)
					Dim tmp_feedback_default_recipient As String = CType(Settings("donein_feedback_default_recipient"), String)
					Dim tmp_feedback_show_account_number As String = CType(Settings("donein_feedback_show_account_number"), String)
					Dim tmp_feedback_require_account_number As String = CType(Settings("donein_feedback_require_account_number"), String)
					Dim tmp_feedback_show_address As String = CType(Settings("donein_feedback_show_address"), String)
					Dim tmp_feedback_require_address As String = CType(Settings("donein_feedback_require_address"), String)
					Dim tmp_feedback_show_phone As String = CType(Settings("donein_feedback_show_phone"), String)
					Dim tmp_feedback_require_phone As String = CType(Settings("donein_feedback_require_phone"), String)
					Dim tmp_feedback_show_fax As String = CType(Settings("donein_feedback_show_fax"), String)
					Dim tmp_feedback_require_fax As String = CType(Settings("donein_feedback_require_fax"), String)
					Dim tmp_feedback_show_url As String = CType(Settings("donein_feedback_show_url"), String)
					Dim tmp_feedback_require_url As String = CType(Settings("donein_feedback_require_url"), String)
					
					
					If tmp_feedback_show_RTE = "" Then
						rad_show_RTE.SelectedValue = "-1"
					Else
						rad_show_RTE.SelectedValue = tmp_feedback_show_RTE
					End If
					
					
					If tmp_feedback_textbox_width = ""
						txt_textbox_width.Text = "512"
					Else
						txt_textbox_width.Text = tmp_feedback_textbox_width
					End If	
									
					
					If tmp_feedback_textbox_height = ""
						txt_textbox_height.Text = "256"
					Else
						txt_textbox_height.Text = tmp_feedback_textbox_height
					End If	
					
					
					If tmp_feedback_show_sender = "" Then
						rad_show_sender.SelectedValue = "1"
					Else
						rad_show_sender.SelectedValue = tmp_feedback_show_sender
					End If
					
					
					If tmp_feedback_show_subject = "" Then
						rad_show_subject.SelectedValue = "0"
					Else
						rad_show_subject.SelectedValue = tmp_feedback_show_subject
					End If
					
					
					If tmp_feedback_show_send_copy = "" Then
						rad_show_send_copy.SelectedValue = "1"
					Else
						rad_show_send_copy.SelectedValue = tmp_feedback_show_send_copy
					End If
					
					
					If tmp_feedback_enable_captcha = "" Then
						rad_enable_captcha.SelectedValue = "-1"
					Else
						rad_enable_captcha.SelectedValue = tmp_feedback_enable_captcha
					End If
					
					
					If tmp_portal_version_number >= 40300 Then
						rad_enable_captcha.Enabled = True
					Else
						rad_enable_captcha.Enabled = False
					End If 
					
					
					If tmp_feedback_default_sender = "" Then
						rad_default_sender.SelectedValue = "-1"
					Else
						rad_default_sender.SelectedValue = tmp_feedback_default_sender
					End If
					
										
					If tmp_feedback_default_recipient = "" Then
						txt_default_recipient.Text = Common.Globals.HostSettings("HostEmail").ToString.Trim
					Else
						txt_default_recipient.Text = tmp_feedback_default_recipient.Trim
					End If
					
					'=================================================================================
					
					If tmp_feedback_show_account_number = "" Then
						rad_show_account_number.SelectedValue = "1"
					Else
						rad_show_account_number.SelectedValue = tmp_feedback_show_account_number
					End If
					
					If tmp_feedback_require_account_number = "" Then
						rad_require_account_number.SelectedValue = "1"
					Else
						rad_require_account_number.SelectedValue = tmp_feedback_require_account_number
					End If
					
					If rad_show_account_number.SelectedValue = "-1" Then
						rad_require_account_number.Enabled = "False"
						rad_require_account_number.SelectedValue = "-1" 
					End If
					
					'=================================================================================

					If tmp_feedback_show_address = "" Then
						rad_show_address.SelectedValue = "1"
					Else
						rad_show_address.SelectedValue = tmp_feedback_show_address
					End If
					
					If tmp_feedback_require_address = "" Then
						rad_require_address.SelectedValue = "1"
					Else
						rad_require_address.SelectedValue = tmp_feedback_require_address
					End If
					
					If rad_show_address.SelectedValue = "-1" Then
						rad_require_address.Enabled = "False"
						rad_require_address.SelectedValue = "-1" 
					End If
					
					'=================================================================================
					
					If tmp_feedback_show_phone = "" Then
						rad_show_phone.SelectedValue = "1"
					Else
						rad_show_phone.SelectedValue = tmp_feedback_show_phone
					End If
					
					If tmp_feedback_require_phone = "" Then
						rad_require_phone.SelectedValue = "1"
					Else
						rad_require_phone.SelectedValue = tmp_feedback_require_phone
					End If
					
					If rad_show_phone.SelectedValue = "-1" Then
						rad_require_phone.Enabled = "False"
						rad_require_phone.SelectedValue = "-1" 
					End If
					
					'=================================================================================
					
					If tmp_feedback_show_fax = "" Then
						rad_show_fax.SelectedValue = "1"
					Else
						rad_show_fax.SelectedValue = tmp_feedback_show_fax
					End If
					
					If tmp_feedback_require_fax = "" Then
						rad_require_fax.SelectedValue = "1"
					Else
						rad_require_fax.SelectedValue = tmp_feedback_require_fax
					End If
					
					If rad_show_fax.SelectedValue = "-1" Then
						rad_require_fax.Enabled = "False"
						rad_require_fax.SelectedValue = "-1" 
					End If
					
					'=================================================================================
					
					If tmp_feedback_show_url = "" Then
						rad_show_url.SelectedValue = "1"
					Else
						rad_show_url.SelectedValue = tmp_feedback_show_url
					End If
					
					If tmp_feedback_require_url = "" Then
						rad_require_url.SelectedValue = "1"
					Else
						rad_require_url.SelectedValue = tmp_feedback_require_url
					End If
					
					If rad_show_url.SelectedValue = "-1" Then
						rad_require_url.Enabled = "False"
						rad_require_url.SelectedValue = "-1" 
					End If
					

				End If
				
			End Sub

		#End Region



		
		
		#Region " Page: Localize "

 			Private Sub module_localize()
 				
				btn_update.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_update.Text", LocalResourceFile)
				btn_cancel.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_cancel.Text", LocalResourceFile)
				
				rad_show_RTE.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_RTE.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_sender.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_sender.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_subject.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_dropdown.Text", LocalResourceFile), "1"))
				rad_show_subject.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_textbox.Text", LocalResourceFile), "0"))
				rad_show_subject.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_send_copy.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_send_copy.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_enable_captcha.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_enable_captcha.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_anonymous.Text", LocalResourceFile), "0"))
				rad_enable_captcha.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_default_sender.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_user_email.Text", LocalResourceFile), "1"))
				rad_default_sender.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_portal_email.Text", LocalResourceFile), "0"))
				rad_default_sender.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_host_email.Text", LocalResourceFile), "-1"))
				
				rad_show_account_number.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_account_number.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_require_account_number.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_require_account_number.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_address_us.Text", LocalResourceFile), "1"))
				rad_show_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_address_ca.Text", LocalResourceFile), "2"))
				rad_show_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes_address_all.Text", LocalResourceFile), "3"))
				rad_show_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_require_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_require_address.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_phone.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_phone.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_require_phone.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_require_phone.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_fax.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_fax.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_require_fax.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_require_fax.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_show_url.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_show_url.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
				
				rad_require_url.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile), "1"))
				rad_require_url.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile), "-1"))
							
			End Sub 

		#End Region
			
			
			
			
		
		#Region " Handle: Radiobutton List Dependencies "

			
    		Private Sub rad_show_account_number_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_show_account_number.SelectedIndexChanged
				radiobutton_list_handle(rad_show_account_number, rad_require_account_number)
			End Sub
			
			Private Sub rad_show_address_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_show_address.SelectedIndexChanged
				radiobutton_list_handle(rad_show_address, rad_require_address)
			End Sub
			
			Private Sub rad_show_phone_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_show_phone.SelectedIndexChanged
				radiobutton_list_handle(rad_show_phone, rad_require_phone)
			End Sub
			
			Private Sub rad_show_fax_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_show_fax.SelectedIndexChanged
				radiobutton_list_handle(rad_show_fax, rad_require_fax)
			End Sub
			
			Private Sub rad_show_url_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rad_show_url.SelectedIndexChanged
				radiobutton_list_handle(rad_show_url, rad_require_url)
			End Sub
			
			Private Sub radiobutton_list_handle(ByVal radiobutton_list_parent As System.Web.UI.WebControls.RadioButtonList, ByVal radiobutton_list_child As System.Web.UI.WebControls.RadioButtonList)
				If radiobutton_list_parent.SelectedValue = "-1" Then
					radiobutton_list_child.SelectedValue = "-1"
					radiobutton_list_child.Enabled = False
				Else
					radiobutton_list_child.Enabled = True
				End If
			End Sub
			

		#End Region
		
		
		
		
		
		#Region " Handle: Update Button (btn_update) "

			Private Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
				Try
					Dim obj_modules As New ModuleController
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_RTE", rad_show_RTE.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_textbox_width", txt_textbox_width.Text)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_textbox_height", txt_textbox_height.Text)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_sender", rad_show_sender.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_subject", rad_show_subject.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_send_copy", rad_show_send_copy.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_enable_captcha", rad_enable_captcha.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_default_sender", rad_default_sender.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_default_recipient", txt_default_recipient.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_account_number", rad_show_account_number.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_require_account_number", rad_require_account_number.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_address", rad_show_address.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_require_address", rad_require_address.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_phone", rad_show_phone.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_require_phone", rad_require_phone.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_fax", rad_show_fax.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_require_fax", rad_require_fax.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_show_url", rad_show_url.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_feedback_require_url", rad_require_url.SelectedValue)
					
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception 
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub
			
		#End Region
		
		
		
		
		
		#Region " Handle: Cancel Button (btn_cancel) "

			Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
				Try
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception		  
					ProcessModuleLoadException(Me, ex)
				End Try
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

		
		
		
		
		#Region "Optional Interfaces"

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_update.Text", LocalResourceFile), "", "", "", get_update_url("DONEIN.NET Feedback"), False, DotNetNuke.Security.SecurityAccessLevel.Host, True, True) '// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_response.Text", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Response"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_subject.Text", LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Subject"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
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

			'Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			'End Function

		#End Region
		
		
		
	
    End Class

End Namespace