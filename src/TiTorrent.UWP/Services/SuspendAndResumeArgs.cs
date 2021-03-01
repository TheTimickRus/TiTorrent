using System;

namespace TiTorrent.UWP.Services
{
    public class SuspendAndResumeArgs : EventArgs
    {
        public SuspensionState SuspensionState { get; set; }
        public Type Target { get; }

        public SuspendAndResumeArgs(SuspensionState suspensionState, Type target)
        {
            SuspensionState = suspensionState;
            Target = target;
        }
    }
}
