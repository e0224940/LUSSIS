<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemAdding.aspx.cs" Inherits="Department_Employee_ItemAdding" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">

Items
    <div>
        <asp:GridView ID="GVAddItem" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GVAddItem_SelectedIndexChanged" 
            >
             <Columns>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Username" />
                 <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                 
                <asp:CommandField ShowSelectButton="True" />
        </Columns>
        </asp:GridView>

        <asp:GridView ID="GVShowItem" runat="server" AutoGenerateColumns="false" >
            <Columns>
 <asp:TemplateField HeaderText="Description" SortExpression="description">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
 <asp:TemplateField HeaderText="Qty">
                    
                        <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    
<%--                        <asp:Label ID="lblQuantity" runat="server" ></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:Label ID="lblHiddenQty" runat="server" Text="Label" Visible="False"></asp:Label>
    </div>
 </asp:Content>
