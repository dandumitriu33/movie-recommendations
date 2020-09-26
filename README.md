# movie-recommendations
Educational project - recommending movies from a database based on usage.  

A simple recommendation system for now only Content Based Filtering based on the last movie watched.

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
