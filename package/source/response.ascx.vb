Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.ModuleSettingsBase
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Entities.Users

Namespace DONEIN_NET.Feedback

	Public Class Response

		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable



		#Region " Declare: Shared Classes "
			
			Private database As New database(System.Configuration.ConfigurationSettings.AppSettings("SiteSqlServer"), "SqlClient")
			
		#End Region



		#Region " Declare: Local Objects "
		
			Protected pl_subject As UI.UserControls.LabelControl
			Protected WithEvents ddl_subject As System.Web.UI.WebControls.DropDownList
			Protected WithEvents dg_response As System.Web.UI.WebControls.DataGrid
			Protected WithEvents btn_return As System.Web.UI.WebControls.LinkButton
			
		#End Region
		
		

		#Region " Page: Load "
		
			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				If Not IsPostBack Then
					module_localize() '// LOCALIZE THIS MODULE
					ddl_subject_bind()
				End If				
			End Sub
			
		#End Region
	    
	    
		    
		#Region " Page: Localization "

 			Private Sub module_localize()
 				
 				btn_return.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_return.Text", LocalResourceFile)
								
			End Sub 

		#End Region
		
		
		
		#Region " Bind: Subject Dropdown (ddl_subject) "
		
			Private Sub ddl_subject_bind()
				Dim dt_subject As New DataTable
				database.CreateCommand("donein_feedback_subject_R", CommandType.StoredProcedure)
				database.AddParameter("@int_ID", "0")
				database.AddParameter("@int_module", ModuleID.ToString) 
				database.Execute(dt_subject)
				If dt_subject.Rows.Count > 0 Then
					ddl_subject.DataSource = dt_subject
					ddl_subject.DataTextField = "vch_subject"
					ddl_subject.DataValueField = "ID"
					ddl_subject.DataBind()
				End If				
				dt_subject.Clear
				dt_subject.Dispose	
				
				ddl_subject.Items.Insert(0, New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_subject_user_entered.Text", LocalResourceFile), "0"))
 				ddl_subject.Items.Insert(0, New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_subject_default.Text", LocalResourceFile), "-1"))
 					
			End Sub			
			
		#End Region		
		
		
		
		#Region " Bind: DataGrid (dg_response) "

 			Private Sub dg_response_bind()
 				Services.Localization.Localization.LocalizeDataGrid(dg_response, Me.LocalResourceFile)
 				
				Dim dt_response As New DataTable
				database.CreateCommand("donein_feedback_R", CommandType.StoredProcedure)
				database.AddParameter("@int_ID", "0")
				database.AddParameter("@int_subject", ddl_subject.SelectedValue)
				database.AddParameter("@int_module", ModuleID.ToString)
				database.Execute(dt_response)
				If dt_response.Rows.Count > 0 Then
					dg_response.PageSize = 10
					dg_response.DataSource = dt_response
					dg_response.DataBind
					dg_response.Visible = True
					
					If dt_response.Rows.Count > 10 Then
						dg_response.PagerStyle.Visible = False
						dg_response.AllowPaging = False
					Else
						dg_response.PagerStyle.Visible = True
						dg_response.AllowPaging = True	
					End If	
					
				Else
					dg_response.Visible = False
				End If			
			End Sub 

		#End Region
		
		
		
		#Region " Handle: DataGrid Paging (dg_response) "

			Private Sub dg_response_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_response.PageIndexChanged
				dg_response.CurrentPageIndex = e.NewPageIndex	
			End Sub
		
		#End Region	
	    
	    
	    
	    #Region " Handle: Datagrid Row Deletion (dg_response) "
	    
			Public Sub dg_response_delete(sender As Object, e As DataGridCommandEventArgs)
				Dim tmp_ID As Integer = Convert.ToInt32(dg_response.DataKeys(e.Item.ItemIndex)) * -1
				database.CreateCommand("donein_feedback_CUD", CommandType.StoredProcedure)
				database.AddParameter("@int_ID", tmp_ID.ToString)
				database.ExecuteNonQuery()
				dg_response_bind()
			End Sub
			
		#End Region
		
		
		
	    #Region " Handle: Subject Dropdown Change (ddl_subject) "
	    
			Private Sub ddl_subject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_subject.SelectedIndexChanged
				dg_response_bind()
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



		#Region " Optional Interfaces "

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_update.Text", LocalResourceFile), "", "", "", get_update_url("DONEIN.NET Feedback"), False, DotNetNuke.Security.SecurityAccessLevel.Host, True, True) '// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
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

		#End Region
		
		
		
	End Class

End Namespace