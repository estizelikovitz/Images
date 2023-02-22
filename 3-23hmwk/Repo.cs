using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_23hmwk
{
    public class Repo
    {
        private string _connectionString;
        public Repo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(string password, string imageSource)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO UploadedImages (Password, ImagePath) VALUES (@password, @path) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@path", imageSource);
            conn.Open();
            return (int)(decimal)cmd.ExecuteScalar();
        }

        public List<Image> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM  UploadedImages";
            conn.Open();
            List<Image> result = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Image
                {
                    Id = (int)reader["Id"],
                    Password = (string)reader["Password"],
                    ImageSource = (string)reader["ImagePath"]
                }) ;
            }

            return result;
        }

    }
    public class Image
    {
        public int Id { get; set; }
        public int? Views { get; set; }
        public string Password { get; set; }
        public string ImageSource { get; set; }
    }
}
