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
