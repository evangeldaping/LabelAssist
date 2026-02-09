using System.Data.SQLite;
using System.IO;

namespace LabelAssist
{
    public static class Database
    {
        private static string folder =
            @"C:\Users\Public\LabelAssist";

        private static string dbPath =
            Path.Combine(folder, "labels.db");

        private static string connection =
            $"Data Source={dbPath};Version=3;";

        // Returns a ready-to-use connection
        public static SQLiteConnection GetConnection()
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (!File.Exists(dbPath))
                CreateDatabase();

            return new SQLiteConnection(connection);
        }

        // Create database and table on first run
        private static void CreateDatabase()
        {
            SQLiteConnection.CreateFile(dbPath);

            using var conn = new SQLiteConnection(connection);
            conn.Open();

            string sql =
            @"CREATE TABLE Labels (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            );";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
