﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdjustmentVoucherList.aspx.cs" Inherits="Store_Clerk_AdjustmentVoucherList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h1>Pending Adjustment Vouchers</h1>
        </div>

        <div class="row">
            <% if (Session["AVProcessed"] != null)
                { %>
            <div class="alert alert-success">
                Adjustment Voucher #<%= Session["AVProcessed"] %> Deleted Successfully.
            </div>
            <% 
                    Session.Remove("AVProcessed");
                }
            %>
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
            <asp:GridView
                runat="server"
                ID="PendingAdjustmentVoucherGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped"
                OnRowCommand="PendingAdjustmentVoucherGridView_RowCommand"
                OnRowDeleting="PendingAdjustmentVoucherGridView_Delete">
                <Columns>

                    <asp:BoundField DataField="AVNo" HeaderText="AV#" SortExpression="AVNo" />
                    <asp:BoundField DataField="DateIssued" HeaderText="Date Issued" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="DateIssued" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />

                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Button ID="ButtonDetails" CssClass="btn btn-primary" runat="server" Text="Details" CommandName="Details" CommandArgument='<%# Bind("AVNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="true" />

                </Columns>
                <EmptyDataTemplate>No Pending Adjustment Vouchers</EmptyDataTemplate>
            </asp:GridView>
        </div>

    </div>
</asp:Content>

