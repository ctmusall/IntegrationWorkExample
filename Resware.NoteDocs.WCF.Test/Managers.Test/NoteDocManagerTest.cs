using Adeptive.ResWare.Services;
using Effort;
using Effort.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReceiveNote.Managers;
using ReceiveNote.Readers;
using Resware.Data.Context;
using Resware.Data.NoteDoc.Repository;
using ReswareCommon.Messages;

namespace Resware.NoteDocs.WCF.Test.Managers.Test
{
    [TestClass]
    public class NoteDocManagerTest
    {
        private INoteDocManager _noteDocManager;
        private NoteDocReader _noteDocReader;
        private NoteDocRepository _noteDocRepository;

        [TestInitialize]
        public void Setup()
        {
            _noteDocReader = new NoteDocReader();
            //EffortProviderConfiguration.RegisterProvider();
            var connection = DbConnectionFactory.CreateTransient();
            var reswareDbContext = new ReswareDbContext(connection);
            _noteDocRepository = new NoteDocRepository(reswareDbContext);
            _noteDocManager = new NoteDocManager(_noteDocReader, _noteDocRepository);
        }

        [TestMethod]
        public void PlaceNoteDoc_passed_in_null_data_should_return_note_doc_result_with_negative_one_and_exception_message()
        {
            // Act
            var result = _noteDocManager.PlaceNoteDoc(null);

            // Assert
            Assert.AreEqual(-1, result.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.Message));
            Assert.IsTrue(result.Message.Contains("Exception"));
        }

        [TestMethod]
        public void PlaceNoteDoc_passed_in_receive_note_doc_data_but_does_not_contain_file_number_should_return_zero_result_and_file_number_is_null_message()
        {
            // Arrange
            var data = new ReceiveNoteData {FileNumber = string.Empty};

            // Act
            var result = _noteDocManager.PlaceNoteDoc(data);

            // Assert
            Assert.AreEqual(0, result.Result);
            Assert.AreEqual(ValidationMessages.FileNumberIsNull, result.Message);
        }

        [TestMethod]
        public void PlaceNoteDoc_passed_in_receive_note_data_should_return_a_note_doc_result_with_a_result_of_one_and_no_message()
        {
            // Arrange
            var data = new ReceiveNoteData { FileNumber = "123456", NoteBody = "Test Body", NoteSubject = "Test Subject"};
            
            // Act
            var result = _noteDocManager.PlaceNoteDoc(data);

            // Assert
            Assert.AreEqual(1, result.Result);
            Assert.IsTrue(string.IsNullOrWhiteSpace(result.Message));
        }
    }
}
