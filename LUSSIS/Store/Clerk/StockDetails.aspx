<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockDetails.aspx.cs" Inherits="Store_Clerk_StockDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <div class="form-inline">
                <h1>Stock Details <%= (string)Session["stockNo"] %></h1>
            </div>
        </div>
        <div class="row">
            <table class="table table-striped">
                <tr>
                    <th>Item No:</th>
                    <td>
                        <asp:Label ID="ItemNoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Item Description:</th>
                    <td>
                        <asp:Label ID="ItemDescriptionLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Bin#:</th>
                    <td>
                        <asp:Label ID="BinNoLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>Quantity:</th>
                    <td>
                        <asp:Label ID="QtyLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>UOM:</th>
                    <td>
                        <asp:Label ID="UomLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>1st Supplier:</th>
                    <td>
                        <asp:Label ID="FirstSupplierLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>2nd Supplier:</th>
                    <td>
                        <asp:Label ID="SecondSupplierLabel" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <th>3rd Supplier:</th>
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

        <div class="row">
            <h4>Add Incoming Quantity From Supplier</h4>
            <div class="form-group">
                <label>Choose Supplier : </label>
                <br />
                <asp:DropDownList CssClass="btn btn-default dropdown-toggle" runat="server" ID="SupplierDropDownList"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Enter Quantity: </label>
                <br />
                <asp:TextBox CssClass="form-input" runat="server" ID="QuantityTextBox" TextMode="Number" />
                <asp:RangeValidator runat="server" ControlToValidate="QuantityTextBox" MinimumValue="1" MaximumValue="999999" />
            </div>

            <div class="form-group">
                <asp:Button runat="server" ID="AddQuantityButton" Text="Add Quantity" CssClass="btn btn-primary" OnClick="AddQuantityButton_Click" />
            </div>
        </div>
    </div>
</asp:Content>

