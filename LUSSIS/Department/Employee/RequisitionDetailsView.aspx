<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionDetailsView.aspx.cs" Inherits="Department_Employee_RequisitionDetailsView" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Requisition Details</h1>
    <asp:Label ID="empName" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="empNameshow" runat="server" Text=" "></asp:Label>

    <br />

    <asp:Label ID="reuNo" runat="server" Text="ReqNo:"></asp:Label>
    <asp:Label ID="Num" runat="server" Text=" "></asp:Label>

    <br />
    <br />

    <asp:GridView ID="detailGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="detailGrid_Delete" OnRowEditing="detailGrid_Edit">
        <Columns>
            <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="No" runat="server" Text='<%# Bind("ItemNo") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description" SortExpression="description">
                <ItemTemplate>
                    <asp:Label ID="No" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:TextBox ID="lblQuantity" runat="server" Width="50px" ReadOnly="true" Text='<%# Bind("quantity") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="New Qty">
                <ItemTemplate>
                    <asp:TextBox ID="changeQty" runat="server" Width="80px" Text="" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="true" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>

    </asp:GridView>

    <br />
    <asp:Button ID="submit" runat="server" Text="Submit" />
    <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" />
</asp:Content>
