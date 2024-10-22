using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CapaDatos;
using CapaEntidad;

namespace CapaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;

        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> empleados = await _empleadoData.Lista();
            return StatusCode(StatusCodes.Status200OK, empleados);
        }
    }
}
