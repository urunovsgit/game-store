// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

const addNewGameBtn = $('#addNewGameBtn');
addNewGameBtn.click(postNewGameData);

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

function editCommentEventHandler(element, commentId, text) {
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
            formData.append('id', commentId);
            formData.append('comment', comment);

            $.ajax({
                url: '/Game/EditComment',
                data: formData,
                contentType: false,
                processData: false,
                async: false,
                type: 'POST',
                success: function () {
                    const commentTextEl = document.querySelector('#textContent' + commentId);
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
                replyBtn.id = 'replyBtn' + commentId;
                replyBtn.classList.add('comment-button', 'btn', 'btn-link');
                replyBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    beginCommentEventHandler(this, username, commentId, listItemIndex, itemLevel);
                });
                replyBtn.innerText = 'Reply';

                const editBtn = document.createElement('button');
                editBtn.id = 'editBtn' + commentId;
                editBtn.classList.add('comment-button', 'btn', 'btn-link');
                editBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    editCommentEventHandler(this, commentId, comment);
                });
                editBtn.innerText = 'Edit';

                const deleteBtn = document.createElement('button');
                deleteBtn.id = 'deleteBtn' + commentId;
                deleteBtn.classList.add('comment-button', 'btn', 'btn-link');
                deleteBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    deleteCommentEventHandler(commentId);
                });
                deleteBtn.innerText = 'Delete';

                const restoreBtn = document.createElement('button');
                restoreBtn.id = 'restoreBtn' + commentId;
                restoreBtn.classList.add('comment-button', 'btn', 'btn-link', 'hidden');
                restoreBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    restoreCommentEventHandler(commentId, comment);
                });
                restoreBtn.innerText = 'Restore';

                const commentContentDiv = document.createElement('div');
                commentContentDiv.id = 'comment-content' + commentId;
                commentContentDiv.classList.add('comment-content');
                commentContentDiv.appendChild(usernameHeader);
                commentContentDiv.appendChild(commentTextEl);
                commentContentDiv.appendChild(replyBtn);
                commentContentDiv.appendChild(editBtn);
                commentContentDiv.appendChild(deleteBtn);
                commentContentDiv.appendChild(restoreBtn);

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
    commentForm.appendChild(commentTextbox);
    commentForm.appendChild(commentBtnsDiv);

    return commentForm;
}

function deleteCommentEventHandler(commentId) {
    const textElement = document.querySelector('#textContent' + commentId)
    textElement.textContent = "This review has been removed.";
    textElement.classList.add('comment-removed');

    const replyBtn = document.querySelector('#replyBtn' + commentId);
    replyBtn.classList.add('hidden');

    const editBtn = document.querySelector('#editBtn' + commentId);
    editBtn.classList.add('hidden');

    const deleteBtn = document.querySelector('#deleteBtn' + commentId);
    deleteBtn.classList.add('hidden');

    const restoreBtn = document.querySelector('#restoreBtn' + commentId);
    restoreBtn.classList.remove('hidden');

    const data = { commentId: commentId };

    $.ajax({
        url: '/Game/DeleteComment',
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        async: false,
        type: 'POST'
    });
}

function restoreCommentEventHandler(commentId, commentText) {
    const textElement = document.querySelector('#textContent' + commentId)
    textElement.textContent = commentText;
    textElement.classList.remove('comment-removed');

    const replyBtn = document.querySelector('#replyBtn' + commentId);
    replyBtn.classList.remove('hidden');

    const editBtn = document.querySelector('#editBtn' + commentId);
    editBtn.classList.remove('hidden');

    const deleteBtn = document.querySelector('#deleteBtn' + commentId);
    deleteBtn.classList.remove('hidden');

    const restoreBtn = document.querySelector('#restoreBtn' + commentId);
    restoreBtn.classList.add('hidden');

    const data = { commentId: commentId };

    $.ajax({
        url: '/Game/RestoreComment',
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        async: false,
        type: 'POST'
    });
}

function addGameIntoCart(gameId) {
    event.preventDefault();

    const form = $('#gameDataForm' + gameId)[0];
    const formData = new FormData(form);

    $.ajax({
        url: '/Cart/AddGameIntoCart',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST',
        success: function () {
            const toastElem = document.getElementById('AddIntoCartToast');
            const toast = new bootstrap.Toast(toastElem);
            toast.show();

            const buyBtn = document.querySelector('#buyBtn' + gameId);
            buyBtn.remove();

            const cartLink = document.createElement('a');
            cartLink.classList.add('site-btn');
            cartLink.classList.add('btn-sm');
            cartLink.setAttribute('asp-action', 'Index');
            cartLink.setAttribute('asp-controller', 'Cart');
            cartLink.innerText = 'View in cart';

            const divContent = document.querySelector('#rgi' + gameId);
            divContent.appendChild(cartLink);

            const upperCartLink = document.querySelector('#cartLink');
            const textEl = upperCartLink.lastChild;
            textEl.remove();
            upperCartLink.innerHTML += (parseInt(textEl.textContent) + 1);
        }
    });
}

function decreaseGameQuantity (itemId) {
    const form = $('#cartItemForm' + itemId)[0];
    const formData = new FormData(form);

    $.ajax({
        url: '/Cart/DecreaseGameQuantity',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST',
        success: function (result) {
            document.querySelector('#sumItem' + itemId).innerText = '$' + result.itemSum.toFixed(2);
            const quantityElem = document.querySelector('#quantityItem' + itemId);
            quantityElem.innerText = result.quantity;

            const totalSumEl = document.querySelector('#totalSum');
            totalSumEl.innerText = 'Cart total sum: $' + result.totalSum.toFixed(2);
        }
    });
}

function increaseGameQuantity(itemId) {
    const form = $('#cartItemForm' + itemId)[0];
    const formData = new FormData(form);

    $.ajax({
        url: '/Cart/IncreaseGameQuantity',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST',
        success: function (result) {
            document.querySelector('#sumItem' + itemId).innerText = '$' + result.itemSum.toFixed(2);
            const quantityElem = document.querySelector('#quantityItem' + itemId);
            quantityElem.innerText = result.quantity;

            const totalSumEl = document.querySelector('#totalSum');
            totalSumEl.innerText = 'Cart total sum: $' + result.totalSum.toFixed(2);
        }
    });
}

function removeGameFromCart(itemId) {
    const form = $('#cartItemForm' + itemId)[0];
    const formData = new FormData(form);

    $.ajax({
        url: '/Cart/RemoveGameFromCart',
        data: formData,
        contentType: false,
        processData: false,
        async: false,
        type: 'POST',
        success: function (result) {
            const itemDiv = document.querySelector('#cartItem' + itemId);
            itemDiv.remove();

            const upperCartLink = document.querySelector('#cartLink');
            const textEl = upperCartLink.lastChild;
            textEl.remove();
            upperCartLink.innerHTML += (parseInt(textEl.textContent) - 1);

            if (result == 0) {
                const makeOrderForm = document.querySelector('#makeOrderForm');
                const p = document.createElement('p');
                p.innerText = "There is no games."
                p.classList.add('game-title');
                makeOrderForm.after(p);
                makeOrderForm.remove();
            }
            else {
                const totalSumEl = document.querySelector('#totalSum');
                totalSumEl.innerText = 'Cart total sum: $' + result.toFixed(2);
            }
        }
    });
}