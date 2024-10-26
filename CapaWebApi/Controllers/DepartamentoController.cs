using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoData _departamentoData;

        public DepartamentoController(DepartamentoData departamentoData)
        {
            _departamentoData = departamentoData;
        } 

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Departamento> departamentos = await _departamentoData.Lista();
            return StatusCode(StatusCodes.Status200OK, departamentos);
        }
    }
}
