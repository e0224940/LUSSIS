<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RaisedRequisition.aspx.cs" Inherits="Department_Employee_RaisedRequisition"MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
   
    <h1>Raised Requisition</h1>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="Name" runat="server"></asp:Label>
        <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
        <br />
<asp:Button ID="AddItem" runat="server" Text="Add Item" OnClick="AddItem_Click" Width="99px" />
<br />
</asp:Content>

