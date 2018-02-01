<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovePOList.aspx.cs" Inherits="Store_Supervisor_ApprovePOList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
        <asp:Label ID="approve" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Approve Purchase Order"></asp:Label>
        <br />
        <br />
        <asp:Label ID="pending" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Pending Orders List"></asp:Label>
        <br />

        <asp:GridView ID="ApprovePOListGridView" 
            runat="server" 
            cssclass="table table-striped" 
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
                    <asp:TemplateField HeaderText="Purchase Order #" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="test" runat="server" Text='<%# Eval("PONo") %>'
                                 NavigateUrl='<%#"~/Store/Supervisor/ApprovePODetails.aspx?PO=" + Eval("PONo")%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</div>
</asp:Content>

