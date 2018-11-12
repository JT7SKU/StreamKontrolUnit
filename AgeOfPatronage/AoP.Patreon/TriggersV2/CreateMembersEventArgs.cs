using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfPatronage.Patreon.TriggersV2
{
    // members:create
    public class CreateMembersEventArgs : EventArgs
    {
        // Triggered when a new member is created. Note that you may get more than one of these per patron if they delete and renew their membership. 
        // Member creation only occurs if there was no prior payment between patron and creator.
    }
}
