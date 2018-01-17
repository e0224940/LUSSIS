<!--  if(!System.Web.HttpContext.Current.User.IsInRole("rolenamehere")) {  -->
<li class="dropdown">
    <a href="<%= Page.ResolveUrl("~/Department/Employee/Default.aspx") %>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Employee
        <span class="caret"></span>
    </a>
    <ul class="dropdown-menu">
        <li><a href="<%= Page.ResolveUrl("~/Department/Employee/Default.aspx") %>">Submenu1</a></li>
    </ul>
</li>
<!-- } -->