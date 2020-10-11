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
fetchMovieBatch();
async function fetchMovieBatch() {
    let URL = `https://localhost:44311/api/parties/getBatchBefore/${firstMovieId}/andAfter/${lastMovieId}`
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            currentBatch[i] = data[i];
        }
    });
    console.log(currentBatch);
}
