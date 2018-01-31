<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdjustmentVoucherDetails.aspx.cs" Inherits="Store_Clerk_AdjustmentVoucherDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h2>Details of AdjustmentVoucher #<%= Session["AVNo"] %>
                <asp:Button runat="server" CommandArgument="Delete" ID="DeleteButton" CssClass="btn btn-danger" Text="Delete Voucher" OnClick="Button_Click" />
            </h2>
        </div>

        <div class="row">
            <div class="col-xs-4">
                Voucher#: <%= Session["AVNo"] %>
            </div>
            <div class="col-xs-4">
                Date Issued: <%= ((DateTime)aV.DateIssued).ToString("dd-MM-yyyy") %>
            </div>
            <div class="col-xs-4">
                Status: <%= aV.Status %>
            </div>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="AVDetailsGridView"
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

                    <asp:TemplateField HeaderText="Qty Adjusted">
                        <ItemTemplate>
                            <asp:Label ID="LabelQty" runat="server" Text='<%# Bind("Qty") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxQty" runat="server" Text='<%# Bind("Qty") %>'></asp:TextBox>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TextBoxQty" ErrorMessage="Qty Field is Empty" Display="Dynamic" Style="color: red"></asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" 
                                    Operator="DataTypeCheck" Type="Integer"
                                    ControlToValidate="TextBoxQty" ErrorMessage="Value must be a Number" Display="Dynamic" Style="color: red" />
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AdjustmentAmount">
                        <ItemTemplate>
                            <asp:Label ID="AdjustmentAmount" runat="server" Text='<%# Bind("AdjustmentAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />

                    <asp:CommandField
                        HeaderText="Action"
                        ButtonType="Button"
                        ShowEditButton="True"
                        ShowDeleteButton="True" />

                </Columns>
                <EmptyDataTemplate>No Items In This Adjustment Voucher</EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="row">
            <a href="AdjustmentVoucherList.aspx">Back to Adjustment Voucher List Page</a>
        </div>

    </div>
</asp:Content>

