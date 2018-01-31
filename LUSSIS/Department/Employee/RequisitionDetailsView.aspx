<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionDetailsView.aspx.cs" Inherits="Department_Employee_RequisitionDetailsView" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <asp:label id="lbl_emp_raisedItem" runat="server" font-bold="True" font-size="XX-Large" text="View Requisition Details"></asp:label>
        <br />
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    <asp:label id="empName" runat="server" text="Employee Name:"></asp:label>
                </div>
                <div class="col-xs-2">
                    <asp:textbox id="empNameshow" runat="server" cssclass="btn-default form-control" autopostback="True" enabled="False">DeputyHeadNo</asp:textbox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="form-inline">

                <div class="col-xs-2">
                    <asp:label id="reuNo" runat="server" text="Requisition No:"></asp:label>
                </div>
                <div class="col-xs-2">
                    <asp:textbox id="ReqId" runat="server" cssclass="btn-default form-control" autopostback="True" enabled="False">DeputyHeadNo</asp:textbox>
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
                <asp:gridview gridlines="None" data
                    keynames="ReqNo" runat="server"
                    cssclass="table table-striped"
                    id="GridViewForDetail"
                    autogeneratecolumns="False"
                    onrowcancelingedit="OnRowCancelingEdit"
                    onrowupdating="OnRowUpdating"
                    onrowdeleting="detailGrid_Delete"
                    onrowediting="detailGrid_Edit">
        <Columns>
                <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo" >
                <ItemTemplate>
                    <asp:Label ID="ItemNO" runat="server" Text='<%# Bind("itemNo") %>' ></asp:Label>
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
            <asp:CommandField ShowEditButton="true"/>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:gridview>
            </div>
        </div>
    </div>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:button id="cancel" runat="server" text="Back to History Page"  cssclass="btn btn-primary"  onclick="cancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
