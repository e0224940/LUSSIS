﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReorderReportList.aspx.cs" Inherits="Store_Supervisor_ReorderReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="container">
        <asp:Label ID="reorder" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="View Reorder Report"></asp:Label>
        <br />
        <br />
       

    <asp:GridView ID="reorderReportListGridView" 
        runat="server"
        cssclass="table table-striped" 
        AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N" >
                        <ItemTemplate>
                            <asp:Label ID="SN"  width="150px" height="50px" font-size="Medium" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" >
                        <ItemTemplate>
                            <asp:Hyperlink ID="test" width="150px" height="50px" font-size="Medium" Font-Bold="true" runat="server" Text='<%# Bind("DateIssued","{0:MMM-yyyy}") %>' 
                                NavigateUrl='<%#"~/Store/Supervisor/ReorderReportDetails.aspx?SNO=" + (Container.DataItemIndex + 1)%>'></asp:Hyperlink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
</div>
</asp:Content>

