using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class DocumentByMetadata
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileType
        {
            get
            {
                if (_fileType == string.Empty)
                    return null;
                return _fileType;
            }
            set
            {
                _fileType = value;
            }
        }

        public Guid? ExternalId { get; set; }

        public DateTime? DownloadedDate { get; set; } //by user themselves
        public DateTime? AcceptedDate { get; set; }
        public string AcceptedByName { get; set; }
        public string Url
        {
            get
            {
                if (_url == string.Empty)
                    return null;
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public string MimeType
        {
            get
            {
                if (_mimeType == string.Empty)
                    return null;
                return _mimeType;
            }
            set
            {
                _mimeType = value;
            }
        }

        private string _fileType { get; set; }
        private string _mimeType { get; set; }
        private string _url { get; set; }
    }
}
