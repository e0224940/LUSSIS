<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <title>Login</title>

    <!-- To Center the Login Screen -->
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
                    <div class="col-md-5 col-md-offset-3">
                        <form id="form1" runat="server">
                            <asp:Login ID="LoginForm" runat="server" OnLoggedIn="LoginForm_LoggedIn">
                                <LayoutTemplate>
                                    <h2>LUSSIS</h2>
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group"></div>
                                    <div class="form-group">
                                        <asp:Button CssClass="btn btn-lg btn-primary btn-block" ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                    </div>
                                    <div>
                                        <div class="form-group">
                                            <asp:CheckBox  ID="RememberMe" runat="server" Text="Remember me next time." Visible="false" />
                                        </div>
                                    </div>
                                    <div style="color: Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </div>
                                </LayoutTemplate>
                            </asp:Login>
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
    <script>window.jQuery || document.write('<script src="js/jquery.min.js"><\/script>')</script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
