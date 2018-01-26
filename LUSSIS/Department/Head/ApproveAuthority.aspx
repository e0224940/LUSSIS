<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveAuthority.aspx.cs" Inherits="Department_Head_ApproveAuthority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
    <asp:Label ID="lbl_appAuth_currentHeadTxt" runat="server" Text="Current Acting Head:"></asp:Label>
    <asp:TextBox ID="txtBox_appAuth_currentHead" runat="server" AutoPostBack="True" Enabled="False">DeputyHeadNo</asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lbl_appAuth_appActingHeadTxt" runat="server" Font-Bold="True" Font-Underline="True" Text="Appoint Acting Head"></asp:Label>
    <br />
    <asp:Label ID="lbl_appAuth_appointHeadToTxt" runat="server" Text="Appoint Acting Head Authority To:"></asp:Label>
&nbsp;<asp:DropDownList ID="ddl_appAuth_deptEmps" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <div class="form-inline">
        <div class="row">
    <asp:Label ID="lbl_appAuth_startDate" runat="server" Text="Start From: "></asp:Label>
&nbsp;<asp:TextBox ID="txtbox_dateStart" runat="server" CssClass="form-control" style="margin-top: 0px" TextMode="Date" AutoPostBack="True" OnTextChanged="txtbox_dateStart_TextChanged"></asp:TextBox>
    <br />
        </div>
        <div class="row">
    <asp:Label ID="Label6" runat="server" Text="End At:"></asp:Label>
&nbsp;<asp:TextBox ID="txtbox_dateEnd" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True"></asp:TextBox>
    <br />
    </div>
    <br />
    <asp:Button ID="button_appAuth_appoint" runat="server" Text="Confirm Appointment" OnClick="button_appAuth_appoint_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="button_appAuth_remove" runat="server" Text="Remove Appointment" OnClick="button_appAuth_remove_Click" />
    <br />
    <br />
        </div>
</asp:Content>

