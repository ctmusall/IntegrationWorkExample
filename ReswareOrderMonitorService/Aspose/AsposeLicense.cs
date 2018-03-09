using WordLicense = Aspose.Words.License;
using PdfLicense = Aspose.Pdf.License;

namespace ReswareOrderMonitorService.Aspose
{
    internal static class AsposeLicense
    {
        private const string LicenseName = "Aspose.Total.lic";

        internal static void SetLicenses()
        {
            var wordLicense = new WordLicense();
            wordLicense.SetLicense(LicenseName);

            var pdfLicense = new PdfLicense();
            pdfLicense.SetLicense(LicenseName);
        }
    }
}
