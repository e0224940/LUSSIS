<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="form-group">
        <asp:TextBox CssClass="form-control" ID="OldPassword" runat="server" placeholder="Old Password"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="OldPassword" ErrorMessage="Old Password Required" />
    </div>
    <div class="form-group">
        <asp:TextBox CssClass="form-control" ID="NewPassword" runat="server" placeholder="New Password"/>
        <asp:CompareValidator runat="server" ControlToValidate="NewPassword" ControlToCompare="ConfirmPassword" Operator="Equal" ErrorMessage="Password Mismatch" />
    </div>
    <div class="form-group">
        <asp:TextBox CssClass="form-control" ID="ConfirmPassword" runat="server" placeholder="Confirm Password"/>
        <asp:CompareValidator runat="server" ControlToValidate="ConfirmPassword" ControlToCompare="NewPassword" Operator="Equal" ErrorMessage="Password Mismatch"/>
    </div>
    <div class="form-group">
        <asp:Button ID="ChangePasswordButton" CssClass="btn btn-primary" runat="server" OnClick="ChangePasswordButton_Click" Text="Change Password"/>
    </div>
    <%if (SuccessLabel.Text.Length > 0)
        { %>
    <div class="form-group">
        <div class="alert alert-success">
            <asp:Label runat="server" ID="SuccessLabel" Text="" />
        </div>
    </div>
    <%} %>
    <%if (ErrorLabel.Text.Length > 0)
        { %>
    <div class="form-group">
        <div class="alert alert-danger">
            <asp:Label runat="server" ID="ErrorLabel" Text="" />
        </div>
    </div>
    <%} %>
</asp:Content>
