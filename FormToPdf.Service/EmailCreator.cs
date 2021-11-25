using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormToPdf.Service
{
    public class EmailCreator : IEmailCreator
    {
        public string CreateFromTemplate(string pathToTemplate, JObject variables)
        {
            var template = File.ReadAllText(pathToTemplate);

            var dict = variables.Properties().ToDictionary(key => key.Name, value => value.Value.Value<string>());

            foreach (var variable in dict)
            {
                template = template.Replace($"{{{variable.Key}}}", variable.Value);
            }

            return template;
        }
    }
}
