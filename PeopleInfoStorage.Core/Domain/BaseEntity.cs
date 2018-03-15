using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleInfoStorage.Core.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set;}
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
