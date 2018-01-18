<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemPage.aspx.cs" Inherits="Department_Employee_AddItemPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Add Item</h1>
    <div style="float: right">
        <asp:TextBox ID="SearchItem" runat="server"></asp:TextBox>
    <asp:Button ID="Search" runat="server" Text="Search" />
        <br />
    <asp:Label ID="Label1" runat="server" Text="Quantity:"></asp:Label>
   
    <asp:TextBox ID="Quantity" runat="server" Width="50px"></asp:TextBox>
     <br />
    <asp:Button ID="Confirm" runat="server" OnClick="Confirm_Click" Text="Confirm" />
    </div>
    <div>
    
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="LUSSIS" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
        </Columns>
    </asp:GridView>
    &nbsp;<asp:SqlDataSource ID="LUSSIS" runat="server" ConnectionString="<%$ ConnectionStrings:LussisConnectionString %>" SelectCommand="SELECT [Description] FROM [StationeryCatalogue]"></asp:SqlDataSource>
    <br />
        </div>
</asp:Content>
