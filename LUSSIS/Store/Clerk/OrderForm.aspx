<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderForm.aspx.cs" Inherits="Store_Clerk_OrderForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <div class="form-inline">
                <h1 class="sub-header">Order Form</h1>
                <asp:Button ID="BlankButton" runat="server" Text="Blank" CssClass="btn btn-primary" OnClick="BlankButton_Click" />
                <asp:Button ID="AutoGenerateButton" runat="server" CssClass="btn btn-info" Text="AutoGenerate" OnClick="AutoGenerateButton_Click" />
            </div>
        </div>



        <% if (Session["OProcessed"] == null)
            {  %>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>

                    <div class="row">
                        <% if (Session["Error"] != null)
                            { %>
                        <div class="alert alert-danger">
                            <%= (string)Session["Error"] %>
                        </div>
                        <% 
                                Session.Remove("Error");
                            }
                        %>
                    </div>

                    <div class="row">
                        <div class="form-inline">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th rowspan="2" style="vertical-align: middle;">Item No</th>
                                        <th rowspan="2" style="vertical-align: middle;">Description</th>
                                        <th rowspan="2">Qty on Hand</th>
                                        <th rowspan="2">Reorder Level</th>
                                        <th rowspan="2">Reorder Qty</th>
                                        <th colspan="2">Breakdown By Supplier</th>
                                    </tr>
                                    <tr>
                                        <th>Supplier Name</th>
                                        <th>Qty</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>

                                            <tr>
                                                <td rowspan="3" style="vertical-align: middle;">
                                                    <asp:Label ID="ItemNoLabel" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                                                    <div>
                                                        <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("ItemNo") %>' OnClick="DeleteButton_Click" />
                                                    </div>
                                                </td>

                                                <td rowspan="3" style="vertical-align: middle;">
                                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                </td>

                                                <td rowspan="3" style="vertical-align: middle;">
                                                    <asp:Label ID="QtyOnHandLabel" runat="server" Text='<%# Eval("QtyOnHand") %>'></asp:Label>
                                                </td>

                                                <td rowspan="3" style="vertical-align: middle;">
                                                    <asp:Label ID="ReorderLevelLabel" runat="server" Text='<%# Eval("ReorderLevel") %>'></asp:Label>
                                                </td>

                                                <td rowspan="3" style="vertical-align: middle;">
                                                    <asp:Label ID="ReorderQtyLabel" runat="server" Text='<%# Eval("ReorderQty") %>'></asp:Label>
                                                </td>

                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="Supplier1Label" runat="server" Text='<%# Eval("Supplier1") %>'></asp:Label>
                                                </td>

                                                <td style="vertical-align: middle;">
                                                    <asp:TextBox ID="Qty1TextBox" CssClass="btn-default form-control" runat="server" Text='<%# Eval("Qty1") %>'></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Qty1TextBox" ErrorMessage="Field cannot be empty" Display="Dynamic" Style="color: red;"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="Qty1TextBox" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0" ErrorMessage="Valid Numbers only" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="Supplier2Label" runat="server" Text='<%# Eval("Supplier2") %>'></asp:Label>
                                                </td>

                                                <td style="vertical-align: middle;">
                                                    <asp:TextBox ID="Qty2TextBox" CssClass="btn-default form-control" runat="server" Text='<%# Eval("Qty2") %>'></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Qty2TextBox" ErrorMessage="Field cannot be empty" Display="Dynamic" Style="color: red;"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="Qty2TextBox" Operator="GreaterThanEqual" ValueToCompare="0" Type="Integer" ErrorMessage="Valid Numbers only" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="vertical-align: middle;">
                                                    <asp:Label ID="Supplier3Label" runat="server" Text='<%# Eval("Supplier3") %>'></asp:Label>
                                                </td>

                                                <td style="vertical-align: middle;">
                                                    <asp:TextBox ID="Qty3TextBox" CssClass="btn-default form-control" runat="server" Text='<%# Eval("Qty3") %>'></asp:TextBox>
                                                    <div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Qty3TextBox" ErrorMessage="Field cannot be empty" Display="Dynamic" Style="color: red;"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="Qty3TextBox" Operator="GreaterThanEqual" ValueToCompare="0" Type="Integer" ErrorMessage="Valid Numbers only" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                                                    </div>
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-inline">
                            <asp:DropDownList ID="StockDropDownList" CssClass="btn btn-default dropdown-toggle" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="AddButton" runat="server" CssClass="btn btn-info" Text="Add Item" OnClick="AddButton_Click" />
                            <asp:Button ID="SubmitButton" runat="server" CssClass="btn btn-success" Text="Submit Order Form" Style="grid-column-align" OnClick="SubmitButton_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row">
            <div class="form-inline">
                <h2>View Supplier Details</h2>
            </div>
        </div>

        <br />

        <asp:UpdatePanel ID="SupplierUpdatePanel" runat="server">
            <ContentTemplate>
                <div>

                    <div class="row">
                        <asp:DropDownList ID="SupplierDropDownList" CssClass="btn btn-default dropdown-toggle" runat="server" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>

                    <div class="row">
                        <table>
                            <tr style="border-collapse: separate; border-spacing: 5em;">
                                <td>Person-In-Charge</td>
                                <td>Contact Number</td>
                            </tr>

                            <tr style="border-collapse: separate; border-spacing: 5em;">
                                <td>
                                    <asp:TextBox ID="SupplierTextBox" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox ID="ContactTextBox" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <% }
            else
            { %>

        <div class="row">
            <div class="alert alert-success">
                Order Sent for Approval.
            </div>
        </div>

        <div class="row">
            <a href="OrderForm.aspx">New Order</a>
        </div>

        <% 
            Session.Remove("OProcessed");
        %>
        <% } %>
    </div>
</asp:Content>

