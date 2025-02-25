// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Dark mode code source: https://oakharborwebdesigns.com/blog/how-to-add-dark-mode-to-your-website/

function enableDarkMode() {
    document.body.classList.add('dark-mode');
    document.getElementById('navbar-top').classList.add('bg-dark');
    localStorage.setItem('theme', 'dark');
}
function disableDarkMode() {
    document.body.classList.remove('dark-mode');
    document.getElementById('navbar-top').classList.remove('bg-dark');
    localStorage.setItem('theme', 'light');
}

// determines a new users dark mode preferences
function detectColorScheme() {
    // default to the light theme
    let theme = 'light';
    // check localStorage for a saved 'theme' variable. if it's there, the user has visited before, so apply the necessary theme choices
    if (localStorage.getItem('theme')) {
        theme = localStorage.getItem('theme');
    }
    // if it's not there, check to see if the user has applied dark mode preferences themselves in the browser
    else if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        theme = 'dark';
    }
    // if there is no preference set, the default of light will be used. apply accordingly
    theme === 'dark' ? enableDarkMode() : disableDarkMode();
}





document.addEventListener("DOMContentLoaded", function () {
    // run on page load
    detectColorScheme();
    // add event listener to the dark mode button toggle
    document.getElementById('dark-mode-toggle').addEventListener('click', () => {
        // on click, check localStorage for the dark mode value, use to apply the opposite of what's saved
        localStorage.getItem('theme') === 'light' ? enableDarkMode() : disableDarkMode();
    });
});