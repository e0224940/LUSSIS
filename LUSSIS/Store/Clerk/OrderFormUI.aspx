<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrderFormUI.aspx.cs" Inherits="Store_Clerk_OrderFormUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <asp:Label ID="HeaderText1" runat="server" Text="Order Form" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>

        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <br />
                        <asp:Button ID="BlankButton" runat="server" Text="Blank" Width="100px" OnClick="BlankButton_Click" />
                        &nbsp;
                        <asp:Button ID="AutoGenerateButton" runat="server" Text="Auto-Gen" Width="100px" OnClick="AutoGenerateButton_Click" />
                    </div>
                    <div>
                        <table>
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
                                        <!--ROW START-->
                                        <tr>
                                            <!--rowspan is number of suppliers-->
                                            <td rowspan="3" style="vertical-align: middle;">
                                                <asp:Label ID="ItemNoLabel" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="DeleteRow" CommandArgument='<%# Eval("ItemNo") %>' OnClick="DeleteButton_Click" />
                                            </td>

                                            <td rowspan="3" style="vertical-align: middle;">
                                                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>

                                            <td rowspan="3" style="vertical-align: middle;">
                                                <asp:Label ID="QtyOnHandLabel" runat="server" Text='<%# Eval("QtyOnHand") %>'></asp:Label></td>

                                            <td rowspan="3" style="vertical-align: middle;">
                                                <asp:Label ID="ReorderLevelLabel" runat="server" Text='<%# Eval("ReorderLevel") %>'></asp:Label></td>

                                            <td rowspan="3" style="vertical-align: middle;">
                                                <asp:Label ID="ReorderQtyLabel" runat="server" Text='<%# Eval("ReorderQty") %>'></asp:Label></td>

                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="Supplier1Label" runat="server" Text='<%# Eval("Supplier1") %>'></asp:Label></td>
                                            <td style="vertical-align: middle;">
                                                <asp:TextBox ID="Qty1TextBox" runat="server" Text='<%# Eval("Qty1") %>'></asp:TextBox>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="Supplier2Label" runat="server" Text='<%# Eval("Supplier2") %>'></asp:Label></td>
                                            <td style="vertical-align: middle;">
                                                <asp:TextBox ID="Qty2TextBox" runat="server" Text='<%# Eval("Qty2") %>'></asp:TextBox>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: middle;">
                                                <asp:Label ID="Supplier3Label" runat="server" Text='<%# Eval("Supplier3") %>'></asp:Label></td>
                                            <td style="vertical-align: middle;">
                                                <asp:TextBox ID="Qty3TextBox" runat="server" Text='<%# Eval("Qty3") %>'></asp:TextBox>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div>
                        <br />
                        <asp:DropDownList ID="StockDropDownList" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="AddButton" runat="server" Text="Add Item" OnClick="AddButton_Click" />
                        <asp:Button ID="SubmitButton" runat="server" Text="Submit Order Form" Style="grid-column-align" OnClick="SubmitButton_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div>
            <br />
            <asp:Label ID="HeaderText2" runat="server" Text="View Supplier Details" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>

        <div>
            <asp:UpdatePanel ID="SupplierUpdatePanel" runat="server">
                <ContentTemplate>
                    <div>
                        <br />
                        <asp:DropDownList ID="SupplierDropDownList" runat="server" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Person-In-Charge"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Contact Number"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="SupplierTextBox" runat="server" Enabled="false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="ContactTextBox" runat="server" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

</asp:Content>

