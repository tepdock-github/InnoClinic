namespace DocumentsService.PdfGenerator
{
    public class PdfConfiguration : IPdfConfiguration
    {
        public int HeaderFontSize { get; set; } = 13;
        public int HeaderSpacing { get; set; } = 5;
        public int TextBoxSpacing { get; set; } = 3;
        public float FooterBorderWidth { get; set; } = 0.25f;
        public float TableBorderWidth { get; set; } = 0.5f;
    }
}
