using InvoiceSdk.Fluent;
using InvoiceSdk.Models;
using InvoiceSdk.Models.Payments;
using InvoiceSdk.Renderer;
using InvoiceSdk.Renderer.Configuration;
using InvoiceSdk.Renderer.Internal;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace InvoiceSdk.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class InvoiceGeneratorBenchmarks
{
    private static Invoice _invoice;
    private static InvoiceConfiguration _configuration;
    private static IInvoiceRenderer _invoiceRenderer;

    [GlobalSetup]
    public void Setup()
    {
        #region Setup Invoice

        _invoice = new Invoice()
        {
            Id = Guid.NewGuid(),
            Title = "Example invoice",
            Subject = "This invoice has been created with InvoiceSdk",
            SellerAddress = new Address()
            {
                CompanyName = "Your Company.",
                Street = "Main Str. 1",
                Email = "info@companymail.com",
                City = "Athens",
                Country = "Greece",
                ZipCode = "11050",
                State = "Attiki",
                Phone = "+302105244890"
            },
            CustomerAddress = new Address()
            {
                CompanyName = "Sherlock Holmes",
                Email = "sherlock@gmail.com",
                Street = "Baker St. 221B",
                Country = "United Kingdom",
                City = "London",
                ZipCode = "12345",
                State = "London Greater",
                Phone = "+4412135447"
            },
            Number = 1239078,
            InvoiceCurrency = new InvoiceCurrencySymbol(InvoiceCurrency.Euro),
            Status = InvoiceStatus.Paid,
            IssuedAt = DateTime.Now,
            DueAt = DateTime.Now.AddDays(7),
            UpdatedAt = null,
            Note =
                "This invoice has been created using InvoiceSdk. This will appear at the end of the invoice, before the footer as a highlighted remark."
        };

        _invoice.Items = new List<InvoiceItem>()
        {
            new()
            {
                Id = 534,
                InvoiceId = _invoice.Id,
                Description = "Subscription #1",
                Name = "Subscription",
                UnitPriceWithoutVat = 10,
                Quantity = 1,
                VatPercentage = 24
            },
            new()
            {
                Id = 5435345,
                InvoiceId = _invoice.Id,
                Description = "Credits #1",
                Name = "Credits",
                UnitPriceWithoutVat = 3,
                Quantity = 5,
                VatPercentage = 24
            },
            new()
            {
                Id = 6544,
                InvoiceId = _invoice.Id,
                Description = "Lorem ipsum dolor sit amet dolor amet lorem",
                Name = "Upgrade",
                UnitPriceWithoutVat = 23,
                Quantity = 1,
                VatPercentage = 24
            }
        };

        _invoice.Payments = new List<Payment>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceId = _invoice.Id,
                Amount = 48,
                Method = new() { Name = "PayPal", ProviderUrl = "paypal.com" },
                PaidAt = DateTime.Now,
                Status = PaymentStatus.Paid
            },
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceId = _invoice.Id,
                Amount = 10,
                Method = new() { Name = "Stripe", ProviderUrl = "stripe.com" },
                PaidAt = DateTime.Now,
                Status = PaymentStatus.Paid
            }
        };

        #endregion

        #region Setup Invoice Configuration

        _configuration = InvoiceConfigurationFactory
            .CreateConfiguration()
            .WithGlobalFont(new Font("Calibri"))
            .ConfigureHeader()
            .WithTextColor("#2196f3")
            .ConfigureAddress()
            .WithHeaders("Beneficiary", "Receipient")
            .ThatDoesNotShowLabels()

            // Uncomment these lines and set the path to your logo
            //.ConfigureLogo()
            //.WithLogoHeightCm(50f)
            //.WithLogoFile("C:/Users/Admin/Desktop/Logo.png")
            .ConfigureItemTable()
            .ThatDisplaysItemDescriptions()
            .WithHeader("Purchases Goods/Services")
            .ThatShowsAlertWithoutItems("No items!", "You did not purchase any goods or services!")
            .ConfigurePaymentTable()
            .ThatShowsAlertWithoutItems("No payments!", "You did not make any payments for this invoice!")
            .WithHeader("Invoice Payments")
            .ConfigureFooter()
            .WithText("Made with InvoiceSdk!")
            .WithTextColor("#2196f3")
            .Build();

        #endregion

        _invoiceRenderer = new InvoiceRenderer();
    }

    [Benchmark]
    public void GeneratePdfInvoice()
    {
        IDocument document = _invoiceRenderer.RenderInvoice(_invoice, _configuration);
        byte[] _ = document.GeneratePdf();
    }

    [Benchmark]
    public void GenerateXpsInvoice()
    {
        IDocument document = _invoiceRenderer.RenderInvoice(_invoice, _configuration);
        byte[] _ = document.GenerateXps();
    }
    
    [Benchmark]
    public async Task GeneratePdfInvoiceAsync()
    {
        IDocument document = await _invoiceRenderer.RenderInvoiceAsync(_invoice, _configuration);
        byte[] _ = document.GeneratePdf();
    }

    [Benchmark]
    public async Task GenerateXpsInvoiceAsync()
    {
        IDocument document = await _invoiceRenderer.RenderInvoiceAsync(_invoice, _configuration);
        byte[] _ = document.GenerateXps();
    }
}