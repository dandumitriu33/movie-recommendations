console.log('p1 hi, this is the top right chart');

let chart2 = document.getElementById('chart2').getContext('2d');

let topRightChart = new Chart(chart2, {
    type: 'pie', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
    data: {
        labels: ['Boston', 'Worcester', 'Springfield', 'Lowell', 'Cambridge', 'New Bedford'],
        datasets: [{
            label: 'Population',
            data: [
                617594,
                181045,
                153060,
                106519,
                105162,
                95072
            ],
            backgroundColor: [
                'green',
                'blue',
                'red',
                'brown',
                'yellow',
                'cyan'
            ],
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 2,
            hoverBorderColor: '#000'
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Temporary Placeholder - working on real data', // Community Favourite Genres
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