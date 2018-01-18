<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionDetails.aspx.cs" Inherits="Department_Employee_ViewRequisitionDetails" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Requisition Details</h1>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Employee Name:"></asp:Label>
        <asp:Label ID="name" runat="server"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="EntityDataSource1">
        </asp:GridView>
        <asp:EntityDataSource ID="EntityDataSource1" runat="server">
        </asp:EntityDataSource>
    </p>
    <p>
        <asp:Button ID="Submit" runat="server" Text="Submit" />
    </p>
</asp:Content>
