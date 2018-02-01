<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateRequisitionTrend.aspx.cs" Inherits="Store_Supervisor_GenerateRequisitionTrend" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
      <div class="container">
          <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Generate Requisition Trend"></asp:Label>
        <br />
        <br />
    <asp:GridView ID="GridView3" cssclass="table table-striped" runat="server"></asp:GridView>
    <asp:GridView ID="GridView4" cssclass="table table-striped" runat="server"></asp:GridView>
    <div class="col-xs-6">
                <asp:Label ID="Labelitem" Font-Bold="True" Font-Size="Large" runat="server" Text="All Items"></asp:Label>
             </div>
             <div class="col-xs-6">
                <asp:Label ID="Labelselected" Font-Bold="True"  Font-Size="Large" runat="server" Text="Selected Items"></asp:Label>
            </div>
              <div class="row">
        <div class="form-inline">
            <div class="col-xs-5">
                <asp:ListBox ID="DeptListBox" class="list-group-item" runat="server" Width="400px" Height="135px" SelectionMode="Multiple"></asp:ListBox>
            </div>
            <div class="col-xs-1">
                 &nbsp;&nbsp;&nbsp;
                <asp:Button ID="rqToLB2" runat="server" CssClass="btn btn-success" Text="---->" OnClick="rqToLB2_Click" />
                    &nbsp;&nbsp;&nbsp;             
                <asp:Button ID="rqToLB1" runat="server" CssClass="btn btn-danger" Text="<----" OnClick="rqToLB1_Click" />
             </div>       
            <div class="col-xs-4">            
                <asp:ListBox ID="SelectedDeptListBox" class="list-group-item" runat="server" Width="400px" Height="135px" SelectionMode="Multiple"></asp:ListBox>
            </div> 
            </div>
       </div>   
    
    <br />
    <br />
          <div class="row">
        <div class="form-inline">
        <div class="col-xs-3">
    <asp:DropDownList ID="ItemsDDL" 
        CssClass="btn btn-default dropdown-toggle"  
        runat="server"></asp:DropDownList>
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

    <asp:Button ID="generateReqReportBut" runat="server" Text="GENERATE" CssClass="btn btn-success" OnClick="generateReportBut_Click" />
    <br />
    <br />
    <br />
    <br />
    <asp:Chart ID="Chart2" runat="server">

        <chartareas>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
</asp:Chart>
          </div>
</asp:Content>

