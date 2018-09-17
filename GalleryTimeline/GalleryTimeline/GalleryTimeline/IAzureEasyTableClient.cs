using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GalleryTimeline
{
    public interface IAzureEasyTableClient<T>
    {
        Task<T> GetAsync(object id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> AddAsync(T obj);
        Task<T> UpdateAsync(object id, T obj);
        Task<T> DeleteAsync(object id);
    }
}
