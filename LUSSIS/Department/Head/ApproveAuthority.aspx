<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveAuthority.aspx.cs" Inherits="Department_Head_ApproveAuthority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
        <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Manage Your Authority"></asp:Label>
        <br />
        <br />
          <div class="row">
    <div class="form-inline">
            <div class="col-xs-2">
    <asp:Label ID="lbl_appAuth_currentHeadTxt" runat="server" Text="Current Acting Head:"></asp:Label>
                </div>
        <div class="col-xs-2">
    <asp:TextBox ID="txtBox_appAuth_currentHead" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False">DeputyHeadNo</asp:TextBox>
                    </div>
        </div>
    </div>
    <br />
    <br />


    <asp:Label ID="lbl_appAuth_appActingHeadTxt" runat="server" Font-Bold="True" Font-Underline="True" Text="Appoint Acting Head"></asp:Label>
    <br />
    <asp:Label ID="lbl_appAuth_appointHeadToTxt" runat="server" Text="Appoint Acting Head Authority To:"></asp:Label>
&nbsp;<asp:DropDownList ID="ddl_appAuth_deptEmps" CssClass="btn btn-default dropdown-toggle" runat="server">
    </asp:DropDownList>
    <br />
    <br />
        <div class="row">
    <div class="form-inline">
            <div class="col-xs-2">
    <asp:Label ID="lbl_appAuth_startDate" runat="server" Text="Start From: "></asp:Label>
&nbsp;<asp:TextBox ID="txtbox_dateStart" runat="server" CssClass="form-control" style="margin-top: 0px" TextMode="Date" AutoPostBack="True" OnTextChanged="txtbox_dateStart_TextChanged"></asp:TextBox>
                        </div>
                <div class="col-xs-2">
    <asp:Label ID="lbl_appAuth_endDate" runat="server" Text="End At:"></asp:Label>
            <asp:TextBox ID="txtbox_dateEnd" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True"></asp:TextBox>
                </div>
    </div>
            </div>
        <br />
                <div class="row">
        <div class="form-inline">
                        <div class="col-xs-2">
    <asp:Button ID="button_appAuth_appoint" runat="server" CssClass="btn btn-success" Text="Confirm Appointment" OnClick="button_appAuth_appoint_Click" />
                            </div>
            <div class="col-xs-2">
    <asp:Button ID="button_appAuth_remove" runat="server" CssClass="btn btn-danger" Text="Remove Appointment" OnClick="button_appAuth_remove_Click" />
    </div>
            </div>
    </div>
    </div>
</asp:Content>

