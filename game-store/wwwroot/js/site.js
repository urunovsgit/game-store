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

function applyGenresFilter() {
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

function beginCommentEvent(element, username, commentId, listItemIndex, itemLevel) {
    const commentTextbox = document.createElement('textarea');
    commentTextbox.id = 'commentText';
    commentTextbox.placeholder = 'Enter your comment (max 600 characters)';

    const saveBtn = document.createElement('button');
    saveBtn.innerText = 'Save';
    saveBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        addCommentEvent(username, commentId, listItemIndex, itemLevel);
    });

    const cancelBtn = document.createElement('button');
    cancelBtn.innerText = 'Cancel';
    cancelBtn.onclick = function (e) {
        e.stopPropagation();
        cancelCommentEvent(commentId);
    }

    const commentBtnsDiv = document.createElement('div');
    commentBtnsDiv.classList.add('comment-buttons');
    commentBtnsDiv.appendChild(saveBtn);
    commentBtnsDiv.appendChild(cancelBtn);

    const commentForm = document.createElement('div');
    commentForm.classList.add('comment-form');
    commentForm.id = 'commentForm' + commentId;
    commentForm.appendChild(commentTextbox);
    commentForm.appendChild(commentBtnsDiv);

    const parrentElement = element.parentElement;
    if (commentId == 0) {
        parrentElement.insertBefore(commentForm, element.nextSibling)
    }
    else {
        parrentElement.appendChild(commentForm);
    }

}

function editCommentEvent(formIdEnd) {
    const commentForm = document.querySelector('#commentForm' + formIdEnd);
    const commentText = document.querySelector('#textContent' + formIdEnd);

    const textBox = commentForm.getElementsByTagName('textbox');
    textBox.val = commentText.textContent;

    const saveBtn = document.querySelector('#comment-save' + formIdEnd);
    const cancelBtn = document.querySelector('#comment-cancel' + formIdEnd);


    commentForm.classList.remove('hidden');
}

function addCommentEvent(username, formIdEnd, listItemIndex, itemLevel) {
    const commentText = document.querySelector('#commentText');

    const comment = commentText.value;
    if (comment.length > 0 && comment.length <= 600) {
        const form = $('#commentDataForm')[0];
        const formData = new FormData(form);
        formData.append('comment', comment);
        formData.append('parrentId', formIdEnd);

        $.ajax({
            url: '/Game/AddComment',
            data: formData,
            contentType: false,
            processData: false,
            async: false,
            type: 'POST',
            success: function (commentId) {
                const commentDateTime = document.createElement('span');
                commentDateTime.textContent = new Date().toLocaleString();

                const usernameHeader = document.createElement('h5');
                usernameHeader.textContent = username + ' ';
                usernameHeader.appendChild(commentDateTime);

                const commentTextEl = document.createElement('p');
                commentTextEl.textContent = comment;
                commentTextEl.id = 'textContent' + commentId;

                const replyBtn = document.createElement('button');
                replyBtn.classList.add('comment-button', 'btn-sm', 'btn-link');
                replyBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    beginCommentEvent(this, username, commentId, listItemIndex, itemLevel);
                });
                replyBtn.innerText = 'Reply';

                const commentContentDiv = document.createElement('div');
                commentContentDiv.id = 'comment-content' + commentId;
                commentContentDiv.classList.add('comment-content');
                commentContentDiv.appendChild(usernameHeader);
                commentContentDiv.appendChild(commentTextEl);
                commentContentDiv.appendChild(replyBtn);

                const commentItemDiv = document.createElement('div');
                commentItemDiv.classList.add('comment');
                commentItemDiv.appendChild(commentContentDiv);

                const commentItem = document.createElement('li');
                commentItem.appendChild(commentItemDiv);

                if (itemLevel == 0)
                    commentItem.classList.add('comment-first-level');
                else if (itemLevel == 1)
                    commentItem.classList.add('comment-second-level');
                else if (itemLevel == 2)
                    commentItem.classList.add('comment-third-level');
                else if(itemLevel >= 3)
                    commentItem.classList.add('comment-fourth-level');

                //commentItem.style.cssText = `margin-left: ${(itemLevel + 1) * 20}px;`;

                const commentsList = document.querySelector('.comment-list');

                if (listItemIndex != -1) {
                    const currentNode = commentsList.children[listItemIndex];
                    currentNode.parentNode.insertBefore(commentItem, currentNode.nextSibling);

                    document.getelem
                }
                else {
                    commentsList.appendChild(commentItem);
                }

                cancelCommentEvent();
            }
        });
    } else {
        alert('Comment must be between 1 and 600 characters.');
    }
}

function cancelCommentEvent() {
    const commentForm = document.querySelector('.comment-form');
    const parrentElement = commentForm.parentElement;
    parrentElement.removeChild(commentForm);
}

