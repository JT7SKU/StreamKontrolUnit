using System;
using System.Collections.Generic;
using System.Text;

namespace SKU.Stream.Twitch
{
    public class NewFollowersEventArgs : EventArgs
    {
        private int foundFollowerCount;
        public NewFollowersEventArgs(int foundFollowerCount)
        {
            this.foundFollowerCount = foundFollowerCount;
        }
        public int FollowerCount => foundFollowerCount;
    }
}
