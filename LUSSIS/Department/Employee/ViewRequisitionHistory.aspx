<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionHistory.aspx.cs" Inherits="Department_Employee_ViewRequisitionHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <asp:Label ID="lbl_emp_raisedItem" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Requisition History"></asp:Label>
        <br />
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    <asp:Label ID="label" runat="server" Text="Employee Name:"></asp:Label>
                </div>
                <div class="col-xs-1">
                    <asp:TextBox ID="name" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False">DeputyHeadNo</asp:TextBox>
                </div>
                <br />
                <br />
                <br />
                <br />
                <asp:GridView
                    runat="server"
                    ID="DetailGridView"
                    DataKeyNames="ReqNo"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    CssClass="table table-striped"
                    OnRowDeleting="detailGridView_Delete"
                    OnSelectedIndexChanged="DetailGridView_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ReqNo" HeaderText="Req No" SortExpression="ReqNo" />
                        <asp:BoundField DataField="DateIssued" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date" SortExpression="DateIssued" />
                        <%-- <asp:BoundField DataField="ReqNo" HeaderText="Requisition Form" SortExpression="ReqNo" />--%>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />


                       <%-- <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("Status") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextStatus" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:CommandField ShowSelectButton="True" />

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="Delete" Visible='<%# Eval("Status").Equals("Pending") %>' CommandArgument='<%# Eval("ReqNo") %>' OnClick="DeleteButton_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
</asp:Content>
