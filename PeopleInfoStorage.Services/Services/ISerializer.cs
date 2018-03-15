using PeopleInfoStorage.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleInfoStorage.Services.Services
{
    public interface ISerializer<T> where T : BaseEntity , new()
    {
        Task SerializeAsync(List<T> collection, string path);
        Task<List<T>> DeserializeAsync(string path);
        Task<bool> CompareAsync(List<T> collection1, List<T> collection2);
    }
}
