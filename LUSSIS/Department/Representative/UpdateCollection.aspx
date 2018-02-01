<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCollection.aspx.cs" Inherits="Department_Representative_UpdateCollection" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">

    <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Update Collection Point"></asp:Label>
    <br /><br /><br />
    <div class="row">
    <div class="form-inline">
        <div class="col-xs-2">
    <asp:Label ID="Label1" runat="server" Text="Current Location:"></asp:Label>
        </div>
    <div class="col-xs-2">
        <asp:TextBox ID="oldLocationText" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False"></asp:TextBox>
    </div>
    </div>
    </div>
    <br />
    <br />
        <div class="row">
    <div class="form-inline">
    <div class="col-xs-2">    
    <asp:Label ID="Label2" runat="server" Text="Change it to:"></asp:Label>
    </div>
    
         <div class="col-xs-2">  
    <asp:DropDownList ID="NewLocationDDL" runat="server" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="ChooseLocation_DDList" AutoPostBack="true">
        <asp:ListItem Value="-1">Choose New Location</asp:ListItem>
        <asp:ListItem Value="0">Management Store</asp:ListItem>
        <asp:ListItem Value="1">Stationery Store - Administration Building</asp:ListItem>
        <asp:ListItem Value="2">Management School</asp:ListItem>
        <asp:ListItem Value="3">Medical School</asp:ListItem>
        <asp:ListItem Value="4">Engineering School</asp:ListItem>
        <asp:ListItem Value="5">Science School</asp:ListItem>
        <asp:ListItem Value="6">University Hospital</asp:ListItem>
    </asp:DropDownList> &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text=" " style="color:red"></asp:Label>
             </div>
        </div>
            </div>
    <br />
        
         <div class="row">
    <div class="form-inline">
     
     <%--Hidden Label--%>         
    
    

    <div class="col-xs-2">
    <asp:Label ID="Label3" runat="server" Text="New Collection Time:"></asp:Label>
    </div>
    <div class="col-xs-2">
    <asp:TextBox ID="newCollectionTimeText" runat="server"  CssClass="btn-default form-control" AutoPostBack="True" Enabled="False" ReadOnly ="true"></asp:TextBox>
    </div>

     <%--Hidden Label--%>  
    <asp:Label ID="newCollectionTimeLBs" runat="server" Text=""></asp:Label>
  
    </div>
    
    </div>

    <br /><br />
     <div class="row">
    <div class="form-inline">
    <div class="col-xs-2">
    <asp:Button ID="confirm" runat="server" CssClass="btn btn-success" Text="Confirm New Location" OnClick="confirm_Click" />
    </div>
    <div class="col-xs-2">
    <asp:Button ID="cancel" runat="server" CssClass="btn btn-danger" Text="Cancel Update Location" OnClick="cancel_Click" />
    </div>
    <asp:Label ID="Label5" runat="server" Text="" Visible="false"></asp:Label>
    </div>
         </div>
        
</div>
</asp:Content>
