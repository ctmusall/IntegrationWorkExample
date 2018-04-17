using System.Collections.Generic;
using eClosings.Entities.Persons;
using eClosings.Entities.Services;

namespace eClosings.Entities.Attorneys
{
    public class Attorney : Person
    {
        public string AttorneyId { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
