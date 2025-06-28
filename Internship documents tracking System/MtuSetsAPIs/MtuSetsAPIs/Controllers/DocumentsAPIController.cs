using CloudinaryDotNet.Core;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MtuSetsAPIs.Global;
using MtuSetsAPIs.Models.Documents;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Documents")]
    [ApiController]
    public class DocumentsAPIController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllDocuments")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DocumentsDTO>> GetAllDocuments()
        {
            List<DocumentsDTO> DocumentsList = BusinessLayer.Documents.GetAllDocuments();
            if (DocumentsList.Count == 0)
            {
                return NotFound("No document Found!");
            }
            return Ok(DocumentsList);
        }


        [HttpGet("{id}", Name = "GetDocumentById")]
        [Authorize(Roles = "3953,9763,1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentsDTO> GetDocumentById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }


            BusinessLayer.Documents document = BusinessLayer.Documents.Find(id);

            if (document == null)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            //here we get only the DTO object to send it back.
            DocumentsDTO DDTO = document.DDTO;

            //we return the DTO not the student object.
            return Ok(DDTO);

        }



        [HttpGet("Student/{id}", Name = "GetDocumentByStudentId")]
        [Authorize(Roles = "3953,9763,1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DocumentsDTO> GetDocumentByStudentId(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }


            BusinessLayer.Documents document = BusinessLayer.Documents.GetDocumentByStudentId(id);

            if (document == null)
            {
                return NotFound($"Document with student ID {id} not found.");
            }

            //here we get only the DTO object to send it back.
            DocumentsDTO DDTO = document.DDTO;

            //we return the DTO not the student object.
            return Ok(DDTO);

        }


        [HttpPost(Name = "AddDocument")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DataAccessLayer.DocumentsDTO>> AddDocument(dtoAdd newDocumentDTO)
        {
            //we validate the data here
            if (newDocumentDTO == null || newDocumentDTO.SGKStajFormu == null
                || newDocumentDTO.StajBasvuruFormu == null || newDocumentDTO.StajKabulFormu == null 
                || newDocumentDTO.StajTaahhutnameFormu == null || newDocumentDTO.StudentId < 1)
            {
                return BadRequest("Invalid Document data.");
            }

            List<IFormFile> files = new List<IFormFile>();
            files.Add(newDocumentDTO.SGKStajFormu); 
            files.Add(newDocumentDTO.StajBasvuruFormu); 
            files.Add(newDocumentDTO.StajKabulFormu); 
            files.Add(newDocumentDTO.StajTaahhutnameFormu);

            var pdfsLinks = await CloudinaryService.UploadPdfsAsync(files);

            BusinessLayer.Documents Document = new BusinessLayer.Documents(new DocumentsDTO(newDocumentDTO.Id, 
                newDocumentDTO.StudentId, pdfsLinks[0], pdfsLinks[1],pdfsLinks[2], pdfsLinks[3],
                "Beklemede",DateTime.Now));

            Document.Save();

            dtoAdded d = new dtoAdded(Document);


            //we return the DTO only not the full student object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("GetDocumentById", new { id = newDocumentDTO.Id }, d);
        }


        [HttpPut("{id}", Name = "UpdateDocument")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DataAccessLayer.DocumentsDTO>> UpdateDocument(int id, dtoAdd updatedDocument)
        {
            if (id < 1 || updatedDocument == null || updatedDocument.SGKStajFormu == null
                || updatedDocument.StajBasvuruFormu == null || updatedDocument.StajKabulFormu == null
                || updatedDocument.StajTaahhutnameFormu == null || updatedDocument.StudentId < 1)
            {
                return BadRequest("Invalid Document data.");
            }


            BusinessLayer.Documents Document = BusinessLayer.Documents.Find(id);


            if (Document == null)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            List<IFormFile> files = new List<IFormFile>();
            files.Add(updatedDocument.SGKStajFormu);
            files.Add(updatedDocument.StajBasvuruFormu);
            files.Add(updatedDocument.StajKabulFormu);
            files.Add(updatedDocument.StajTaahhutnameFormu);

            List<string> oldLinks = new List<string>();
            oldLinks.Add(Document.SGKStajFormu);
            oldLinks.Add(Document.StajBasvuruFormu);
            oldLinks.Add(Document.StajKabulFormu);
            oldLinks.Add(Document.StajTaahhutnameFormu);

            var NewpdfsLinks = await CloudinaryService.ReplacePdfsAsync(oldLinks,files);


            Document.SGKStajFormu = NewpdfsLinks[0];
            Document.StajBasvuruFormu = NewpdfsLinks[1];
            Document.StajKabulFormu = NewpdfsLinks[2];
            Document.StajTaahhutnameFormu = NewpdfsLinks[3];
            Document.Status = "Beklemede";
            Document.UploadTime = DateTime.Now;
            Document.StudentId = updatedDocument.StudentId;


            Document.Save();

            //we return the DTO not the full student object.
            return Ok(Document.DDTO);

        }


        [HttpPut("{documentId}/{studentId}/{teacherId}/{status}/{message}", Name = "UpdateDocumentStatus")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateDocumentStatus(int documentId, int studentId, int teacherId, string status, string message)
        {
            List<string> statusList = new List<string> { "danisman onayladi", "danisman reddetti", "hoca onayladi", "hoca reddetti" };

            if (documentId < 1 || string.IsNullOrEmpty(status) || !statusList.Contains(status.ToLower()) || string.IsNullOrEmpty(message))
            {
                return BadRequest("Invalid Document data.");
            }

            if (BusinessLayer.Documents.UpdateDocumentStatus(documentId, studentId, teacherId, status, message))
                return Ok("Updated");
            else
                return StatusCode(500, "Internal server error");
        }


        [HttpGet("Status/{id}", Name = "GetDocumentStatus")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> GetDocumentStatus(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }


            string status = BusinessLayer.Documents.GetDocumentsStatus(id);

            if (status == null || status == "")
            {
                return NotFound($"Document with ID {id} not found.");
            }


            return Ok(status);

        }


        [HttpGet("Pending", Name = "GetPendingDocuments")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DocumentsDTO>> GetPendingDocuments()
        {
            List<DocumentsDTO> DocumentsList = BusinessLayer.Documents.GetPendingDocuments();
            if (DocumentsList.Count == 0)
            {
                return NotFound("No document Found!");
            }
            return Ok(DocumentsList);
        }
    }
}
