using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Botrix.Services
{
    public class WhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _phoneNumberId;

        public WhatsAppService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _accessToken = config["WhatsAppSettings:AccessToken"];
            _phoneNumberId = config["WhatsAppSettings:PhoneNumberId"];
        }

        // ✅ Enviar mensajes de texto normales
        public async Task<bool> SendMessage(string to, string text)
        {
            var url = $"https://graph.facebook.com/v25.0/{_phoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = to,
                type = "text",
                text = new { body = text }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Respuesta de WhatsApp API (texto): {content}");

            return response.IsSuccessStatusCode;
        }

        // ✅ Enviar mensajes de plantilla con parámetros
        public async Task<bool> SendTemplateMessageAsync(string to, string templateName, string languageCode = "en_US", object[] parameters = null)
        {
            var url = $"https://graph.facebook.com/v25.0/{_phoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = to,
                type = "template",
                template = new
                {
                    name = templateName,
                    language = new { code = languageCode },
                    components = parameters != null ? new[]
                    {
                        new {
                            type = "body",
                            parameters = parameters
                        }
                    } : null
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Respuesta de WhatsApp API (template): {content}");

            return response.IsSuccessStatusCode;
        }
    }
}
