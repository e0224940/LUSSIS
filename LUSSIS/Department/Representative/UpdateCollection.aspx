<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCollection.aspx.cs" Inherits="Department_Representative_UpdateCollection" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>Update Collection Location</h1>
    <asp:Label ID="Label1" runat="server" Text="Old Location:"></asp:Label>
    <br />
    <asp:TextBox ID="oldLocationText" runat="server" Width="305px"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Change it to:"></asp:Label>
    <br />
    
    
    <asp:DropDownList ID="NewLocationDDL" runat="server" OnSelectedIndexChanged="ChooseLocation_DDList" AutoPostBack="true">
        <asp:ListItem Value="-1">Choose New Location</asp:ListItem>
        <asp:ListItem Value="0">Management Store</asp:ListItem>
        <asp:ListItem Value="1">Stationery Store - Administration Building</asp:ListItem>
        <asp:ListItem Value="2">Management School</asp:ListItem>
        <asp:ListItem Value="3">Medical School</asp:ListItem>
        <asp:ListItem Value="4">Engineering School</asp:ListItem>
        <asp:ListItem Value="5">Science School</asp:ListItem>
        <asp:ListItem Value="6">University Hospital</asp:ListItem>


    </asp:DropDownList>

    <br />
    <asp:Label ID="Label4" runat="server" Text="" style="color:red"></asp:Label>


    <br />
    <asp:Label ID="Label3" runat="server" Text="New Collection Time:"></asp:Label>
    <br />
    <asp:TextBox ID="newCollectionTimeText" runat="server" Width="162px" ReadOnly ="true"></asp:TextBox>
    <asp:Label ID="newCollectionTimeLBs" runat="server" Text=""></asp:Label>
   

    <br /><br />
    <asp:Button ID="confirm" runat="server" Text="Confirm" OnClick="confirm_Click" />
    
    <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" />

    <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
