<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisbursementListUI.aspx.cs" Inherits="Store_Clerk_DisbursementListUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="Label1" runat="server" Text="Pending" Font-Bold="True"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="200px" Width="634px">
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <asp:label ID="GridLabel1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="DisbursementDate" DataNavigateUrlFormatString="RetrivalList.aspx" DataTextField="DisbursementDate" DataTextFormatString="{0:yyyy-MM-dd}" HeaderText="DisbursementDate">
                <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CollectionPointNo" HeaderText="CollectionPointNo" SortExpression="CollectionPointNo" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Completed" Font-Bold="True"></asp:Label>
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Height="245px" Width="639px">
            <Columns>
                 <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <asp:label ID="GridLabel2" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:label>
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:HyperLinkField DataNavigateUrlFields="DisbursementDate" DataNavigateUrlFormatString="RetrivalList.aspx" DataTextField="DisbursementDate" DataTextFormatString="{0:yyyy-MM-dd}" HeaderText="DisbursementDate">
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:HyperLinkField>
                <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="CollectionPointNo" HeaderText="CollectionPointNo" SortExpression="CollectionPointNo" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
