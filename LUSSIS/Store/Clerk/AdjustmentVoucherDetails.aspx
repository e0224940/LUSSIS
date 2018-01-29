﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdjustmentVoucherDetails.aspx.cs" Inherits="Store_Clerk_AdjustmentVoucherDetails" %>

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
                Date Issued: <%= aV.DateIssued.Value.ToString("dd MMM yyyy") %>
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

                    <asp:BoundField DataField="Qty" HeaderText="Qty Adjusted" SortExpression="Qty" />
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
            <a href="AdjustmentVoucherList.aspx">Back to Adjustment Voucher List Page</a>
        </div>

    </div>
</asp:Content>

