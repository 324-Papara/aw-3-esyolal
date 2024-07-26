using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public CustomerReportController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpGet("customer-report")]
        public async Task<IActionResult> GetCustomerReport()
        {
            string query = @"
                SELECT 
                    c.Id, c.Name, c.Email,
                    cd.DetailId, cd.DetailInfo,
                    ca.AddressId, ca.AddressLine
                FROM Customers c
                LEFT JOIN CustomerDetails cd ON c.Id = cd.CustomerId
                LEFT JOIN CustomerAddresses ca ON c.Id = ca.CustomerId
                WHERE c.IsActive = 1";

            var result = await _dbConnection.QueryAsync<CustomerReportDto>(query);
            return Ok(result);
        }
    }

    public class CustomerReportDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long DetailId { get; set; }
        public string DetailInfo { get; set; }
        public long AddressId { get; set; }
        public string AddressLine { get; set; }
    }
}
