using System.ComponentModel.DataAnnotations;

namespace ContactManager
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string FullAddress { get; set; } = "";

        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Cell { get; set; } = "";

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public int Age { get; set; }

        public string? Image { get; set; }
    }
}