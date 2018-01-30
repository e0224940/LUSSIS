<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetails.aspx.cs" Inherits="Store_Clerk_PurchaseOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h2>Details of Purchase Order #<%= Session["PONo"] %>
                <asp:Button runat="server" CommandArgument="Delete" ID="DeleteButton" CssClass="btn btn-danger" Text="Delete Voucher" OnClick="Button_Click" />
            </h2>
        </div>

        <div class="row">
            <div class="col-xs-3">
                PO#: <%= Session["PONo"] %>
            </div>
            <div class="col-xs-3">
                Supplier: <%= pO.Supplier.SupplierName %>
            </div>
            <div class="col-xs-3">
                Date Issued: <%= pO.DateIssued %>
            </div>
            <div class="col-xs-3">
                Status: <%= pO.Status %>
            </div>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="PODetailsGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped"
                OnRowDeleting="OnRowDeleting"
                OnRowEditing="OnRowEditing"
                OnRowCancelingEdit="OnRowCancelingEdit"
                OnRowUpdating="OnRowUpdating">

                <Columns>

                    <asp:TemplateField HeaderText="Item No">
                        <ItemTemplate>
                            <asp:Label ID="LabelItemNo" runat="server" Text='<%# Bind("ItemNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:Label ID="LabelItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <asp:Label ID="LabelUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />

                    <asp:TemplateField HeaderText="SubTotal">
                        <ItemTemplate>
                            <asp:Label ID="LabelSubTotal" runat="server" Text='<%# Bind("SubTotal") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField
                        HeaderText="Action"
                        ButtonType="Button"
                        ShowEditButton="True"
                        ShowDeleteButton="True" />

                </Columns>
                <EmptyDataTemplate>No Items In This Purchase Order</EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="row">
            <% if (Session["Error"] != null)
                { %>
            <div class="alert alert-danger">
                An error has occured : <%= (string)Session["Error"] %>
            </div>
            <% 
                    Session.Remove("Error");
                }
            %>
        </div>

        <div class="row">
            <a href="PurchaseOrderList.aspx">Back to Purchase Order List Page</a>
        </div>

    </div>
</asp:Content>

