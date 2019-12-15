# MovieReviewAPI
## ASP.Net Core Web API - Client: Razor Page

### Summary:
- This project will implement a client web application that allows users can search, comment, and rating on an existing movie. 
- The Web API app plays a role in querying data in the database and return movie information(title, comments, rating, date released) to the client application.

### Features:
- List Recent Movies
- Allow Users to Search Movie by Title or Actor
- Allow Users to Write a Review for a movie
- Allow Users to give rating for a movie
- Allow Users to view existing comments and view average rating
- Allow Users to update their own review 
- Users can delete their own comments.

### Technology Architecture:
Back-end side: 
- ASP.NET CORE  Web API:
    Repository pattern
    Automapper
    Get By Id/All, Post, Put and Delete
    AWS RDS - SQL Server
    Swagger Docs(API Specification)
    AWS Elastic Beanstalk (API Deployment)
    API Management(Apigee): Security Policy: API-Verify-Key 
- Client Web Application
ASP.NET Core Razor Pages
AWS Elastic Beanstalk - Deployment

### Home Page

![index](https://user-images.githubusercontent.com/39202933/70865000-5eec8a80-1f26-11ea-96d4-0488852e98d6.gif)

### Movie Page

![movie](https://user-images.githubusercontent.com/39202933/70865120-bd663880-1f27-11ea-9306-6339387b9eba.gif)
