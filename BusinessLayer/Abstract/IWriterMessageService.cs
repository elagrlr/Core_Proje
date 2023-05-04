using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWriterMessageService:IGenericService<WriterMessage>
    {
        // Göndericisi oldugum mesajları listele
        List<WriterMessage> GetListSenderMessage(string p);

        // Alıcısı oldugum mesajları listele
        List<WriterMessage> GetListReceiverMessage(string p);


    }
}
