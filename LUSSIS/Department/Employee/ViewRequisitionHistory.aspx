<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionHistory.aspx.cs" Inherits="Department_Employee_ViewRequisitionHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Requisition History</h1>
    <asp:Label ID="label" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="name" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="ReqHistory" runat="server" GridLines="None" AutoGenerateColumns="False">
        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="DateIssued" />
        <asp:BoundField DataField="RequisitionForm" HeaderText="Requisition Form" SortExpression="ReqNo" />
        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
    </asp:GridView>
</asp:Content>
