<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagerCreateSupplier.aspx.cs" Inherits="Store_Manager_StoreManagerCreateSupplier" MasterPageFile="../../MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
   <%-- <form id="form1"></form>--%>
   <%-- <div class="form-group"></div>--%>

    <div class="form-group">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>

    <form id="form1">


        <div class="container">
            <div class="row">
                <div class="col-xs-6">
                    <asp:Label ID="Label1" runat="server" Text="Create New Supplier" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <br />
                    <br />
                    <table class="table table-striped">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Supplier Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Supplier Name"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="GST Registration"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Contact Name"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Phone No"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Fax No"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Address"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add" /></td>
                        </tr>
                    </table>

                </div>
                <div class="col-xs-6"></div>
            </div>
        </div>
        <br />
        <br />
        <br />


    </form>
</asp:Content>
