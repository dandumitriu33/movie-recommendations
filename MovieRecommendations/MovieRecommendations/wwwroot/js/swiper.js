import { setCookie, getCookie, eraseCookie } from './utils.js';

// get the party name to construct the cookies
let partyName = $("#partyName").text();
let partyId = $("#partyId").text();

// construct cookie names and get values
let newestMovieIdCookieName = partyName.replaceAll(" ", "") + "NewestMovieId";
let newestMovieId = getCookie(newestMovieIdCookieName);
let oldestMovieIdCookieName = partyName.replaceAll(" ", "") + "OldestMovieId";
let oldestMovieId = getCookie(oldestMovieIdCookieName);

// in memory list of movies to pass to swiper
let currentBatch = [];

// fetch Batch (array) of movies for swiper
currentBatch = fetchMovieBatch();

// swiper feeder mechanism
let batchIndex = 0;
$("#rejectMovie").click(rejectMovieAction);

function rejectMovieAction() {
    if (batchIndex >= 10) {
        //batchIndex = 0;
        fetchMovieBatch();
    } else {
        // batchIndex is the next movie to be loaded, the current movie is the -1
        oldestMovieId = currentBatch[batchIndex - 1].id;
        loadSwiper(currentBatch[batchIndex]);
        setCookie(oldestMovieIdCookieName, oldestMovieId);
    }
}

$("#acceptMovie").click(acceptMovieAction);

function acceptMovieAction() {
    if (batchIndex >= 10) {
        batchIndex = 0;
        fetchMovieBatch();
    } else {
        // batchIndex is the next movie to be loaded, the current movie is the -1
        addMovieToPartyChoices(currentBatch[batchIndex - 1]);
        oldestMovieId = currentBatch[batchIndex - 1].id;
        loadSwiper(currentBatch[batchIndex]);
        setCookie(oldestMovieIdCookieName, oldestMovieId);
    }
}

async function addMovieToPartyChoices(movie) {
    let URL = `https://localhost:44311/api/parties/partyChoices/addChoice`;
    var obj = JSON.stringify({ PartyId: partyId, MovieId: movie.id, Score: 1})
    await $.ajax({
        type: "POST",
        url: URL,
        data: obj,
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            console.log("Choice added successfully.");
        },
        error: function (jqXHR, status) {
            console.log(jqXHR);
            console.log('fail' + status.code);
        }
    })
}

async function fetchMovieBatch() {
    let URL = `https://localhost:44311/api/parties/getBatchBefore/${newestMovieId}/andAfter/${oldestMovieId}`
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            currentBatch[i] = data[i];
        }
    });

    // set local variables and cookies
    if (currentBatch[0].id > newestMovieId) {
        newestMovieId = currentBatch[0].id;
        setCookie(newestMovieIdCookieName, newestMovieId);
    }
    if (oldestMovieId == 0) {
        setCookie(oldestMovieIdCookieName, newestMovieId);
    }
    // reset the batchIndex to start the new list from 0
    batchIndex = 0;

    // load swiper
    loadSwiper(currentBatch[batchIndex]);
}

function loadSwiper(movie) {
    let element = `
                    <div class="card mb-4 swipeDraggable" id="swipeDraggable" draggable="true">
                        <img class="card-img-top" src="https://localhost:44318/img/${movie.mainGenre.toLowerCase()}.jpg" alt="Card image cap" draggable="false">
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
};

// export
export { rejectMovieAction, acceptMovieAction };

