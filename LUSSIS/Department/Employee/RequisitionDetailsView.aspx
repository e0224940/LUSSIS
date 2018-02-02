<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionDetailsView.aspx.cs" Inherits="Department_Employee_RequisitionDetailsView" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <asp:Label ID="lbl_emp_raisedItem" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="View Requisition Details"></asp:Label>
        <br />
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    <asp:Label ID="empName" runat="server" Text="Employee Name:"></asp:Label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="empNameshow" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False">DeputyHeadNo</asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-inline">

                <div class="col-xs-2">
                    <asp:Label ID="reuNo" runat="server" Text="Requisition No:"></asp:Label>
                </div>
                <div class="col-xs-2">
                    <asp:TextBox ID="ReqId" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False">DeputyHeadNo</asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:GridView GridLines="None"
                    keynames="ReqNo" runat="server"
                    CssClass="table table-striped"
                    ID="GridViewForDetail"
                    AutoGenerateColumns="False"
                    OnRowCancelingEdit="OnRowCancelingEdit"
                    OnRowUpdating="OnRowUpdating"
                    OnRowDeleting="detailGrid_Delete"
                    OnRowEditing="detailGrid_Edit">
                    <Columns>
                        <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo">
                            <ItemTemplate>
                                <asp:Label ID="ItemNO" runat="server" Text='<%# Bind("itemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" SortExpression="description">
                            <ItemTemplate>
                                <asp:Label ID="des" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qty" SortExpression="quantity">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Visible='<%# Bind("isEditable") %>' />
                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Visible='<%# Bind("isEditable") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:Button ID="cancel" runat="server" Text="Back to History Page" CssClass="btn btn-primary" OnClick="cancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
