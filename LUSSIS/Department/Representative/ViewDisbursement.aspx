<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDisbursement.aspx.cs" Inherits="_Default" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <%if (Session["DNo"] != null)
        { %>

    <div class="row">
        <h1>Details of Disbursement #<%= Session["DNo"] %></h1>
    </div>

    <div class="row">
        <div class="col-xs-4">
            Department: <%= d.Department.DeptName %>
        </div>
        <div class="col-xs-4">
            Collection Date: <%= ((DateTime)d.DisbursementDate).ToString("dd-MM-yyyy") %>
        </div>
        <div class="col-xs-4">
            Collection Point: <%= d.CollectionPoint.CollectionPointDetails %>
        </div>
    </div>

    <div class="row">
        <asp:GridView
            runat="server"
            ID="DisbursementDetailsGridView"
            AutoGenerateColumns="False"
            GridLines="None"
            CssClass="table table-striped"
            DataKeyNames="ItemNo">
            <Columns>

                <asp:BoundField DataField="ItemNo" Visible="false" />

                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Description">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delivered">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Delivered") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate>No Items in this Disbursement</EmptyDataTemplate>
        </asp:GridView>
    </div>
    <%}
        else
        { %>
    <div class="row">
        <h1>No Pending Disbursement Right Now</h1>
    </div>
    <% }%>
</asp:Content>
