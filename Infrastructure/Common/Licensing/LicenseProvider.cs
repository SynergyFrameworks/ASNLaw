using Microsoft.Extensions.Options;
using Infrastructure.Common.Assets;
using Infrastructure.Common.Extensions;
using Infrastructure.Licensing;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Web.Licensing
{
    public class LicenseProvider
    {
        private readonly PlatformOptions _platformOptions;
        private readonly IBlobStorageProvider _blobStorageProvider;
        private readonly IBlobUrlResolver _blobUrlResolver;

        public LicenseProvider(IOptions<PlatformOptions> platformOptions, IBlobStorageProvider blobStorageProvider, IBlobUrlResolver blobUrlResolver)
        {
            _platformOptions = platformOptions.Value;
            _blobStorageProvider = blobStorageProvider;
            _blobUrlResolver = blobUrlResolver;
        }

        public async Task<License> GetLicenseAsync()
        {
            License license = null;

            string licenseUrl = _blobUrlResolver.GetAbsoluteUrl(_platformOptions.LicenseBlobPath);
            if (await LicenseExistsAsync(licenseUrl))
            {
                string rawLicense = string.Empty;
                using (Stream stream = _blobStorageProvider.OpenRead(licenseUrl))
                {
                    rawLicense = stream.ReadToString();
                }

                license = License.Parse(rawLicense, _platformOptions.LicensePublicKeyResourceName);

                if (license != null)
                {
                    license.RawLicense = null;
                }
            }

            // Fallback to the old file system implementation
            if (license == null)
            {
                license = GetLicenseFromFile();
            }

            return license;
        }

        public License GetLicenseFromFile()
        {
            License license = null;

            string licenseFilePath = Path.GetFullPath(_platformOptions.LicenseFilePath);
            if (File.Exists(licenseFilePath))
            {
                string rawLicense = File.ReadAllText(licenseFilePath);
                license = License.Parse(rawLicense, _platformOptions.LicensePublicKeyResourceName);

                if (license != null)
                {
                    license.RawLicense = null;
                }
            }

            return license;
        }

        public void SaveLicense(License license)
        {
            using (Stream stream = _blobStorageProvider.OpenWrite(_blobUrlResolver.GetAbsoluteUrl(_platformOptions.LicenseBlobPath)))
            {
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(license.RawLicense);
                streamWriter.Flush();
            }
        }

        private async Task<bool> LicenseExistsAsync(string licenseUrl)
        {
            BlobInfo blobInfo = await _blobStorageProvider.GetBlobInfoAsync(licenseUrl);
            return blobInfo != null;
        }
    }
}
