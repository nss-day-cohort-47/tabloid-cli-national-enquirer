using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class ColorRepository : DatabaseConnector, IRepository<Color>
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
                    cmd.CommandText = @"SELECT c.ColorChoice
                                               
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
                                ColorChoice = reader.GetInt32(reader.GetOrdinal("ColorChoice")),
                            };
                        }
                    }
                    reader.Close();

                    return Color;
                }
            }
        }
        public void Update(Color ColorChoice)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Color,
                                               SET ColorChoice = @choice
                                               WHERE id = @id";
                     cmd.Parameters.AddwithValue("@id", color.id);
                     cmd.Parameters.AddWithValue("@choice", color.ColorChoice);
                     

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }    
}