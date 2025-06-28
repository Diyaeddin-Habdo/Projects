using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Message
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public MessageDTO MDTO
        {
            get
            {
                return (new MessageDTO(this.Id, this.MessageContent, this.FromId, this.ToId, this.Time));
            }
        }

        public int Id { get; set; }
        public string MessageContent { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime Time { get; set; }


        public Message(MessageDTO MDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = MDTO.Id;
            this.MessageContent = MDTO.Message;
            this.FromId = MDTO.FromId;
            this.ToId = MDTO.ToId;
            this.Time = MDTO.Time;

            Mode = cMode;
        }

        /// <summary>
        /// Adds a new message using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the addition was successful.
        /// Returns true if the message was added successfully; otherwise, false.
        /// </returns>
        private bool _AddNewMesaj()
        {
            try {
                //call DataAccess Layer 

                this.Id = MessageData.AddMessage(MDTO);

                return (this.Id != -1);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing message using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the update was successful.
        /// Returns true if the message was updated successfully; otherwise, false.
        /// </returns>
        private bool _UpdateMesaj()
        {
            try {
                return MessageData.UpdateMessage(MDTO);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of all messages from the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A list of MessageDTO objects representing all messages in the system.
        /// </returns>
        public static List<MessageDTO> GetAllMessages()
        {
            try {
                return MessageData.GetAllMessages();
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Finds a Message by its ID in the Data Access Layer.
        /// </summary>
        /// <param name="ID">The ID of the Message to find.</param>
        /// <returns>
        /// A Message object if found; otherwise, null.
        /// </returns>
        public static Message? Find(int ID)
        {
            try {
                var HDTO = MessageData.GetMessageById(ID);

                if (HDTO != null)
                //we return new object of that student with the right data
                {

                    return new Message(HDTO, enMode.Update);
                }
                else
                    return null;
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of messages sent to a specific user.
        /// </summary>
        /// <param name="ID">The ID of the recipient.</param>
        /// <returns>A list of messages sent to the user, or null if an error occurs.</returns>
        public static List<MessageDTO> GetMessagesByToId(int ID)
        {
            try
            {
                return MessageData.GetMessagesByToId(ID);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of messages sent from a specific user.
        /// </summary>
        /// <param name="ID">The ID of the sender.</param>
        /// <returns>A list of messages sent from the user, or null if an error occurs.</returns>
        public static List<MessageDTO> GetMessageByFromId(int ID)
        {
            try {
                return MessageData.GetMessagesByFromId(ID);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Saves the current Message object to the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the save operation was successful.
        /// Returns true if the Message was added or updated successfully; otherwise, false.
        /// </returns>
        public bool Save()
        {
            try
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewMesaj())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return _UpdateMesaj();

                }
            }
            catch {
                throw;
            }

            return false;
        }

        /// <summary>
        /// Deletes a Message from the Data Access Layer by its ID.
        /// </summary>
        /// <param name="ID">The ID of the Message to delete.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// Returns true if the Message was deleted successfully; otherwise, false.
        /// </returns>
        /// 
        public static bool DeleteMessage(int ID)
        {
            try
            {
                return MessageData.DeleteMessage(ID);
            }
            catch {
                throw;
            }
        }
    }
}
