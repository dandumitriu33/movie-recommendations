console.log('p1 hi, this is the top left chart');

let chart1 = document.getElementById('chart1').getContext('2d');

// Global options - affects all charts on HTML not just this one
//Chart.defaults.global.defaultFontFamily = 'Lato';
Chart.defaults.global.defaultFontSize = 16;
Chart.defaults.global.defaultFontColor = '#000';

let topLeftChart = new Chart(chart1, {
    type: 'bar', // bar, horizontalBar, pie, line, doughnut, radar, polarArea
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
            //backgroundColor: [ // can be color name, 'rgba(255, 99, 132, 0.6)', hexadecimal value '#777'
            //    'green',
            //    'blue',
            //    'red',
            //    'brown',
            //    'yellow',
            //    'cyan'
            //]
            backgroundColor: 'green',
            borderWidth: 1,
            borderColor: '#000',
            hoverBorderWidth:2,
            hoverBorderColor:'#000'
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
                fontColor:'green'
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