function applyGamesFilter() {
    // Get form
    const form = $('#gamesFilterForm')[0];

    // Create an FormData object
    const formData = new FormData(form);

    const genres = []
    $('#genres input[type=checkbox]:checked').each(function () {
        const genre = $(this).val();
        genres.push(genre)
    });

    // If you want to add an extra field for the FormData
    for (let i = 0; i < genres.length; i++) {
        formData.append('AppliedGenres[]', genres[i]);
    }

    $.ajax({
        url: '/Home/ApplyFilterOptions',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST'
    });
}

function postNewGameData() {
    // stop submit the form, we will post it manually.
    event.preventDefault();

    // Get form
    const form = $('#newGameForm')[0];

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
        url: '/Game/AddGame',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST',
        success: function (result) {
            window.location.href = result;
        }
    });
}

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
        async: false,
        type: 'POST',
        success: function (result) {
            window.location.href = result;
        }
    });
}

