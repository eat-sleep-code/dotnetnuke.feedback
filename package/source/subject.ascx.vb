Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.ModuleSettingsBase
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Entities.Tabs


Namespace DONEIN_NET.Feedback

	Public Class Subject
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable



		#Region " Declare: Shared Classes "
			
			Private database As New database(System.Configuration.ConfigurationSettings.AppSettings("SiteSqlServer"), "SqlClient")
			Private window As New components.window()
			
		#End Region



		#Region " Declare: Local Objects "
		
			Protected WithEvents btn_create As System.Web.UI.WebControls.LinkButton
			Protected WithEvents btn_return As System.Web.UI.WebControls.LinkButton		
			Protected WithEvents btn_update As System.Web.UI.WebControls.LinkButton		
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton		
			
			Protected WithEvents dg_subject As System.Web.UI.WebControls.DataGrid
			Protected WithEvents tbl_subject_edit As System.Web.UI.HtmlControls.HtmlTable
				
			Protected pl_subject As UI.UserControls.LabelControl
			Protected pl_recipient As UI.UserControls.LabelControl
			Protected pl_success_message As UI.UserControls.LabelControl
			
			Protected WithEvents txt_ID As System.Web.UI.HtmlControls.HtmlInputHidden
			Protected WithEvents txt_subject As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_recipient As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_success_message As System.Web.UI.WebControls.TextBox
			
			Private obj_user As New UserController
			Private obj_user_info As UserInfo  = obj_user.GetCurrentUserInfo
			
			
		#End Region
		
		

		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				Try
					If Not IsPostBack Then
						If ModuleId > 0 Then
							module_localize() '// LOCALIZE THE MODULE
							dg_subject_bind() '// BIND THE DATAGRID
							tbl_subject_edit.Visible = False
						End If
					End If
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub

		#End Region



		#Region " Page: Localize "

 			Private Sub module_localize()
 				
 				btn_create.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_create.Text", LocalResourceFile)
				btn_return.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_return.Text", LocalResourceFile)
				btn_update.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_update.Text", LocalResourceFile)
				btn_cancel.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_cancel.Text", LocalResourceFile)
				
				pl_subject.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_subject.Text", LocalResourceFile)
				pl_recipient.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_recipient.Text", LocalResourceFile)
				pl_success_message.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_success_message.Text", LocalResourceFile)
				
			End Sub 

		#End Region



		#Region " Bind: DataGrid (dg_subject) "

 			Private Sub dg_subject_bind()
 				Services.Localization.Localization.LocalizeDataGrid(dg_subject, Me.LocalResourceFile)
 				
				Dim dt_subject As New DataTable
				database.CreateCommand("donein_feedback_subject_R", CommandType.StoredProcedure)
				database.AddParameter("@int_ID", "0")
				database.AddParameter("@int_module", ModuleID.ToString)
				database.Execute(dt_subject)
				If dt_subject.Rows.Count > 0 Then
					dg_subject.DataSource = dt_subject
					dg_subject.DataBind
					dg_subject.Visible = True
				Else
					dg_subject.Visible = False
				End If			
				btn_create.Visible = True
			End Sub 

		#End Region
		
		
		
		#Region " Handle: DataGrid ItemCommand (dg_subject) "

 			Private Sub dg_subject_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_subject.ItemCommand
 				If e.CommandName.ToLower.Trim = "help" Then
 						Dim url_feedback_sid As String = ""
 							Dim bln_friendly_urls As Boolean = False
 							
 							If Common.Globals.HostSettings.ContainsKey("UseFriendlyUrls") Then
								If Common.Globals.HostSettings("UseFriendlyUrls").ToString = "Y" Then
									bln_friendly_urls = True
								Else
									bln_friendly_urls = False
								End If
							Else
								bln_friendly_urls = False
							End If
							
 							If bln_friendly_urls = True Then
 								Try
 									Dim obj_tab As New TabController
									Dim obj_tab_info As TabInfo
									url_feedback_sid = FriendlyUrl(obj_tab_info, "/tabid/" + obj_tab_info.TabID.ToString + "/feedback_sid/" + e.CommandArgument.ToString)
								Catch ex As Exception
									url_feedback_sid = ""
									'// WE WANT TO SUPPRESS ANY ERRORS
								End Try
							Else
								url_feedback_sid = NavigateURL() + "&feedback_sid=" + e.CommandArgument.ToString
							End If
						
						window.alert(DotNetNuke.Services.Localization.Localization.GetString("pl_feedback_sid.Text", LocalResourceFile) + url_feedback_sid)
					
 				Else If e.CommandName.ToLower.Trim = "delete" Then
 					database.CreateCommand("donein_feedback_subject_CUD", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", (CType(e.CommandArgument,Integer) * -1).ToString)
					database.ExecuteNonQuery()
					dg_subject_bind()
				Else
					Dim dt_subject_edit As New DataTable
					database.CreateCommand("donein_feedback_subject_R", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", CType(e.CommandArgument,Integer).ToString)
					database.Execute(dt_subject_edit)
					If dt_subject_edit.Rows.Count > 0 Then
						txt_ID.Value = dt_subject_edit.Rows(0).Item("ID").ToString
						txt_subject.Text = dt_subject_edit.Rows(0).Item("vch_subject").ToString	
						txt_recipient.Text = dt_subject_edit.Rows(0).Item("vch_recipient").ToString	
						txt_success_message.Text = dt_subject_edit.Rows(0).Item("vch_success_message").ToString	
						dg_subject.Visible = False
						btn_create.Visible = False
						btn_return.Visible = False
						tbl_subject_edit.Visible = True
					End If					
				End If 				
			End Sub 

		#End Region
		
		

		#Region " Handle: Update Button (btn_update) "

  			Private Sub btn_update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_update.Click	
				Try
					Dim dt_subject_new As New DataTable
					database.CreateCommand("donein_feedback_subject_CUD", CommandType.StoredProcedure)
					database.AddParameter("@int_ID", CType(txt_ID.Value, Integer).ToString)
					database.AddParameter("@vch_subject", txt_subject.Text)
					database.AddParameter("@vch_recipient", txt_recipient.Text)
					database.AddParameter("@vch_success_message", txt_success_message.Text)
					database.AddParameter("@int_module", ModuleID.ToString)
					database.AddParameter("@int_author", CType(obj_user_info.UserID, Integer).ToString)
					database.Execute(dt_subject_new)
					If dt_subject_new.Rows.Count > 0 Then
						dg_subject_bind()	
						tbl_subject_edit.Visible = False
						btn_return.Visible = True
					Else
						tbl_subject_edit.Visible = False
						btn_return.Visible = True
						Exit Sub													
					End If		
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		#End Region
		
		
		
		#Region " Handle: Create Button (btn_create) "

  			Private Sub btn_create_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_create.Click	
				Try
					dg_subject.Visible = False
					btn_create.Visible = False
					btn_return.Visible = False
					tbl_subject_edit.Visible = True
					txt_ID.Value = "0"
					txt_subject.Text = ""
					txt_recipient.Text = ""
					txt_success_message.Text = ""
				Catch ex As Exception
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		#End Region


		
		#Region " Handle: Cancel Button (btn_cancel) "

 			Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_cancel.Click	
				Try
					dg_subject.Visible = True
					btn_create.Visible = True
					btn_return.Visible = True
					tbl_subject_edit.Visible = False	
				Catch ex As Exception		  
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub 

		 #End Region

             
        
        #Region " Handle: Return Button (btn_return) "

			Private Sub btn_return_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_return.Click
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

			'NOTE: The following placeholder declaration is required by the Web Form Designer.
			'Do not delete or move it.
			Private designerPlaceholderDeclaration As System.Object

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

		#End Region


		
	End Class

End NameSpace
