using Botrix.Models;
using System.Text;

namespace Botrix.Services
{
    public class RuleEngineService
    {
        // Diccionario en memoria con las reglas iniciales (FAQ)
        private readonly Dictionary<string, string> _faq = new Dictionary<string, string>
        {
            { "horario", "Nuestro horario es de lunes a viernes de 9 AM a 6 PM." },
            { "ubicacion", "Estamos en Santo Domingo, Av. Principal #123." },
            { "catalogo", "Puedes ver nuestro catálogo en www.negocio.com/catalogo." },
            { "pago", "Aceptamos efectivo, tarjetas y transferencias." },
            { "contacto", "Puedes llamarnos al 809-555-1234." }
        };

        /// <summary>
        /// Devuelve todas las reglas en formato ResponseRule (para el AdminController).
        /// </summary>
        public IEnumerable<ResponseRule> GetAllRules() =>
            _faq.Select(f => new ResponseRule { Keyword = f.Key, Answer = f.Value });

        /// <summary>
        /// Agrega una nueva regla. Si la keyword ya existe, no la sobrescribe.
        /// </summary>
        public void AddRule(ResponseRule rule)
        {
            var keyword = Normalize(rule.Keyword);
            if (!_faq.ContainsKey(keyword))
            {
                _faq[keyword] = rule.Answer;
            }
        }

        /// <summary>
        /// Actualiza una regla existente o la crea si no existe.
        /// </summary>
        public void UpdateRule(ResponseRule rule)
        {
            var keyword = Normalize(rule.Keyword);
            _faq[keyword] = rule.Answer;
        }

        /// <summary>
        /// Elimina una regla por keyword.
        /// </summary>
        public void DeleteRule(string keyword)
        {
            var normalized = Normalize(keyword);
            _faq.Remove(normalized);
        }

        /// <summary>
        /// Busca una respuesta automática según el mensaje del usuario.
        /// </summary>
        public string? GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("hola"))
            {
                return "Bienvenido 👋, selecciona una opción:\n" +
                       "1️⃣ Horario\n" +
                       "2️⃣ Ubicación\n" +
                       "3️⃣ Catálogo\n" +
                       "4️⃣ Formas de pago\n" +
                       "5️⃣ Hablar con un agente";
            }

            switch (input)
            {
                case "1":
                    return "Nuestro horario es de lunes a viernes de 9 AM a 6 PM.";
                case "2":
                    return "Estamos ubicados en Santo Domingo, Distrito Nacional.";
                case "3":
                    return "Puedes ver nuestro catálogo en: https://midominio.com/catalogo";
                case "4":
                    return "Aceptamos pagos en efectivo, tarjeta y transferencias.";
                case "5":
                    return null;
                default:
                    return "No entendí tu mensaje 🤔. Por favor selecciona una opción del menú.";
            }
        }



        /// <summary>
        /// Normaliza texto para coincidencias más robustas.
        /// </summary>
        private string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            return input.ToLower().Trim();
        }
    }
}
