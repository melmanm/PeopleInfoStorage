using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PeopleInfoStorage.Core.Domain;

namespace PeopleInfoStorage.Services.Services
{
    public class XmlSerializer<T> : ISerializer<T> where T : BaseEntity, new()
    {

        private readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
        public async Task<bool> CompareAsync(List<T> collection1, List<T> collection2)
        {
            return await Task.FromResult(String.Equals(Serialize(collection1), Serialize(collection2)));
        }

        public async Task<List<T>> DeserializeAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return (List<T>) await Task.FromResult(xmlSerializer.Deserialize(reader));
            }
        }

        public async Task SerializeAsync(List<T> collection, string path)
        {
            await Task.Run(() =>
            {
                using (var writer = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(writer, collection);
                }
            });
        }

        private string Serialize(ICollection<T> collection)
        {
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, collection);
                return writer.ToString();
            }
        }
    }
}
