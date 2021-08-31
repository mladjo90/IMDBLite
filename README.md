# IMDBLite
Lite movie rating engine

The application that was created is the Lite Movie engine. Basically the user has the ability to see
movies or series ranked at average (from highest to lowest).

It also has the additional ability to evaluate movies or series.

SOLUTION:

The solution consists of 8 projects:

*IMDBLite.API.DataModels*
	It contains data models from the database, helper classes for executing stored procedures, as well as additional
	a class for extracting data from the User Secrets environment.
	
*IMDBLite.API.Repository*
	It contains Interfaces and implementations for repository, if there is a need for some specific actions on certain repositories. Also contains configuration for Repository dependency (DI).
	Base repo contains basic actions such as FindAll, Insert, Delete, Update ...
	
*IMDBLite.BLL*
	Business Layer Logic contains functions that are used to retrieve data. 
	
*IMDBLite.Server* 
	It is the API part of the solution that is in charge of logic and data acquisition.
	Additionally, there is the AuthAPIController.(This is something I would discuss).
	Basically, I was authorizing a client application on an API (client credentials flow), however,
	in Blazor I came across the following problem where the WebAssembly application is downloaded to the user's computer
	and as such opens the possibility of retrieving logging data. This way I hid that information, but
	opened up new problems. (Auth method is Auth0)
	This problem could be solved perhaps with Basic authorization on the AuthApi service.
	
*IMDBLite.DTO*
	Contains Data Transfer Request and Response objects.

*IMDBLite.Client*
	A client application that contains pages and the logic of their behavior
	
*IMDBLite.Shared*
	It would contain some enums and things used among other projects.
	Let’s say it now contains a class that serves as a model for pagination.
	
*IMDBLite.ServiceClientContracts*
	Represents the connection between the client and the API. The client calls ServiceClientContractor which further
	sends calls to the App app.
		
I did not manage to complete the user authentication, but it could also be inserted via Auth0.

Additionally the export database is also inside git, a file called imdblite.sql
After restore database, it is necessary in the appsettings.json within the IMDBLite.Server project to modify ***
with the right data to connect the application to the database.