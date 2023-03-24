using AutoMapper;
using Business;
using Data.Interfaces;
using game_store_business.Models;
using game_store_business.ServiceInterfaces;
using game_store_domain.Data;
using game_store_domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServicesProviders
{
    public class CartServiceProvider : ICartService
    {
        private readonly IUnitOfWork _gsUnitOfWork;
        private readonly IMapper _mapperProfile;

        public CartServiceProvider(IUnitOfWork unitOfWork, IMapper mapperProfile)
        {
            _gsUnitOfWork = unitOfWork;
            _mapperProfile = mapperProfile;
        }

        public async Task<IEnumerable<CartModel>> GetAllAsync()
        {
            var orders = await _gsUnitOfWork.OrderRepository.GetAllAsync();
            return _mapperProfile.Map<IEnumerable<CartModel>>(orders);
        }

        public async Task<CartModel> GetByIdAsync(int id)
        {
            var order = await _gsUnitOfWork.OrderRepository.GetByIdAsync(id);
            return _mapperProfile.Map<CartModel>(order);
        }

        public async Task<CartModel> CreateAsync(CartModel modelDTO)
        {
            var newCart = _mapperProfile.Map<Cart>(modelDTO);

            await _gsUnitOfWork.CartRepository.AddAsync(newCart);
            await _gsUnitOfWork.SaveAsync();

            var user = await _gsUnitOfWork.UserManager.FindByIdAsync(newCart.UserId.ToString());
            user.CartId = newCart.Id;
            await _gsUnitOfWork.UserManager.UpdateAsync(user);
            await _gsUnitOfWork.SaveAsync();

            return _mapperProfile.Map<CartModel>(newCart);
        }

        public async Task<CartModel> AddGameToCartAsync(int gameId, int cartId)
        {
            var cartItem = new CartItem
            {
                CartId = cartId,
                GameId = gameId,
                Quantity = 1,
            };

            await _gsUnitOfWork.CartItemRepository.AddAsync(cartItem);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);

            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<CartModel> RemoveGameFromCartAsync(int cartId, int itemId)
        {
            await _gsUnitOfWork.CartItemRepository.DeleteByIdAsync(itemId);
            await _gsUnitOfWork.SaveAsync();

            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);
            return _mapperProfile.Map<CartModel>(cart);
        }

        public async Task<CartItemUpdateResponse> IncreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity++;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task<CartItemUpdateResponse> DecreaseGameQuantityAsync(int cartItemId)
        {
            var cartItem = await _gsUnitOfWork.CartItemRepository.GetByIdAsync(cartItemId);
            cartItem.Quantity--;
            _gsUnitOfWork.CartItemRepository.Update(cartItem);
            await _gsUnitOfWork.SaveAsync();

            return new CartItemUpdateResponse
            {
                Quantity = cartItem.Quantity,
                ItemSum = cartItem.Quantity * cartItem.Game.Price,
                CartSum = cartItem.Cart.Items.Sum(ci => ci.Quantity * ci.Game.Price)
            };
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _gsUnitOfWork.OrderRepository.DeleteByIdAsync(id);
            await _gsUnitOfWork.SaveAsync();
        }

        public async Task<CartModel> UpdateAsync(CartModel modelDTO)
        {
            var order = _mapperProfile.Map<Order>(modelDTO);
            _gsUnitOfWork.OrderRepository.Update(order);
            await _gsUnitOfWork.SaveAsync();

            order = await _gsUnitOfWork.OrderRepository.GetByIdAsync(order.Id);
            return _mapperProfile.Map<CartModel>(order);
        }

        public async Task<OrderModel> CreateOrderForCartAsync(int cartId)
        {
            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(cartId);
            var user = await _gsUnitOfWork.UserManager.FindByIdAsync(cart.UserId.ToString());

            if (user == null) return null;

            return new OrderModel
            {
                UserId = cart.UserId,
                CartId = cartId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<OrderModel> ConfirmOrderCreationAsync(OrderModel orderModel)
        {
            var order = _mapperProfile.Map<Order>(orderModel);
            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(order.CartId);

            await _gsUnitOfWork.OrderRepository.AddAsync(order);

            foreach (var item in cart.Items)
            {
                _gsUnitOfWork.CartItemRepository.Delete(item);
            }

            await _gsUnitOfWork.SaveAsync();
            return _mapperProfile.Map<OrderModel>(order);
        }

        public async Task<CartModel> GetCartByUserId(int userId)
        {
            var user = await _gsUnitOfWork.UserManager.FindByIdAsync(userId.ToString());
            var cart = await _gsUnitOfWork.CartRepository.GetByIdAsync(user.CartId);

            return _mapperProfile.Map<CartModel>(cart);
        }
    }
}
