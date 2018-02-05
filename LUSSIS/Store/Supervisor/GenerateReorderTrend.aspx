<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateReorderTrend.aspx.cs" Inherits="Store_Supervisor_GenerateReorderTrend" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Generate Reorder Trend"></asp:Label>
        <br />
        <br />

        <div class="col-xs-6">
                <asp:Label ID="Labelitem" Font-Bold="True" Font-Size="Large" runat="server" Text="All Items"></asp:Label>
             </div>
             <div class="col-xs-6">
                <asp:Label ID="Labelselected" Font-Bold="True"  Font-Size="Large" runat="server" Text="Selected Items"></asp:Label>
            </div>

        <div class="row">
        <div class="form-inline">
            <div class="col-xs-5">
                <asp:ListBox ID="ItemsInCatalogueListBox"  class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Single"></asp:ListBox>
            </div>
            <div class="col-xs-1">
               
                <br />
                <asp:Button ID="toLB2" CssClass="btn btn-success" runat="server" Text=">>>" OnClick="toLB2_Click"/>
                &nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="toLB1" CssClass="btn btn-danger" runat="server" Text="<<<" OnClick="toLB1_Click" />
       
                 </div>       
            <div class="col-xs-4">
                <asp:ListBox ID="SelectedItemsListBox" class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Single"></asp:ListBox>
     </div>    

            </div>
</div>
        <br />
        <div class="col-xs-6">
                <asp:Label ID="allCategoryLabel" Font-Bold="True" Font-Size="Large" runat="server" Text="All Categories"></asp:Label>
             </div>
             <div class="col-xs-6">
                <asp:Label ID="selectedCategoryLabel" Font-Bold="True"  Font-Size="Large" runat="server" Text="Selected Categories"></asp:Label>
            </div>
                <div class="row">
        <div class="form-inline">
            <div class="col-xs-5">
                <asp:ListBox ID="CategoryListBox"  class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Single"></asp:ListBox>
            </div>
            <div class="col-xs-1">
               
                <br />
                <asp:Button ID="toLB2C" CssClass="btn btn-success" runat="server" Text=">>>" OnClick="toLB2C_Click" />
                &nbsp;&nbsp;&nbsp;
                <br />
                <br />
                <asp:Button ID="toLB1C" CssClass="btn btn-danger" runat="server" Text="<<<" OnClick="toLB1C_Click" />
       
                 </div>       
            <div class="col-xs-4">
                <asp:ListBox ID="selectedCategoryListBox" class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Single"></asp:ListBox>
     </div>    

            </div>
</div>
    <br />
    <br />
        <div class="row">
        <div class="form-inline">
        <div class="col-xs-1">
<asp:Label ID="Label3" runat="server" Text="Supplier: "></asp:Label>
            </div>
            <div class="col-xs-3">
    <asp:DropDownList ID="SupplierDDL" CssClass="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
            </div>
            </div>
            </div>
    <br />
    <br />
        <div class="row">
        <div class="form-inline">
            <div class="col-xs-2">
                 <asp:Label ID="Label1" runat="server" Text="Start From:"></asp:Label>
    <asp:TextBox ID="FromDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="False"></asp:TextBox>
                </div>
            <div class="col-xs-2">
                 <asp:Label ID="lbl_appAuth_endDate" runat="server" Text="End At:"></asp:Label>
    <asp:TextBox ID="EndDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="False"></asp:TextBox>
                </div>
           
            </div>
            </div>
    <br />
    <br />
    <asp:Button ID="generateReportBut" runat="server" CssClass="btn btn-success" Text="GENERATE Trend" OnClick="generateReportBut_Click" />
    <br />
        <asp:Label ID="Label4" runat="server"></asp:Label>
    <br />
            <asp:GridView ID="GridView1" 
                GridLines="None" 
                cssclass="table table-striped" 
                runat="server" 
                AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="PONo" HeaderText="PO.No" />
                <asp:BoundField DataField="DateReviewed"  HeaderText="Date Reviewed" DataFormatString="{0:dd MMM yyyy}"/>
                <asp:BoundField DataField="ItemNo" HeaderText="ItemNo"  />
                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                <asp:BoundField DataField="Description" HeaderText="Description"/>
                <asp:BoundField DataField="SupplierCode" HeaderText="SupplierCode" />
                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
            </Columns>
        </asp:GridView>

    <asp:GridView ID="GridView2" cssclass="table table-striped"  runat="server"></asp:GridView>
    <br />
    <br />
    <asp:Chart ID="Chart1" runat="server">
        <%--        <Series>
            <asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Line">
            </asp:Series>
        </Series>--%>

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
        </div>

</asp:Content>


<%--<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateReorderTrend.aspx.cs" Inherits="Store_Supervisor_GenerateReorderTrend" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>

    <table border="1">
        <tr>
                            <asp:Label ID="ItemsInCatalogueListBoxLabel" runat="server" Text="All Items"></asp:Label>
                            <asp:Label ID="SelectedItemsListBoxLabel" runat="server" Text="Selected Items"></asp:Label>

            <td>
                <asp:ListBox ID="ItemsInCatalogueListBox" runat="server" Width="400px" Height="100px" SelectionMode="Single"></asp:ListBox>
            </td>
            <td>
                <table border="1">
                    <tr>
                        <td>
                            <asp:Button ID="toLB2" runat="server" Text="---->" OnClick="toLB2_Click" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                                     <asp:Button ID="toLB1" runat="server" Text="<----" OnClick="toLB1_Click" />
                        </td>
               
                    </tr>
                </table>


            </td>

            <td>

                <asp:ListBox ID="SelectedItemsListBox" runat="server" Width="400px" Height="100px" SelectionMode="Single"></asp:ListBox>
            </td>

        </tr>
    </table>


    <br />
    <asp:Label ID="supplierLabel" runat="server" Text="Supplier: "></asp:Label>
    <asp:DropDownList ID="SupplierDDL" runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="dateLabel" runat="server" Text="Date: "></asp:Label>
    <asp:TextBox ID="FromDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:Label ID="dateLabelTo" runat="server" Text="  to  "></asp:Label>
    <asp:TextBox ID="EndDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="generateReportBut" runat="server" Text="GENERATE NOW!!!!" OnClick="generateReportBut_Click" />
    <br />
    <br />
    <br />
    <br />
    <asp:Chart ID="Chart1" runat="server">
        <%--        <Series>
            <asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Line">
            </asp:Series>
        </Series>--%>

<%--        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</asp:Content>--%>

