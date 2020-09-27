# movie-recommendations
Educational project - Recommender System (Recommender Engine) - recommending movies from a database based on usage.  
https://en.wikipedia.org/wiki/Recommender_system

A simple movie recommendation system with Content Based Filtering, Community Based Filtering and a mix of the 2 for Personalized User recommendations.  

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

![alt text](Screenshots/MixedConentCommunity.jpg?raw=true)
![alt text](Screenshots/MixedConentCommunityRefresh1.jpg?raw=true)
![alt text](Screenshots/MixedConentCommunityRefresh2.jpg?raw=true)
