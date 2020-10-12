import { rejectMovieAction, acceptMovieAction } from './swiper.js';

let swipeLandingRedElement = document.querySelector('.swipeLandingRed');
let swipeLandingGreenElement = document.querySelector('.swipeLandingGreen');

// Drag events on Red
swipeLandingRedElement.addEventListener('dragover', dragOverRed);
swipeLandingRedElement.addEventListener('dragenter', dragEnterRed);
swipeLandingRedElement.addEventListener('dragleave', dragLeaveRed);
swipeLandingRedElement.addEventListener('drop', dragDropRed);

// Landing Drag functions
function dragOverRed(e) {
    e.preventDefault();
}

function dragEnterRed(e) {
    e.preventDefault();
    this.className += ' hoveredLandingRed';
}

function dragLeaveRed() {
    this.className = 'swipeLandingRed';
}

function dragDropRed() {
    this.className = 'swipeLandingRed';
    rejectMovieAction();
}

// Drag events on Green
swipeLandingGreenElement.addEventListener('dragover', dragOverGreen);
swipeLandingGreenElement.addEventListener('dragenter', dragEnterGreen);
swipeLandingGreenElement.addEventListener('dragleave', dragLeaveGreen);
swipeLandingGreenElement.addEventListener('drop', dragDropGreen);

// Landing Drag functions
function dragOverGreen(e) {
    e.preventDefault();
}

function dragEnterGreen(e) {
    e.preventDefault();
    this.className += ' hoveredLandingGreen';
}

function dragLeaveGreen() {
    this.className = 'swipeLandingGreen';
}

function dragDropGreen() {
    this.className = 'swipeLandingGreen';
    acceptMovieAction();
}