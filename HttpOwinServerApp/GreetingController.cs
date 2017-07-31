using System;
using System.Web.Http;

namespace HttpOwinServerApp
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class Greeting
    {
        public string Message { get; set; }
    }

    [Route("api/greeting")]
    public class GreetingController : ApiController
    {
        public Greeting Post(Person person)
        {
            return new Greeting { Message = $"Hello, {person.Name}." };
        }
    }
}
