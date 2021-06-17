using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class ColorRepository : DatabaseConnector, IRepository<Colort>
    {
        public ColorRepository(string connectionString) : base(connectionString) { }

        public List<Color> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Name, Id
                                            FROM Color
                                            WHERE setBackgroundColor=true";

                    List<Color> Colors = new List<Color>();
                }
            }
        }

        public Color Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.id,
                                               c.Name,
                                          FROM Color c";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Color Color = null;
                    while (reader.Read())
                    {
                        if (Color == null)
                        {
                            Color = new Color()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("ColorName")),
                            };
                        }
                    }
                    reader.Close();

                    return Color;
                }
            }
        }
        public Color Update (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Color,
                                               SET Name = @name,
                                               WHERE id = @id"
                     cmd.Parameters.AddwithValue("@id", color.id);
                     cmd.Parameters.AddWithValue("@name", color.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }    
}