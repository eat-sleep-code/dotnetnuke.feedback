<%@ Control Language="vb" AutoEventWireup="false" Explicit="true" Codebehind="base.ascx.vb" Inherits="DONEIN_NET.Feedback.Base" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls"%>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>

<ASP:LITERAL RUNAT="server" ID="ltl_script" />

<ASP:LABEL RUNAT="server" ID="lbl_message" CSSCLASS="donein_feedback_message" STYLE="padding-left: 10px;" />

<TABLE RUNAT="server" ID="tbl_feedback" BORDER="0" CELLSPACING="1" CELLPADDING="3">
	<TR RUNAT="server" ID="tr_sender_name" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_name" CONTROLNAME="txt_sender_name" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_name" COLUMNS="35" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_email" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_email" CONTROLNAME="txt_sender_email" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_email" COLUMNS="35" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_account_number" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_account_number" CONTROLNAME="txt_sender_account_number" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_account_number" COLUMNS="35" MAXLENGTH="16" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_address" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_address" CONTROLNAME="ddl_sender_address_country" SUFFIX=":" />
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_sender_address_country" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" AUTOPOSTBACK="true" CAUSESVALIDATION="false" />
			<ASP:LITERAL RUNAT="server" ID="ltl_sender_address_country"><BR /></ASP:LITERAL>
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_address_street_01" AUTOCOMPLETETYPE="HomeStreetAddress" COLUMNS="35" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
			<BR />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_address_street_02" COLUMNS="35" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
			<BR />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_address_city" AUTOCOMPLETETYPE="HomeCity" COLUMNS="25" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_sender_address_state_province" STYLE="MARGIN-LEFT: 3px; width: 160px;" ENABLEVIEWSTATE="True" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_address_zipcode" AUTOCOMPLETETYPE="HomeZipCode" COLUMNS="10" MAXLENGTH="10" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_phone" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_phone" CONTROLNAME="txt_sender_phone" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_phone" COLUMNS="35" MAXLENGTH="16" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" ONKEYPRESS="return phone_number_check(event);" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_fax" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_fax" CONTROLNAME="txt_sender_fax" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_fax" COLUMNS="35" MAXLENGTH="16" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" ONKEYPRESS="return phone_number_check(event);" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_sender_url" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_sender_url" CONTROLNAME="txt_sender_url" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_sender_url" COLUMNS="35" MAXLENGTH="1024" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_subject" VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_subject" CONTROLNAME="txt_subject" SUFFIX=":" />
			<ASP:TEXTBOX RUNAT="server" ID="txt_subject" COLUMNS="35" MAXLENGTH="128" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_subject" STYLE="MARGIN-LEFT: 3px;" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR VALIGN="top">
		<TD CLASS="NormalBold">
			<DNN:LABEL RUNAT="server" ID="pl_body" CONTROLNAME="txt_body" SUFFIX=":"  />
			<DNN:TEXTEDITOR RUNAT="server" ID="txt_body" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_send_copy" VALIGN="top">
		<TD CLASS="NormalBold" NOWRAP>
			<ASP:CHECKBOX RUNAT="server" ID="chk_send_copy" />
			<DNN:LABEL RUNAT="server" ID="pl_send_copy" CONTROLNAME="chk_send_copy" SUFFIX="" ENABLEVIEWSTATE="True" />
		</TD>
	</TR>
	<TR RUNAT="server" ID="tr_captcha" VALIGN="top">
		<TD CLASS="NormalBold" NOWRAP>
			<BR />
			<DNN:LABEL RUNAT="server" ID="pl_captcha" CONTROLNAME="txt_captcha" SUFFIX="" />
			<DNN:CAPTCHACONTROL RUNAT="server" ID="dnn_captcha" CAPTCHAWIDTH="130" CAPTCHAHEIGHT="40" />
		</TD>
	</TR>
	<TR VALIGN="top">
		<TD ALIGN="left">
			<ASP:LINKBUTTON RUNAT="server" ID="btn_send" RESOURCEKEY="pl_send" CSSCLASS="CommandButton" CAUSESVALIDATION="false" />
			<ASP:LABEL RUNAT="server" ID="lbl_divider" CLASS="divider" />
			<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" RESOURCEKEY="pl_cancel" CSSCLASS="CommandButton" CAUSESVALIDATION="false" />
		</TD>
	</TR>
</TABLE>