<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAllPendingRequisitions.aspx.cs" Inherits="_Default" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h2 class="sub-header">Requisitions Pending Approval</h2>
    <% if (Session["RequisitionProcessed"] != null)
        { %>
    <div class="alert alert-info">
        <asp:Label ID="SuccessLabel" runat="server">Requisition #<% Response.Write(Session["RequisitionProcessed"]); %> has been processed</asp:Label>
    </div>
    <% 
            Session.Remove("RequisitionProcessed");
        }
    %>
    <% if (Session["Error"] != null)
        { %>
    <div class="alert alert-danger">
        <asp:Label ID="Label1" Text='An error has occured : Could not Process Requisition' runat="server"></asp:Label>
    </div>
    <% 
            Session.Remove("Error");
        }
    %>
    <asp:GridView
        GridLines="None"
        CssClass="table table-striped"
        runat="server"
        ID="PendingRequisitionGridView"
        AutoGenerateColumns="False"
        OnRowCommand="PendingRequisitionGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:Label ID="LabelReqNo" runat="server" Text='<%# Bind("ReqNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Issued" >
                <ItemTemplate>
                    <asp:Label ID="LabelDateIssued" runat="server" Text='<%# Eval("DateIssued", "{0:dd MMM yyyy}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee">
                <ItemTemplate>
                    <asp:Label ID="LabelEmpName" runat="server" Text='<%# Bind("EmpName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="LabelEmail" runat="server" Text='<%# Bind("Email") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details">
                <ItemTemplate>
                    <asp:Button ID="ButtonDetails" CssClass="btn btn-primary" runat="server" Text="View" CommandName="Details" CommandArgument='<%# Bind("ReqNo") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No Pending Requisitions Found</EmptyDataTemplate>
    </asp:GridView>
</asp:Content>
