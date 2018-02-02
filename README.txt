READ AND FOLLOW THE STEPS IN SEQUENCE : 

* Database backup is in the "LUSSIS Backend" Project in "Database" Folder. 

* Ensure Visual Studio Enterprise 2015 and MSSQL 2014 are installed

* Start Visual Studio in Administrator Mode

* Each time the Solution is Rebuilt in visual studio using
Build->Rebuild Solution
The Database is restored from backup 
IF it is not found.
(Messages can be observed in the Output Window)

* To Deploy the website, Right Click on the  LUSSIS Project
(Not the LUSSIS Solution!) and click on "Publish Web App"
In Preview, Choose "Publish To Local IIS Server" and Click 
"Publish"

* Then, to ensure that default logins are inserted into the database,
Right Click on LUSSIS Project again and click on "View in Browser"

LOGINS ARE:

EMP NO - ROLES - Username - Password

// STORE
1011 - StoreManager - esther - estherestheresther!
1012 - StoreSupervisor - robin - robinrobinrobin!
1013 - StoreClerk - bruce - brucebrucebruce!

// ENGL Department
1002 - DepartmentHead, DepartmentDeputy - ezra - ezraezraezra!
1001 - DepartmentRepresentative, DepartmentEmployee - pamela - pamelapamelapamela!
1014 - DepartmentEmployee - thet - thetthetthet!

// CPSC Department
1002 - DepartmentHead, DepartmentDeputy - wee - weeweewee!
1001 - DepartmentRepresentative, DepartmentEmployee - fatt - fattfattfatt!
1014 - DepartmentEmployee - zrui - zruizruizrui!

* Website is http://localhost/LUSSIS/