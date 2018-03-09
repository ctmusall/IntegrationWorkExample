using System.IO;
using System.Linq;
using System.Reflection;
using WordLicense = Aspose.Words.License;
using PdfLicense = Aspose.Pdf.License;

namespace ReswareOrderMonitorService.Aspose
{
    internal static class AsposeLicense
    {
        private static Stream GetLicenseStream()
        {
            var ea = Assembly.GetExecutingAssembly();
            var resourceName = ea.GetManifestResourceNames().Single(x => x.Contains("Aspose.Total.lic"));
            return ea.GetManifestResourceStream(resourceName);
        }

        internal static void SetWordsLicense()
        {
            var wordLicense = new WordLicense();

            using (var stream = GetLicenseStream())
            {
                wordLicense.SetLicense(stream);
            }
        }

        internal static void SetPdfLicense()
        {
            var pdfLicense = new PdfLicense();

            using (var stream = GetLicenseStream())
            {
                pdfLicense.SetLicense(stream);
            }
        }
    }
}
