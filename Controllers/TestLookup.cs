using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace demopetapi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TestLookup : ControllerBase
    {
        private Dictionary<string, CatDetails> CatDictionary = new Dictionary<string, CatDetails>
        {
            {"895210", new CatDetails { PetID = "895210", Name = "Fluffy", Breed = "Persian", Age = 2, Location = "Paw Pad", PhoneNumber = "(111) 111-1111", Email = "info@pawpad.com"}},
            {"895211", new CatDetails { PetID = "895211", Name = "Mister Meanface", Breed = "Main Coon", Age = 5, Location = "The Scratching Post", PhoneNumber = "(222) 222-2222", Email = "meow@scratchingpost.com"}},
            {"895212", new CatDetails { PetID = "895212", Name = "Fluffy", Breed = "Siamese", Age = 3, Location = "Paw Pad", PhoneNumber = "(111) 111-1111", Email = "info@pawpad.com"}},
            {"895213", new CatDetails { PetID = "895213", Name = "Poof", Breed = "Russian Blue", Age = 7, Location = "Zoomies", PhoneNumber = "(444) 444-4444", Email = "info@zoomies.org"}},
            // Add more mappings as needed
        };


        [HttpGet("GetByPetID/{petID}")]
        public IActionResult GetCatInfoByPetID(string petID)
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

        [HttpGet("GetByAge/{age}")]
        public IActionResult GetCatInfoByAge(int age)
        {
            var CatDetailsForAge = new CatDetails { Age = age }; // Use int directly
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

        [HttpGet("GetByName/{name}")]
        public IActionResult GetCatInfoByName(string name)
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
            [RegularExpression("^[0-9]{6}$", ErrorMessage = "PetID must be a 6-digit number")]
            public string PetID { get; set; }

            [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Name must be alphanumeric")]
            public string Name { get; set; }

            [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Breed must be alphanumeric")]
            public string Breed { get; set; }

            public int Age { get; set; }

            [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Location must be alphanumeric")]
            public string Location { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }

        }
    }
}