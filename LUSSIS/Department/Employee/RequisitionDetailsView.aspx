<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionDetailsView.aspx.cs" Inherits="Department_Employee_RequisitionDetailsView" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Requisition Details</h1>
    <asp:Label ID="empName" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="empNameshow" runat="server" Text=" "></asp:Label>

    <br/>

    <asp:Label ID="date" runat="server" Text="Date:"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>

    <br /> <br />

    <asp:GridView ID="detailGrid" runat="server" AutoGenerateColumns ="False">
        <Columns>
            <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="No" runat="server" Text='<%# Bind("ItemNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Description" SortExpression="description">
                <ItemTemplate>
                    <asp:Label ID="No" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Qty">
                 <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" width="50px" Text='<%# Bind("quantity") %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>

            <asp:CommandField ShowDeleteButton="True" />

        </Columns>

    </asp:GridView>


</asp:Content>