<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemPage.aspx.cs" Inherits="Department_Employee_AddItemPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Raised Requisition</h1>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="NameLB" runat="server"></asp:Label>
    <br />
    <asp:Label runat="server" Text="Date:" ID="Label2"></asp:Label>
    <asp:Label runat="server" Text=" " ID="date"></asp:Label>
    <br />
    <div style="float: right">
        <asp:TextBox ID="SearchItemText" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />

        <asp:Button ID="CancelSearch" runat="server" Text="Cancel" OnClick="CancelSearch_Click" />

    </div>

    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:GridView GridLines="None" runat="server" ID="StationeryGridView" AutoGenerateColumns="False" OnSelectedIndexChanged="StationeryGridView_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" SortExpression="ItemNo" Visible="false" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="Quantity" runat="server" TextMode="Number" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" />

                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-6">
                <asp:GridView GridLines="Horizontal" DataKeyNames="ItemNo" runat="server" ID="Cart" AutoGenerateColumns="False" OnRowDeleting="Cart_GridViewDelete">
                    <Columns>
                        <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ItemNo" runat="server" Text='<%# Bind("ItemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="description">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">

                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Width="50px" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="Confirm" runat="server" OnClick="Confirm_Click" Text="Confirm" />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;
            <asp:Button runat="server" Text="Cancel" ID="Cancel" OnClick="Cancel_Click" />
        </div>
    </div>

    <br />
</asp:Content>
