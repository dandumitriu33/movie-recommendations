console.log('p1 hi, this is the bot left chart');

let chart3 = document.getElementById('chart3').getContext('2d');

let botLeftChart = new Chart(chart3, {
    type: 'line', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
    data: {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        datasets: [{
            label: 'Users',
            data: [
                23012,
                34231,
                45123,
                47322,
                105162,
                102992,
                123012,
                134231,
                145123,
                147322,
                205162,
                222321,
                253021,
                264104
            ],
            fill: false,
            backgroundColor: 'green',
            borderWidth: 1,
            borderColor: '#777',
            hoverBorderWidth: 2,
            hoverBorderColor: '#000'
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Monthly Global User Count*',
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