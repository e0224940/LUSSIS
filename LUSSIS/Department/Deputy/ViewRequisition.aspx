<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisition.aspx.cs" Inherits="_Default" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <div class="row">
            <h2 class="sub-header">Details of Requisition #
                <asp:Label runat="server"><% Response.Write(Session["ReqNo"]); %></asp:Label>
            </h2>
        </div>
        <div class="row">
            <asp:GridView
                GridLines="None"
                CssClass="table table-striped"
                runat="server"
                ID="RequisitionDetailsGridView"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label ID="LabelItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="LabelItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="LabelQuantity" runat="server" Text='<%# Bind("Qty") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>No Items In This Requisition</EmptyDataTemplate>
            </asp:GridView>
        </div>
        <div class="row">
            <asp:TextBox runat="server" CssClass="form-control" placeholder="Remarks, if any" TextMode="MultiLine" ID="TextBoxReason"></asp:TextBox>
            <div class="form-group"></div>
        </div>
        <div class="row">
            <asp:Button runat="server" CommandArgument="Approve" ID="ApproveButton" CssClass="btn btn-success" Text="Approve" OnClick="Button_Click" />
            <asp:Button runat="server" CommandArgument="Reject" ID="RejectButton" CssClass="btn btn-danger" Text="Reject" OnClick="Button_Click" />
            <asp:Button runat="server" CommandArgument="Cancel" ID="BackButton" CssClass="btn" Text="Go Back" OnClick="Button_Click" />
        </div>
    </div>
</asp:Content>
