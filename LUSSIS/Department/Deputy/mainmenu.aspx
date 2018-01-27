<% if (System.Web.HttpContext.Current.User.IsInRole("DepartmentDeputy"))
    { %>
<li class="dropdown">
    <a href="<%= Page.ResolveUrl("~/Department/Deputy/Default.aspx") %>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Deputy
        <span class="caret"></span>
    </a>
    <ul class="dropdown-menu">
        <li><a href="<%= Page.ResolveUrl("~/Department/Deputy/ViewAllPendingRequisitions.aspx") %>">Pending Requisitions</a></li>
        <li><a href="<%= Page.ResolveUrl("~/Department/Deputy/SelectRepresentative.aspx") %>">Select Representative</a></li>
    </ul>
</li>
<%} %>