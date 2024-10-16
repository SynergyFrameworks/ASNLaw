using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Habanero.Licensing.Shared;
using Habanero.Licensing.Validation;

using Atebion.Common;


namespace ProfessionalDocAnalyzer
{
    public class LicenseMgr
    {
        private string _licFile = string.Concat(AppFolders.AppDataPath_Local, @"\", "ProfDocAnalyzer.lic");

        public bool SaveLicense(string licText)
        {
            Files.WriteStringToFile(licText, _licFile);

            string results = Validate();
            if (results == "Invalid")
                return false;
            else
                return true;
        }

        public string GetLicense()
        {
            string result = string.Empty;

            result = Files.ReadFile(_licFile);

            return result;
        }

        public string ActivateTrial(int qtyDays)
        {
            LicenseValidationResult valResult = Validator.ActivateTrial(qtyDays);

            string results = valResult.RawLicenseData;

            return results;
        }


        public string Validate()
        {
            var validationResults = Validator.CheckLicense();

            if (validationResults.State == LicenseState.Invalid)
            {
                return "Invalid";
            }
            else if (validationResults.State == LicenseState.Trial)
            {
                _dtExpirationDate = validationResults.ExpirationDate; // Added 8.21.2014
                //  _ExpirationDate = validationResults.ExpirationDate.ToString();
                _ExpirationDate = _dtExpirationDate.ToString();
                return _ExpirationDate;
            }
            else
            {
                _dtExpirationDate = validationResults.ExpirationDate; // Added 8.21.2014
                //  _ExpirationDate = validationResults.ExpirationDate.ToString();
                _ExpirationDate = _dtExpirationDate.ToString();

                return "Valid";
            }

        }

        private string _ExpirationDate = string.Empty;

        public string ExpirationDate
        {
            get { return _ExpirationDate; }
        }

        // Added 08.21.2014
        private DateTime? _dtExpirationDate;
        public DateTime? dtExpirationDate
        {
            get { return _dtExpirationDate; }
        }



        //create code for applicationsecret
        byte[] applicationSecret = Convert.FromBase64String("z21eedi/hEOgrMV55x6zSQ==");
        //create code for public key
        byte[] publicKey = Convert.FromBase64String("BgIAAACkAABSU0ExAAIAAAEAAQAjUJMMDvhuG8wqOONLl2azgHagiDwTgRsfmLT52cJiXA2SoGsYGjjBzry+ubEn6sMfJ9SzIynr0n5nhgLisV+R");
        private Habanero.Licensing.Validation.LicenseValidator Validator
        {
            get
            {
                //this version is for file system - Isolated storage is anther option
                return new Habanero.Licensing.Validation.LicenseValidator(Habanero.Licensing.Validation.LicenseLocation.File, string.Concat(AppFolders.AppDataPath_Local, @"\", "ProfDocAnalyzer.lic"), "ProfDocAnalyzer", publicKey, applicationSecret, ThisVersion);

            }
        }

        private static Version ThisVersion
        {
            get
            {
                //Get the executing files filesversion
                var fileversion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var thisVersion = new Version(fileversion.FileMajorPart, fileversion.FileMinorPart, fileversion.FileBuildPart, fileversion.FilePrivatePart);

                return thisVersion;
            }
        }

        private void DoLicenseCheck()
        {
            LicenseValidationResult result = Validator.CheckLicense();
            if (result.State == LicenseState.Invalid)
            {
                if (result.Issues.Contains(LicenseIssue.NoLicenseInfo))
                {
                    //inform user there is no license info
                }
                else
                {
                    if (result.Issues.Contains(LicenseIssue.ExpiredDateSoft))
                    {
                        //inform user that their license has expired but
                        //that they may continue using the software for a period
                    }
                    if (result.Issues.Contains(LicenseIssue.ExpiredDateHard))
                    {
                        //inform user that their license has expired
                    }
                    if (result.Issues.Contains(LicenseIssue.ExpiredVersion))
                    {
                        //inform user that their license is for an earlier version
                    }
                    //other messages
                }

                //prompt user for trial or to insert license info then decide what to do
                //activate trial
                result = Validator.ActivateTrial(45);
                //or save license
                string userLicense = "Get the license string from your user";
                result = Validator.CheckLicense(userLicense);
                //decide if you want to svae the license...
                Validator.SaveLicense(userLicense);
            }
            if (result.State == LicenseState.Trial)
            {
                //activate trial features
            }
            if (result.State == LicenseState.Valid)
            {
                //activate product
                if (Validator.IsEdition("Pro"))
                {
                    //activate pro features...
                }
            }
        }
    }
}
