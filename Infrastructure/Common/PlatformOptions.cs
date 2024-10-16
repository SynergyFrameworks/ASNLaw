using System.ComponentModel.DataAnnotations;

namespace Infrastructure
{
    public class PlatformOptions
    {
        public string DemoCredentials { get; set; }

        public string DemoResetTime { get; set; }

        [Required]
        public string LocalUploadFolderPath { get; set; } = "App_Data/Uploads";

        //The public url for license activation
        [Url]
        public string LicenseActivationUrl { get; set; } = "https://scionanalytics.com/admin/api/licenses/activate/";

        //Local path for license file
        public string LicenseFilePath { get; set; } = "App_Data/scionanalytics.lic";


        //Name of the license file with blob container
        public string LicenseBlobPath { get; set; } = "license/scionanalytics.lic";

        //Name of the public key embedded resource
        public string LicensePublicKeyResourceName { get; set; } = "scionanalyticse_rsa.pub";


        //Local path to public key for license 
        public string LicensePublicKeyPath { get; set; } = "App_Data/scionanalytics.pub";

        //Local path to private key for signing license
        public string LicensePrivateKeyPath { get; set; }

        //Local path for countries list
        public string CountriesFilePath { get; set; } = "localization/common/countries.json";
        public string CountryRegionsFilePath { get; set; } = "localization/common/countriesRegions.json";

        //Url for discovery sample data for initial installation
        //e.g. http://scionanalytics.blob.core.windows.net/sample-data
        [Url]
        public string SampleDataUrl { get; set; }

        //Default path to store export files 
        public string DefaultExportFolder { get; set; } = "App_Data/Export/";

        public string DefaultExportFileName { get; set; } = "exported_data.zip";

        //Local path to running process like WkhtmlToPdf
        public string ProcessesPath { get; set; }

        //This options controls how the OpenID Connect
        //server (ASOS) handles the incoming requests to arriving on non-HTTPS endpoints should be rejected or not. By default, this property is set to false to help
        //mitigate man-in-the-middle attacks.
        public bool AllowInsecureHttp { get; set; }
    }

}
