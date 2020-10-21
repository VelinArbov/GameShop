namespace GameShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GameShop.Data.Common.Repositories;
    using GameShop.Data.Models;
    using GameShop.Services.Mapping;

    public class GameService : IGameService
    {
        private readonly IDeletableEntityRepository<Game> repository;

        public GameService(IDeletableEntityRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateAsync(string title, string description, string imageUrl, decimal price)
        {
            var category = this.repository.AddAsync(new Game
            {
                Title = title,
                Description = description,
                ImageURL = imageUrl ?? "https://arbikas.com/pub/media/brands/asi.jpg",
                Price = price,
            });

            var result = await this.repository.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(string id)
        {
            var game = this.repository.All().FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                throw new Exception(
                    string.Format($"No game with this ID {0}", id));
            }

            this.repository.Delete(game);

            await this.repository.SaveChangesAsync();
        }

        public async Task EditAsync(string id, string title, string description, string imageUrl, decimal price)
        {
            var game = this.repository.All().FirstOrDefault(x => x.Id == id);
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description)
                                                 || string.IsNullOrWhiteSpace(imageUrl))
            {
                game.Title = title == null ? game.Title : title;
                game.Description = description == null ? game.Description : description;
                game.ImageURL = imageUrl == null ? game.ImageURL : imageUrl;
                game.Price = price == null ? game.Price : price;
            }
            else if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description)
                                                         || string.IsNullOrEmpty(imageUrl))
            {
                game.Title = title == null ? game.Title : title;
                game.Description = description == null ? game.Description : description;
                game.ImageURL = imageUrl == null ? game.ImageURL : imageUrl;
                game.Price = price == null ? game.Price : price;

            }
            else
            {
                game.Title = title;
                game.ImageURL = imageUrl;
                game.Description = description;
                game.Price = price;
            }

            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            IQueryable<Game> query =
                    this.repository.All().OrderByDescending(x => x.RealaseDate).Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetJobById<T>(string id)
        {
            var game = this.repository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();

            return game;
        }
    }
}
