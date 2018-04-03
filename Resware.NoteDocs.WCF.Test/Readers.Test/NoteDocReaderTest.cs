using Adeptive.ResWare.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceiveNote.Readers;

namespace Resware.NoteDocs.WCF.Test.Readers.Test
{
    [TestClass]
    public class NoteDocReaderTest
    {
        private NoteDocReader _noteDocReader;

        [TestInitialize]
        public void Setup()
        {
            _noteDocReader = new NoteDocReader();
        }

        [TestMethod]
        public void ParseInput_passed_in_receive_note_data_with_no_documents_should_return_valid_note_doc_reader_result()
        {
            // Arrange
            var data = new ReceiveNoteData {FileNumber = "123456", NoteBody = "Test Body", NoteSubject = "Test Subject"};
            
            // Act
            var result = _noteDocReader.ParseInput(data);

            // Assert
            Assert.AreEqual(data.FileNumber, result.Note.FileNumber);
            Assert.AreEqual(data.NoteBody, result.Note.NoteBody);
            Assert.AreEqual(data.NoteSubject, result.Note.NoteSubject);
            Assert.IsFalse(result.Note.Processed);
            Assert.AreEqual(0, result.Documents.Count);
        }

        [TestMethod]
        public void ParseInput_passed_in_receive_note_data_with_one_document_should_return_valid_note_doc_reader_result()
        {
            // Arrange
            var data = new ReceiveNoteData { FileNumber = "123456", NoteBody = "Test Body", NoteSubject = "Test Subject", Documents = new [] { new ReceiveNoteDocument() { FileName = "TestFile.txt", Description = "Test Description", DocumentBody = new byte[0], DocumentTypeID = 1} } };

            // Act
            var result = _noteDocReader.ParseInput(data);

            // Assert
            Assert.AreEqual(data.FileNumber, result.Note.FileNumber);
            Assert.AreEqual(data.NoteBody, result.Note.NoteBody);
            Assert.AreEqual(data.NoteSubject, result.Note.NoteSubject);
            Assert.AreEqual(1, result.Documents.Count);
        }
    }
}
