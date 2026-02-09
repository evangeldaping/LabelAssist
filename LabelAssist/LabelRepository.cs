using System.Collections.Generic;
using System.Data.SQLite;

namespace LabelAssist
{
    public static class LabelRepository
    {
        // Read all labels
        public static List<string> GetAll()
        {
            var labels = new List<string>();

            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "SELECT Name FROM Labels ORDER BY Name", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                labels.Add(reader.GetString(0));

            return labels;
        }

        // Add new label
        public static void Add(string name)
        {
            using var conn = Database.GetConnection();
            conn.Open();

            var cmd = new SQLiteCommand(
                "INSERT INTO Labels(Name) VALUES(@name)", conn);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
        }

        // Delete label
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
