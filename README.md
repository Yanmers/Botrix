# <img width="1024" height="1024" alt="Botrix" src="https://github.com/user-attachments/assets/5f583228-baa1-4bb0-a867-4980afd23f7b" />

Botrix es un **bot administrativo con integración a la API de WhatsApp Business**, desarrollado en **ASP.NET Core MVC**.  
Su objetivo es automatizar respuestas frecuentes (FAQ), escalar a soporte humano cuando sea necesario y ofrecer un panel administrativo modular para gestionar reglas y estadísticas.

---

## Características principales

- **[Integración WhatsApp API](ca://s?q=Integracion_con_WhatsApp_API)**: envío y recepción de mensajes en tiempo real.
- **[FAQ Bot](ca://s?q=FAQ_Bot_con_valores_fijos)**: respuestas automáticas con valores fijos (sin base de datos).
- **[Panel Administrativo](ca://s?q=Panel_Administrativo_en_ASP_NET_MVC)**: gestión de reglas, estadísticas y políticas de seguridad.
- **[Modularidad](ca://s?q=Arquitectura_modular_en_ASP_NET_MVC)**: Controllers, Services y Models bien definidos.
- **[Políticas de Seguridad](ca://s?q=Vista_de_politicas_de_seguridad)**: vista dedicada para transparencia y cumplimiento.
- **[Optimización Hardware](ca://s?q=Optimizacion_PC_para_programacion)**: pensado para correr en entornos de desarrollo con multitarea y virtualización.

---

## Estructura del proyecto
Botrix/
├── Controllers/
│   ├── AdminController.cs
│   ├── BotController.cs
│   └── PoliceController.cs
├── Services/
│   ├── WhatsAppService.cs
│   └── RuleEngineService.cs
├── Models/
│   ├── ResponseRule.cs
│   └── MessageModel.cs
├── Views/
│   ├── Admin/
│   │   ├── Index.cshtml
│   │   └── Stats.cshtml
│   ├── Police/
│   │   └── Index.cshtml
│   └── Shared/
│       └── _Layout.cshtml
└── wwwroot/
├── lib/bootstrap/
└── css/


---

## Instalación y ejecución

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tuusuario/botrix.git
   cd botrix
Restaura dependencias:

bash
dotnet restore
Ejecuta el proyecto:

bash
dotnet run --urls "http://localhost:7038"
Exponer con ngrok:

bash
ngrok http 7038
Configura el webhook en Meta con la URL pública de ngrok:

Código
https://<tu-url-ngrok>/api/webhook
Políticas de Seguridad
Botrix incluye una vista dedicada (Police.cshtml) donde se muestran las políticas de seguridad y recuperación ante desastres.
Esto garantiza transparencia y cumplimiento con normativas de protección de datos.

Identidad Visual
Logo oficial de Botrix:

sandbox:/attachments/generated/botrix_logo.png


