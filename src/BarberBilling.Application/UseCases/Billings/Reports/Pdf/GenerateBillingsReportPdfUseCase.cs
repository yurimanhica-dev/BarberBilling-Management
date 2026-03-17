using System.Reflection;
using BarberBilling.Application.Extensions;
using BarberBilling.Application.Helper;
using BarberBilling.Application.Settings;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf.colors;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf.fonts;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Domain.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace BarberBilling.Application.UseCases.Billings.Reports.Pdf;

public class GenerateBillingsReportPdfUseCase : IGenerateBillingsReportPdfUseCase
{
    private readonly CompanySettings _settings;
    private readonly IBillingReadOnlyRepository _billingsRepository;
    private readonly IStringLocalizer<ResourceReportGenerationMessages> _localizer;
    public GenerateBillingsReportPdfUseCase(IBillingReadOnlyRepository billingsRepository,
    IStringLocalizer<ResourceReportGenerationMessages> localizer,
    IOptions<CompanySettings> options)
    {
        _billingsRepository = billingsRepository;
        _localizer = localizer;
        _settings = options.Value;
        GlobalFontSettings.FontResolver = new BillingsReportFontResolver();
    }
    public async Task<byte[]> ExecuteWeekly(DateOnly weekStart)
    {
        var billings = await _billingsRepository.GetByRange(weekStart, weekStart.AddDays(6));
        return await GenerateReport(billings, _localizer["WeeklyBillingTitle"].Value, weekStart, weekStart.AddDays(6));
    }

    public async Task<byte[]> ExecuteMonthly(int year, int month)
    {
        var start = new DateOnly(year, month, 1);
        var end = start.AddMonths(1).AddDays(-1);
        var billings = await _billingsRepository.GetByRange(start, end);
        return await GenerateReport(billings, _localizer["MonthlyBillingTitle"].Value, start, end);
    }
    private async Task<byte[]> GenerateReport(
    List<Billing> billings, 
    string title, 
    DateOnly periodStart, 
    DateOnly periodEnd)
    {
        var document = CreateDocument(periodStart);
        var page = CreatePage(document);
        CreateHeaderWithImage(page);

        CreateTotalBillingsSection(page, billings.Sum(b => b.Amount), title);

        foreach (var billing in billings)
            AddBillingTable(page, billing);
        
        CreateFooter(page);

        return RenderDocument(document);
    }
    private Document CreateDocument(DateOnly weekStart) 
    {
        var document = new Document();
        document.Info.Title = $"{_localizer["ExpenseRptPage"].Value} {weekStart:Y}";
        document.Info.Author = "Yuri Manhiça";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.Default;

        return document;
    }
    private static Section CreatePage(Document document) 
    {
        var section = document.AddSection();

        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.Orientation = Orientation.Portrait;

        section.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);

