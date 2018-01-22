<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemPage.aspx.cs" Inherits="Department_Employee_AddItemPage" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Raised Requisition</h1>
    <br />
    <asp:label id="Label1" runat="server" text="Employee Name:"></asp:label>
    <asp:label id="NameLB" runat="server"></asp:label>
    <br />
    <asp:label runat="server" text="Date:" id="Label2"></asp:label>
    <asp:label runat="server" text="Date" id="date"></asp:label>
    <br />
    <div style="float: right">
        <asp:textbox id="SearchItem" runat="server"></asp:textbox>
        <asp:button id="Search" runat="server" text="Search" onclick="Search_Click" />
        <br /><br /><br /><br />
        <asp:button id="Confirm" runat="server" onclick="Confirm_Click" text="Confirm" />
        <asp:button runat="server" text="Cancel" id="Cancel" OnClick="Cancel_Click" />
    </div>

    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-sm-6">
                <asp:gridview gridlines="None" runat="server" id="StationeryGridView" autogeneratecolumns="False" onselectedindexchanged="StationeryGridView_SelectedIndexChanged">
                    <Columns>
                     <asp:BoundField DataField="Description" HeaderText="Description:" SortExpression="Description" />
                     <asp:TemplateField HeaderText="Quantity:">
                      <ItemTemplate>
                        <asp:TextBox ID="Quantity" runat="server" ></asp:TextBox>
                      </ItemTemplate>
                        </asp:TemplateField>
                     <asp:CommandField ShowSelectButton="true" />
                   </Columns>           
                 </asp:gridview>
            </div>
            <div class="col-sm-6">
                <asp:gridview gridlines="None" runat="server" id="Cart" autogeneratecolumns="False">
          <Columns>
                <asp:TemplateField HeaderText="Description" SortExpression="description">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Qty">
                    
                        <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    
<%--                        <asp:Label ID="lblQuantity" runat="server" ></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
           </asp:gridview>
            </div>
        </div>
    </div>

    <asp:gridview id="SearchRes" runat="server" gridlines="None" autogeneratecolumns="False">
            <Columns>
                    <asp:BoundField DataField="Description" HeaderText="Description:" SortExpression="Description" />
                    <asp:TemplateField HeaderText="Quantity:">
                     <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                    </ItemTemplate>
                        </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="true" />
                </Columns>
                
        </asp:gridview>
    <br />
</asp:Content>
