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

function beginCommentEventHandler(element, username, commentId, listItemIndex, itemLevel) {
    let commentForm = document.querySelector('.comment-form');

    if (commentForm != null) return;

    commentForm = createCommentForm();
    const saveBtn = commentForm.querySelector('#saveBtn');
    const cancelBtn = commentForm.querySelector('#cancelBtn');

    saveBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        addCommentEventHandler(username, commentId, listItemIndex, itemLevel);
    });
    cancelBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        cancelCommentEventHandler(commentId);
    });

    const parrentElement = element.parentElement;
    if (commentId == 0) {
        parrentElement.insertBefore(commentForm, element.nextSibling)
    }
    else {
        parrentElement.appendChild(commentForm);
    }
}

function editCommentEventHandler(element, formIdEnd, text) {
    let commentForm = document.querySelector('.comment-form');

    if (commentForm != null) return;

    commentForm = createCommentForm();
    const textbox = commentForm.querySelector('#commentText');
    textbox.value = text;

    const saveBtn = commentForm.querySelector('#saveBtn');
    const cancelBtn = commentForm.querySelector('#cancelBtn');

    saveBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        const commentText = document.querySelector('#commentText');
        const comment = commentText.value;

        if (comment.length > 0 && comment.length <= 600) {
            const form = $('#commentDataForm')[0];
            const formData = new FormData(form);
            formData.append('comment', comment);
            formData.append('parrentId', formIdEnd);

            $.ajax({
                url: '/Game/EditComment',
                data: formData,
                contentType: false,
                processData: false,
                async: false,
                type: 'POST',
                success: function () {
                    const commentTextEl = document.querySelector('#textContent' + formIdEnd);
                    commentTextEl.textContent = comment;
                }
            });

            cancelCommentEventHandler();
        } else {
            alert('Comment must be between 1 and 600 characters.');
        }
    });

    cancelBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        cancelCommentEventHandler(formIdEnd);
    });

    const parrentElement = element.parentElement;
    if (formIdEnd == 0) {
        parrentElement.insertBefore(commentForm, element.nextSibling)
    }
    else {
        parrentElement.appendChild(commentForm);
    }
}

function addCommentEventHandler(username, formIdEnd, listItemIndex, itemLevel) {
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
                else if (itemLevel >= 3)
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

                cancelCommentEventHandler();
            }
        });
    } else {
        alert('Comment must be between 1 and 600 characters.');
    }
}

function cancelCommentEventHandler() {
    const commentForm = document.querySelector('.comment-form');
    const parrentElement = commentForm.parentElement;
    parrentElement.removeChild(commentForm);
}

function createCommentForm() {
    const commentTextbox = document.createElement('textarea');
    commentTextbox.id = 'commentText';
    commentTextbox.placeholder = 'Enter your comment (max 600 characters)';

    const saveBtn = document.createElement('button');
    saveBtn.id = 'saveBtn';
    saveBtn.innerText = 'Save';

    const cancelBtn = document.createElement('button');
    cancelBtn.innerText = 'Cancel';
    cancelBtn.id = 'cancelBtn';

    const commentBtnsDiv = document.createElement('div');
    commentBtnsDiv.classList.add('comment-buttons');
    commentBtnsDiv.appendChild(saveBtn);
    commentBtnsDiv.appendChild(cancelBtn);

    const commentForm = document.createElement('div');
    commentForm.classList.add('comment-form');
    //commentForm.id = 'commentForm' + commentId;
    commentForm.appendChild(commentTextbox);
    commentForm.appendChild(commentBtnsDiv);

    return commentForm;
}
