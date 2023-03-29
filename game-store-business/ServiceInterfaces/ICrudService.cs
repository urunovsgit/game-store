using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_store_business.ServiceInterfaces
{
    public interface ICrudService<TModel> where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> CreateAsync(TModel modelDTO);
        Task<TModel> UpdateAsync(TModel modelDTO);
        Task DeleteByIdAsync(int id);
    }
}
