<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RetrivalListUI.aspx.cs" Inherits="Store_Clerk_RetrivalListUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="Retrival List"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Current"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RetrievalNo">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="RetrievalNo" DataTextField="RetrievalNo" HeaderText="RetrievalNo" />
            </Columns>
        </asp:GridView>
        <br />



        <asp:Label ID="Label3" runat="server" Text="History"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="RetrievalNo">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="RetrievalNo" DataNavigateUrlFormatString="DisbursementDetailsUI.aspx?DisbursementNo={0}" DataTextField="RetrievalNo" HeaderText="RetrievalNo" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" DataFormatString="{0:yyyy-MM-dd}" />
            </Columns>
        </asp:GridView>
       
        <br />
        <br />
        <br />
    </form>
</body>
</html>
