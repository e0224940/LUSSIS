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
                <asp:GridView ID="GridView3" cssclass="table table-striped" GridLines="None" runat="server"></asp:GridView>
<%--    <asp:GridView ID="GridView4" runat="server"></asp:GridView>--%>
    <br />
    <br />
    <br />
    <asp:Chart ID="Chart2" runat="server">

        <chartareas>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
</asp:Chart>
</asp:Content>



<%--<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateRequisitionTrend.aspx.cs" Inherits="Store_Supervisor_GenerateRequisitionTrend" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:GridView ID="GridView3" runat="server"></asp:GridView>
    <asp:GridView ID="GridView4" runat="server"></asp:GridView>

        <table border="1">
        <tr>
                            <asp:Label ID="DeptListBoxLabel" runat="server" Text="All Departments"></asp:Label>
                            <asp:Label ID="SelectedDeptListBoxLabel" runat="server" Text="Selected Department"></asp:Label>
            <td>
                    <asp:ListBox ID="DeptListBox" runat="server" Width="400px" Height="100px" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td>
                <table border="1">
                    <tr>
                        <td>
                            <asp:Button ID="rqToLB2" runat="server" Text="---->" OnClick="rqToLB2_Click" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                             <asp:Button ID="rqToLB1" runat="server" Text="<----" OnClick="rqToLB1_Click" />
                        </td>
               
                    </tr>
                </table>

            </td>

            <td>
                <asp:ListBox ID="SelectedDeptListBox" runat="server" Width="400px" Height="100px" SelectionMode="Multiple"></asp:ListBox>
            </td>

        </tr>
    </table>
    <br />
    <asp:Label ID="ItemsLabel" runat="server" Text="Item: "></asp:Label>
    <asp:DropDownList ID="ItemsDDL" runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="dateLabel2" runat="server" Text="Date: "></asp:Label>
    <asp:TextBox ID="FromDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:Label ID="dateLabelTo2" runat="server" Text="  to  "></asp:Label>
    <asp:TextBox ID="EndDate" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="generateReqReportBut" runat="server" Text="GENERATE NOW!!!!" OnClick="generateReportBut_Click" />
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
</asp:Content>
--%>
