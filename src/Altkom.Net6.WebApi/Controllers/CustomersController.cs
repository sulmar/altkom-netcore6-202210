using Altkom.Net6.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Altkom.Net6.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository repository;

        // snippet: ctor x 2 Tab
        public CustomersController(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        // GET api/customers

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            var customers = repository.Get();

            return Ok(customers);
        }

        // GET api/customers/{id}

        // [HttpGet("api/customers/{id:int}")]
        //public Customer Get(int id)
        //{
        //    var customer = repository.Get(id);  

        //    return customer;
        //}


        // GET api/customers/{id}
        [HttpGet("{id:int}", Name = "GetCustomerById")]   // Route Params
        public ActionResult<Customer> Get(int id)
        {
            var customer = repository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customer);
            }

            if (customer == null)
                return new NotFoundResult();
            else
                return new OkObjectResult(customer);

        }

        // GET api/customers?email={email}&email={email}&email={email} // Query String
        [HttpGet("email")]
        public ActionResult<Customer> GetByEmail([FromQuery] string[] email)
        {
            throw new NotImplementedException();

            //var customer = repository.GetByEmail(email);

            //if (customer == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    return Ok(customer);
            //}
        }

        [HttpPost]
        public ActionResult<Customer> Post(Customer customer, [FromServices] IValidator<Customer> validator)   // [FromBody] na podst. konwencji dzięki [ApiController]
        {
            var validationResults = validator.Validate(customer);

            if (!validationResults.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResults.ToDictionary()));
            }

            repository.Add(customer);

            return CreatedAtRoute("GetCustomerById", new { customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Customer customer)   // [FromBody] na podst. konwencji dzięki [ApiController]
        {
            if (id != customer.Id)
                return BadRequest();

            repository.Update(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            repository.Remove(id);

            return Ok();
        }

       

    }
}


