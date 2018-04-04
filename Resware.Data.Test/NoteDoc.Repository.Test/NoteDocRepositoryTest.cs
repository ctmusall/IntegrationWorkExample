using System;
using System.Collections.Generic;
using System.Linq;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Resware.Data.Context;
using Resware.Data.NoteDoc.Repository;
using Resware.Entities.Notes;
using Resware.Entities.Notes.Documents;

namespace Resware.Data.Test.NoteDoc.Repository.Test
{
    [TestClass]
    public class NoteDocRepositoryTest
    {
        private NoteDocRepository _noteDocRepository;
        private ReswareDbContext _reswareDbContext;

        [TestInitialize]
        public void Setup()
        {
            EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            _reswareDbContext = new ReswareDbContext(connection);
            _noteDocRepository = new NoteDocRepository(_reswareDbContext);
        }

        [TestMethod]
        public void SaveNewNoteDoc_passed_in_null_note_should_not_save_any_new_note_doc_and_return_negative_one()
        {
            // Act
            var result = _noteDocRepository.SaveNewNoteDoc(null, new List<Document>());
            
            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewNoteDoc_passed_in_null_document_list_should_not_save_any_new_note_doc_and_return_negative_one()
        {
            // Act
            var result = _noteDocRepository.SaveNewNoteDoc(new Note(), null);

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SaveNewNoteDoc_passed_in_valid_note_doc_should_save_both_and_return_two()
        {
            // Arrange
            var newNote = new Note {NoteBody = "Test Body", NoteSubject = "Test Subject"};
            var newDocumentList = new List<Document> {new Document {FileName = "File.txt", Description = "Test", DocumentBody = new[] {new byte()}}};

            // Act
            var result = _noteDocRepository.SaveNewNoteDoc(newNote, newDocumentList);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetAllNoteDocs_should_return_an_empty_list_of_note_docs()
        {
            // Act
            var result = _noteDocRepository.GetAllNoteDocs();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetAllNoteDocs_should_return_list_of_one_note_and_one_document_attaced_to_the_note()
        {
            // Arrange
            _reswareDbContext.Notes.Add(new Note
            {
                FileNumber = "123456",
                NoteSubject = "Test Subject",
                NoteBody = "Test Body",
                Documents = new List<Document>
                {
                    new Document
                    {
                        Description = "Test Description",
                        DocumentBody = new byte[0],
                        DocumentTypeId = 1,
                        FileName = "File.txt"
                    }
                }
            });

            _reswareDbContext.SaveChanges();

            // Act
            var result = _noteDocRepository.GetAllNoteDocs();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Documents.Count);
            Assert.AreEqual("123456", result[0].FileNumber);
        }

        [TestMethod]
        public void UpdateNoteDoc_passed_in_note_entity_but_no_id_match_should_return_zero()
        {
            // Arrange
            var note = new Note();

            // Act
            var result = _noteDocRepository.UpdateNoteDoc(note);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void UpdateNoteDoc_passed_in_note_with_match_and_updated_processed_field_should_update_note_and_return_one()
        {
            // Arrange
            var note = new Note { FileNumber = "123456", NoteBody = "Test Body", NoteSubject = "Test Subject" };

            _reswareDbContext.Notes.Add(note);
            _reswareDbContext.SaveChanges();

            note = _reswareDbContext.Notes.First();

            note.Processed = true;
            note.ProcessedDateTime = DateTime.Now;

            // Act
            var result = _noteDocRepository.UpdateNoteDoc(note);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
