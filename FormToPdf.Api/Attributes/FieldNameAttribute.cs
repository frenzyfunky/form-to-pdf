using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; }
    }
}
