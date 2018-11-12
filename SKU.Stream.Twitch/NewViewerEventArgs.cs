using System;
using System.Collections.Generic;
using System.Text;

namespace SKU.Stream.Twitch
{
    public class NewViewerEventArgs : EventArgs
    {
        private int foundViewerCount;

        public NewViewerEventArgs(int foundViewerCount)
        {
            this.foundViewerCount = foundViewerCount;
        }

        public int ViewerCount => foundViewerCount;
    }
}
