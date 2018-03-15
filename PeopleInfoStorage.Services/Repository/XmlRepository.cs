using PeopleInfoStorage.Core.Domain;
using PeopleInfoStorage.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PeopleInfoStorage.Services.Repository
{
    public class XmlRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private ISerializer<T> _Serializer;
        private readonly string _Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private const string _FILENAME = "data.xml";
        private string _Path
        {
            get { return Path.Combine(_Directory, _FILENAME); }
        }
        public XmlRepository(ISerializer<T> serializer)
        {
            _Serializer = serializer;
            if (!File.Exists(_Path))
            {
                (OverwriteAsync(new List<T>())).RunSynchronously();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _Serializer.DeserializeAsync(_Path);
        }


        public async Task OverwriteAsync(List<T> collection)
        {
             await _Serializer.SerializeAsync(collection, _Path);
        }
    }
}
