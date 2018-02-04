<!--  if(!System.Web.HttpContext.Current.User.IsInRole("rolenamehere")) {  -->
<li class="dropdown">
    <a href="<%= Page.ResolveUrl("~/Store/Supervisor/Default.aspx") %>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Supervisor
        <span class="caret"></span>
    </a>
    <ul class="dropdown-menu">
        <li><a href="<%= Page.ResolveUrl("~/Store/Supervisor/ReorderReportDetails.aspx") %>">Reorder Report</a></li>
        <li><a href="<%= Page.ResolveUrl("~/Store/Supervisor/ApprovePOList.aspx") %>">Approve Purchase Order</a></li>
        <li><a href="<%= Page.ResolveUrl("~/Store/Supervisor/ApproveInventoryAdjustmentList.aspx") %>">Approve Adjustment Voucher</a></li>
        <li><a href="<%= Page.ResolveUrl("~/Store/Supervisor/GenerateReorderTrend.aspx") %>">Generate Reorder Trend</a></li>
        <li><a href="<%= Page.ResolveUrl("~/Store/Supervisor/GenerateRequisitionTrend.aspx") %>">Generate Requisition Trend</a></li>
    </ul>
</li>
<!-- } -->