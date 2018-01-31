<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagerUpdate.aspx.cs" Inherits="Store_Manager_StoreManagerUpdate" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <form id="form1">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Supplier List" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </div>

        <div class="form-group">
            <asp:GridView ID="GridView1" GridLines="None" CssClass="table table-striped" Width="90%" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierCode" Height="118px" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" Font-Size="Larger">
                <Columns>
                    <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" ReadOnly="True" SortExpression="SupplierCode">
                        <HeaderStyle BackColor="White" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" SortExpression="PhoneNo">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FaxNo" HeaderText="FaxNo" SortExpression="FaxNo">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GstNo" HeaderText="GstNo" SortExpression="GstNo">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="form-group">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" align="center" OnClick="Button1_Click" Text="Add New Supplier" Font-Size="Medium" Height="80%" />
        </div>

        <div class="form-group">
            <%if (Session["SupplierProcessed"] != null)
                { %>
            <div>
                <% Response.Write("<script type=\"text/javascript\">alert('New supplier added successfully!');</script>"); %>
            </div>
            <% Session.Remove("SupplierProcessed");
                }%>
        </div>

    </form>
</asp:Content>
