using AirLiquide.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AirLiquide.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Insert(Customer c)
        {

            return await _connection.ExecuteAsync("insert into customer(Id, Name, Age) values( @id, @name, @age)", new { c.Id, c.Name, c.Age });
        }

        public async Task<int> Update(Customer c)
        {
            return await _connection.ExecuteAsync("update customer set Name = @name, Age=@age where Id=@id", new { c.Name, c.Age, c.Id });
        }

        public async Task<int> Delete(Guid customerId)
        {
            return await _connection.ExecuteAsync("delete from customer where Id=@customerId", new { customerId });
        }
        
        public async Task<Customer> Get(Guid customerId)
        {
            return await _connection.QueryFirstOrDefaultAsync<Customer>("select id, name, age from customer where id = @customerId", new { customerId });
        }

        public async Task<IEnumerable<Customer>> List()
        {
            return await _connection.QueryAsync<Customer>("select id, name, age from customer");
        }
    }

    public class CancelationToken
    {
    }

    public interface ICustomerRepository
    {
        Task<int> Insert(Customer c);
        Task<int> Update(Customer c);
        Task<int> Delete(Guid customerId);
        Task<IEnumerable<Customer>> List();
        Task<Customer> Get(Guid customerId);
    }
}
