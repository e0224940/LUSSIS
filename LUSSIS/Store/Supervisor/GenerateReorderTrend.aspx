<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateReorderTrend.aspx.cs" Inherits="Store_Supervisor_GenerateReorderTrend" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container">

        <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Generate Reorder Trend"></asp:Label>
        <br />
        <br />

    <asp:GridView ID="GridView1" cssclass="table table-striped"  runat="server"></asp:GridView>
    <asp:GridView ID="GridView2" cssclass="table table-striped"  runat="server"></asp:GridView>

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
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="toLB2" CssClass="btn btn-success" runat="server" Text="---->" OnClick="toLB2_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="toLB1" CssClass="btn btn-danger" runat="server" Text="<----" OnClick="toLB1_Click" />
            </div>       
            <div class="col-xs-4">
                <asp:ListBox ID="SelectedItemsListBox" class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Single"></asp:ListBox>
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
    <asp:TextBox ID="FromDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True"></asp:TextBox>
                </div>
            <div class="col-xs-2">
                 <asp:Label ID="lbl_appAuth_endDate" runat="server" Text="End At:"></asp:Label>
    <asp:TextBox ID="EndDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="True"></asp:TextBox>
                </div>
           
            </div>
            </div>
    <br />
    <br />
    <asp:Button ID="generateReportBut" runat="server" CssClass="btn btn-success" Text="GENERATE Trend" OnClick="generateReportBut_Click" />
    <br />
    <br />
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

