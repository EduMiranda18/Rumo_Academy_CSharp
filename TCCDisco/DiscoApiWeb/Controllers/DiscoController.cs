using DiscoApi.Respositorios.Exceptions;
using DiscoApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace DiscoApiWeb.Controllers
{
    public class DiscoController: ControllerBase
    {

        private readonly DiscoService _service;

        public DiscoController (DiscoService service)
        {
            _service = service;
        }

        [HttpGet("disco")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            return StatusCode(200, _service.Listar(nome));
        }

        [HttpPost("disco")]
        public IActionResult Inserir()
        {
            try
            {
                _service.Inserir();
                return StatusCode(201);
            }
            catch (ValidacaoException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
