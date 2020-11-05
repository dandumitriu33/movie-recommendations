console.log("members.js hi");

$("#addMemberButton").click(function () {
    event.preventDefault();
    console.log("MEMBERS: Add member pressed.");
    let memberEmail = $("#inlineFormInputEmail").val();
    let partyId = $("#partyId").text();
    addMemberToParty(partyId, memberEmail);
})

async function addMemberToParty(partyId, memberEmail) {
    if (validateEmail(memberEmail) == true) {
        let URL = `https://localhost:44311/api/parties/partyMembers/${partyId}/addMember/${memberEmail}`;
        let data = JSON.stringify({ "email": memberEmail });
        await $.ajax({
            type: "POST",
            url: URL,
            data: data,
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
}

function validateEmail(email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}



setInterval(async function () {
    updatePartyMembers();
    console.log("Party members updated.");
}, 10000);

async function updatePartyMembers() {
    let partyId = $("#partyId").text();
    let URL = `https://localhost:44311/api/parties/getMembers/${partyId}`;
    await $.getJSON(URL, function (data) {
        $("#memberContainer").empty();
        for (var i = 0; i < data.length; i++) {
            let member = data[i];
            let element = `
                            <div class="card mb-4" style="min-width: 12rem; max-width: 12rem">
                                <img class="card-img-top" src="https://localhost:44318/img/default-avatar.jpg" alt="Card image cap">
                                <div class="card-body">
                                    <h5 class="card-title">User: ${member.email}</h5>
                                </div>
                            </div>
                          `;
            $("#memberContainer").append(element);
        }
    })
}