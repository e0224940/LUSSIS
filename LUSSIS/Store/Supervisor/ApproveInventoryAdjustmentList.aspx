<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveInventoryAdjustmentList.aspx.cs" Inherits="Store_Supervisor_ApproveInventoryAdjustmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="container">
         <asp:Label ID="list" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Approve Inventory Adjustment List"></asp:Label>
        <br />
        <br />

         <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="Pending Inventory Adjustment List"></asp:Label>

        <asp:GridView ID="approveInventoryAdjustmentListGridView" 
            runat="server" 
            cssclass="table table-striped" 
            GridLines="none"
            AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DateIssued","{0:MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Adjustment Voucher #" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="test" runat="server" Text='<%# Eval("AvNo") %>'
                                 NavigateUrl='<%#"~/Store/Supervisor/ApproveInventoryAdjustmentDetails.aspx?IAV=" + Eval("AvNo")%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
         </div>
</asp:Content>



<%--<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveInventoryAdjustmentList.aspx.cs" Inherits="Store_Supervisor_ApproveInventoryAdjustmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                <h2 class="sub-header">Approve Inventory Adjustment List</h2>
    <br />
    <h3>Pending Orders List</h3>
        <asp:GridView ID="approveInventoryAdjustmentListGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("DateIssued","{0:MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Adjustment Voucher #" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="test" runat="server" Text='<%# Eval("AvNo") %>'
                                 NavigateUrl='<%#"~/Store/Supervisor/ApproveInventoryAdjustmentDetails.aspx?IAV=" + Eval("AvNo")%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</asp:Content>--%>

