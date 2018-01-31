<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GenerateReorderTrend.aspx.cs" Inherits="Store_Supervisor_GenerateReorderTrend" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>

    <table border="1">
        <tr>
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
    <br />
    <asp:DropDownList ID="SupplierDDL" runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:TextBox ID="FromDate" runat="server" TextMode="Date"></asp:TextBox>
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

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</asp:Content>

