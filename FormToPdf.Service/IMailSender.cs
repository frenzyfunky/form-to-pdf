using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FormToPdf.Service
{
    public interface IMailSender
    {
        void Send(string body, Stream attachment, string filename, params string[] to);
    }
}
