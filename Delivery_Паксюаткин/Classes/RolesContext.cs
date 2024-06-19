
using Delivery_Паксюаткин.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Паксюаткин.Classes
{
    public class RolesContext : Roles
    {
        /// <summary>
        /// Метод класса Роли(Roles)
        /// </summary>
        public RolesContext(int Id, string IdRole) : base(Id, IdRole) { }
        /// <summary>
        /// Метод вывода  Роли(Roles)
        /// </summary>
        public static List<RolesContext> Select()
        {
            List<RolesContext> AllRoles = new List<RolesContext>();
            string SQL = "SELECT * FROM `role`";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                AllRoles.Add(new RolesContext(
                    Data.GetInt32(0),
                    Data.GetString(1)
               ));
            }
            Connection.CloseConnection(connection);
            return AllRoles;
        }
    }
}
