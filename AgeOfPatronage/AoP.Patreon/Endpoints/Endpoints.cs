using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfPatronage.Patreon.Endpoints
{
    public enum ResourceEndpoints
    {
        Identity,
        Campaigns,
        CampaignID,
        CampaignMembers,
        Members
    }
    public enum WebHookEndpoints
    {
        GetWebhooks,
        PostWebHooks,
        TriggersV2,
        WebhookResponses,
        PatchWebhook
    }

}
