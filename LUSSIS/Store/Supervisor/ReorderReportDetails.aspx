<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReportDetails.aspx.cs" Inherits="Store_Supervisor_ReorderReportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="container">
         <div class="alert alert-success">
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br /> <br />
    <asp:Label ID="Label2" font-size="Medium" runat="server"></asp:Label>
    <asp:Label ID="Label3" runat="server" font-size="Large"></asp:Label>
    <br /> <br />
    <asp:Label ID="Label4" runat="server"></asp:Label>
             </div>
                      <asp:GridView ID="ReorderReportDetailsGridView" 
                          runat="server" 
                          cssclass="table table-striped" 
                          AutoGenerateColumns="False">
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
                            <asp:TemplateField HeaderText="Reorder Level" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("ReorderLevel") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reorder Quantity" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("ReorderQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="PO#" >
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("PONo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    <br /> 
    <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" Text="Return" OnClick="Button1_Click" />
        </div>
</asp:Content>

