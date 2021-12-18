using FormToPdf.Infrastructure.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormToPdf.Application.UseCases
{
    public interface IUseCase<T>
    {
        Task<T> Execute(JObject formFields);
    }
}
