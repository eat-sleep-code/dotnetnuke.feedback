<%@ Control Language="vb" AutoEventWireup="false" Codebehind="settings.ascx.vb" Inherits="DONEIN_NET.Feedback.Settings" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<TABLE BORDER="0" CELLSPACING="0" CELLPADDING="2" ALIGN="left">
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_RTE" CONTROLNAME="rad_show_RTE" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_RTE" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_textbox_width" CONTROLNAME="txt_textbox_width" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_textbox_width" WIDTH="80px" MAXLENGTH="4" CSSCLASS="NormalTextBox" />
			<ASP:RANGEVALIDATOR RUNAT="server" ID="val_textbox_width" RESOURCEKEY="val_textbox_width.ErrorMessage" CONTROLTOVALIDATE="txt_textbox_width" MINIMUMVALUE="0" MAXIMUMVALUE="2048" TYPE="Integer" DISPLAY="Dynamic" CSSCLASS="NormalRed" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_textbox_height" CONTROLNAME="txt_textbox_height" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_textbox_height" WIDTH="80px" MAXLENGTH="4" CSSCLASS="NormalTextBox" />
			<ASP:RANGEVALIDATOR RUNAT="server" ID="val_textbox_height" RESOURCEKEY="val_textbox_height.ErrorMessage" CONTROLTOVALIDATE="txt_textbox_height" MINIMUMVALUE="0" MAXIMUMVALUE="2048" TYPE="Integer" DISPLAY="Dynamic" CSSCLASS="NormalRed" />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_sender" CONTROLNAME="rad_show_sender" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_sender" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_subject" CONTROLNAME="rad_show_subject" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_subject" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_send_copy" CONTROLNAME="rad_show_send_copy" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_send_copy" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_enable_captcha" CONTROLNAME="rad_enable_captcha" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_enable_captcha" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_default_sender" CONTROLNAME="rad_default_sender" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_default_sender" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_default_recipient" CONTROLNAME="txt_default_recipient" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:TEXTBOX RUNAT="server" ID="txt_default_recipient" WIDTH="300px" MAXLENGTH="255" CSSCLASS="NormalTextBox" />
		</TD>
	</TR>
	
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_account_number" CONTROLNAME="rad_show_account_number" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_account_number" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" CAUSESVALIDATION="false"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_require_account_number" CONTROLNAME="rad_require_account_number" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_require_account_number" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_address" CONTROLNAME="rad_show_address" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_address" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" CAUSESVALIDATION="false"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_require_address" CONTROLNAME="rad_require_address" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_require_address" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_phone" CONTROLNAME="rad_show_phone" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_phone" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" CAUSESVALIDATION="false"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_require_phone" CONTROLNAME="rad_require_phone" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_require_phone" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_fax" CONTROLNAME="rad_show_fax" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_fax" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" CAUSESVALIDATION="false"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_require_fax" CONTROLNAME="rad_require_fax" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_require_fax" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_show_url" CONTROLNAME="rad_show_url" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_url" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal" AUTOPOSTBACK="true" CAUSESVALIDATION="false"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="middle">
			<DNN:LABEL RUNAT="server" ID="pl_require_url" CONTROLNAME="rad_require_url" SUFFIX=":" />
		</TD>
		<TD WIDTH="480" ALIGN="left" VALIGN="middle">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_require_url" CSSCLASS="NormalTextBox" REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
		</TD>
	</TR>
	
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="center" VALIGN="middle">
			<HR NOSHADE />
		</TD>
	</TR>
	
		
	<TR HEIGHT="30">
		<TD COLSPAN="2" ALIGN="left" VALIGN="middle">
			<ASP:LINKBUTTON RUNAT="server" ID="btn_update" />
			&nbsp;&nbsp;	
			<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" />
		</TD>        
	</TR>
</TABLE>
