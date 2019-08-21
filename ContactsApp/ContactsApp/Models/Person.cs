using SQLite;

namespace ContactsApp.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string FullName { get; set; }

        public double Rating { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        public int PostalCode { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(20)]
        public string Latitude { get; set; }
        [MaxLength(20)]
        public string Longitude { get; set; }
        [MaxLength(100)]
        public string ImageURL { get; set; }

    }
}
