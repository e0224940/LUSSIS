<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPurchaseOrderHistory.aspx.cs" Inherits="Store_Clerk_ViewPurchaseOrderHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Purchase Order History</h1>
    <br />
    <asp:GridView 
        GridLines="None"
        CssClass="table table-striped"
        ID="PurchaseOrderGridView" runat="server" DataKeyNames="PONo" AutoGenerateColumns="False" OnRowDeleting="PurchaseOrderGridView_Delete">
        <Columns>
            <asp:BoundField DataField="PONo" HeaderText="PONo" SortExpression="PONo" Visible="false" />
            <asp:BoundField DataField="DateIssued" HeaderText="Date" SortExpression="DateIssued" />
            <asp:BoundField DataField="PONo" HeaderText="PO Number" SortExpression="PONo" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Button ID="PODetails_btn" CssClass="btn btn-primary" runat="server" Text="Details" CommandName="PODetails" CommandArgument='<%# Bind("PONo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>