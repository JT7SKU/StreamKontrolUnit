using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfPatronage.Patreon.TriggersV2
{
    // members:pledge:create
    public class CreateMembersPledgeEventArgs :EventArgs
    {
        // Triggered when a new pledge is created for a member. This includes when a member is created through pledging, and when a follower becomes a patron.
    }
}
