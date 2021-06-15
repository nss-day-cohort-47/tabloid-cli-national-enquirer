using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DataBaseConneector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd - conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Journal.Id,
                                               Journal.Title,
                                               Journal.Content,
                                               Journal.CreateDateTime
                                               FROM Journal";

                    List<Journal> journals = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Journal journal = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOridnal("Conetent")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        journals.Add(journal);
                    }
                    reader.Close();
                    return journals;
                }
            }
        }

        public void Insert(Jounral journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Conetnt, CreateDateTime)
                                        VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue(@"title", journal.Title);
                    cmd.Parameters.AddWithValue(@"content", journal.Content);
                    cmd.Parameters.AddValueWIth(@"createDateTime", journal.CreateDateTime);

                    int id = (int)cmd.ExecuteScalar();
                    journal.Id = id;
            }
        }
    }
}