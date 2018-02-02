<li class="dropdown">
    <a href="<%= Page.ResolveUrl("~/Department/Representative/Default.aspx") %>" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Representative
       
        <span class="caret"></span>
    </a>
    <ul class="dropdown-menu">
        <li><a href="<%= Page.ResolveUrl("~/Department/Representative/UpdateCollection.aspx") %>">Update Collection Point</a></li>
   
        <li><a href="<%= Page.ResolveUrl("~/Store/Clerk/DisbursementList.aspx") %>">Disbursement List</a></li>
     </ul>
</li>
