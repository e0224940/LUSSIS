<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionHistory.aspx.cs" Inherits="Department_Employee_ViewRequisitionHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
        <h1 style="color: #000000">Requisition History</h1>
<asp:Label ID="label" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="name" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</asp:Content>
