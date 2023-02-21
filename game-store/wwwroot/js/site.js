// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const gameUpdateBtn = $('#gameUpdateSbmtBtn');
gameUpdateBtn.click(gameUpdateHandler);

function gameUpdateHandler(event) {
    //stop submit the form, we will post it manually.
    event.preventDefault();

    // Get form
    const form = $('#gameDataForm')[0];

    // Create an FormData object
    const formData = new FormData(form);

    const genres = []
    $('#genres input[type=checkbox]:checked').each(function () {
        const genre = $(this).val();
        genres.push(genre)
    });

    // If you want to add an extra field for the FormData
    for (let i = 0; i < genres.length; i++) {
        formData.append('Genres[]', genres[i]);
    }

    $.ajax({
        url: '/Game/UpdateGameData',
        data: formData,
        contentType: false,
        processData: false,
        type: 'POST',
        success: function (result) {
            window.location.href = result;
        }
    });
}

//const filterGenresBtn = $('#applyGenresFilterBtn');
//filterGenresBtn.click(applyGenres);

//function applyGenres() {
//    //stop submit the form, we will post it manually.
//    //event.preventDefault();

//    const genres = []
//    $('#genres input[type=checkbox]:checked').each(function () {
//        const genre = $(this).val();
//        genres.push(genre)
//    });

//    const filterOptions = {
//        Page: $("#Page").val(),
//        PageSize: $("#PageSize").val(),
//        OrderOption: $("#OrderOption").val(),
//        FilterOption: 'Genre',//$("#FilterOption").val(),
//        FilterValue: genres
//    };

//    const DTO = JSON.stringify(filterOptions);

//    $.ajax({
//        url: '/Home/ApplyGenreFilterOptions',
//        cache: false,
//        type: 'POST',
//        contentType: 'application/json; charset=utf-8',
//        data: genres,
//        async: false,
//        dataType: "json"
//    });
//}

const filterGenresBtn = $('#applyGenresFilterBtn');
filterGenresBtn.click(applyGenresFilter);

function applyGenresFilter(event) {
    //stop submit the form, we will post it manually.
    //event.preventDefault();

    // Get form
    const form = $('#genresFilterForm')[0];

    // Create an FormData object
    const formData = new FormData(form);

    const genres = []
    $('#genres input[type=checkbox]:checked').each(function () {
        const genre = $(this).val();
        genres.push(genre)
    });

    // If you want to add an extra field for the FormData
    for (let i = 0; i < genres.length; i++) {
        formData.append('genres[]', genres[i]);
    }

    $.ajax({
        url: '/Home/ApplyGenreFilterOptions',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST'
        //success: function (result) {
        //    window.location.href = result;
        //}
    });
}

