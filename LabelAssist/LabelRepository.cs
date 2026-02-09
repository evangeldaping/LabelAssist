using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace LabelAssist
{
    public static class LabelRepository
    {
        public static List<string> GetAll()
        {
            var labels = new List<string>();

            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Labels ORDER BY Name";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                labels.Add(reader.GetString(0));
            }

            return labels;
        }

        public static void Add(string name)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Labels(Name) VALUES ($name)";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.ExecuteNonQuery();
        }

        public static void Update(string oldName, string newName)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "UPDATE Labels SET Name=$newName WHERE Name=$oldName";
            cmd.Parameters.AddWithValue("$newName", newName);
            cmd.Parameters.AddWithValue("$oldName", oldName);
            cmd.ExecuteNonQuery();
        }

        public static void Delete(string name)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Labels WHERE Name=$name";
            cmd.Parameters.AddWithValue("$name", name);
            cmd.ExecuteNonQuery();
        }
    }
}
