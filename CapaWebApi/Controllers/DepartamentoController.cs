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

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Departamento departamento = await _departamentoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Departamento objeto)
        {
            bool respuesta = await _departamentoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Departamento objeto)
        {
            bool respuesta = await _departamentoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _departamentoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
