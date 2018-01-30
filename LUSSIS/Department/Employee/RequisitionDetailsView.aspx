<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionDetailsView.aspx.cs" Inherits="Department_Employee_RequisitionDetailsView" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <h1>View Requisition Details</h1>
    <asp:Label ID="empName" runat="server" Text="Employee Name:"></asp:Label>
    <asp:Label ID="empNameshow" runat="server" Text=" "></asp:Label>

    <br />

    <asp:Label ID="reuNo" runat="server" Text="Requisition No:"></asp:Label>
    <asp:Label ID="ReqId" runat="server" Text=""></asp:Label>

    <br />
    <br />

    <asp:GridView ID="GridViewForDetail" runat="server" DataKeyNames="ReqNo" 
        AutoGenerateColumns="False" 
        OnRowCancelingEdit="OnRowCancelingEdit"
        OnRowUpdating="OnRowUpdating" 
        OnRowDeleting="detailGrid_Delete" 
        OnRowEditing="detailGrid_Edit">
        <Columns>
                <asp:TemplateField HeaderText="ItemNo" SortExpression="ItemNo" >
                <ItemTemplate>
                    <asp:Label ID="ItemNO" runat="server" Text='<%# Bind("itemNo") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
            <asp:TemplateField HeaderText="Description" SortExpression="description">
                <ItemTemplate>
                    <asp:Label ID="des" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          
             <asp:TemplateField HeaderText="Qty" SortExpression="quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="New Qty">
                <ItemTemplate>
                    <asp:TextBox ID="changeQty" runat="server" Width="80px" Text="" TextMode="Number"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>--%>

            
            <asp:CommandField ShowEditButton="true"/>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>

    </asp:GridView>

    

    <br />
   <%-- <asp:Button ID="submit" runat="server" Text="Submit" />
    &nbsp&nbsp&nbsp&nbsp--%>
    <asp:Button ID="cancel" runat="server" Text="Back to History Page" OnClick="cancel_Click" />
</asp:Content>
