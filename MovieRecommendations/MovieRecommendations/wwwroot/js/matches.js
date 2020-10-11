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
    let URL = `https://localhost:44311/api/parties/partyCount/${partyId}`
    await $.getJSON(URL, function (data) {
        partyCount = data.partyCount;
    })
}

async function updateMatches(partyId, partyCount) {

}