using Botrix.Models;
using Botrix.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Botrix.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly RuleEngineService _ruleEngine;
        private readonly WhatsAppService _whatsAppService;

        public WebhookController(RuleEngineService ruleEngine, WhatsAppService whatsAppService)
        {
            _ruleEngine = ruleEngine;
            _whatsAppService = whatsAppService;
        }


        [HttpGet]
        public IActionResult Verify(string hub_mode, string hub_challenge, string hub_verify_token)
        {
            if (hub_mode == "subscribe" && hub_verify_token == "MiTokenSeguro123")
                return Ok(hub_challenge);
            return Unauthorized();
        }


        [HttpGet("test")]
        public async Task<IActionResult> TestMessage()
        {
            var success = await _whatsAppService.SendMessage("18098358694", "Hola mensaje desde mi bot de whatsApp prueba desde backend");


            return Ok(success ? "Mensaje enviado" : "Error al enviar");
        }


        [HttpGet("testtemplate")]
        public async Task<IActionResult> TestTemplate()
        {
            var success = await _whatsAppService.SendTemplateMessageAsync(
                "18098358694",
                "jaspers_market_order_confirmation_v1",
                "en_US",
                new object[]
                {
                    new { type = "text", text = "John Doe" },
                    new { type = "text", text = "123456" },
                    new { type = "text", text = "May 8, 2026" }
                }
            );

            return Ok(success ? "Template enviado" : "Error al enviar template");
        }


        [HttpPost]
        public async Task<IActionResult> Receive([FromBody] JObject body)
        {
            Console.WriteLine("Webhook recibido: " + body.ToString());

            var entry = body["entry"]?[0]?["changes"]?[0]?["value"]?["messages"]?[0];
            if (entry != null)
            {
                var from = entry["from"]?.ToString();
                var text = entry["text"]?["body"]?.ToString();

                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(text))
                {
                    var response = _ruleEngine.GetResponse(text);

                    if (response != null)
                    {
                        await _whatsAppService.SendMessage(from, response);
                    }
                    else
                    {
                        await EscalarAHumano(from);
                    }
                }
            }

            return Ok();
        }


        private async Task EscalarAHumano(string from)
        {
            await _whatsAppService.SendMessage(from, "Gracias, un agente te atenderá en breve.");

        }
    }
}
