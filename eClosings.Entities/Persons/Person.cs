using eClosings.Entities.Addresses;

namespace eClosings.Entities.Persons
{
    public class Person
    {
        public Address Address { get; set; }
        public string CellPhone { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string FaxNumber1 { get; set; }
        public string FaxNumber2 { get; set; }
        public string FirstName { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Salutation { get; set; }
        public string Suffix { get; set; }
        public string WorkPhone { get; set; }
    }
}
