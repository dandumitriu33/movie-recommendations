import { setCookie, getCookie, eraseCookie } from './utils.js';

// get the party name to construct the cookies
let partyName = $("#partyName").text();
console.log(partyName.replace(" ", ""));

// construct cookie names and get values
let newestMovieIdCookieName = partyName.replace(" ", "") + "NewestMovieId";
let newestMovieId = getCookie(newestMovieIdCookieName);
console.log("newestMovieId: " + newestMovieId);
let oldestMovieIdCookieName = partyName.replace(" ", "") + "OldestMovieId";
let oldestMovieId = getCookie(oldestMovieIdCookieName);
console.log("oldestMovieId: " + oldestMovieId);

// in memory list of movies to pass to swiper
let currentBatch = [];

// fetch Batch (array) of movies for swiper
currentBatch = fetchMovieBatch();

// swiper feeder mechanism
let batchIndex = 0;
$("#rejectMovie").click(function () {
    console.log("Reject Movie Clicked.");
    if (batchIndex == 10) {
        batchIndex = 0;
        fetchMovieBatch()
    } else {
        loadSwiper(currentBatch[batchIndex]);
    }
    
})

$("#acceptMovie").click(function () {
    console.log("Accept Movie Clicked.");
    if (batchIndex == 10) {
        batchIndex = 0;
        fetchMovieBatch()
    } else {
        loadSwiper(currentBatch[batchIndex]);
    }
})



async function fetchMovieBatch() {
    let URL = `https://localhost:44311/api/parties/getBatchBefore/${newestMovieId}/andAfter/${oldestMovieId}`
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            currentBatch[i] = data[i];
        }
    });

    console.log(currentBatch);
    console.log(currentBatch.length);

    // set local variables and cookies
    console.log("Setting newestMovieId and oldestMovieId");
    if (currentBatch[0].id > newestMovieId) {
        newestMovieId = currentBatch[0].id;
        setCookie(newestMovieIdCookieName, newestMovieId);
    }
    oldestMovieId = currentBatch[9].id;
    console.log("newestMovieId: " + newestMovieId + " oldestMovieId: " + oldestMovieId);
    setCookie(oldestMovieIdCookieName, oldestMovieId);

    // load swiper
    loadSwiper(currentBatch[batchIndex]);
    
}
console.log("-----------");
console.log(currentBatch);

function loadSwiper(movie) {
    let element = `
                    <div class="card mb-4" style="min-width: 12rem; max-width: 12rem">
                        <img class="card-img-top" src="https://localhost:44318/img/${movie.mainGenre.toLowerCase()}.jpg" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">${movie.title} (${movie.releaseYear})</h5>
                            <p class="card-text">IMDB: ${movie.rating}</p>
                            <p class="card-text">Genres: ${movie.mainGenre}, ${movie.subGenre1}, ${movie.subGenre2}</p>
                        </div>
                    </div>
                  `;
    $("#swiperCardContainer").empty();
    $("#swiperCardContainer").append(element);
    batchIndex++;
}


