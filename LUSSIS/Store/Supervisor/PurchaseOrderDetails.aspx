﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetails.aspx.cs" Inherits="Store_Supervisor_PurchaseOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
    <asp:Label ID="logicUniversityTitle" runat="server" Text="LOGIC University"></asp:Label>
    <br />
    <asp:Label ID="stationeryPurchaseOrderTitle" runat="server" Text="Stationery Purchase Order"></asp:Label>
    <br />
    <asp:Label ID="supplierLabel" runat="server" Text="Supplier: "></asp:Label>
    <asp:Label ID="supplierNameLabel" runat="server"></asp:Label>
    <asp:Label ID="poLabel" runat="server" Text="PO Number: "></asp:Label>
    <asp:Label ID="poNumberLabel" runat="server" Text="123 "></asp:Label>
    <br />
    <br />

    <asp:GridView ID="GridViewTest" GridLines="None"
                CssClass="table table-striped" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewTest_DataBound1">
        <Columns>
            <asp:BoundField DataField="ItemNo" HeaderText="Item No." />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Price" />
            <asp:TemplateField HeaderText="Amount">
                <ItemTemplate>
                    <asp:Label ID="amt" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Label ID="orderByLabel" runat="server" Text="Ordered by: "></asp:Label>
    <asp:Label ID="orderedByLabel" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="dateLabel" runat="server" Text="Date: "></asp:Label>
    <asp:Label ID="datedLabel" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="approveByLabel" runat="server" Text="Approved by: "></asp:Label>
    <asp:Label ID="approvedByLabel" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="date2Label" runat="server" Text="Date: "></asp:Label>
    <asp:Label ID="dated2Label" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Button ID="purchaseOrderDetailBut" CssClass="btn btn-info" runat="server" Text="Back" OnClick="purchaseOrderDetailBut_Click" />
        </div>
</asp:Content>

