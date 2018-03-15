using PeopleInfoStorage.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleInfoStorage.Services.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<List<T>> GetAllAsync();
        Task OverwriteAsync(List<T> collection);
    }
}
