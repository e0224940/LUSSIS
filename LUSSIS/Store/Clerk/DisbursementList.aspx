<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementList.aspx.cs" Inherits="Store_Clerk_DisbursementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <div class="container">
        <div class="row">
            <h1>Pending Disbursements</h1>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="PendingDisbursementGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped"
                OnRowCommand="DisbursementGridView_RowCommand">
                <Columns>

                    <asp:BoundField DataField="DisbursementNo" HeaderText="Disbursement #" SortExpression="DisbursementNo" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName" />
                    <asp:BoundField DataField="Date" DataFormatString="{0:dd MMM yyyy}" HeaderText="Collection Date" SortExpression="Dates" />
                    <asp:BoundField DataField="CollectionPoint" HeaderText="Collection Point" SortExpression="CollectionPoint" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />

                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Button ID="ButtonDetails" CssClass="btn btn-primary" runat="server" Text="Details" CommandName="Details" CommandArgument='<%# Bind("DisbursementNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>No Pending Disbursements</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

