<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionHistory.aspx.cs" Inherits="Department_Employee_ViewRequisitionHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Requisition History</h1>
    <asp:Label ID="label" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="name" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="DetailGridView" runat="server" DataKeyNames="ReqNo" AutoGenerateColumns="False" OnRowDeleting="detailGridView_Delete" OnSelectedIndexChanged="DetailGridView_SelectedIndexChanged">
      <Columns>
            <asp:BoundField DataField="ReqNo" HeaderText="ReqNo" SortExpression="ReqNo" Visible="false" />
            <asp:BoundField DataField="DateIssued" HeaderText="Date" SortExpression="DateIssued" />
            <asp:BoundField DataField="ReqNo" HeaderText="Requisition Form" SortExpression="ReqNo" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        
             <asp:CommandField ShowSelectButton="True" />
             <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
