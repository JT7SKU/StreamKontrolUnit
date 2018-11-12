using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamKernelUnit
{
    public interface IStreamService
    {
        string Name { get; }
        int CurrentFollowerCount { get; }
        int CurrentViewerCount { get; }
        ValueTask<TimeSpan?> UpTime();

        /// <summary>
        /// Event raised when the number of Followers ir Viewers changes
        /// </summary>
        event EventHandler<ServiceUpdatedEventArgs> Updated;
    }
}
