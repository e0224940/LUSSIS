<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementList.aspx.cs" Inherits="Store_Clerk_DisbursementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
             <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Pending Disbursements"></asp:Label>
           
        </div>

        <div class="row">
            <% if (Session["DProcessed"] != null)
                { %>
            <div class="alert alert-success">
                Disbursement #<%= Session["DProcessed"] %> Delievered Successfully.
            </div>
            <% 
                    Session.Remove("DProcessed");
                }
            %>
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
                    <asp:BoundField DataField="Date" HeaderText="Collection Date" SortExpression="Dates" DataFormatString="{0:dd-MM-yyyy}" />
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

