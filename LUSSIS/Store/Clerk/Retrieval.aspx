<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Retrieval.aspx.cs" Inherits="_Default" MasterPageFile="../../MasterPage.master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="body" runat="server">
    <div class="Container">
        <div class="row">
            <div class="form-group">
                <label>Select A Previous Retrieval To View</label>
                <br />
                <asp:DropDownList runat="server" AutoPostBack="true" ID="SelectRetrevialDropDownList" OnSelectedIndexChanged="SelectRetrevialDropDownList_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <label>Or Create A New One</label>
                <br />
                <asp:Button runat="server" ID="Button1" Text="Create Retrieval" CssClass="btn btn-primary" OnClick="CreateNewRetrievalButton_Click" />
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <% if (Session["RetrievalProcessed"] != null)
                    { %>
                <div class="alert alert-info">
                    <asp:Label ID="SuccessLabel" runat="server"><% Response.Write(Session["RetrievalProcessed"]); %></asp:Label>
                </div>
                <% 
                        Session.Remove("RetrievalProcessed");
                    } %>
                <% if (Session["Error"] != null)
                    { %>
                <div class="alert alert-danger">
                    <asp:Label ID="ErrorLabel" runat="server"><% Response.Write(Session["Error"]); %></asp:Label>
                </div>
                <% 
                        Session.Remove("Error");
                    }
                %>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <%if ((int)Session["RetrievalNo"] >= 0)
                    { %>
                <asp:Repeater
                    ID="BigRepeater"
                    runat="server"
                    OnItemDataBound="BigRepeater_ItemDataBound">
                    <HeaderTemplate>
                        <div class="table-responsive">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th rowspan="2" style="vertical-align: middle;">Bin #</th>
                                        <th rowspan="2" style="vertical-align: middle;">Stationery Description</th>
                                        <th colspan="3">Total Quantity</th>
                                        <th colspan="4">Breakdown By Department</th>
                                    </tr>
                                    <tr>
                                        <th>Needed</th>
                                        <th>Backlog</th>
                                        <th>Retrieved</th>
                                        <th>Dept name</th>
                                        <th>Needed</th>
                                        <th>Backlog</th>
                                        <th>Actual</th>
                                    </tr>
                                </thead>
                                <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <!--ROW START-->
                        <tr>
                            <!--colspan is number of departments-->
                            <td class='<%# Eval("CssClass") %>' rowspan='<%# Eval("DepartmentCount") %>' style="vertical-align: middle;"><%# Eval("Bin") %></td>
                            <td class='<%# Eval("CssClass") %>' rowspan='<%# Eval("DepartmentCount") %>' style="vertical-align: middle;"><%# Eval("Description") %></td>
                            <td class='<%# Eval("CssClass") %>' rowspan='<%# Eval("DepartmentCount") %>' style="vertical-align: middle;"><%# Eval("Needed") %></td>
                            <td class='<%# Eval("CssClass") %>' rowspan='<%# Eval("DepartmentCount") %>' style="vertical-align: middle;"><%# Eval("Backlog") %></td>
                            <td class='<%# Eval("CssClass") %>' rowspan='<%# Eval("DepartmentCount") %>' style="vertical-align: middle;"><%# Eval("Reterived") %></td>

                            <asp:Repeater
                                runat="server"
                                ID="SmallRepeater"
                                DataSource='<%# Eval("Breakdown") %>'>
                                <ItemTemplate>
                                    <!--SMALL ROW START-->
                                    <td class='<%# Eval("CssClass") %>'><%# Eval("Department") %></td>
                                    <td class='<%# Eval("CssClass") %>'><%# Eval("Needed") %></td>
                                    <td class='<%# Eval("CssClass") %>'><%# Eval("Backlog") %></td>
                                    <td class='<%# Eval("CssClass") %>'>
                                        <% if ((bool)Session["Editable"] == true)
                                            { %>
                                        <asp:TextBox
                                            runat="server"
                                            ID="ActualTextBox"
                                            ToolTip='<%# Eval("ItemNo")%>'
                                            placeholder='<%# Eval("Department")%>'
                                            TextMode="Number"
                                            Text='<%# Eval("Actual")%>'></asp:TextBox>
                                        <% }
                                            else
                                            { %>
                                        <%# Eval("Actual") %>
                                        <%} %>
                                    </td>
                                    </tr>
                        <tr>
                            <!--SMALL ROW END-->
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                        </tr>
                        <!--ROW END-->
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="9">
                                <asp:Label ID="lblEmptyData" Text="No Requisitions to Process" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        </tbody>
            </table>
            </div>
                    </FooterTemplate>
                </asp:Repeater>
                <%} %>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <% if ((bool)Session["Editable"] == true)
                    { %>
                <asp:Button runat="server" CssClass="btn btn-primary" ID="SaveFormButton" Text="Save Retreival" OnClick="SaveFormButton_Click" />
                <%} %>
            </div>
        </div>
    </div>
</asp:Content>
