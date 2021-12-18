using FormToPdf.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace FormToPdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IUseCase<bool> _useCase;

        public FormController(
                IUseCase<bool> useCase
            )
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject formFields)
        {
            try
            {
                var result = await _useCase.Execute(formFields);

                if (!result)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
