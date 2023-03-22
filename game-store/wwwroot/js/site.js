﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




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