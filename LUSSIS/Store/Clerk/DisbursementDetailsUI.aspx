<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DisbursementDetailsUI.aspx.cs" Inherits="Store_Clerk_DisbursementDetailsUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

        <div>
            <asp:Label ID="HeaderText1" runat="server" Text="Disbursement Details" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>
        <% if (disbursement != null)
            { %>
        <br />
        <div>
            <table style="margin-left: 20px; border: 1px solid black">
                <tr>
                    <th></th>
                    <th></th>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label1" runat="server" Text="Department:" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    <td>
                        <asp:Label ID="DepartmentNameLabel" runat="server" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label2" runat="server" Text="Date:" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    <td>
                        <asp:Label ID="DisbursementDateLabel" runat="server" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label3" runat="server" Text="Collection Point:" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    <td>
                        <asp:Label ID="CollectionPointLabel" runat="server" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="Label4" runat="server" Text="Status:" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                    <td>
                        <asp:Label ID="StatusLabel" runat="server" Font-Size="Medium"></asp:Label></td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <asp:Label ID="HeaderText2" runat="server" Text="Disbursement Items" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>
        <br />
        <div style="margin-left: 20px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemNo" Width="40%">
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="GridLabel1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="GridLabel2" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="GridLabel3" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delivered" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <% if (disbursement.Status == "Pending")
                                { %>
                            <asp:TextBox ID="GridTextBox1" runat="server" Text='<%# Bind("Delivered") %>' Style="border: thick solid grey"></asp:TextBox>
                            <% }
                                else
                                { %>
                            <asp:Label ID="GridLabel4" runat="server" Text='<%# Bind("Delivered") %>'></asp:Label>
                            <% } %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div>
            <asp:Label ID="HeaderText3" runat="server" Text="Delivery Confirmation" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>
        <br />
        <div style="margin-left: 20px;">
            <asp:Label ID="Label5" runat="server" Text="Department Representative" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <asp:TextBox ID="DeptRepNameTextBox" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            <br />
            <asp:Label ID="Label6" runat="server" Text="Employee Number" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <asp:TextBox ID="DeptRepNoTextBox" runat="server" Width="250px" Enabled="False"></asp:TextBox>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Enter PIN to Confirm Received" Font-Bold="True" Font-Size="Medium"></asp:Label>
            <br />
            <asp:TextBox ID="PinTextBox" runat="server" Width="250px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Confirm Delivery" OnClick="Button1_Click" Style="height: 30px;" />
            <br />
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
        </div>
        <% } %><% else
                   { %>
        <asp:Label ID="NotFoundLabel" runat="server" ForeColor="Red" Font-Size="Medium" Text="Disbursement Not Found"></asp:Label>
        <br />
        <% } %>
        <br />
        <a href="DisbursementListUI.aspx" style="font-size: small; margin-left: 20px">Back to Disbursement List Page</a>

</asp:Content>

