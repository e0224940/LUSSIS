<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendTestEmail.aspx.cs" Inherits="Admin_NewAccount" %>

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
                            <h3>Send Email</h3>

                            <div>
                                <div class="alert alert-danger">DO NOT MISUSE THIS SERVICE. EVER.</div>
                                <asp:TextBox runat="server" CssClass="form-control" ID="Email" placeholder="Email" TextMode="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Email Required" ControlToValidate="Email"></asp:RequiredFieldValidator>

                                <asp:TextBox runat="server" CssClass="form-control" ID="Subject" placeholder="Subject"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubject" runat="server" ErrorMessage="Subject Required" ControlToValidate="Subject"></asp:RequiredFieldValidator>

                                <asp:TextBox runat="server" CssClass="form-control" ID="Message" placeholder="Message"></asp:TextBox>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMessage" runat="server" ErrorMessage="Message Required" ControlToValidate="Message"></asp:RequiredFieldValidator>
                            </div>

                            <asp:Button runat="server" CssClass="btn btn-primary btn-block" ID="SendEmailButton" Text="Send Email" OnClick="SendEmailButton_Click" />

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
