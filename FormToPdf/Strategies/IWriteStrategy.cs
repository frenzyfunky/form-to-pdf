using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Strategies
{
    public interface IWriteStrategy
    {
        MemoryStream Execute(JObject formValues); 
    }
}
