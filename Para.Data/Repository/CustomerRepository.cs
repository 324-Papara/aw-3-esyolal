using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Para.Data.Domain;

namespace Para.Data.DapperRepository
{
    public class CustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("MsSqlConnection"));
        }

        public async Task<Customer?> GetCustomerWithDetailsAsync(long customerId)
        {
            const string query = @"
                --Customer
                SELECT * 
                FROM Customer 
                WHERE Id = @CustomerId;
                
               --Details
                SELECT *
                FROM CustomerDetail 
                WHERE CustomerId = @CustomerId;

                --Addresses
                SELECT *
                FROM CustomerAddress 
                WHERE CustomerId = @CustomerId;

                --Phones
                SELECT * 
                FROM CustomerPhone 
                WHERE CustomerId = @CustomerId;
            ";

            try
            {
                using (var connection = CreateConnection())
                {
                    using (var multi = await connection.QueryMultipleAsync(query, new { CustomerId = customerId }))
                    {
                        var customer = await multi.ReadFirstOrDefaultAsync<Customer>();
                        if (customer != null)
                        {
                            customer.CustomerDetail = (await multi.ReadAsync<CustomerDetail>()).FirstOrDefault();
                            customer.CustomerAddresses = (await multi.ReadAsync<CustomerAddress>()).ToList();
                            customer.CustomerPhones = (await multi.ReadAsync<CustomerPhone>()).ToList();
                        }
                        return customer;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving data.", ex);
            }
        }
    }
}
