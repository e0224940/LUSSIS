<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreManagerCreateSupplier.aspx.cs" Inherits="Store_Manager_StoreManagerCreateSupplier" MasterPageFile="../../MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

        .auto-style2 {
            height: 28px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">



    <div class="container">
        <div class="row">


            <div class="col-xs-9">
                <asp:Label ID="Label1" runat="server" Text="Create New Supplier" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <br />
                <table class="table table-striped">
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label2" runat="server" Text="Supplier Code"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Supplier Code"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1" ErrorMessage="Supplier Code can not be blank!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Supplier Name"></asp:TextBox>
                        </td>

                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2" ErrorMessage="Name can not be blank!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="GST Registration"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" placeholder="Enter GST Registration"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox3" ErrorMessage="GST Registration can not be blank!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label5" runat="server" Text="Contact Name"></asp:Label></td>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox4" runat="server" placeholder="Enter Contact Name"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox4" ErrorMessage="Contact Name can not be blank!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label7" runat="server" Text="Fax No"></asp:Label></td>
                        <td class="auto-style2">
                            <asp:TextBox ID="TextBox6" runat="server" placeholder="Enter Fax No"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox6" ErrorMessage="Invalid Fax No!" ForeColor="Red" ValidationExpression="\d{7}"></asp:RegularExpressionValidator>
                            <br/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox6" ErrorMessage="Fax No can not be blank!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                             </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Address"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" placeholder="Enter Address"></asp:TextBox>
                        </td>

                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox7" ErrorMessage="Address can not be blank!" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>

                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Phone No"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" placeholder="Enter Phone No"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="Invalid Phone No!" ForeColor="Red" ValidationExpression="\d{7}"></asp:RegularExpressionValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox5" ErrorMessage="Phone No can not be blank!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <div class="form-group">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Add New Supplier" />
                            </div>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>



                </table>

                <% if (Session["SupplierProcessed"] != null)
                    { %>
                <div>
                    <%Response.Write("<script type=\"text/javascript\">alert('New supplier added successfully!');</script>");%>
                </div>
                <%Session.Remove("SupplierProcessed");
                    } %>
            </div>
            <div class="col-xs-6"></div>
        </div>
    </div>
    <br />
    <br />
    <br />

</asp:Content>
