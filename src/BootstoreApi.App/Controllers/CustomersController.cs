using System.Data.SqlClient;
using BookstoreApi.Core.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.App.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Customer> GetAsync([FromRoute(Name = "id")]int id)
        {
            if (id <= 0)
            {
                return this.BadRequest();
            }

            var sql = "SELECT * FROM Customers WHERE CustomerId = @Id";

            using var connection = new SqlConnection(@"Server=DESKTOP-C5T4GM0\SQLEXPRESS;Database=BOOKSTORE;Trusted_Connection=True;");

            var customer = connection.QuerySingleOrDefault<Customer>(sql, new
            {
                Id = id
            });

            if (customer == null)
            {
                return this.NotFound();
            }

            return customer;
        }
    }
}
