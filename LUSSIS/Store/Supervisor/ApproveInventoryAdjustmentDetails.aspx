<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveInventoryAdjustmentDetails.aspx.cs" Inherits="Store_Supervisor_ApproveInventoryAdjustmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<div class="container">
    <asp:Label ID="approve" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Approve Inventory Adjustment"></asp:Label>
        <br />
        <br />
    <div class="row">
    <div class="form-inline">
        <div class="col-xs-2">
    <asp:Label ID="approveInvAdjLabel" runat="server" Text="Adjustment Voucher# : "></asp:Label>
                    </div>
        <div class="col-xs-2"> 
    <asp:Label ID="invAdjLabel" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False"></asp:Label>
                    </div>
        </div>
   </div>
    <br /> <br />
     <div class="row">
            <div class="form-inline">
                <div class="col-xs-2"> 
    <asp:Label ID="byLabel" runat="server" Text="By: "></asp:Label>
                                    </div>
                <div class="col-xs-2">
    <asp:Label ID="invAdjClerkLabel" runat="server"></asp:Label>
                                    </div>
            </div>
        </div>
    <br /> <br />
     <div class="row">
            <div class="form-inline">
                <div class="col-xs-2">  
    <asp:Label ID="dateRaisedLabel" runat="server" Text="Date Raised: "></asp:Label>
                                     </div>
                 <div class="col-xs-2"> 
                     <asp:TextBox ID="dateRaisedText" runat="server" CssClass="btn-default form-control" AutoPostBack="True" Enabled="False"></asp:TextBox>
                    </div>
             </div>
          </div>
                     <br /> <br />
                    <asp:GridView ID="ApproveInventoryAdjustmentDetailsGridView" 
                    runat="server" 
                    GridLines="None"
                    cssclass="table table-striped" 
                    AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="S/N">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemCode" >
                        <ItemTemplate>
                            <asp:Label ID="ItemCode" runat="server" Text='<%# Eval("ItemNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity Adjusted" >
                        <ItemTemplate>
                            <asp:Label ID="qtyAdjustedAmt" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason" >
                        <ItemTemplate>
                            <asp:Label ID="Remarks" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    <br />
            <div class="row">
            <div class="form-inline">
     <div class="col-xs-2">  
    <asp:Button ID="approveAdjustmentButton"  CssClass="btn btn-success" runat="server" Text="Approve" OnClick="approveAdjustmentButton_Click" />     
     </div>
                <div class="col-xs-2">    
    <asp:Button ID="rejectAdjustmentButton" CssClass="btn btn-danger" runat="server" Text="Reject" OnClick="rejectAdjustmentButton_Click" />
                          </div>
                <div class="col-xs-2"> 
    <asp:Button ID="approveAVBackBut" CssClass="btn btn-info" runat="server" Text="Back" OnClick="approveAVBackBut_Click" />
      </div> 
                    
                </div>
            </div>
</div>
</asp:Content>

