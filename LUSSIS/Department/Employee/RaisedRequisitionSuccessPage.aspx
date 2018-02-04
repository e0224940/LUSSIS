<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaisedRequisitionSuccessPage.aspx.cs" Inherits="Department_Employee_RaisedRequisitionSuccessPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">

        <div class="row">
            <h1>Requisition Raised Successfully.</h1>
        </div>

        <div class="row">
            <% if (Session["success"] != null)
                { %>
            <div class="alert alert-success">
               Thank You!
               Requisition Raised Successfully. <br />
               Status: <b>Pending</b> 
            </div>
            <% 
                    Session.Remove("success");
                }
            %>
        </div>
            <div style="float: none">
        <asp:Button ID="view" runat="server" Text="View History Page" class="btn btn-primary" OnClick="view_Click"/>
                </div>
</div>
</asp:Content>