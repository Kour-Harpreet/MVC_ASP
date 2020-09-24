using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_4Point1.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace MVC_4Point1.Controllers
{
    // In-Class Practice: 
    // Add a PersonController method to get all people whose name starts with J.
    // Add an API endpoint that will return the results of that method to "People/NamesStartingJ".

    // Challenge:
    // Modify the PersonController method to accept a character.
    // Make the API endpoint respond to all possible starting characters at "People/StartsWith/{char}"
    // Add and API endpoint at "People/ID/{id}" that will return only the person with that ID.
    // In-Class Practice Part 2:
    // Add a second endpoint for the "name starts with" that uses the query string and not the URL.


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

        //1.
        [HttpGet("People/All")]
        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            // This is what we are returning. It gets serialized as JSON if we return an object.
            return new PersonController().GetPeople();
        }
        // This determines the second segment of the path.

        //2.
        
        [HttpGet("People/MultiplePhones")]
        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<IEnumerable<Person>> GetPeopleWithMultiplePhones()
        {
            return new PersonController().GetPeopleWithMultiplePhoneNumbers();
        }
        // This determines the second segment of the path.

        //3.
        // Make the API endpoint respond to all possible starting characters at "People/StartsWith/{char}"
        [HttpGet("People/StartsWith/{startChar:alpha}")]
        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<IEnumerable<Person>> GetPeopleWhoseNamesStartWith(string startChar)
        {
            // Assuming we aren't using the controller again, we might as well just instantiate it where we need it the one time.
            return new PersonController().GetPeopleStartingWith(startChar);
        }





        //4.
        // This method of GET parameters will retrieve the argument from the URL.
        [HttpGet("People/ID/{id}")]
        // This is the return type of the request. The method name is irrelevant as far as the clients are concerned.
        public ActionResult<object> GetPersonWithID(int id)
        {
            // This is what we are returning. It gets serialized as JSON if we return an object.
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
        }



        //5.
        // This method of GET parameters will retrieve the argument from the query string (after the ? in the URL).
        [HttpGet("People/ID")]
        public ActionResult<object> GetPersonWithIDParameterized(int id)
        {
            // This is what we are returning. It gets serialized as JSON if we return an object.
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
        }

        //6.
        // Add a second endpoint for the "name starts with" that uses the query string and not the URL.
        [HttpGet("People/StartsWith")]
        public ActionResult<object> GetPeopleWhoseNamesStartWithParameterized(string startChar)
        {
            // This is what we are returning. It gets serialized as JSON if we return an object.
         return new PersonController().GetPeopleStartingWith(startChar);

           
        }
    }
}
