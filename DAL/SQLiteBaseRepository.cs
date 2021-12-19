using Dapper;
using Microsoft.Data.Sqlite;
using System.IO;

namespace Dev_OpsWPF.DAL
{
    class SQLiteBaseRepository
    {
        public SQLiteBaseRepository()
        {
        }
         
        public static SqliteConnection DbConnectionFactory()
        {
            return new SqliteConnection(@"DataSource=CharactersDB.sqlite");
        }
        protected static bool DatabaseExists()
        {
            return File.Exists(@"CharactersDB.sqlite");
        }

        protected static void CreateDatabase()
        {
            using (var connection = DbConnectionFactory())
            {
                connection.Open();
                connection.Execute(
                    @"CREATE TABLE Character
                    (
                    Id                                  INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name                                VARCHAR(100)
                    Species                             VARCHAR(50)
                    )");
            }
        }
    }
}
