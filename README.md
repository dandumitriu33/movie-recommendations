# movie-recommendations
Educational project - Recommender System (Recommender Engine) - recommending movies from a database based on usage.  
https://en.wikipedia.org/wiki/Recommender_system

A simple movie recommendation system with Content Based Filtering, Community Based Filtering and Rabbit Hole suggestions and a mix of the 3, for Personalized User recommendations.  

ASP.NET Core 3.1  
ASP.NET Core Identity 3.1  
Entity Framework Core 3.1  
Data generated with Mockaroo - https://mockaroo.com/  
Bootstrap 4  

## Screenshots

### Simple recommendation in descending order by year and rating  

![alt text](Screenshots/BasicFiltering.jpg?raw=true)

### The User's history where the last movie is considered  

![alt text](Screenshots/UserHistory.jpg?raw=true)

### Simple recommendation based on the most watched movies by all the users - Community Based Filtering  
Every time someone watches a movie, it is added to their history and the CommunityLikes table gets an incrementation on that movie's score.  
This is the list of top movies in that table, based on score, in descending order.

![alt text](Screenshots/CommunityBasedFiltering.jpg?raw=true)

### Simple recommendation based on the last movie watched by the user - Content Based Filtering  
The movies are the newest that are at least the previous movie rating -2 rating points - for example if you watched a 6 you would get movies with a row as low as 4.  
The recommendation is also sorting by year, newest first.  
Watching a movie will change the recommendations.  

![alt text](Screenshots/ContentBasedFiltering.jpg?raw=true)

### Personalized User Recommendations - mix between Content and Community  
The movies in the personalized section are based on the last movie watched from history and one of the community top picks.  
The Recommendations change with every page refresh, there is an offset in the query.  
The refresh has a cycle when it reaches the end of available suggestions, implemented via a cookie.  

![alt text](Screenshots/MixedContentCommunity.jpg?raw=true)
![alt text](Screenshots/MixedContentCommunityRefresh1.jpg?raw=true)
![alt text](Screenshots/MixedContentCommunityRefresh2.jpg?raw=true)

### Personalized User Recommendations - with Rabbit Hole
The movies in the Rabbit Hole pick are based on community activity. When a user watches a movie, we look at the user's history, take the last watched movie and make a new entry in the Rabbit Hole (NextMovies table) with the current movie as a next step for the previous one. Each time this occurs, the score of the connection is increased and moved up the list.  
The Rabbit Hole recommendations change on every refresh, there is an offset in the query.  
The refresh has a cycle when it reaches the end of available suggestions, implemented via a cookie.  

![alt text](Screenshots/MixedWithRabbitHole.jpg?raw=true)

### Rabbit Hole path on the history page  
The history has changed a bit from the previous screenshots.  
The Rabbit Hole from "First Movie" to "Fifth Movie" was created with 3 other test users to increase strength and better represent the concept.  
The first movie is the Rabbit Hole start, the second is the best NextMovie for the first, the third is the best NextMovie for the second, the fourth is the best NextMovie for the third and the fifth is the best NextMovie for the fourth. They are not the list of best next movies for "First Movie", that is not the Rabbit Hole, just the movies people watched after they just saw "First Movie".  

![alt text](Screenshots/CurrentRabbitHole.jpg?raw=true)
