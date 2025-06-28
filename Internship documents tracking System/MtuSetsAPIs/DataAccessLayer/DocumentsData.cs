using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DocumentsDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string SGKStajFormu { get; set; }
        public string StajBasvuruFormu { get; set; }
        public string StajKabulFormu { get; set; }
        public string StajTaahhutnameFormu { get; set; }

        public string Status { get; set; }
        public DateTime UploadTime { get; set; }



        public DocumentsDTO(int id,int studentId,string SGKStajFormu,string StajBasvuruFormu,string StajKabulFormu
            ,string StajTaahhutnameFormu,string status, DateTime uploadTime)
        {
            this.Id = id;
            this.StudentId = studentId;
            this.SGKStajFormu = SGKStajFormu;
            this.StajBasvuruFormu = StajBasvuruFormu;
            this.StajKabulFormu = StajKabulFormu;
            this.StajTaahhutnameFormu = StajTaahhutnameFormu;
            this.Status = status;
            this.UploadTime = uploadTime;
        }
    }
    public class DocumentsData
    {
        static string _connectionString = "Server=localhost;Database=MtuSETS;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        /// <summary>
        /// Retrieves all documents from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="DocumentsDTO"/> objects containing details of each document.
        /// </returns>
        public static List<DocumentsDTO> GetAllDocuments()
        {
            var StajEvraklarilarList = new List<DocumentsDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllDocuments", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StajEvraklarilarList.Add(new DocumentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("StudentId")),
                                    reader.GetString(reader.GetOrdinal("SGKStajFormu")),
                                    reader.GetString(reader.GetOrdinal("StajBasvuruFormu")),
                                    reader.GetString(reader.GetOrdinal("StajKabulFormu")),
                                    reader.GetString(reader.GetOrdinal("StajTaahhutnameFormu")),
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetDateTime(reader.GetOrdinal("UploadTime"))
                                ));
                            }
                        }
                    }
                    catch { }
                }


                return StajEvraklarilarList;
            }

        }

        /// <summary>
        /// Retrieves a document by its ID from the database.
        /// </summary>
        /// <param name="DocumentsId">The ID of the document to retrieve.</param>
        /// <returns>
        /// A <see cref="DocumentsDTO"/> object containing the document's details,
        /// or null if no document is found with the specified ID.
        /// </returns>
        public static DocumentsDTO GetDocumentsById(int DocumentsId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetDocumentsById", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DocumentsId", DocumentsId);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new DocumentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("StudentId")),
                                    reader.GetString(reader.GetOrdinal("SGKStajFormu")),
                                    reader.GetString(reader.GetOrdinal("StajBasvuruFormu")),
                                    reader.GetString(reader.GetOrdinal("StajKabulFormu")),
                                    reader.GetString(reader.GetOrdinal("StajTaahhutnameFormu")),
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetDateTime(reader.GetOrdinal("UploadTime"))

                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch { }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves a document associated with a specific student from the database.
        /// </summary>
        /// <param name="StudentId">The ID of the student whose document is to be retrieved.</param>
        /// <returns>
        /// A <see cref="DocumentsDTO"/> object containing the document's details,
        /// or null if no document is found for the specified student ID.
        /// </returns>
        public static DocumentsDTO GetDocumentByStudentId(int StudentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetDocumentByStudentId", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", StudentId);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new DocumentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("StudentId")),
                                    reader.GetString(reader.GetOrdinal("SGKStajFormu")),
                                    reader.GetString(reader.GetOrdinal("StajBasvuruFormu")),
                                    reader.GetString(reader.GetOrdinal("StajKabulFormu")),
                                    reader.GetString(reader.GetOrdinal("StajTaahhutnameFormu")),
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetDateTime(reader.GetOrdinal("UploadTime"))

                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch { }
                }
            }
            return null;
        }


        /// <summary>
        /// Adds a new document record to the database.
        /// </summary>
        /// <param name="DocumentsDTO">The document data transfer object containing the details of the document to be added.</param>
        /// <returns>
        /// The ID of the newly added document, or -1 if the operation fails.
        /// </returns>
        public static int AddDocuments(DocumentsDTO DocumentsDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_AddDocuments", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@StudentId", DocumentsDTO.StudentId);
                        command.Parameters.AddWithValue("@SGKStajFormu", DocumentsDTO.SGKStajFormu);
                        command.Parameters.AddWithValue("@StajBasvuruFormu", DocumentsDTO.StajBasvuruFormu);
                        command.Parameters.AddWithValue("@StajKabulFormu", DocumentsDTO.StajKabulFormu);
                        command.Parameters.AddWithValue("@StajTaahhutnameFormu", DocumentsDTO.StajTaahhutnameFormu);
                        command.Parameters.AddWithValue("@Status", DocumentsDTO.Status);
                        command.Parameters.AddWithValue("@UploadTime", DocumentsDTO.UploadTime);

                        var outputIdParam = new SqlParameter("@addedId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return (int)outputIdParam.Value;
                    }
                    catch { }
                }
            }
            return -1;
        }

        /// <summary>
        /// Updates an existing document record in the database.
        /// </summary>
        /// <param name="DocumentsDTO">The document data transfer object containing the updated details of the document.</param>
        /// <returns>
        /// True if the update is successful; otherwise, false.
        /// </returns>
        public static bool UpdateDocuments(DocumentsDTO DocumentsDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateDocuments", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", DocumentsDTO.Id);
                        command.Parameters.AddWithValue("@StudentId", DocumentsDTO.StudentId);
                        command.Parameters.AddWithValue("@SGKStajFormu", DocumentsDTO.SGKStajFormu);
                        command.Parameters.AddWithValue("@StajBasvuruFormu", DocumentsDTO.StajBasvuruFormu);
                        command.Parameters.AddWithValue("@StajKabulFormu", DocumentsDTO.StajKabulFormu);
                        command.Parameters.AddWithValue("@StajTaahhutnameFormu", DocumentsDTO.StajTaahhutnameFormu);
                        command.Parameters.AddWithValue("@Status", DocumentsDTO.Status);
                        command.Parameters.AddWithValue("@UploadTime", DocumentsDTO.UploadTime);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        /// <summary>
        /// Updates the status of a document in the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to update.</param>
        /// <param name="StudentId">The ID of the student associated with the document.</param>
        /// <param name="TeacherId">The ID of the teacher associated with the document.</param>
        /// <param name="status">The new status of the document.</param>
        /// <param name="message">An optional message regarding the status update.</param>
        /// <returns>
        /// True if the status update is successful; otherwise, false.
        /// </returns>
        public static bool UpdateDocumentStatus(int documentId, int StudentId, int TeacherId, string status, string message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateDocumentsStatus", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DocumentId", documentId);
                        command.Parameters.AddWithValue("@StudentId", StudentId);
                        command.Parameters.AddWithValue("@TeacherId", TeacherId);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@Message", message);
                        command.Parameters.AddWithValue("@UploadTime", DateTime.Now);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        /// <summary>
        /// Deletes a document from the database by its ID.
        /// </summary>
        /// <param name="DocumentsId">The ID of the document to delete.</param>
        /// <returns>
        /// True if the document was successfully deleted; otherwise, false.
        /// </returns>
        public static bool DeleteDocuments(int DocumentsId)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteDocuments", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DocumentsId", DocumentsId);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected == 1);
                    }
                    catch { }
                }
            }
            return false;
        }

        /// <summary>
        /// Retrieves the status of documents for a specific student.
        /// </summary>
        /// <param name="StudentId">The ID of the student whose document status is to be fetched.</param>
        /// <returns>
        /// The status of the documents as a string. Returns a message if no documents are uploaded,
        /// or an error message if an exception occurs.
        /// </returns>
        public static string GetDocumentsStatus(int StudentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetDocumentsStatus", connection)) // Use correct procedure name
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", StudentId);

                        connection.Open();

                        var status = command.ExecuteScalar(); // Fetch single value

                        if (status != null)
                        {
                            return status.ToString();
                        }
                        else
                        {
                            return "Belgeler yuklenmedi";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error (ex) if needed
                        return $"Error: {ex.Message}";
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a list of pending documents from the database.
        /// </summary>
        /// <returns>A list of DocumentsDTO representing the pending documents.</returns>
        public static List<DocumentsDTO> GetPendingDocuments()
        {
            var StajEvraklarilarList = new List<DocumentsDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPendingDocuments", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StajEvraklarilarList.Add(new DocumentsDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("StudentId")),
                                    reader.GetString(reader.GetOrdinal("SGKStajFormu")),
                                    reader.GetString(reader.GetOrdinal("StajBasvuruFormu")),
                                    reader.GetString(reader.GetOrdinal("StajKabulFormu")),
                                    reader.GetString(reader.GetOrdinal("StajTaahhutnameFormu")),
                                    reader.GetString(reader.GetOrdinal("Status")),
                                    reader.GetDateTime(reader.GetOrdinal("UploadTime"))
                                ));
                            }
                        }
                    }
                    catch { }
                }


                return StajEvraklarilarList;
            }

        }
    }
}
