<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCollectionSuccessPage.aspx.cs" Inherits="Department_Representative_UpdateCollectionSuccessPage" MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h1>New Collection Point</h1>
        </div>

        <div class="row">
            <% if (Session["NewLoc"] != null)
                { %>
            <div class="alert alert-success">
                New Collection Point is <%= Session["NewLoc"] %> and New Collection Time is 
                <%= Session["NewTime"] %> 
            </div>
            <% 
                    Session.Remove("NewLoc");
                    Session.Remove("NewTime");
                }
            %>
        </div>
</div>
    </asp:Content>