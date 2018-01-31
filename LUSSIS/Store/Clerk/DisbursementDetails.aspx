<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementDetails.aspx.cs" Inherits="Store_Clerk_DisbursementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <h1>Details of Disbursement #<%= Session["DNo"] %></h1>
        </div>

        <div class="row">
            <% if (Session["Error"] != null)
                { %>
            <div class="alert alert-danger">
                <%= (string) Session["Error"] %>
            </div>
            <% 
                    Session.Remove("Error");
                }
            %>
        </div>

        <%if (Session["DNo"] != null)
            { %>

        <div class="row">
            <div class="col-xs-4">
                Department: <%= d.Department.DeptName %>
            </div>
            <div class="col-xs-4">
                Collection Date: <%= d.DisbursementDate %>
            </div>
            <div class="col-xs-4">
                Collection Point: <%= d.CollectionPoint.CollectionPointDetails %>
            </div>
        </div>

        <div class="row">
            <asp:GridView
                runat="server"
                ID="DisbursementDetailsGridView"
                AutoGenerateColumns="False"
                GridLines="None"
                CssClass="table table-striped"
                DataKeyNames="ItemNo"
                OnRowEditing="OnRowEditing"
                OnRowUpdating="OnRowUpdating"
                OnRowCancelingEdit="OnRowCancelingEdit">
                <Columns>

                    <asp:BoundField DataField="ItemNo" Visible="false" />

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("ItemDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delivered">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Eval("Delivered") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxDelivered" runat="server" Text='<%# Bind("Delivered") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField HeaderText="Action" ButtonType="Button" ShowEditButton="True" />

                </Columns>
                <EmptyDataTemplate>No Items in this Disbursement</EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="row">
            <div class="col-xs-2">
                <asp:Label ID="lbl1" runat="server" Text="Delivery Confirmation:"></asp:Label>
            </div>

        </div>

        <div class="row">
            <div class="col-xs-2">
                <asp:Label ID="Label1" runat="server" Text="Department Representative:"></asp:Label>
            </div>

            <asp:TextBox runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" ID="RepTextBox" Width="250px"></asp:TextBox><br />
            Employee Number:<br />
            <asp:TextBox runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" ID="RepNoTextBox" Width="250px"></asp:TextBox><br />
            Enter PIN to Confirm Received:<br />
            <asp:TextBox ID="PinTextBox" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" runat="server" Width="250px"></asp:TextBox>
        </div>

        <div class="row">
            <asp:Button ID="SubmitButton" CssClass="btn btn-primary" runat="server" Text="Confirm Delivery" OnClick="SubmitButton_Click" />
        </div>

        <% } %>

        <div class="row">
            <a href="DisbursementList.aspx">Back to Disbursement List Page</a>
        </div>
    </div>
</asp:Content>

