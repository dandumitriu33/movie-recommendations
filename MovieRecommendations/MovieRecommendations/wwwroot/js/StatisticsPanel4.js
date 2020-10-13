console.log('p1 hi, this is the bot right chart');

let chart4 = document.getElementById('chart4').getContext('2d');

let botRightChart = new Chart(chart4, {
    type: 'horizontalBar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
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
            backgroundColor: 'green',
            borderWidth: 1,
            borderColor: '#000',
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