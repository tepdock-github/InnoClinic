namespace DocumentsService.PdfGenerator
{
    public interface IPdfConfiguration
    {
        public int HeaderFontSize { get; set; }
        public int HeaderSpacing { get; set; }
        public int TextBoxSpacing { get; set; }
        public float FooterBorderWidth { get; set; }
        public float TableBorderWidth { get; set; }
    }
}
