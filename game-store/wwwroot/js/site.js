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

function beginCommentEvent(formIdEnd) {
    const commentForm = document.querySelector('#commentForm' + formIdEnd);

    commentForm.classList.remove('hidden');
}

function addCommentEvent(username, formIdEnd, listItemIndex) {
    const commentForm = document.querySelector('#commentForm' + formIdEnd);
    const commentText = document.querySelector('#commentText' + formIdEnd);


    const comment = commentText.value;
    if (comment.length > 0 && comment.length <= 600) {
        const form = $('#commentForm')[0];
        const formData = new FormData(form);
        formData.append('comment', comment);

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

                const replyBtn = document.createElement('button');
                replyBtn.classList.add('comment-button', 'btn', 'btn-link');
                replyBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    beginCommentEvent(commentId);
                });
                replyBtn.innerText = 'Reply';

                const commentTextbox = document.createElement('textarea');
                commentTextbox.id = 'commentText' + commentId;
                commentTextbox.placeholder = 'Enter your comment (max 600 characters)';

                const saveBtn = document.createElement('button');
                saveBtn.innerText = 'Save';
                saveBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    addCommentEvent(username, commentId, listItemIndex);
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
                commentForm.classList.add('hidden');
                commentForm.id = 'commentForm' + commentId;
                commentForm.appendChild(commentTextbox);
                commentForm.appendChild(commentBtnsDiv);

                const commentContentDiv = document.createElement('div');
                commentContentDiv.classList.add('comment-content');
                commentContentDiv.appendChild(usernameHeader);
                commentContentDiv.appendChild(commentTextEl);
                commentContentDiv.appendChild(replyBtn);
                commentContentDiv.appendChild(commentForm);

                const commentItemDiv = document.createElement('div');
                commentItemDiv.classList.add('comment');
                commentItemDiv.appendChild(commentContentDiv);

                const commentItem = document.createElement('li');
                commentItem.appendChild(commentItemDiv);

                const commentsList = document.querySelector('.comment-list');

                if (listItemIndex != -1) {                    
                    const existingNode = commentsList.children[listItemIndex];
                    existingNode.parentNode.insertBefore(commentItem, existingNode.nextSibling);
                }
                else {
                    commentsList.appendChild(commentItem);
                }

            }
        });

        commentForm.classList.add('hidden');
        commentText.value = '';
    } else {
        alert('Comment must be between 1 and 600 characters.');
    }
}

//function createCommentPostForm(commentId, username, listItemIndex) {
//    const commentDateTime = document.createElement('span');
//    commentDateTime.textContent = new Date().toLocaleString();

//    const usernameHeader = document.createElement('h5');
//    usernameHeader.textContent = username + ' ';
//    usernameHeader.appendChild(commentDateTime);

//    const commentTextEl = document.createElement('p');
//    commentTextEl.textContent = '';

//    const commentTextbox = document.createElement('textarea');
//    commentTextbox.id = 'commentText' + commentId;
//    commentTextbox.placeholder = 'Enter your comment (max 600 characters)';

//    const saveBtn = document.createElement('button');
//    saveBtn.addEventListener('click', function (e) {
//        e.stopPropagation();
//        addCommentEvent(username, commentId, listItemIndex);
//    })
//    //saveBtn.id = 'comment-save';
//    // saveBtn.onclick = addCommentEvent(username, commentId, listItemIndex);

//    const cancelBtn = document.createElement('button');
//    //cancelBtn.id = 'comment-cancel';
//    saveBtn.onclick = function (e) {
//        e.stopPropagation();
//        cancelCommentEvent(commentId);
//    }

//    const commentBtnsDiv = document.createElement('div');
//    commentBtnsDiv.classList.add('comment-buttons');
//    commentBtnsDiv.appendChild(saveBtn);
//    commentBtnsDiv.appendChild(cancelBtn);

//    const commentForm = document.createElement('div');
//    commentForm.classList.add('comment-form');
//    commentForm.classList.add('hidden');
//    commentForm.id = 'commentForm' + commentId;
//    commentForm.appendChild(commentTextbox);
//    commentForm.appendChild(commentBtnsDiv);

//    const commentContentDiv = document.createElement('div');
//    commentContentDiv.classList.add('comment-content');
//    commentContentDiv.appendChild(usernameHeader);
//    commentContentDiv.appendChild(commentTextEl);
//    commentContentDiv.appendChild(commentForm);

//    const commentItemDiv = document.createElement('div');
//    commentItemDiv.classList.add('comment');
//    commentItemDiv.appendChild(commentContentDiv);

//    const commentItem = document.createElement('li');
//    commentItem.appendChild(commentItemDiv);
//    commentsList.appendChild(commentItem);

//    return commentItem;
//}

function cancelCommentEvent(formIdEnd) {
    const commentForm = document.querySelector('#commentForm' + formIdEnd);
    const commentText = document.querySelector('#commentText' + formIdEnd);

    commentText.value = '';
    commentForm.classList.add('hidden');
}

