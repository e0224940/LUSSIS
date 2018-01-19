<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemPage.aspx.cs" Inherits="Department_Employee_AddItemPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Add Item</h1>
    <div style="float: right">
        <asp:TextBox ID="SearchItem" runat="server"></asp:TextBox>
    <asp:Button ID="Search" runat="server" Text="Search" />
    <asp:Button ID="Confirm" runat="server" OnClick="Confirm_Click" Text="Confirm" />
    </div>
    <div>
    
    <br />
    <br />
                <asp:GridView   GridLines="None" runat="server" ID="StationeryGridView" AutoGenerateColumns="False" OnSelectedIndexChanged="StationeryGridView_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description:" SortExpression="Description" />
                    <asp:TemplateField HeaderText="Quantity:">
                     <ItemTemplate>
                        <asp:TextBox ID="Quantity" runat="server" ></asp:TextBox>
                    </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="successLabel" runat="server" Text="Success!" Visible="false">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="true" />
                </Columns>
                
            </asp:GridView>
   
  
        <asp:GridView   GridLines="None" runat="server" ID="Cart" AutoGenerateColumns="False">
          <Columns>
            <asp:TemplateField HeaderText="Description:">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity:">
            </asp:TemplateField>

          </Columns>
        </asp:GridView>
   
  
        </div>
</asp:Content>
