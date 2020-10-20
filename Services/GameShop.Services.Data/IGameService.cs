namespace GameShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGameService
    {
        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        T GetJobById<T>(string id);

        Task<int> CreateAsync(string title, string description, string imageUrl, decimal price);

    }
}
