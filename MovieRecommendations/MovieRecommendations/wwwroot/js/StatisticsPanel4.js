console.log('p1 hi, this is the bot right chart');

var horrorLabels = [];
var horrorRatings = [];
var genreColor = "gray";

getLatestHorrorMovies();

async function getLatestHorrorMovies() {

    let URL = `https://localhost:44311/api/statistics/inventory/horror`;
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            let item = data[i];
            horrorLabels.push(item.title + " (" + item.releaseYear + ")");
            horrorRatings.push(item.rating);
        }
    })
    refreshPanelHorror();
}

let botRightChart;

function refreshPanelHorror() {

    let chart4 = document.getElementById('chart4').getContext('2d');

    let botRightChart = new Chart(chart4, {
        type: 'horizontalBar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
        data: {
            labels: horrorLabels,
            datasets: [{
                label: 'Latest Horror Movies',
                data: horrorRatings,
                backgroundColor: genreColor,
                borderWidth: 1,
                borderColor: '#000',
                hoverBorderWidth: 2,
                hoverBorderColor: '#000'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Latest Horror Movies w/ Rating', 
                fontSize: 16
            },
            legend: {
                display: false, // or true to show, makes more sense in pie charts
                position: 'right', // top, bottom, left, right
                labels: {
                    fontColor: 'green'
                }
            },
            layout: {
                padding: {
                    left: 120,
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
