console.log('p1 hi, this is the top left chart');

// Get the list of genres w/ count via API

var genreLabels = [];
var genreCounts = [];
var genreColors = [];
getGenreCount();
console.log(genreLabels);
console.log(genreCounts);
console.log(genreColors);

async function getGenreCount() {
    
    let URL = `https://localhost:44311/api/statistics`;
    await $.getJSON(URL, function (data) {
        for (var i = 0; i < data.length; i++) {
            let item = data[i];
            genreLabels.push(item.genreName);
            genreCounts.push(item.count);
            genreColors.push(item.color);
        }
    })
    //populatePanel(genreLabels, genreCounts, genreColors);
    refreshPanel();
}

let topLeftChart;

function refreshPanel() {
    // CHART configuration area

    let chart1 = document.getElementById('chart1').getContext('2d');

    // Global options - affects all charts on HTML not just this one
    //Chart.defaults.global.defaultFontFamily = 'Lato';
    Chart.defaults.global.defaultFontSize = 16;
    Chart.defaults.global.defaultFontColor = '#000';

    topLeftChart = new Chart(chart1, {
        type: 'bar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
        data: {
            labels: genreLabels,
            datasets: [{
                label: 'Population',
                data: genreCounts,
                backgroundColor: genreColors,
                //backgroundColor: [ // can be color name, 'rgba(255, 99, 132, 0.6)', hexadecimal value '#777'
                //    'green',
                //    'blue',
                //    'red',
                //    'brown',
                //    'yellow',
                //    'cyan'
                //]
                //backgroundColor: 'green',
                borderWidth: 1,
                borderColor: '#000',
                hoverBorderWidth: 2,
                hoverBorderColor: '#000'
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Inventory Genres',
                fontSize: 18
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

};

