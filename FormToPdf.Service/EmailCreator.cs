using Newtonsoft.Json.Linq;
using System.Linq;

namespace FormToPdf.Service
{
    public class EmailCreator : IEmailCreator
    {
        public string CreateFromTemplate(string emailTemplate, JObject variables)
        {
            var dict = variables.Properties().ToDictionary(key => key.Name, value => value.Value.Value<string>());

            foreach (var variable in dict)
            {
                emailTemplate = emailTemplate.Replace($"{{{variable.Key}}}", variable.Value);
            }

            return emailTemplate;
        }
    }
}
