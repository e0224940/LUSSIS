<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionHistory.aspx.cs" Inherits="Department_Employee_ViewRequisitionHistory" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <asp:label id="lbl_emp_raisedItem" runat="server" font-bold="True" font-size="XX-Large" text="Requisition History"></asp:label>
        <br />
        <br />
        <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">
                    
                    <asp:label id="label" runat="server" text="Employee Name:"></asp:label>
                </div>
                <div class="col-xs-1">
                    <asp:textbox id="name" runat="server" cssclass="btn-default form-control" autopostback="True" enabled="False">DeputyHeadNo</asp:textbox>
                </div>
                <br /><br /><br />
                <br />
                <asp:gridview
                    runat="server"
                    id="DetailGridView"
                    datakeynames="ReqNo"
                    autogeneratecolumns="False"
                    GridLines="None"
                    cssclass="table table-striped"
                    onrowdeleting="detailGridView_Delete"
                    onselectedindexchanged="DetailGridView_SelectedIndexChanged">
      <Columns>
            <asp:BoundField DataField="ReqNo" HeaderText="ReqNo" SortExpression="ReqNo" Visible="false" />
            <asp:BoundField DataField="DateIssued" DataFormatString="{0:dd MMM yyyy}" HeaderText="Date" SortExpression="DateIssued" />
            <asp:BoundField DataField="ReqNo" HeaderText="Requisition Form" SortExpression="ReqNo" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
        
             <asp:CommandField ShowSelectButton="True" />
             <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:gridview>
</asp:Content>
