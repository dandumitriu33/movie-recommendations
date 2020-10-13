console.log('p1 hi, this is the bot left chart');

let chart3 = document.getElementById('chart3').getContext('2d');

let botLeftChart = new Chart(chart3, {
    type: 'line', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
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
            text: 'Largest cities in Massachusetts',
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