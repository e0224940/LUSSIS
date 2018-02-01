<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateRequisitionTrend.aspx.cs" Inherits="Store_Supervisor_GenerateRequisitionTrend" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:GridView ID="GridView3" runat="server"></asp:GridView>
    <asp:GridView ID="GridView4" runat="server"></asp:GridView>

        <table border="1">
        <tr>
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
    <br />
    <asp:DropDownList ID="ItemsDDL" CssClass="btn btn-default dropdown-toggle"  runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:TextBox ID="FromDate" runat="server" TextMode="Date"></asp:TextBox>
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

