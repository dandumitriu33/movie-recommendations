import { setCookie, getCookie, eraseCookie } from './utils.js';

// get the party name to construct the cookies
let partyName = $("#partyName").text();
console.log(partyName.replace(" ", ""));

// construct cookie names and get values
let firstMovieIdCookieName = partyName.replace(" ", "") + "FirstMovieId";
let firstMovieId = getCookie(firstMovieIdCookieName);
console.log("firstMovieId: " + firstMovieId);
let lastMovieIdCookieName = partyName.replace(" ", "") + "LastMovieId";
let lastMovieId = getCookie(lastMovieIdCookieName);
console.log("lastMovieId: " + lastMovieId);

// in memory list of movies to pass to swiper
let currentBatch = [];

// fetch Batch (array) of movies for swiper
currentBatch = fetchMovieBatch();










async function fetchMovieBatch() {
    let URL = `https://localhost:44311/api/parties/getBatchBefore/${firstMovieId}/andAfter/${lastMovieId}`
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            currentBatch[i] = data[i];
        }
    });

    console.log(currentBatch);
    console.log(currentBatch.length);

    // load swiper
    loadSwiper(currentBatch[0]);
}
console.log("-----------");
console.log(currentBatch);

function loadSwiper(movie) {
    let element = `
                    <div class="card mb-4" style="min-width: 12rem; max-width: 12rem">
                        <img class="card-img-top" src="~/img/${movie.mainGenre.toLowerCase()}.jpg" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">${movie.title} (${movie.releaseYear})</h5>
                            <p class="card-text">IMDB: ${movie.rating}</p>
                            <p class="card-text">Genres: ${movie.mainGenre}, ${movie.subGenre1}, ${movie.subGenre2}</p>
                        </div>
                    </div>
                  `;
    $("#swiperCardContainer").empty();
    $("#swiperCardContainer").append(element);
}


