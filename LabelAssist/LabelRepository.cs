using System.Collections.Generic;
using System.Data.SQLite;

namespace LabelAssist
{
    public static class LabelRepository
    {
        // Read all labels from the database
        public static List<string> GetAll()
        {
            var labels = new List<string>();

            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "SELECT Name FROM Labels ORDER BY Name", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                labels.Add(reader.GetString(0));
            }

            return labels;
        }

        // Add a new label
        public static void Add(string name)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "INSERT INTO Labels(Name) VALUES(@name)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
        }

        // Update an existing label
        public static void Update(string oldName, string newName)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "UPDATE Labels SET Name=@newName WHERE Name=@oldName", conn);
            cmd.Parameters.AddWithValue("@newName", newName);
            cmd.Parameters.AddWithValue("@oldName", oldName);
            cmd.ExecuteNonQuery();
        }

        // Delete a label
        public static void Delete(string name)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "DELETE FROM Labels WHERE Name=@name", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
        }
    }
}
