console.log("members.js hi");

$("#addMemberButton").click(function () {
    event.preventDefault();
    console.log("MEMBERS: Add member pressed.");
    let memberEmail = $("#inlineFormInputEmail").val();
    let partyId = $("#partyId").text();
    addMemberToParty(partyId, memberEmail);
})

async function addMemberToParty(partyId, memberEmail) {
    let URL = `https://localhost:44311/api/parties/partyMembers/${partyId}/addMember/${memberEmail}`;
    await $.ajax({
        type: "POST",
        url: URL,
        data: {},
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            console.log("Member added successfully.");
        },
        error: function (jqXHR, status) {
            console.log(jqXHR);
            console.log('fail' + status.code);
        }
    })
}

let partyId = $("#partyId").text();

setInterval(async function () {
    updatePartyMembers(partyId);
    console.log("Party members updated.");
}, 10000);

async function updatePartyMembers(partyId) {

}