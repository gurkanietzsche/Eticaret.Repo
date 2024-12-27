const connection = new signalR.HubConnectionBuilder()
    .withUrl("/favoritesHub")
    .build();

connection.on("UpdateFavoritesCount", function (count) {
    document.getElementById("favoritesCount").innerText = count;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

window.addEventListener('DOMContentLoaded', event => {
    // Simple-DataTables
    // https://github.com/fiduswriter/Simple-DataTables/wiki

    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple);
    }
});
