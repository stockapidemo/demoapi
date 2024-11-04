using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demopetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CatLookup : ControllerBase
    {
        private Dictionary<string, CatDetails> CatDictionary = new Dictionary<string, CatDetails>
        {
            {"C895210", new CatDetails { PetID = "C895210", Name = "Fluffy", Breed = "Persian", Age = 2, Location = "Paw Pad", PhoneNumber = "(111) 111-1111", Email = "info@pawpad.com"}},
            {"C895211", new CatDetails { PetID = "C895211", Name = "Mister Meanface", Breed = "Main Coon", Age = 5, Location = "The Scratching Post", PhoneNumber = "(222) 222-2222", Email = "meow@scratchingpost.com"}},
            {"C895212", new CatDetails { PetID = "C895212", Name = "Fluffy", Breed = "Siamese", Age = 3, Location = "Paw Pad", PhoneNumber = "(111) 111-1111", Email = "info@pawpad.com"}},
            {"C895213", new CatDetails { PetID = "C895213", Name = "Poof", Breed = "Russian Blue", Age = 7, Location = "Zoomies", PhoneNumber = "(444) 444-4444", Email = "info@zoomies.org"}},
            // Add more mappings as needed
        };

        /// <summary>
        /// Get information about a Cat by PetID.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="400">The petid field is missing or not accepted.</response>
        /// 
        [HttpGet("GetByPetID")]
        public IActionResult GetCatInfoByPetID([FromQuery] string petID)
        {
            // Manually validate the PetID using CatDetails validation attributes
            var CatDetails = new CatDetails { PetID = petID };

            // Specify the member name to validate (PetID in this case)
            var validationContext = new ValidationContext(CatDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(CatDetails.PetID)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the PetID property
            if (!Validator.TryValidateObject(CatDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for PetID
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingCats = CatDictionary
                .Where(entry => entry.Value.PetID == petID)
                .Select(entry => entry.Value)
                .ToList();

            if (matchingCats.Any())
            {
                var result = new
                {
                    Cats = matchingCats
                };

                return Ok(result);
            }

            var noCat = new
            {
                Message = $"No Cats found with the ID '{petID}'."
            };

            return NotFound(noCat);
        }

        /// <summary>
        /// Get information about Cats by name.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="400">The name field is missing or not accepted.</response>
        /// <response code="404">If no Cats are found.</response>
        [HttpGet("GetByName")]
        public IActionResult GetCatInfoByName([FromQuery] string name)
        {
            // Manually validate the Name using CatDetails validation attributes
            var CatDetails = new CatDetails { Name = name };

            // Specify the member name to validate (Name in this case)
            var validationContext = new ValidationContext(CatDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(CatDetails.Name)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Name property
            if (!Validator.TryValidateObject(CatDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Name
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingCats = CatDictionary
                .Where(entry => entry.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingCats.Any())
            {
                var result = new
                {
                    Cats = matchingCats
                };

                return Ok(result);
            }

            var noCat = new
            {
                Message = $"No Cats found with the name '{name}'."
            };

            return NotFound(noCat);
        }

        /// <summary>
        /// Get information about Cats by breed.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="400">The breed field is missing or not accepted.</response>
        /// <response code="404">If no Cats are found.</response>
        [HttpGet("GetByBreed")]
        public IActionResult GetCatInfoByBreed([FromQuery] string breed)
        {
            // Manually validate the Breed using CatDetails validation attributes
            var CatDetails = new CatDetails { Breed = breed };

            // Specify the member name to validate (Breed in this case)
            var validationContext = new ValidationContext(CatDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(CatDetails.Breed)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Breed property
            if (!Validator.TryValidateObject(CatDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Breed
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingCats = CatDictionary
                .Where(entry => entry.Value.Breed.Equals(breed, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingCats.Any())
            {
                var result = new
                {
                    Cats = matchingCats
                };

                return Ok(result);
            }

            var noCat = new
            {
                Message = $"No Cats found with the breed '{breed}'."
            };

            return NotFound(noCat);
        }

        /// <summary>
        /// Get information about Cats by age.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="400">The age field is missing or not accepted.</response>
        /// <response code="404">If no Cats are found.</response>
        [HttpGet("GetByAge")]
        public IActionResult GetCatInfoByAge([FromQuery] int age)
        {
            var CatDetailsForAge = new CatDetails { Age = age };
            var validationContextForAge = new ValidationContext(CatDetailsForAge, serviceProvider: null, items: null);
            var validationResultsForAge = new List<ValidationResult>();

            if (!Validator.TryValidateObject(CatDetailsForAge, validationContextForAge, validationResultsForAge, validateAllProperties: true))
            {
                // Validation failed for Age
                var validationErrorsForAge = validationResultsForAge.Select(result => result.ErrorMessage);
                return BadRequest(validationErrorsForAge);
            }

            var matchingCats = CatDictionary
                .Where(entry => entry.Value.Age == age)
                .Select(entry => entry.Value)
                .ToList();

            if (matchingCats.Any())
            {
                var result = new
                {
                    Cats = matchingCats
                };

                return Ok(result);
            }

            var noCat = new
            {
                Message = $"No Cats found with the age '{age}'."
            };

            return NotFound(noCat);
        }

        /// <summary>
        /// Get information about Cats by location.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="400">The location field is missing or not accepted.</response>
        /// <response code="404">If no Cats are found.</response>
        [HttpGet("GetByLocation")]
        public IActionResult GetCatInfoByLocation([FromQuery] string location)
        {
            // Manually validate the Location using CatDetails validation attributes
            var CatDetails = new CatDetails { Location = location };

            // Specify the member name to validate (Location in this case)
            var validationContext = new ValidationContext(CatDetails, serviceProvider: null, items: null)
            {
                MemberName = nameof(CatDetails.Location)
            };

            var validationResults = new List<ValidationResult>();

            // Validate only the Location property
            if (!Validator.TryValidateObject(CatDetails, validationContext, validationResults, validateAllProperties: true))
            {
                // Validation failed for Location
                var validationErrors = validationResults.Select(result => result.ErrorMessage);
                return BadRequest(validationErrors);
            }

            var matchingCats = CatDictionary
                .Where(entry => entry.Value.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
                .Select(entry => entry.Value)
                .ToList();

            if (matchingCats.Any())
            {
                var result = new
                {
                    Cats = matchingCats
                };

                return Ok(result);
            }

            var noCat = new
            {
                Message = $"No Cats found at the location '{location}'."
            };

            return NotFound(noCat);
        }

        /// <summary>
        /// Get information about all Cats.
        /// </summary>
        /// <response code="200">Returns Cat information.</response>
        /// <response code="404">If no Cats are found.</response>
        [HttpGet("GetAllCats")]
        public IActionResult GetAllCats()
        {
            var allCats = CatDictionary.Values.ToList();

            if (allCats.Count > 0)
            {
                var result = new
                {
                    AllCats = allCats
                };

                return new JsonResult(result);
            }

            var noCats = new
            {
                Message = "No Cats found in the dictionary."
            };

            return NotFound(noCats);
        }

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
}