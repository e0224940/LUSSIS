<% if (System.Web.HttpContext.Current.User.IsInRole("StoreClerk"))
    { %>
<li class="dropdown">
    <a href="<%= Page.ResolveUrl("~/Store/Clerk/Default.aspx") %>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Clerk
        <span class="caret"></span>
    </a>
    <ul class="dropdown-menu">
        <li><a href="<%= Page.ResolveUrl("~/Store/Clerk/Retrieval.aspx") %>">Retrievals</a></li>
    </ul>
</li>
<%} %>