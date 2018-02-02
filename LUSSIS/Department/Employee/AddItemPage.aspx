<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemPage.aspx.cs" Inherits="Department_Employee_AddItemPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <asp:label id="lbl_emp_raisedItem" runat="server" font-bold="True" font-size="XX-Large" text="Create Requisition"></asp:label>
        <br />
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    <asp:label id="Label1" runat="server" text="Employee Name:"></asp:label>
                </div>
                <div class="col-xs-2">
                    <asp:textbox id="NameLB" runat="server" cssclass="btn-default form-control" autopostback="True" enabled="False">DeputyHeadNo</asp:textbox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    <asp:label id="Label2" runat="server" text="Date:"></asp:label>
                </div>
                <div class="col-xs-2">
                    <asp:textbox id="date" runat="server" cssclass="btn-default form-control" autopostback="True" enabled="False">Date</asp:textbox>
                </div>
            </div>
        </div>
        <br />
        <br />

        <div class="row">
            <div class="form-inline">
                <div class="col-xs">
                    &nbsp&nbsp&nbsp<asp:label id="Label3" runat="server" text="Search by description:"></asp:label>
                </div>
                <div class="col-xs-3">
                    <asp:textbox id="SearchItemText" runat="server" cssclass="btn-default form-control" autopostback="false" enabled="True"></asp:textbox>
                </div>
                <div class="col-xs-1">
                    <asp:button id="Button1" runat="server" text="Search" cssclass="btn btn-primary" onclick="Search_Click" />
                </div>
                <div class="col-xs-4">
                    <asp:button id="Button2" runat="server" text="Cancel" onclick="CancelSearch_Click" cssclass="btn btn-danger" />
                </div>
                <div class="col-xs-2">
                    <asp:button runat="server" text="Back To History Page" width="300px" id="Cancel" cssclass="btn btn-default" onclick="Cancel_Click" />
                </div>
            </div>
        </div>
    </div>


    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:gridview gridlines="None" 
                    runat="server" 
                    cssclass="table table-striped" 
                    id="StationeryGridView" 
                    autogeneratecolumns="False" 
                    onselectedindexchanged="StationeryGridView_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" SortExpression="ItemNo" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="Quantity" runat="server" TextMode="Number" cssclass="btn-default form-control" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" />
                    </Columns>
                </asp:gridview>
                <asp:label id="showerrorLb" runat="server" text=""></asp:label>
<%--                <asp:Label ID="itemcheckText" runat="server" Text="Label"></asp:Label>--%>
            </div>
            <div class="col-sm-6">
                <asp:gridview GridLines="None" datakeynames="ItemNo" runat="server" cssclass="table table-striped" id="Cart" autogeneratecolumns="False" onrowdeleting="Cart_GridViewDelete">
                    <Columns>
                        <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo">
                            <ItemTemplate>
                                <asp:Label ID="ItemNo" runat="server" Text='<%# Bind("ItemNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="description">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">

                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Width="50px" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="deleteButton" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:gridview>
            </div>
            <br /><br /><br /><br /><br /><br /><br />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:button id="Confirm" runat="server" onclick="Confirm_Click" text="Submit" cssclass="btn btn-success" width="100px" visible="false" />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:button id="Delete" runat="server" onclick="Delete_Click" text="Delete" cssclass="btn btn-danger" width="100px" visible="false" />

            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <br /><br /><br />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp;&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:label id="Msg" runat="server" style="color:red"></asp:label>
        </div>
    </div>

    <br />
</asp:Content>
