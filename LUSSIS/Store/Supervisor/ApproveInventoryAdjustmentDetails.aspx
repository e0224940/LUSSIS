<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveInventoryAdjustmentDetails.aspx.cs" Inherits="Store_Supervisor_ApproveInventoryAdjustmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                    <h2 class="sub-header">Approve Inventory Adjustment</h2>

    <asp:Label ID="approveInvAdjLabel" runat="server" Text="Adjustment Voucher# : "></asp:Label>
    <asp:Label ID="invAdjLabel" runat="server"></asp:Label>
    <br /> <br />
    <asp:Label ID="byLabel" runat="server" Text="By: "></asp:Label>
    <asp:Label ID="invAdjClerkLabel" runat="server"></asp:Label>
    <br /> <br />
    <asp:Label ID="dateRaisedLabel" runat="server" Text="Date Raised: "></asp:Label>
    <asp:Label ID="dateLabel" runat="server" Text="30-Dec-2017"></asp:Label>
    <br /> <br />
                          <asp:GridView ID="ApproveInventoryAdjustmentDetailsGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemCode" >
                        <ItemTemplate>
                            <asp:Label ID="ItemCode" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity Adjusted" >
                        <ItemTemplate>
                            <asp:Label ID="qtyAdjustedAmt" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" >
                        <ItemTemplate>
                            <asp:Label ID="Remarks" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    <br />
    <asp:Button ID="approveAdjustmentButton" runat="server" Text="Approve" OnClick="approveAdjustmentButton_Click" />
    <asp:Button ID="rejectAdjustmentButton" runat="server" Text="Reject" OnClick="rejectAdjustmentButton_Click" />
    <asp:Button ID="approveAVBackBut" runat="server" Text="Back" OnClick="approveAVBackBut_Click" />

</asp:Content>

