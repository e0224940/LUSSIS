<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReportDetails.aspx.cs" Inherits="Store_Supervisor_ReorderReportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
    <asp:Label ID="Label2" font-size="Medium" runat="server"></asp:Label>
    <asp:Label ID="Label3" runat="server" font-size="Large"></asp:Label>
    <br /> <br />
                <asp:GridView ID="ReorderReportDetailsGridView" 
                    GridLines="None" 
                    cssclass="table table-striped"  
                    runat="server" 
                    AutoGenerateColumns="False" 
                    OnPreRender="ReorderReportDetailsGridView_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label CSSClass="TxtAlignMid" ID="GridLabel1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Current Qty" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("CurrentQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty Ordered" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="PO#" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="pOLink" runat="server" Text='<%# Bind("PONo") %>' 
                            NavigateUrl='<%#"~/Store/Supervisor/PurchaseOrderDetails.aspx?SNO=" + Eval("PONo")%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    <br />
            <asp:Button ID="Button1" runat="server" Text="Back" OnClick="Button1_Click" />
            </div>
</asp:Content>

