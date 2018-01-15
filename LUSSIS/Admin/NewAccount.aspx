<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAccount.aspx.cs" Inherits="Admin_NewAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap core CSS -->
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <title>Create Account</title>

    <!-- To Center the Account Creation Screen -->
    <style>
        body,
        html {
            height: 100%;
            display: grid;
        }

        .center-screen {
            /* thing to center */
            margin: auto;
            width: 100%;
        }
    </style>
</head>
<body>
    <div class="center-screen">
        <div class="jumbotron">
            <div class="container">
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <form id="form1" runat="server">
                            <h3>Create New User</h3>

                            <div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="Username" placeholder="Username"></asp:TextBox>
                                <asp:TextBox runat="server" CssClass="form-control" ID="Password" placeholder="Password"></asp:TextBox>
                                <asp:TextBox runat="server" CssClass="form-control" ID="Name" placeholder="Name"></asp:TextBox>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="Department" ToolTip="Select Department"></asp:DropDownList>
                            </div>

                            <div class="form-group"></div>
                            <asp:CheckBoxList runat="server">
                                <asp:ListItem Text="Department Head" Value="DepartmentHead" />
                                <asp:ListItem Text="Department Head Deputy" Value="DepartmentHeadDeputy" />
                                <asp:ListItem Text="Department Employee" Value="DepartmentEmployee" />
                                <asp:ListItem Text="Department Representative" Value="DepartmentRepresentative" />
                                <asp:ListItem Text="Store Supervisor" Value="StoreSupervisor" />
                                <asp:ListItem Text="Store Manager" Value="StoreManager" />
                                <asp:ListItem Text="Store Clerk" Value="StoreClerk" />
                            </asp:CheckBoxList>
                            <div class="form-group"></div>

                            <asp:Button runat="server" CssClass="btn btn-primary btn-block" ID="NewUserButton" Text="Create User" OnClick="NewUserButton_Click" />

                            <div style="color: black;">
                                <asp:Literal ID="StatusText" runat="server" EnableViewState="False"></asp:Literal>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="../js/jquery.min.js"><\/script>')</script>
    <script src="../js/bootstrap.min.js"></script>
</body>
</html>
