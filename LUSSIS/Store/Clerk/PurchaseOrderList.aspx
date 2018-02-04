<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrderList.aspx.cs" Inherits="Store_Clerk_PurchaseOrderList" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">

    <% Session.Remove("PONo"); %>

</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">

        <div class="row">
            <h1>Pending Purchase Orders</h1>
        </div>

        <div class="row">
            <% if (Session["POProcessed"] != null)
                { %>
            <div class="alert alert-success">
                Purchase Order #<%= Session["POProcessed"] %> Deleted Successfully.
            </div>
            <% 
                    Session.Remove("POProcessed");
                }
            %>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="PendingPurchaseOrderGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped"
                OnRowCommand="PendingPurchaseOrderGridView_RowCommand"
                OnRowDeleting="PendingPurchaseOrderGridView_Delete">
                <Columns>
                    <asp:BoundField DataField="PONo" HeaderText="PO#" SortExpression="PONo" />
                    <asp:BoundField DataField="DateIssued" HeaderText="Date" SortExpression="DateIssued" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" SortExpression="Supplier" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Button ID="PODetails_btn" CssClass="btn btn-primary" runat="server" Text="Details" CommandName="PODetails" CommandArgument='<%# Bind("PONo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
                <EmptyDataTemplate>No Pending Purchase Orders Found</EmptyDataTemplate>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
