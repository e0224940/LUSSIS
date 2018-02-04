<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovePOList.aspx.cs" Inherits="Store_Supervisor_ApprovePOList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
            <h2 class="sub-header">Approve Purchase Order</h2>
    <br />
    <h3>Pending Orders List</h3>
        <asp:GridView ID="ApprovePOListGridView" runat="server" AutoGenerateColumns="False">
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
                    <asp:TemplateField HeaderText="Purchase Order #" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="test" runat="server" Text='<%# Eval("PONo") %>'
                                 NavigateUrl='<%#"~/Store/Supervisor/ApprovePODetails.aspx?PO=" + Eval("PONo")%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

</asp:Content>

