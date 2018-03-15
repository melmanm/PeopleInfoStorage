using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PeopleInfoStorage.Core.Domain;
using PeopleInfoStorage.Services.Repository;

namespace PeopleInfoStorage.Services.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IRepository<Person> _PeopleRepository;
        private readonly ISerializer<Person> _PeopleSerializer;
        public PeopleService(IRepository<Person> peopleRepository, ISerializer<Person> peopleSerializer )
        {
            _PeopleRepository = peopleRepository;
            _PeopleSerializer = peopleSerializer;
        }
        public async Task<List<Person>> GetPeopleAsync()
        {
            return await _PeopleRepository.GetAllAsync();
        }

        public async Task<bool> HasChangesAsync(List<Person> people)
        {
            return !await _PeopleSerializer.CompareAsync(people, await _PeopleRepository.GetAllAsync());
        }

        public async Task SaveChangesAsync(List<Person> people)
        {
            await _PeopleRepository.OverwriteAsync(people);
        }
    }
}
