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
                Collection Date: <%= ((DateTime)d.DisbursementDate).ToString("dd-MMM-yyyy") %>
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
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxDelivered" ErrorMessage="Field cannot be empty" Display="Dynamic" Style="color: red;"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxDelivered" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Enter only integer" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
                                <asp:RangeValidator ID="range" runat="server" ControlToValidate="TextBoxDelivered" Type="Integer" MaximumValue='<%# Eval("Qty") %>' MinimumValue="0" ErrorMessage="Value out of Range" Display="Dynamic" Style="color: red;"></asp:RangeValidator>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ButtonType="Button" ShowEditButton="True" ControlStyle-CssClass="btn btn-primary"/>

                </Columns>
                <EmptyDataTemplate>No Items in this Disbursement</EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="row">
            <h2>Delivery Confirmation:</h2>
        </div>

        <div class="row">
            <h5>Department Representative:</h5>
            <div>
                <asp:TextBox runat="server" CssClass="btn-default form-control" Enabled="False" ID="RepTextBox" Width="250px"></asp:TextBox><br />
            </div>
            <h5>Employee Number:</h5>
            <div>
                <asp:TextBox runat="server" CssClass="btn-default form-control" Enabled="False" ID="RepNoTextBox" Width="250px"></asp:TextBox><br />
            </div>
            <h5>Enter PIN  Confirm Received:</h5>
            <div>
                <asp:TextBox runat="server" CssClass="btn-default form-control" ID="PinTextBox" Width="250px"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="PinTextBox" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Enter Numeric Pin only" Display="Dynamic" Style="color: red;"></asp:CompareValidator>
            </div>
        </div>
        <div><p></p></div>
        <div class="row" >
            <asp:Button ID="SubmitButton" CssClass="btn btn-primary" runat="server" Text="Confirm Delivery" OnClick="SubmitButton_Click" />
        </div>
            

        <% } %>

        <div class="row">
            <a href="DisbursementList.aspx">Back to Disbursement List Page</a>
        </div>
    </div>
</asp:Content>

