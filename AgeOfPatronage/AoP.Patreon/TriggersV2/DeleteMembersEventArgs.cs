using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfPatronage.Patreon.TriggersV2
{
    // members:delete
    public class DeleteMembersEventArgs : EventArgs
    {
        // Triggered when a membership is deleted. Note that you may get more than one of these per patron if they delete and renew their membership. 
        // Deletion only occurs if no prior payment happened, otherwise pledge deletion is an update to member status.
        public string Member { get; set; }
    }
}
