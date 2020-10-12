# movie-recommendations
Educational project - Recommender System (Recommender Engine) - recommending movies from a database based on usage.  
https://en.wikipedia.org/wiki/Recommender_system

A simple movie recommendation system with Content Based Filtering, Community Based Filtering and Rabbit Hole suggestions and a mix of the 3, for Personalized User recommendations. Also "Tinder for movies" inspired by this tweet https://twitter.com/hello__caitlin/status/1304270134681448449.  

ASP.NET Core 3.1  
ASP.NET Core Identity 3.1  
Entity Framework Core 3.1  
Data generated with Mockaroo - https://mockaroo.com/  
Bootstrap 4  

**Simple Filtering** - just a query getting the newest movies with the highest rating  
**Content Based Filtering** - similar movies to what the user watched last (last history entry - by Main Genre and year)  
**Community Based Filtering** - top movies based on what users watched (most views are first) - can be implemented on timeframes  
**Rabbit Hole** - Based on the last entry from history - a suggestion based on connection strength the community created. For example, user1 watches a movie1 and after that movie2. The system marks a connection between movie1 and movie2 with a score of 1. It then suggests movie2 as the next best suggestion for other users that watched movie1.  
**Tinder For Movies** - Create a party(group) of users, add users, start swiping right or left on movies you prefer to watch with those members on movie night. If they swipe Yes on the same movies, they will show up as matches.  


## Preview

### Simple recommendations, Rabbit Hole and lastly "Tinder for movies"

![alt text](Screenshots/Preview.gif?raw=true)
