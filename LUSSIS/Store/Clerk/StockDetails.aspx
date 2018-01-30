<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockDetails.aspx.cs" Inherits="Store_Clerk_StockDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h1>Stock Details <%= (string)Session["stockNo"] %></h1>
        </div>

        <div class="row">
            <table>
                <tr>
                    <td>Item No:</td>
                    <td>
                        <asp:Label ID="ItemNoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Item Description:</td>
                    <td>
                        <asp:Label ID="ItemDescriptionLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Bin#:</td>
                    <td>
                        <asp:Label ID="BinNoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>UOM:</td>
                    <td>
                        <asp:Label ID="UomLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>1st Supplier:</td>
                    <td>
                        <asp:Label ID="FirstSupplierLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>2nd Supplier:</td>
                    <td>
                        <asp:Label ID="SecondSupplierLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>3rd Supplier:</td>
                    <td>
                        <asp:Label ID="ThirdSupplierLabel" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="StockDetailsGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="StockTxnNo" SortExpression="StockTxnNo" Visible="false" />
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                    <asp:BoundField DataField="DeptSupplier" HeaderText="Dept/Supplier" SortExpression="DeptSupplier" />
                    <asp:BoundField DataField="QtyRemarks" HeaderText="Qty" SortExpression="QtyRemarks" />
                    <asp:BoundField DataField="Balance" HeaderText="Balance" SortExpression="Balance" />
                </Columns>
                <EmptyDataTemplate>No Available Stock Txns</EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="row">
            <a href="StockList.aspx">Back to Stock List</a>
        </div>
    </div>
</asp:Content>

