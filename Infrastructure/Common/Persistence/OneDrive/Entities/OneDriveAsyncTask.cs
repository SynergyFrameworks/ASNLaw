using System;

namespace Infrastructure.Common.Persistence.OneDrive.Entities
{

    public class OneDriveAsyncTask
    {
        public OneDriveAsyncTaskStatus Status { get; set; }

        public Uri StatusUri { get; set; }

        public Uri RequestUri { get; set; }

        public OneDriveItem FinishedItem { get; set; }
    }
}
