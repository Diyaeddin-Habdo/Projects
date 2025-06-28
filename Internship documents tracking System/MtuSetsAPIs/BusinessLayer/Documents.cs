using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Documents
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public DocumentsDTO DDTO
        {
            get
            {
                return (new DocumentsDTO(this.Id,this.StudentId, this.SGKStajFormu, this.StajBasvuruFormu, 
                    this.StajKabulFormu, this.StajTaahhutnameFormu, this.Status,this.UploadTime));
            }
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public string SGKStajFormu { get; set; }
        public string StajBasvuruFormu { get; set; }
        public string StajKabulFormu { get; set; }
        public string StajTaahhutnameFormu { get; set; }

        public string Status { get; set; }
        public DateTime UploadTime { get; set; }

        public Documents(DocumentsDTO DDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = DDTO.Id;
            this.StudentId = DDTO.StudentId;
            this.SGKStajFormu = DDTO.SGKStajFormu;
            this.StajKabulFormu = DDTO.StajKabulFormu;
            this.StajBasvuruFormu = DDTO.StajBasvuruFormu;
            this.StajTaahhutnameFormu = DDTO.StajTaahhutnameFormu;
            this.Status = DDTO.Status;  
            this.UploadTime = DDTO.UploadTime;

            Mode = cMode;
        }

        /// <summary>
        /// Adds a new Document using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the addition was successful.
        /// Returns true if the Document was added successfully; otherwise, false.
        /// </returns>
        private bool _AddNewDocuments()
        {
            //call DataAccess Layer 

            this.Id = DocumentsData.AddDocuments(DDTO);

            return (this.Id != -1);
        }

        /// <summary>
        /// Updates an existing Document using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the update was successful.
        /// Returns true if the Document was updated successfully; otherwise, false.
        /// </returns>
        private bool _UpdateDocuments()
        {
            return DocumentsData.UpdateDocuments(DDTO);
        }

        /// <summary>
        /// Retrieves a list of all Documents from the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A list of DocumentsDTO objects representing all Documents in the system.
        /// </returns>
        public static List<DocumentsDTO> GetAllDocuments()
        {
            return DocumentsData.GetAllDocuments();
        }

        /// <summary>
        /// Finds a Document by its ID in the Data Access Layer.
        /// </summary>
        /// <param name="ID">The ID of the Document to find.</param>
        /// <returns>
        /// A student object if found; otherwise, null.
        /// </returns>
        public static Documents? Find(int ID)
        {

            DocumentsDTO DDTO = DocumentsData.GetDocumentsById(ID);

            if (DDTO != null)
            //we return new object of that student with the right data
            {

                return new Documents(DDTO, enMode.Update);
            }
            else
                return null;
        }

        /// <summary>
        /// Retrieves a document for a specific student by their ID.
        /// </summary>
        /// <param name="ID">The ID of the student.</param>
        /// <returns>A Documents object containing the student's document information, or null if not found.</returns>
        public static Documents? GetDocumentByStudentId(int ID)
        {

            DocumentsDTO DDTO = DocumentsData.GetDocumentByStudentId(ID);

            if (DDTO != null)
            //we return new object of that student with the right data
            {

                return new Documents(DDTO, enMode.Update);
            }
            else
                return null;
        }

        /// <summary>
        /// Saves the current Documents object to the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the save operation was successful.
        /// Returns true if the Documents was added or updated successfully; otherwise, false.
        /// </returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDocuments())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDocuments();

            }

            return false;
        }

        /// <summary>
        /// Deletes a Documents from the Data Access Layer by their ID.
        /// </summary>
        /// <param name="ID">The ID of the Documents to delete.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// Returns true if the Documents was deleted successfully; otherwise, false.
        /// </returns>
        public static bool DeleteDocuments(int ID)
        {
            return DocumentsData.DeleteDocuments(ID);
        }

        /// <summary>
        /// Retrieves the status of a document by its ID.
        /// </summary>
        /// <param name="id">The ID of the document.</param>
        /// <returns>The status of the document as a string, or an error message if not found.</returns>
        public static string GetDocumentsStatus(int id)
        {
            return DataAccessLayer.DocumentsData.GetDocumentsStatus(id);
        }

        /// <summary>
        /// Retrieves a list of pending documents.
        /// </summary>
        /// <returns>A list of DocumentsDTO representing pending documents.</returns>
        public static List<DocumentsDTO> GetPendingDocuments()
        {
            return DocumentsData.GetPendingDocuments();
        }

        /// <summary>
        /// Updates the status of a document.
        /// </summary>
        /// <param name="documentId">The ID of the document to update.</param>
        /// <param name="StudentId">The ID of the student associated with the document.</param>
        /// <param name="TeacherId">The ID of the teacher associated with the document.</param>
        /// <param name="status">The new status of the document.</param>
        /// <param name="message">An optional message regarding the status update.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public static bool UpdateDocumentStatus(int documentId, int StudentId, int TeacherId, string status, string message)
        {
            return DataAccessLayer.DocumentsData.UpdateDocumentStatus(documentId, StudentId, TeacherId, status, message);
        }
    }
}
