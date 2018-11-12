using AoP.Patreon.Resources;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AoP.Patreon.Endpoints
{
    public class ResourceEndpoints
    {
        HttpClient client = new HttpClient();
        // GET /api/oauth2/v2/identity
        public async Task GetIdentity()
        {
            ///api/oauth2/v2/identity?include=memberships and /api/oauth2/v2/identity?include=campaign.
            var identityUrl = $"/api/oauth2/v2/identity";
            
            //var user = await client.GetStreamAsync<User>();
        }

        // GET /api/oauth2/v2/campaigns

        // GET /api/oauth2/v2/campaigns/{campaign_id
        
        // GET /api/oauth2/v2/campaigns/{campaign_id}/members

        // GET /api/oauth2/v2/members/{id}
    }
}
