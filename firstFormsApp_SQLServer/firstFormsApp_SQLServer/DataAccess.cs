using Dapper;

namespace firstFormsApp_SQLServer;

public class DataAccess
{
    public List<Person> GetPeople(string lastName)
    {
        using (var connection = new Microsoft.Data.SqlClient.SqlConnection(Helpers.CnnHelper("demoDB")))
        {
            var output = connection.Query<Person>($"SELECT * FROM UsersTable WHERE LastName = '{lastName}'").ToList();
            return output;
        }
    }
}

