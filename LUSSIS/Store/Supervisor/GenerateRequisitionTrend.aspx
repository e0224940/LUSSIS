<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateRequisitionTrend.aspx.cs" Inherits="Store_Supervisor_GenerateRequisitionTrend" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
        <div class="container">

        <asp:Label ID="lbl_header_appAuth" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Generate Requsition Trend"></asp:Label>
            <br />
            <br />

            <div class="col-xs-6">
                    <asp:Label ID="Labelitem" Font-Bold="True" Font-Size="Large" runat="server" Text="All Departments"></asp:Label>
                             </div>
             <div class="col-xs-6">
                    <asp:Label ID="Label1" Font-Bold="True" Font-Size="Large" runat="server" Text="Selected Department"></asp:Label>
                             </div>

        <div class="row">
        <div class="form-inline">
            <div class="col-xs-5">
            <asp:ListBox ID="DeptListBox" class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Multiple"></asp:ListBox>
                            </div>
            <div class="col-xs-1">
                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="rqToLB2" CssClass="btn btn-success" runat="server" Text="---->" OnClick="rqToLB2_Click" />
                                &nbsp;&nbsp;&nbsp;
                             <asp:Button ID="rqToLB1" CssClass="btn btn-danger" runat="server" Text="<----" OnClick="rqToLB1_Click" />
                            </div>       
            <div class="col-xs-4">
                <asp:ListBox ID="SelectedDeptListBox" class="list-group-item" runat="server" Width="400px" Height="130px" SelectionMode="Multiple"></asp:ListBox>
                     </div>    
            </div>
</div>

    <br />
    <br />

        <div class="row">
        <div class="form-inline">
        <div class="col-xs-1">
            <asp:Label ID="Label3" runat="server" Text="Item: "></asp:Label>
            </div>
            <div class="col-xs-3">
    <asp:DropDownList ID="ItemsDDL" CssClass="btn btn-default dropdown-toggle"  runat="server"></asp:DropDownList>
                            </div>
            </div>
            </div>
               <br />
    <br />

    <asp:TextBox ID="FromDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:TextBox ID="EndDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="generateReqReportBut" CssClass="btn btn-success" runat="server" Text="Generate Trend" OnClick="generateReportBut_Click" />
    <br />
    <br />
                            <asp:GridView ID="GridView3" GridLines="None" cssclass="table table-striped" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ReqNo" HeaderText="Req.No" />
                <asp:BoundField DataField="DateReviewed"  HeaderText="Date Reviewed" DataFormatString="{0:dd MMM yyyy}"/>
                <asp:BoundField DataField="ItemNo" HeaderText="ItemNo"  />
                <asp:BoundField DataField="Description" HeaderText="Description"/>
                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                <asp:BoundField DataField="DeptName" HeaderText="DeptName" />
            </Columns>
        </asp:GridView>
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
