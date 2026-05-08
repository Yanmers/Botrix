using Botrix.Services;
using Microsoft.AspNetCore.Mvc;

namespace Botrix.Controllers
{
    public class BotController : Controller
    {
        private readonly WhatsAppService _whatsAppService;

        public BotController(WhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendTestMessage()
        {
            var success = await _whatsAppService.SendTemplateMessageAsync("18098358694", "hello_world");

            if (success)
                return Ok("Mensaje enviado correctamente ");
            else
                return StatusCode(500, "Error al enviar el mensaje ");
        }

    }
}
