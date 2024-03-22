// JavaScript functions to interact with Blazor component

// Function to get the width of the canvas
window.getWidth = function () {
    var canvas = document.getElementById('myCanvas');
    return canvas.width;
};

// Function to get the height of the canvas
window.getHeight = function () {
    var canvas = document.getElementById('myCanvas');
    return canvas.height;
};

// Function to draw a square on the canvas at the specified position
window.drawSquare = function (x, y) {
    var canvas = document.getElementById('myCanvas');
    var ctx = canvas.getContext('2d');

    // Clear the canvas
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Draw the square
    ctx.fillStyle = 'blue';
    ctx.fillRect(x, y, 50, 50); // Adjust size as needed
};
