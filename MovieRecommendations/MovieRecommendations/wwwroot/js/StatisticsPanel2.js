console.log('p1 hi, this is the top right chart');

// Get the list of genres liked by the community via API

var genreLabelsCommunity = [];
var genreScoresCommunity = [];
var genreColorsCommunity = [];
console.log(genreLabelsCommunity);
console.log(genreScoresCommunity);
console.log(genreColorsCommunity);
getCommunityGenreScore();

async function getCommunityGenreScore() {

    let URL = `https://localhost:44311/api/statistics/community`;
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            let item = data[i];
            genreLabelsCommunity.push(item.genreName);
            genreScoresCommunity.push(item.score);
            genreColorsCommunity.push(item.color);
        }
    })
    
    refreshPanelCommunity();
}

let topRightChart;

function refreshPanelCommunity() {
    let chart2 = document.getElementById('chart2').getContext('2d');

    topRightChart = new Chart(chart2, {
        type: 'pie', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
        data: {
            labels: genreLabelsCommunity,
            datasets: [{
                label: 'Genres Community Score',
                data: genreScoresCommunity,
                backgroundColor: genreColorsCommunity,
                borderWidth: 1,
                borderColor: '#777',
                hoverBorderWidth: 2,
                hoverBorderColor: '#000'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Community Favourite Genres',
                fontSize: 18
            },
            legend: {
                display: true, // or true to show, makes more sense in pie charts
                position: 'right', // top, bottom, left, right
                labels: {
                    fontColor: 'green'
                }
            },
            layout: {
                padding: {
                    left: 0,
                    right: 0,
                    bottom: 0,
                    top: 0
                }
            },
            tooltips: {
                enabled: true // true, false for on and off
            }
        }
    })
}

