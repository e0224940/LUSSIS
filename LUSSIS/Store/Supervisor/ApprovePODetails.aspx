<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovePODetails.aspx.cs" Inherits="Store_Supervisor_ApprovePODetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">            
    <asp:Label ID="listt" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Approve Pending Purchase Orders"></asp:Label>
        <br /> <br />

    <asp:Label ID="purchaseOrderDetailsLabel" runat="server" Text="PO Number:"></asp:Label>
    <asp:Label ID="poNumberLabel" runat="server"></asp:Label>
    <br />
    <asp:Label ID="supplierLabel" runat="server" Text="Supplier: "></asp:Label>
    <asp:Label ID="suppliernameLabel" runat="server"></asp:Label>
    <br /> <br />
                          <asp:GridView ID="ApprovePODetailsGridView"
                               cssclass="table table-striped" GridLines="None"
             runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemCode" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    <asp:Label ID="approvePORemarksLabel" runat="server" Text="Remarks: "></asp:Label>
    <asp:TextBox ID="approvePORemarksTB" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="approveButton" CssClass="btn btn-success" runat="server" Text="Approve" OnClick="approveButton_Click" />
    <asp:Button ID="rejectButton" CssClass="btn btn-danger" runat="server" Text="Reject" OnClick="rejectButton_Click" />
    <asp:Button ID="approvePOBackBut" CssClass="btn btn-info" runat="server" Text="Back" OnClick="approvePOBackBut_Click" />
        </div>
</asp:Content>


