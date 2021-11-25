using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormToPdf.Service
{
    public interface IEmailCreator
    {
        string CreateFromTemplate(string pathToTemplate, JObject variables);
    }
}
