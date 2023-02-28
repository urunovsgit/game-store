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

function beginCommentEvent() {
    const commentForm = document.querySelector('.comment-form');

    commentForm.classList.remove('hidden');
}

function addCommentEvent(username) {
    const commentForm = document.querySelector('.comment-form');
    const commentText = document.querySelector('#comment-text');
    const commentsList = document.querySelector('.comment-list');

    const comment = commentText.value;
    if (comment.length > 0 && comment.length <= 600) {
        const commentTextEl = document.createElement('p');
        commentTextEl.textContent = comment;

        const commentDateTime = document.createElement('span');
        commentDateTime.textContent = new Date().toLocaleString();

        const usernameHeader = document.createElement('h5');
        usernameHeader.textContent = username + ' ';
        usernameHeader.appendChild(commentDateTime);

        const commentContentDiv = document.createElement('div');
        commentContentDiv.classList.add('comment-content');
        commentContentDiv.appendChild(usernameHeader);
        commentContentDiv.appendChild(commentTextEl);

        const commentItemDiv = document.createElement('div');
        commentItemDiv.classList.add('comment');
        commentItemDiv.appendChild(commentContentDiv);

        const commentItem = document.createElement('li');
        commentItem.appendChild(commentItemDiv);
        commentsList.appendChild(commentItem);
        commentText.value = '';
        commentForm.classList.add('hidden');
    } else {
        alert('Comment must be between 1 and 600 characters.');
    }
}

function cancelCommentEvent() {
    const commentForm = document.querySelector('.comment-form');
    const commentText = document.querySelector('#comment-text');

    commentText.value = '';
    commentForm.classList.add('hidden');
}

