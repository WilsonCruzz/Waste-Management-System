// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('scroll', function () {
    var header = document.querySelector('nav');
    var scrollY = window.scrollY || document.documentElement.scrollTop;

    if (scrollY > 50) { // Cambiar 100 por la cantidad deseada
        header.classList.add('scrolled');
    } else {
        header.classList.remove('scrolled');
    }
});