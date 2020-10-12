import { rejectMovieAction, acceptMovieAction } from './swiper.js';
console.log('DRAGDROP: hi');

let swipeDraggableElement = document.getElementById('swipeDraggable');
let swipeLandingRedElement = document.querySelector('.swipeLandingRed');
let swipeLandingGreenElement = document.querySelector('.swipeLandingGreen');

function reloadCard() {
    console.log('Draggable changed');
    swipeDraggableElement = document.getElementById('swipeDraggable');
    console.log('Draggable loaded');
}

//// Draggable Element listeners & functions - doesn't work because the lement is changed and not re-aquired
//swipeDraggableElement.addEventListener('ondragstart', dragStart);
//swipeDraggableElement.addEventListener('dragend', dragEnd);

//// Drag functions
//function dragStart() {
//    swipeDraggableElement = document.getElementById('swipeDraggable');
//    console.log('start');
//}

//function dragEnd() {
//    console.log('end');
//}

// Drag events on Red
swipeLandingRedElement.addEventListener('dragover', dragOverRed);
swipeLandingRedElement.addEventListener('dragenter', dragEnterRed);
swipeLandingRedElement.addEventListener('dragleave', dragLeaveRed);
swipeLandingRedElement.addEventListener('drop', dragDropRed);

// Landing Drag functions
function dragOverRed(e) {
    e.preventDefault();
    console.log('over');
}

function dragEnterRed(e) {
    e.preventDefault();
    this.className += ' hoveredLandingRed';
    console.log('enter');
}

function dragLeaveRed() {
    this.className = 'swipeLandingRed';
    console.log('leave');
}

function dragDropRed() {
    this.className = 'swipeLandingRed';
    rejectMovieAction();
    console.log('drop');
}

// Drag events on Green
swipeLandingGreenElement.addEventListener('dragover', dragOverGreen);
swipeLandingGreenElement.addEventListener('dragenter', dragEnterGreen);
swipeLandingGreenElement.addEventListener('dragleave', dragLeaveGreen);
swipeLandingGreenElement.addEventListener('drop', dragDropGreen);

// Landing Drag functions
function dragOverGreen(e) {
    e.preventDefault();
    console.log('over');
}

function dragEnterGreen(e) {
    e.preventDefault();
    this.className += ' hoveredLandingGreen';
    console.log('enter');
}

function dragLeaveGreen() {
    this.className = 'swipeLandingGreen';
    console.log('leave');
}

function dragDropGreen() {
    this.className = 'swipeLandingGreen';
    acceptMovieAction();
    console.log('dropgreen');
}