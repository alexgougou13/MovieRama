# MovieRama Workable Assignment
A simple movie database with user ratings.

![image](https://user-images.githubusercontent.com/92690251/191573668-fb8ea589-db60-4c27-8032-d547743ea4d9.png)

## Tech stack
For the assignment I utilized the following:
* SQL Server for persistence
* Entity Framework Core to interact with the database
* .Net Core 6 for the REST API 
* HTML/JQuery/CSS for the front-end of the application

## Setup
1. Clone the repo
2. Open the back end solution and set the connection string on `appsettings.json` (you may use [localdb](https://stackoverflow.com/a/21565688) if you do not have an SQL Server version installed)
3. Run the `dotnet ef database update` command to generate the database
4. Deploy/run both projects

## Notes
* All requested features have been implemented (login, register, movie posting, movie rating, sorting, viewing a user's submitted movies).
* The website is responsive (up to a certain point).
* To improve UX on searches I utilized the [Damerau-Leveshtein](https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance) algorithm for computing string similarity.
* Both sorting and searching are performed on the server side.