        section.PageSetup.StartingNumber = 1;
        return section;
    }
    private void CreateHeaderWithImage(Section page) 
    {
        var table = page.AddTable();
        table.Borders.Visible = false;

        table.AddColumn();
        table.AddColumn("250");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        
        var pathFile = Path.Combine(directoryName!, "images", "barberLogo.png");
        
        var image = row.Cells[0].AddImage(pathFile);
        image.Height = Unit.FromPoint(100);
        image.Width = Unit.FromPoint(100);
        image.LockAspectRatio = true;

        var barberName = _settings.Name;
        row.Cells[1].AddParagraph($"{barberName.ToUpper()}");

        row.Cells[1].Format.Font = new Font(FontHelper.BebasNeueRegular, 30);
        row.Cells[1].Format.LeftIndent = Unit.FromCentimeter(1);
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }
    private void CreateTotalBillingsSection(Section page, decimal totalBillings, string title) 
    {
        if(totalBillings > 0)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceAfter = "20";
            paragraph.Format.SpaceBefore = "40";

            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RobotoMedium, Size = 15 });

            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddFormattedText($"{CurrencyFormatter.FormatCurrency(totalBillings)} MZN", new Font { Name = FontHelper.BebasNeueRegular, Size = 50 });
        } else
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceAfter = "20";
            paragraph.Format.SpaceBefore = "40";

            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RobotoMedium, Size = 15 });
        }
    }
    private static Table CreateBillingTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
    private void AddServiceNameTitle(Cell cell, string serviceNameTitle)
    {
        cell.AddParagraph(serviceNameTitle);
        cell.Format.Font = new Font { Name = FontHelper.BebasNeueRegular, Size = 15, Color = ColorHelper.White };
        cell.Row!.TopPadding = Unit.FromCentimeter(0.1);
        cell.Row!.BottomPadding = Unit.FromCentimeter(0.1);
        cell.Shading.Color = ColorHelper.Green_dark;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = Unit.FromCentimeter(0.5);
    }
    private void AddBillingAmountHeader(Cell cell) 
    {
        cell.AddParagraph(_localizer["Amount"].Value);
        cell.Format.Font = new Font { Name = FontHelper.BebasNeueRegular, Size = 15, Color = ColorHelper.White };
        cell.Shading.Color = ColorHelper.Red_medium;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void SetStyleBaseForBillingInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.RobotoMedium, Size = 10, Color = ColorHelper.Black };
        cell.Row!.TopPadding = Unit.FromCentimeter(0.1);
        cell.Row!.BottomPadding = Unit.FromCentimeter(0.2);
        cell.Shading.Color = ColorHelper.Green_light;
        cell.Format.LeftIndent = Unit.FromCentimeter(0.5);
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Row.HeightRule = RowHeightRule.AtLeast;
    }
    private void AddBillingAmount(Cell cell, decimal amount) 
    {
        cell.AddParagraph(CurrencyFormatter.FormatCurrency(amount));
        cell.Format.Font = new Font { Name = FontHelper.RobotoMedium, Size = 10, Color = ColorHelper.Black };
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private static void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 20;
        row.Borders.Visible = false;
    }
    private void AddBillingTable(Section page, Billing billing)
    {
        var table = CreateBillingTable(page);

            var row = table.AddRow();

            row.Height = Unit.FromCentimeter(0.5f);
            row.KeepWith = 2;

            AddServiceNameTitle(row.Cells[0], billing.ServiceName);
            AddBillingAmountHeader(row.Cells[3]);

            row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5f);
            row.HeightRule = RowHeightRule.Exactly;

            row.Cells[0].AddParagraph(billing.Date.ToString("D"));
            SetStyleBaseForBillingInformation(row.Cells[0]);

            row.Cells[1].AddParagraph(billing.Date.ToString("t"));
            SetStyleBaseForBillingInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(billing.PaymentMethod.GetDescription(_localizer));
            SetStyleBaseForBillingInformation(row.Cells[2]);

            AddBillingAmount(row.Cells[3], billing.Amount);
            
            if (!string.IsNullOrWhiteSpace(billing.Notes))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = Unit.FromCentimeter(0.5f);
                descriptionRow.HeightRule = RowHeightRule.AtLeast;

                var cell = descriptionRow.Cells[0];
                cell.MergeRight = 2;
                row.Cells[3].MergeDown = 1;

                cell.Row!.TopPadding = Unit.FromCentimeter(0.2);
                cell.Row!.BottomPadding = Unit.FromCentimeter(0.2);

                var paragraph = cell.AddParagraph(billing.Notes!);
                paragraph.Format.Font = new Font { Name = FontHelper.RobotoMedium, Size = 10, Color = ColorHelper.Gray };
                paragraph.Format.LeftIndent = Unit.FromCentimeter(0.5);
                cell.Shading.Color = ColorHelper.Green_lighter;
                cell.VerticalAlignment = VerticalAlignment.Center;
            }

            AddWhiteSpace(table);
    }    
    private void CreateFooter(Section page)
    {
        // Cria um parágrafo no rodapé da seção
        var footer = page.Footers.Primary.AddParagraph();

        // Centraliza o número da página
        footer.Format.Alignment = ParagraphAlignment.Right;

        // Texto: "Página X de Y"
        footer.AddText("Página ");
        footer.AddPageField();      // Número atual da página
        footer.AddText(" de ");
        footer.AddNumPagesField();  // Total de páginas

        // Formatação opcional
        footer.Format.Font.Name = "Arial";
        footer.Format.Font.Size = 9;
        footer.Format.Font.Color = Colors.Gray;
    }
    private byte[] RenderDocument(Document document)
    {
            var renderer = new PdfDocumentRenderer
            {
                Document = document
            };

            renderer.RenderDocument();

            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);

            return file.ToArray();
    }
}