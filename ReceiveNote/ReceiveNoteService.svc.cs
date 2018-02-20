using Adeptive.ResWare.Services;

namespace ReceiveNote
{
    
    public class Service : IReceiveNoteService
    {

        public ReceiveNoteResponse ReceiveNote(ReceiveNoteData NoteData)
        {
            //throw new NotImplementedException();
            // Add business logic here and Error checking below.



            return new ReceiveNoteResponse()
            {
                Message = "(Test Service) Received Note Document Successfully",
                ResponseCode = 0,

            };


        }

        

        
    }

    

    
}
