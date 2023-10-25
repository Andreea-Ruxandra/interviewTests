# interviewTests
Hello, colleagues! :)
This is my assessment. :D 
1. Go to my github: https://github.com/Andreea-Ruxandra/interviewTests.
2. Press on the Code green button and use the https url for cloning the repo in visual studio.
3. Open the project in visual studio 2022
4. Download and install SQL Server Management Studio: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms
5. Download and install SQLExpress: https://www.microsoft.com/en-us/download/details.aspx?id=101064
6. You will have at the end one sql connection like this one: "Server=localhost\\SQLEXPRESS;Database=PricesDb;Trusted_Connection=True;TrustServerCertificate=True;User=user;Password=password"
7. From SSMS create an sql server user for login and add sysadmin systemrole to it.(this is working better in appsettings.json rather than the domain user)
8. Add the sql connection string: "Server=localhost\\SQLEXPRESS;Database=PricesDb;Trusted_Connection=True;TrustServerCertificate=True;User=user;Password=password" to the appsettings.json file in the PricesDb key. 
9. Right click on the project itself and choose Restore Nuget packages.
10. Rebuild the project
11. If the dependencies are not loaded ok, then remove them and re-download them. 
12. Open cmd as admin and install globally: dotnet ef tools with the following command: dotnet tool install --global dotnet-ef --version 6.*
13. In cmd window go to the project path: cd C:\FapticInterviewTest\FapticInterviewTest\FapticInterviewTest   (for example purpose I've used my path)
14. In cmd window type: dotnet-ef database update
15. In cmd window type: dotnet-ef database update InitialCreat
16. In cmd window type: dotnet-ef database update
steps 4-8 will create the sql server database based on the pricemodel
17. Run the project
