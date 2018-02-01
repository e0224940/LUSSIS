* Database backup is in the "LUSSIS Backend" Project. 

* Ensure Visual Studio Enterprise 2015 and MSSQL 2014 are installed

* Start Visual Studio in Administrator Mode

* Each time the project is Rebuilt in visual studio using
Build->Rebuild Solution
The Database is restored from backup 
IF it is not found.
(Messages can be observed in the Output Window)

* To Deploy the website, Right Click on the  LUSSIS Project
(Not the LUSSIS Solution!) and click on "Publish Web App"
In Preview, Choose "Publish To Local IIS Server" and Click 
"Publish"

Website is http://localhost/LUSSIS/