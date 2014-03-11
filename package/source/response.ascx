<%@ Control Language="vb" AutoEventWireup="false" Codebehind="response.ascx.vb" Inherits="DONEIN_NET.Feedback.Response" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<TABLE BORDER="0" WIDTH="100%" CELLPADDING="2" CELLSPACING="1">
	<TR HEIGHT="30">
		<TD WIDTH="240" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_subject" CONTROLNAME="ddl_subject" SUFFIX=":" />
		</TD>
		<TD WIDTH="400" ALIGN="left" VALIGN="middle">
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_subject" WIDTH="360" AUTOPOSTBACK="True" />
		</TD>
	</TR>
	<TR>
		<TD COLSPAN="2">
		
			<ASP:DATAGRID RUNAT="server" ID="dg_response" WIDTH="100%" BORDERWIDTH="0" CELLPADDING="4" CELLSPACING="1" AUTOGENERATECOLUMNS="False" ALLOWPAGING="True" PAGERSTYLE-MODE="NumericPages" ALLOWSORTING="False" ONDELETECOMMAND="dg_response_delete" DATAKEYFIELD="id">
				<HEADERSTYLE CSSCLASS="HEADER" FONT-BOLD="True" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
				<ITEMSTYLE BACKCOLOR="#FEFEFE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
				<ALTERNATINGITEMSTYLE BACKCOLOR="#EEEEEE" HORIZONTALALIGN="Left" VERTICALALIGN="Top" />
				<COLUMNS>
					<ASP:TEMPLATECOLUMN ITEMSTYLE-WIDTH="120" HEADERTEXT="pl_sender" > 
						<ITEMTEMPLATE> 
							<ASP:HYPERLINK RUNAT="server" ID="lnk_sender_email" NAVIGATEURL='<%# "mailto:" + DataBinder.Eval(Container, "DataItem.vch_sender") + "?subject=RE: " + DataBinder.Eval(Container, "DataItem.vch_subject") %>'><%# DataBinder.Eval(Container, "DataItem.vch_sender") %></ASP:HYPERLINK>
						</ITEMTEMPLATE> 
					</ASP:TEMPLATECOLUMN>
					<ASP:TEMPLATECOLUMN HEADERTEXT="pl_message"> 
						<ITEMTEMPLATE> 
							<SPAN STYLE="font-weight: bold;">
								<%# DataBinder.Eval(Container, "DataItem.vch_subject") %>						
							</SPAN>
							<BR />
							<DIV STYLE="text-align: justify;">
								<%# Replace(Replace(Replace(DataBinder.Eval(Container, "DataItem.vch_message"),"&lt;","<"),"&gt;",">"),"&amp;","&") %>
							</DIV>
							<SPAN STYLE="font-weight: bold;">#: </SPAN><%# DataBinder.Eval(Container, "DataItem.vch_account_number") %><BR />
							<%# DataBinder.Eval(Container, "DataItem.vch_address_street_01") %>&nbsp;
							<%# DataBinder.Eval(Container, "DataItem.vch_address_street_02") %><BR />
							<%# DataBinder.Eval(Container, "DataItem.vch_address_city") %>&nbsp;
							<%# DataBinder.Eval(Container, "DataItem.vch_address_state_province") %>&nbsp;
							<%# DataBinder.Eval(Container, "DataItem.vch_address_zipcode") %>&nbsp;
							<%# DataBinder.Eval(Container, "DataItem.vch_address_country") %>
							<BR />
							<SPAN STYLE="font-weight: bold;">P: </SPAN><%# DataBinder.Eval(Container, "DataItem.vch_phone") %>
							<BR />
							<SPAN STYLE="font-weight: bold;">F: </SPAN><%# DataBinder.Eval(Container, "DataItem.vch_fax") %>
							<BR />
							<SPAN STYLE="font-weight: bold;">W: </SPAN><ASP:HYPERLINK RUNAT="server" ID="lnk_sender_web" NAVIGATEURL='<%# "http://" + Replace(DataBinder.Eval(Container, "DataItem.vch_url"), "http://", "") %>'><%# DataBinder.Eval(Container, "DataItem.vch_url") %></ASP:HYPERLINK>
							<BR />
						</ITEMTEMPLATE> 
					</ASP:TEMPLATECOLUMN>
					<ASP:BUTTONCOLUMN BUTTONTYPE="LinkButton" COMMANDNAME="Delete" ITEMSTYLE-WIDTH="40" ITEMSTYLE-HORIZONTALALIGN="Center" TEXT="<IMG SRC=/images/delete.gif BORDER=0>" />
				</COLUMNS>
			</ASP:DATAGRID>
			<BR />
			<ASP:LINKBUTTON RUNAT="server" ID="btn_return" />
		</TD>
	</TR>
</TABLE>