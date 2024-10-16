using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Infrastructure.Assets.AzureBlobStorage
{
    public class AzureBlobOptions
    {
        [Required]
        public string ConnectionString { get; set; }
        public string CdnUrl { get; set; }
        public BlobRequestOptions BlobRequestOptions { get; set; } = new BlobRequestOptions();
    }
}
