using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_4Point1.Models;
using Newtonsoft.Json;

namespace MVC_4Point1.Controllers
{
    // Given:
    // [Route("API/[controller]") and [HttpGet("People/Test")]
    // Our path should be: https://localhost:PORT/API/PersonAPI/People/Test

    // This determines the first segment of the path. 

    [Route("API/[controller]")]

    // This defines the controller as an API controller.
    [ApiController]
    // Our class name (sans 'Controller') is substituted into [controller] in the Route annotation.
    public class PersonAPIController : ControllerBase
    {
        // This determines the second segment of the path.

        [HttpGet("People/All")]


        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            // This is what we are returning. It gets serialized as JSON if we return an object.
            /* PersonController controller = new PersonController();
             return controller.GetPeople();*/
            return new PersonController().GetPeople();
        }

        // This determines the second segment of the path.
        [HttpGet("People/MultiplePhones")]

        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<IEnumerable<Person>> GetPeopleWithMultiplePhones()
        {

            return new PersonController().GetPeopleWithMultiplePhoneNumbers();
        }

        // This determines the second segment of the path.
        [HttpGet("People/StartsWith/{startChar:alpha}")]

        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public List<Person> GetPeopleStartingWith(string startChar)
        {
            using (PersonContext context = new PersonContext())
            {
                return context.People.Where(x => x.FirstName.StartsWith(startChar)).ToList();
            }

        }

        // This determines the second segment of the path.
        [HttpGet("People/ID/{id}")]

        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<object> GetPersonWithID(int id)
        {
            Person person = new PersonController().GetPersonByID(id);

            // This is "kind of" a DTO. We're putting the fields we care about into another object that is not the database model.
            // They help get around errors like the circular references, and (if you use them in the context) the missing virtual properties.
            return new
            {
                id = person.ID,
                firstName = person.FirstName,
                lastName = person.LastName,
                phoneNumbers = person.PhoneNumbers.Select(x => x.Number)
            };

            // This is what we are returning. It gets serialized as JSON if we return an object.

        }

        //Third segment

    }
}