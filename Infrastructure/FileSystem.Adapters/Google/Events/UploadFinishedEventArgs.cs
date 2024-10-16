using System;

namespace Infrastructure.Persistence.Google
{
    public class UploadFinishedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UploadFinishedEventArgs"/> class.
        /// </summary>
        /// <param name="fileName">The uploaded file name.</param>
        public UploadFinishedEventArgs(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        private string FileName { get; }

        /// <summary>
        ///     Gets the <see cref="UploadFinishedEventArgs" /> status.
        /// </summary>
        /// <returns>The uploaded file name of the <see cref="UploadFinishedEventArgs" />.</returns>
        public string GetStatus()
        {
            return FileName;
        }
    }
}
