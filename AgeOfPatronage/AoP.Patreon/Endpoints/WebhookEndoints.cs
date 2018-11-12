using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AoP.Patreon.Endpoints
{
    public class WebhookEndoints
    {
        // GET /api/oauth2/v2/webhooks

        // POST /api/oauth2/v2/webhooks

        //Triggers V2
        public Task GetTriggers()
        {

            return Task.CompletedTask;
        }

        // Webhook Responses

        // PATCH /api/oauth2/v2/webhook/{id}
    }
}
