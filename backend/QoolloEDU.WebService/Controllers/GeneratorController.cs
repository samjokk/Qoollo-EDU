using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QoolloEDU.Database.Repositories.AuthRepository;
using QoolloEDU.Database.Repositories.GeneratorRepository;

namespace QoolloEDU.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratorController: Controller
    {
        private readonly IGeneratorRepository _generatorRepository;

        public GeneratorController(IGeneratorRepository generatorRepository)
        {
            _generatorRepository = generatorRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateData()
        {
            IActionResult response;
            try
            {
                await _generatorRepository.GenerateDataAsync();
                response = StatusCode(StatusCodes.Status204NoContent); 
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
        
        [HttpDelete]
        public async Task<IActionResult> ClearAllData()
        {
            IActionResult response;
            try
            {
                await _generatorRepository.ClearAllDataAsync();
                response = StatusCode(StatusCodes.Status204NoContent); 
            }
            catch(Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError); 
            }
            return response;
        }
    }
}