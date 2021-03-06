using SqlInjection.IServices;
using SqlInjection.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SqlInjection.DbServices
{
    public class DbEmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection connection;

        public DbEmployeeRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public Employee Get(int id)
        {
            string sql = "SELECT [EmployeeID], FirstName, LastName FROM [dbo].[Employees] WHERE EmployeeID = @EmployeeId";

            Employee employee = null;

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmployeeId", id);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = Map(reader);
            }

            connection.Close();

            return employee;
        }

        
        private static Employee Map(SqlDataReader reader)
        {
            Employee employee = new Employee();
            employee.Id = reader.GetInt32(0);
            employee.FirstName = reader.GetString(1);
            employee.LastName = reader.GetString(2);            

            return employee;
        }

        public Employee Validate(string username, string password)
        {
            // string sql = "SELECT [EmployeeID], FirstName, LastName FROM [dbo].[Employees] WHERE [UserName] = '" + username + "'" + " AND [Password] = '" + password + "'";

            string sql = "SELECT [EmployeeID], FirstName, LastName FROM [dbo].[Employees] WHERE [UserName] = @username AND [Password] = @password";


            Employee employee = null;

            SqlParameter userNameParameter = new SqlParameter("@username", username);            
            SqlParameter passwordParameter = new SqlParameter("@password", password);

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add(userNameParameter);
            command.Parameters.Add(passwordParameter);
            
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                employee = Map(reader);
            }

            connection.Close();

            return employee;
        }

        public IEnumerable<Employee> Get(string searchString = "")
        {
            // zła praktyka
           // string sql = "SELECT [EmployeeID], FirstName, LastName FROM [dbo].[Employees] WHERE FirstName = '" + searchString + "'";

            string sql = "SELECT [EmployeeID], FirstName, LastName FROM [dbo].[Employees] WHERE FirstName = @searchString";

            IList<Employee> employees = new List<Employee>();

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@searchString", searchString ?? string.Empty);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(Map(reader));
            }

            connection.Close();

            return employees;
        }
    }
}
