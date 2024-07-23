Pe Schedule Web Application

---Explanation of the Software--- 
This software is a digital schedule of PE classes at a high school. There are pages for Student, Teacher, Location, Course and the Schedule itself.
A record in the schedule is created by selecting a Teacher, Location, Course and Date/Time. 
By using searching and sorting, users can navigate to specific records in the schedule page. One can search by course name, date/time and location to find what they are looking for.
Only a user with the "teacher" role will have permission to Create, Delete or Edit records, as well as view the Student, Teacher, Location and Course pages. 
All other users are restricted to viewing the schedule page, details about each record, and general features on the page (like searching, pagination and sorting).

---Prerequisites---
.NET SDK (Version 6.0 or later) - https://dotnet.microsoft.com/en-us/download
Visual Studio (2022 is advised) - https://visualstudio.microsoft.com/

---Cloning and Opening solution---
After downloading Visual Studio 2022, click on "Clone a repository" and paste my repository link in the box provided. My link: https://github.com/ac122317/PeScheduleDB.git
Click clone and wait for it to load, you should be directed to the project.
Navigate to the solution explorer (may be pinned to the right of the screen you can just click it) by clicking on "View' from the navigation pane at the top and then solution explorer 
(or you can use the keyboard shortcut "Ctrl+Alt+L" on windows).
Then finally in the solution explorer, double click "PeScheduleDB.sln" to open the project solution.

---Package Manager Console and running the application---
From there, click on the "View" tab again and locate "Other windows", hover over it and locate "Package Manager Console", then click it.
A tab at the bottom of the screen should appear. In the Package Manager Console click beside "PM>" and type the command "Update-Database" and press enter.
After this the project may install some necessary NuGet Packages required to run the application.
After updating the database, you are now ready to run the application. On the top of the screen you shall she a filled green arrow with "https" next to it on the right. 
Click this to run the application. After this, register an account and enjoy!





