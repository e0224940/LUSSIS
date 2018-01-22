<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRepresentative.aspx.cs" Inherits="_Default" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row">
            <div class="form-group">
                <h2 class="sub-header">Department Representative
                </h2>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label>Current Department Representative : </label>
                <br />
                <asp:Label runat="server" ID="DepRepLabel"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <% if (ViewState["Error"] != null)
                    { %>
                <div class="alert alert-warning"><% Response.Write(ViewState["Error"]); %></div>
                <%
                    } %>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <h2 class="sub-header"></h2>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label>Change Department Representative To : </label>
                <br />
                <asp:DropDownList runat="server" ID="DepRepDropDownList"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:Button runat="server" CommandArgument="ChangeRepresentative" ID="ChangeRepresentativeButton" CssClass="btn btn-primary" Text="Change" OnClick="Button_Click" />
            </div>
        </div>
    </div>
</asp:Content>
