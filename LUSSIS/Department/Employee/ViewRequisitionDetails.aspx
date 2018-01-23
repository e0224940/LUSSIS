<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequisitionDetails.aspx.cs" Inherits="Department_Employee_ViewRequisitionDetails" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Requisition Details</h1>
    <asp:label id="Label1" runat="server" text="Employee Name:"></asp:label>
    <asp:label id="name" runat="server"></asp:label>
    <br />
    <asp:label runat="server" text="Date:" id="Label2"></asp:label>
    <asp:label runat="server" text="Date" id="date"></asp:label>
    <br />
    <br />
    <asp:gridview gridlines="None" runat="server" id="ReqDetails" autogeneratecolumns="False">
                    <Columns>
                        <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" SortExpression="ItemNo" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Qty" />
                          <ItemTemplate>
                                <asp:TextBox ID="Quantity" runat="server" TextMode="Number" ReadOnly="true"></asp:TextBox>
                          </ItemTemplate>
                        <asp:CommandField ShowEditButton="true" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                </asp:gridview>

    <asp:button id="Submit" runat="server" text="Submit" />

</asp:Content>
