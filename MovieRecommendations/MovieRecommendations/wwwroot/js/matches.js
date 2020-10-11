let partyId = $("#partyId").text();
console.log("MATCHES: partyId: " + partyId);

let partyCount = 0;
getPartyCount(partyId);

setInterval(async function () {
    getPartyCount(partyId);
    console.log("Party count updated to: " + partyCount);
}, 10000);

setInterval(async function () {
    updateMatches(partyId, partyCount);
    console.log("Matches updated.");
}, 5000); 

async function getPartyCount(partyId) {
    let URL = `https://localhost:44311/api/parties/partyCount/${partyId}`;
    await $.getJSON(URL, function (data) {
        partyCount = data.partyCount;
    })
}

async function updateMatches(partyId, partyCount) {
    let URL = `https://localhost:44311/api/parties/getMatches/${partyId}/count/${partyCount}`;
    await $.getJSON(URL, function (data) {
        $("#matchesContainer").empty();
        for (var i = 0; i < data.length; i++) {
            let movie = data[i];
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
            $("#matchesContainer").append(element);
        }
    })
}