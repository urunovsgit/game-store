//function onCommentRoot() {
//    const commentForm = document.querySelector('.comment-form');
//    commentForm.classList.remove('hidden');
//}

function beginCommentEventHandler(element, username, commentId, listItemIndex, itemLevel) {
    let commentForm = document.querySelector('.comment-form');
    //commentForm.classList.remove('hidden');

    if (commentForm != null) commentForm.remove();

    commentForm = createCommentForm(commentId);
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

function addCommentEventHandler(username, commentId, listItemIndex, itemLevel) {
    const commentText = document.querySelector('#commentText' + commentId);

    const comment = commentText.value;
    if (comment.length > 0 && comment.length <= 600) {
        const form = $('#commentDataForm')[0];
        const formData = new FormData(form);
        formData.append('Text', comment);
        formData.append('ParentId', commentId);

        $.ajax({
            url: '/Comment/AddComment',
            data: formData,
            contentType: false,
            processData: false,
            async: false,
            type: 'POST',
            success: function (resultId) {
                const commentDateTime = document.createElement('span');
                commentDateTime.textContent = new Date().toLocaleString();

                const usernameHeader = document.createElement('h5');
                usernameHeader.textContent = username + ' ';
                usernameHeader.appendChild(commentDateTime);

                const commentTextEl = document.createElement('p');
                commentTextEl.textContent = comment;
                commentTextEl.id = 'textContent' + resultId;

                const replyBtn = document.createElement('button');
                replyBtn.id = 'replyBtn' + resultId;
                replyBtn.classList.add('comment-button', 'btn', 'btn-link');
                replyBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    beginCommentEventHandler(this, username, resultId, parseInt(listItemIndex)+1, parseInt(itemLevel)+1);
                });
                replyBtn.innerText = 'Reply';

                const editBtn = document.createElement('button');
                editBtn.id = 'editBtn' + resultId;
                editBtn.classList.add('comment-button', 'btn', 'btn-link');
                editBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    editCommentEventHandler(this, resultId, comment);
                });
                editBtn.innerText = 'Edit';

                const deleteBtn = document.createElement('button');
                deleteBtn.id = 'deleteBtn' + resultId;
                deleteBtn.classList.add('comment-button', 'btn', 'btn-link');
                deleteBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    deleteCommentEventHandler(resultId);
                });
                deleteBtn.innerText = 'Delete';

                const restoreBtn = document.createElement('button');
                restoreBtn.id = 'restoreBtn' + resultId;
                restoreBtn.classList.add('comment-button', 'btn', 'btn-link', 'hidden');
                restoreBtn.addEventListener('click', function (e) {
                    e.stopPropagation();
                    restoreCommentEventHandler(resultId, comment);
                });
                restoreBtn.innerText = 'Restore';

                const commentContentDiv = document.createElement('div');
                commentContentDiv.id = 'comment-content' + resultId;
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

function editCommentEventHandler(element, commentId, text) {
    let commentForm = document.querySelector('.comment-form');

    if (commentForm != null) commentForm.remove();

    commentForm = createCommentForm(commentId);
    const textbox = commentForm.querySelector('#commentText' + commentId);
    textbox.value = text;

    const saveBtn = commentForm.querySelector('#saveBtn');
    const cancelBtn = commentForm.querySelector('#cancelBtn');

    saveBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        const commentText = document.querySelector('#commentText' + commentId);
        const comment = commentText.value;

        if (comment.length > 0 && comment.length <= 600) {
            const form = $('#commentDataForm')[0];
            const formData = new FormData(form);
            formData.append('Id', commentId);
            formData.append('Text', comment);

            $.ajax({
                url: '/Comment/EditComment',
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
        cancelCommentEventHandler();
    });

    const parrentElement = element.parentElement;
    if (commentId == 0) {
        parrentElement.insertBefore(commentForm, element.nextSibling)
    }
    else {
        parrentElement.appendChild(commentForm);
    }
}

function cancelCommentEventHandler() {
    const commentForm = document.querySelector('.comment-form');
    const parrentElement = commentForm.parentElement;
    parrentElement.removeChild(commentForm);
}

function createCommentForm(commentId) {
    const commentTextbox = document.createElement('textarea');
    commentTextbox.id = 'commentText' + commentId;
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
        url: '/Comment/DeleteComment',
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
        url: '/Comment/RestoreComment',
        data: data,
        contentType: 'application/x-www-form-urlencoded',
        async: false,
        type: 'POST'
    });
}