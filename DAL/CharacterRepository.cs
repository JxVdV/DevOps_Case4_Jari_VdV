using Dev_OpsWPF.Model;
using Dapper;
using System.Collections.Generic;

namespace Dev_OpsWPF.DAL
{
    class CharacterRepository : SQLiteBaseRepository
    {
        public CharacterRepository()
        {
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
        }
        public int InsertCharacter(Character character)
        {
            string sql = "INSERT INTO Character (Name) Values (@Name);";

            using (var connection = DbConnectionFactory())
            {
                connection.Open();
                return connection.Execute(sql, character);
            }
        }

        public int DeleteCharacter(string name)
        {
            string sql = "DELETE FROM Character WHERE Name = @Name;";

            using (var connection = DbConnectionFactory())
            {
                connection.Open();
                return connection.Execute(sql, new { Name = name });
            }
        }

        public IEnumerable<Character> GetCharacters()
        {
            string sql = "SELECT * FROM Character;";

            using (var connection = DbConnectionFactory())
            {
                return connection.Query<Character>(sql);
            }
        }

    }
}
