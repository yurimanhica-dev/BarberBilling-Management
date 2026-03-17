using PdfSharp.Fonts;

namespace BarberBilling.Application.UseCases.Billings.Reports.Pdf.fonts;

public class BillingsReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.Default);

        var length = (int)stream!.Length;   
        var buffer = new byte[length];
        stream.ReadExactly(buffer, 0, length); 
        return buffer;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private static Stream? ReadFontFile(string faceName)
    {
        var assembly = typeof(BillingsReportFontResolver).Assembly;
        var stream = assembly.GetManifestResourceStream($"BarberBilling.Application.UseCases.Billings.Reports.Pdf.fonts.{faceName}.ttf");
        return stream;
    }
}