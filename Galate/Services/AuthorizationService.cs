using Galate.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Galate.Services
{
    public sealed class AuthorizationService
    {
        public static bool UserIsExist(string login, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM GalateUsers WHERE " +
                    "Login = @Login AND Password = @Password", db.getConnection());
                command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = password;
                adapter.SelectCommand = command;
                adapter.Fill(table);

                return table.Rows.Count > 0;
            }
        }
    }
}
