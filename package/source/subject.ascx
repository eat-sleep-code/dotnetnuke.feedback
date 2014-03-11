<%@ Control Language="vb" AutoEventWireup="false" Codebehind="subject.ascx.vb" Inherits="DONEIN_NET.Feedback.Subject" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<DIV>
	<BR />
	<ASP:LINKBUTTON RUNAT="server" ID="btn_create" />
	<ASP:DATAGRID RUNAT="server" ID="dg_subject" WIDTH="100%" BORDERWIDTH="0" CELLPADDING="2" CELLSPACING="1" AUTOGENERATECOLUMNS="False" ALLOWPAGING="False" ALLOWSORTING="False" >
		<HEADERSTYLE CSSCLASS="HEADER" FONT-BOLD="True" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<ITEMSTYLE BACKCOLOR="#FEFEFE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<ALTERNATINGITEMSTYLE BACKCOLOR="#EEEEEE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
		<COLUMNS>
			<ASP:TEMPLATECOLUMN ITEMSTYLE-WIDTH="30" ITEMSTYLE-HORIZONTALALIGN="Right"> 
				<ITEMTEMPLATE> 
					<ASP:LINKBUTTON RUNAT="server" ID="btn_dg_help" COMMANDNAME="help" COMMANDARGUMENT='<%# DataBinder.Eval(Container, "DataItem.ID") %>' CAUSESVALIDATION="False"><%# DataBinder.Eval(Container, "DataItem.ID") %></ASP:LINKBUTTON>
				</ITEMTEMPLATE> 
			</ASP:TEMPLATECOLUMN>
			<ASP:BOUNDCOLUMN DATAFIELD="vch_subject" HEADERTEXT="pl_subject" />
			<ASP:TEMPLATECOLUMN ITEMSTYLE-WIDTH="100" > 
				<ITEMTEMPLATE> 
					<ASP:LINKBUTTON RUNAT="server" ID="btn_dg_edit" COMMANDNAME="edit" COMMANDARGUMENT='<%# DataBinder.Eval(Container, "DataItem.ID") %>' RESOURCEKEY="pl_subject_edit" />
					<SPAN STYLE="font-weight: bold;">
						&nbsp;&nbsp;|&nbsp;&nbsp;
					</SPAN>
					<ASP:LINKBUTTON RUNAT="server" ID="btn_dg_delete" COMMANDNAME="delete" COMMANDARGUMENT='<%# DataBinder.Eval(Container, "DataItem.ID") %>' RESOURCEKEY="pl_subject_delete" /> 
				</ITEMTEMPLATE> 
			</ASP:TEMPLATECOLUMN>
		</COLUMNS>
	</ASP:DATAGRID>
	<BR />
	<ASP:LINKBUTTON RUNAT="server" ID="btn_return" />

	<TABLE RUNAT="server" ID="tbl_subject_edit" BORDER="0" WIDTH="100%" CELLPADDING="3" CELLSPACING="1">
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<DNN:LABEL RUNAT="server" ID="pl_subject" CONTROLNAME="txt_subject" SUFFIX=":" />
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:TEXTBOX RUNAT="server" ID="txt_subject" CSSCLASS="NormalText" WIDTH="360" MAXLENGTH="256" />							
			</TD>        
		</TR>
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<DNN:LABEL RUNAT="server" ID="pl_recipient" CONTROLNAME="txt_recipient" SUFFIX=":" />
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:TEXTBOX RUNAT="server" ID="txt_recipient" CSSCLASS="NormalText" WIDTH="360" MAXLENGTH="1024" />							
			</TD>        
		</TR>
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<DNN:LABEL RUNAT="server" ID="pl_success_message" CONTROLNAME="txt_success_message" SUFFIX=":" />
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:TEXTBOX RUNAT="server" ID="txt_success_message" CSSCLASS="NormalText" WIDTH="360" MAXLENGTH="1024" TEXTMODE="MultiLine" ROWS="5" />							
			</TD>        
		</TR>
		<TR HEIGHT="30">
			<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
				<INPUT TYPE="hidden" RUNAT="server" ID="txt_ID" NAME="txt_ID"/>
			</TD>
			<TD WIDTH="400" ALIGN="left" VALIGN="middle">
				<ASP:LINKBUTTON RUNAT="server" ID="btn_update" />
				&nbsp;&nbsp;	
				<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" />
			</TD>        
		</TR>
	</TABLE>
</DIV>

