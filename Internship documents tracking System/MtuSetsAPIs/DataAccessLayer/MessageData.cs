using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime Time { get; set; }

        public MessageDTO(int id, string Message, int fromId,int toId,DateTime time)
        {
            this.Id = id;
            this.Message = Message;
            this.FromId = fromId;
            this.ToId = toId;
            this.Time = time;
        }
    }
    public class MessageData
    {
        public static List<MessageDTO> GetAllMessages()
        {
            var MessagelarList = new List<MessageDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllMessages", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MessagelarList.Add(new MessageDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Message")),
                                    reader.GetInt32(reader.GetOrdinal("FromId")),
                                    reader.GetInt32(reader.GetOrdinal("ToId")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
                                ));
                            }
                        }
                    }
                    catch { }
                }


                return MessagelarList;
            }

        }

        public static MessageDTO GetMessageById(int MessageId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetMessageById", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MessageId", MessageId);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MessageDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Message")),
                                    reader.GetInt32(reader.GetOrdinal("FromId")),
                                    reader.GetInt32(reader.GetOrdinal("ToId")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
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
        /// Retrieves a list of messages sent to a specific user based on the recipient's ID.
        /// </summary>
        /// <param name="toId">The ID of the recipient whose messages are to be retrieved.</param>
        /// <returns>
        /// A list of <see cref="MessageDTO"/> objects containing the messages sent to the specified user.
        /// </returns>
        public static List<MessageDTO> GetMessagesByToId(int toId)
        {
            var MessagelarList = new List<MessageDTO>();
            using (var conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var cmd = new SqlCommand("SP_GetMessageByToId", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ToId",toId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MessagelarList.Add(new MessageDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Message")),
                                    reader.GetInt32(reader.GetOrdinal("FromId")),
                                    reader.GetInt32(reader.GetOrdinal("ToId")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetMessagesByToId method.");
                        throw new DataAccessException("An error occurred while retrieving messages.", ex);
                    }
                }
                return MessagelarList;
            }
        }

        /// <summary>
        /// Retrieves a list of messages sent by a specific user based on the sender's ID.
        /// </summary>
        /// <param name="fromId">The ID of the sender whose messages are to be retrieved.</param>
        /// <returns>
        /// A list of <see cref="MessageDTO"/> objects containing the messages sent by the specified user.
        /// </returns>
        public static List<MessageDTO> GetMessagesByFromId(int fromId)
        {
            var MessagelarList = new List<MessageDTO>();
            using (var conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var cmd = new SqlCommand("SP_GetMessageByFromId", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FromId",fromId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MessagelarList.Add(new MessageDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Message")),
                                    reader.GetInt32(reader.GetOrdinal("FromId")),
                                    reader.GetInt32(reader.GetOrdinal("ToId")),
                                    reader.GetDateTime(reader.GetOrdinal("Time"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetMessagesByFromId method.");
                        throw new DataAccessException("An error occurred while retrieving messages.", ex);
                    }
                }
                return MessagelarList;
            }
        }

        public static int AddMessage(MessageDTO MessageDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddMessage", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Message", MessageDTO.Message);
                        command.Parameters.AddWithValue("@FromId", MessageDTO.FromId);
                        command.Parameters.AddWithValue("@ToId", MessageDTO.ToId);
                        command.Parameters.AddWithValue("@Time", MessageDTO.Time);

                        var outputIdParam = new SqlParameter("@NewMessageId", SqlDbType.Int)
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

        public static bool UpdateMessage(MessageDTO MessageDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateMessage", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", MessageDTO.Id);
                        command.Parameters.AddWithValue("@Message", MessageDTO.Message);
                        command.Parameters.AddWithValue("@FromId", MessageDTO.FromId);
                        command.Parameters.AddWithValue("@ToId", MessageDTO.ToId);
                        command.Parameters.AddWithValue("@Time", MessageDTO.Time);

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

        public static bool DeleteMessage(int MessageId)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DeleteMessage", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MessageId", MessageId);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected == 1);
                    }
                    catch { }
                }
            }
            return false;
        }

    }
}
