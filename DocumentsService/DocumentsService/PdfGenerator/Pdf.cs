namespace DocumentsService.PdfGenerator
{
    public static class Pdf
    {
        private static IPdfConfiguration Configuration { get; set; } = new PdfConfiguration();

        public static void SetConfiguration(IPdfConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static PdfBuilder Create()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return new PdfBuilder(Configuration);
        }
    }
}
