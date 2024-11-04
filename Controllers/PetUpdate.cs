using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demopetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PetAdmin : ControllerBase
    {
        /// <summary>
        /// Update a pet record by PetID.
        /// </summary>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] PetUpdateModel petUpdate)
        {

            string message = $"You have requested to update '{petUpdate.PetID}'.";

            return Ok(new { Message = message });

        }

        /// <summary>
        /// Create a new pet record.
        /// </summary>
        [HttpPost("Submit")]
        public IActionResult Submit([FromBody] PetSubmitModel petRequest)
        {

            string message = $"Thank you for submitting '{petRequest.Name}'.";

            return Ok(new { Message = message });

        }
    }

    public class PetUpdateModel
    {
        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "PetID must be a 6-digit number")]
        public string PetID { get; set; }

        [Required]
        [NameContainsAnimal(ErrorMessage = "Name must contain the word 'Cat' or 'Dog' (case-insensitive)")]
        public string Type { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name must be alphanumeric")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Breed must be alphanumeric")]
        public string Breed { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]?$", ErrorMessage = "Age must be between 1 and 99")]
        public string Age { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Location must be alphanumeric")]
        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "PhoneNumber format (###) ###-####")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }

    public class PetSubmitModel
    {
        [Required]
        [NameContainsAnimal(ErrorMessage = "Name must contain the word 'Cat' or 'Dog' (case-insensitive)")]
        public string Type { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name must be alphanumeric")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Breed must be alphanumeric")]
        public string Breed { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]?$", ErrorMessage = "Age must be between 1 and 99")]
        public string Age { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Location must be alphanumeric")]
        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "PhoneNumber must be in the format (XXX) XXX-XXXX")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
    public class NameContainsAnimalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString().ToLower();
                if (name.Contains("cat") || name.Contains("dog"))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
