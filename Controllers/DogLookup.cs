using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demopetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DogLookup : ControllerBase
    {
        private Dictionary<string, DogDetails> DogDictionary = new Dictionary<string, DogDetails>
        {
            {"d895220", new DogDetails { PetID = "d895220", Name = "Buddy", Breed = "Labrador", Age = 2, Location = "Dog Park", PhoneNumber = "(111) 111-1111", Email = "info@Dogpark.com"}},
            {"d895221", new DogDetails { PetID = "d895221", Name = "Max", Breed = "Golden Retriever", Age = 2, Location = "Backyard", PhoneNumber = "(222) 222-2222", Email = "woof@backyard.com"}},
            {"d895222", new DogDetails { PetID = "d895222", Name = "Coco", Breed = "German Shepherd", Age = 3, Location = "City Streets", PhoneNumber = "(333) 333-3333", Email = "bark@citystreets.com"}},
            {"d895223", new DogDetails { PetID = "d895223", Name = "Buddy", Breed = "Boxer", Age = 4, Location = "Backyard", PhoneNumber = "(222) 222-2222", Email = "woof@backyard.com"}},
            // Add more mappings as needed
        };

        /// <summary>
        /// Get information about a Dog by PetID.
        /// </summary>
        /// <response code="200">Returns Dog information.</response>
        /// <response code="400">The petid field is missing or not accepted.</response>
        /// 
        [HttpGet("GetByPetID")]
        public IActionResult GetDogInfoByPetID([FromQuery] string petID)
        {
            // Manually validate the PetID using DogDetails validation attributes
            var DogDetails = new DogDetails { PetID = petID };

            // Specify the member name to validate (PetID in this case)
            var validationContext = new ValidationContext(DogDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(DogDetails.PetID)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the PetID property
            if (!Validator.TryValidateObject(DogDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for PetID
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingDogs = DogDictionary
                .Where(entry => entry.Value.PetID == petID)
                .Select(entry => entry.Value)
                .ToList();

            if (matchingDogs.Any())
            {
                var result = new
                {
                    Dogs = matchingDogs
                };

                return Ok(result);
            }

            var noDog = new
            {
                Message = $"No Dogs found with the ID '{petID}'."
            };

            return NotFound(noDog);
        }

        /// <summary>
        /// Get information about Dogs by name.
        /// </summary>
        /// <response code="200">Returns Dog information.</response>
        /// <response code="400">The name field is missing or not accepted.</response>
        /// <response code="404">If no Dogs are found.</response>
        [HttpGet("GetByName")]
        public IActionResult GetDogInfoByName([FromQuery] string name)
        {
            // Manually validate the Name using DogDetails validation attributes
            var DogDetails = new DogDetails { Name = name };

            // Specify the member name to validate (Name in this case)
            var validationContext = new ValidationContext(DogDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(DogDetails.Name)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Name property
            if (!Validator.TryValidateObject(DogDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Name
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingDogs = DogDictionary
                .Where(entry => entry.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingDogs.Any())
            {
                var result = new
                {
                    Dogs = matchingDogs
                };

                return Ok(result);
            }

            var noDog = new
            {
                Message = $"No Dogs found with the name '{name}'."
            };

            return NotFound(noDog);
        }

        /// <summary>
        /// Get information about Dogs by breed.
        /// </summary>
        /// <response code="200">Returns Dog information.</response>
        /// <response code="400">The breed field is missing or not accepted.</response>
        /// <response code="404">If no Dogs are found.</response>
        [HttpGet("GetByBreed")]
        public IActionResult GetDogInfoByBreed([FromQuery] string breed)
        {
            // Manually validate the Breed using DogDetails validation attributes
            var DogDetails = new DogDetails { Breed = breed };

            // Specify the member name to validate (Breed in this case)
            var validationContext = new ValidationContext(DogDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(DogDetails.Breed)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Breed property
            if (!Validator.TryValidateObject(DogDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Breed
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingDogs = DogDictionary
                .Where(entry => entry.Value.Breed.Equals(breed, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingDogs.Any())
            {
                var result = new
                {
                    Dogs = matchingDogs
                };

                return Ok(result);
            }

            var noDog = new
            {
                Message = $"No Dogs found with the breed '{breed}'."
            };

            return NotFound(noDog);
        }

        /// <summary>
        /// Get information about Dogs by age.
        /// </summary>
        /// <response code="200">Returns Dog information.</response>
        /// <response code="400">The age field is missing or not accepted.</response>
        /// <response code="404">If no Dogs are found.</response>
        [HttpGet("GetByAge")]
        public IActionResult GetDogInfoByAge([FromQuery] int age)
        {
            var DogDetailsForAge = new DogDetails { Age = age };
            var validationContextForAge = new ValidationContext(DogDetailsForAge, serviceProvider: null, items: null);
            var validationResultsForAge = new List<ValidationResult>();

            if (!Validator.TryValidateObject(DogDetailsForAge, validationContextForAge, validationResultsForAge, validateAllProperties: true))
            {
                // Validation failed for Age
                var validationErrorsForAge = validationResultsForAge.Select(result => result.ErrorMessage);
                return BadRequest(validationErrorsForAge);
            }

            var matchingDogs = DogDictionary
                .Where(entry => entry.Value.Age == age)
                .Select(entry => entry.Value)
                .ToList();

            if (matchingDogs.Any())
            {
                var result = new
                {
                    Dogs = matchingDogs
                };

                return Ok(result);
            }

            var noDog = new
            {
                Message = $"No Dogs found with the age '{age}'."
            };

            return NotFound(noDog);
        }

        /// <summary>
        /// Get information about Dogs by location.
        /// </summary>
        /// <response code="200">Returns Dog information.</response>
        /// <response code="400">The location field is missing or not accepted.</response>
        /// <response code="404">If no Dogs are found.</response>
        [HttpGet("GetByLocation")]
        public IActionResult GetDogInfoByLocation([FromQuery] string location)
        {
            // Manually validate the Location using DogDetails validation attributes
            var DogDetails = new DogDetails { Location = location };

            // Specify the member name to validate (Location in this case)
            var validationContext = new ValidationContext(DogDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(DogDetails.Location)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Location property
            if (!Validator.TryValidateObject(DogDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Location
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingDogs = DogDictionary
                .Where(entry => entry.Value.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingDogs.Any())
            {
                var result = new
                {
                    Dogs = matchingDogs
                };

                return Ok(result);
            }

            var noDog = new
            {
                Message = $"No Dogs found at the location '{location}'."
            };

            return NotFound(noDog);
        }

            /// <summary>
            /// Get information about all Dogs.
            /// </summary>
            /// <response code="200">Returns Dog information.</response>
            /// <response code="404">If no Dogs are found.</response>
            [HttpGet("GetAllDogs")]
        public IActionResult GetAllDogs()
        {
            var allDogs = DogDictionary.Values.ToList();

            if (allDogs.Count > 0)
            {
                var result = new
                {
                    AllDogs = allDogs
                };

                return new JsonResult(result);
            }

            var noDogs = new
            {
                Message = "No Dogs found in the dictionary."
            };

            return NotFound(noDogs);
        }

        public class DogDetails
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
            [EmailAddress(ErrorMessage = "Invalid email format")]

            public int Age { get; set; }

        }
    }
}
