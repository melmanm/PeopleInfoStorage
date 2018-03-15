using PeopleInfoStorage.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleInfoStorage.Services.Services
{
    public interface IPeopleService
    {
        Task<bool> HasChangesAsync(List<Person> people);
        Task SaveChangesAsync(List<Person> people);
        Task<List<Person>> GetPeopleAsync();
    }
}
