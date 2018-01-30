<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagerUpdate.aspx.cs" Inherits="Store_Manager_StoreManagerUpdate" MasterPageFile="~/MasterPage.master"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    
    <form id="form1">
        <div>
        <asp:Label ID="Label1" runat="server" Text="Supplier List"></asp:Label>

        </div>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierCode" Height="118px" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="386px" OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" ReadOnly="True" SortExpression="SupplierCode" />
                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" SortExpression="PhoneNo" />
                <asp:BoundField DataField="FaxNo" HeaderText="FaxNo" SortExpression="FaxNo" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="GstNo" HeaderText="GstNo" SortExpression="GstNo" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add New Supplier" />
        <br />

        <%if (Session["SupplierProcessed"] != null)
            { %>
        <div>
            <% Response.Write("<script type=\"text/javascript\">alert('New supplier added successfully!');</script>"); %>
        </div>
        <% Session.Remove("SupplierProcessed");
            }%>
    </form>
</asp:Content>
