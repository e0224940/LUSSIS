<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetails.aspx.cs" Inherits="Store_Clerk_PurchaseOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h2>Details of Purchase Order #<%= Session["PONo"] %>
                <asp:Button runat="server" CommandArgument="Delete" ID="DeleteButton" CssClass="btn btn-danger" Text="Delete PO" OnClick="Button_Click" />
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
                Date Issued: <%= ((DateTime)pO.DateIssued).ToString("dd-MMM-yyyy") %>
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

                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="LabelQty" runat="server" Text='<%# Bind("Qty") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxQty" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxQty" ErrorMessage="Field cannot be empty" Display="Dynamic" Style="color: red;"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxQty" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Enter only integer" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubTotal">
                        <ItemTemplate>
                            <asp:Label ID="LabelSubTotal" runat="server" Text='<%# Bind("SubTotal") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField
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