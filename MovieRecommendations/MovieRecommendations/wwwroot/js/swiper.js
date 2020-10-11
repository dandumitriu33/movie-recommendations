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

// fetch array of movies for swiper
