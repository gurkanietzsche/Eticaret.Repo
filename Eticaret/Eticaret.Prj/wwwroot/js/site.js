﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/favoritesHub")
    .build();

connection.on("UpdateFavoritesCount", function (count) {
    document.getElementById("favoritesCount").innerText = count;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
