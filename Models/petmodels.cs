namespace PetModels
{
    public class CatDetails
    {
        [RegularExpression("^[a-zA-Z][0-9]{6}$", ErrorMessage = "PetID must start with a letter followed by a 6-digit number")]
        public string PetID { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name must be alphanumeric")]
        public string Name { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Breed must be alphanumeric")]
        public string Breed { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Location must be alphanumeric")]
        public string Location { get; set; }

        [RegularExpression("^\\(\\d{3}\\) \\d{3}-\\d{4}$", ErrorMessage = "PhoneNumber format (###) ###-####")]
        public string PhoneNumber { get; set; }

        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Incorrect Email formate")]
        public string Email { get; set; }

        public int Age { get; set; }

    }
}