<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockList.aspx.cs" Inherits="Store_Clerk_StockList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <div class="row">
            <div class="form-inline">
                <asp:label id="lbl_header_appAuth" runat="server" font-bold="True" font-size="XX-Large" text="Inventory"></asp:label>
            </div>
        </div>

        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <asp:updatepanel id="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <div class="row">
                        <div class="form-inline">
                        <asp:Label ID="lbl" runat="server" Text="Select Bin"></asp:Label>    
                        <asp:DropDownList ID="BinDropDownList" CssClass="btn btn-default dropdown-toggle" runat="server" OnSelectedIndexChanged="BinDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    <br />
                        </div>
                    <div class="row">
                        <div class="form-inline">
                        <strong>OR</strong>
                    </div>
                        </div><br />

                    <div class="row">
                        <div class="form-inline">
                        <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
                        <asp:TextBox ID="SearchTextBox" CssClass="btn-default form-control" runat="server" Placeholder="Search"></asp:TextBox>
                        <asp:Button ID="SearchButton" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="SearchButton_Click" />
                            </div>
                        </div>

                    <div class="row">
                        <% if (Session["Error"] != null)
                            { %>
                        <div class="alert alert-danger">
                            An error has occured : <%= (string)Session["Error"] %>
                        </div>
                        <% 
                                Session.Remove("Error");
                            }
                        %>
                    </div>
                    <br />
                    <div class="row">
                       <asp:Label ID="Labe" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Stock List"></asp:Label>    
                    </div>

                    <div class="row">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th style="vertical-align: middle;">Item No</th>
                                    <th style="vertical-align: middle;">Description</th>
                                    <th style="vertical-align: middle;">Qty</th>
                                    <th style="vertical-align: middle;">Details</th>
                                    <th style="vertical-align: middle;">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <!--ROW START-->
                                        <tr>
                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="ItemNoLabel" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                                            </td>

                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </td>

                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="QtyOnHandLabel" runat="server" Text='<%# Eval("CurrentQty") %>'></asp:Label>
                                            </td>

                                            <td style="vertical-align: middle;">
                                                <asp:Button ID="DetailsButton" CssClass="btn btn-primary" runat="server" Text="Details" OnClick="DetailsButton_Click" CommandArgument='<%# Eval("ItemNo") %>' />
                                            </td>

                                            <td style="vertical-align: middle;">
                                                <asp:Button ID="AdjustmentVoucherButton" CssClass="btn btn-info" runat="server" Text="Adjustment Voucher" OnClick="AdjustmentVoucherButton_Click" CommandArgument='<%# Eval("ItemNo") %>' />
                                                <asp:TextBox ID="NewQtyTextBox" runat="server" CssClass="btn-default form-control" Visible="false" placeholder="Enter Correct Qty"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="NewQtyTextBox" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Enter only integer" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                                <asp:RangeValidator ID="range" runat="server" ControlToValidate="NewQtyTextBox" Type="Integer" MaximumValue="9999999" MinimumValue="0" ErrorMessage="Value out of Range" Display="Dynamic" Style="color: red;"></asp:RangeValidator>
                                                <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="btn-default form-control" Visible="false" placeholder="Enter Reason"></asp:TextBox>
                                                <asp:Button ID="SubmitAdjustmentVoucherButton" CssClass="btn btn-success" runat="server" Visible="false" Text="Submit" OnClick="SubmitAdjustmentVoucherButton_Click" CommandArgument='<%# Eval("ItemNo") %>' OnClientClick="$get('Dog').click();return false;" />
                                                <asp:Button ID="CancelAdjustmentVoucherButton" CssClass="btn btn-warning" runat="server" Visible="false" Text="Cancel" OnClick="CancelAdjustmentVoucherButton_Click" />
                                            </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:updatepanel>
    </div>
</asp:Content>

