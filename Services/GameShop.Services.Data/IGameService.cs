﻿namespace GameShop.Services.Data
{
    using GameShop.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGameService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        T GetJobById<T>(string id);

        Game GetGameById(string id);

        Task<int> CreateAsync(string title, string description, string imageUrl, decimal price, DateTime realaseDate);

        Task EditAsync(string id, string title, string description, string imageUrl, decimal price);

        Task DeleteAsync(string id);

        Task BuyGame(string gameId, string userId);

        IEnumerable<Game> GetGamesById(List<string> ids);

    }
}
