using InvoiceSdk.Models;
using InvoiceSdk.Models.Payments;
using InvoiceSdk.Fluent;
using InvoiceSdk.Renderer.Internal;
using InvoiceSdk.Renderer.Configuration;
using InvoiceSdk.Renderer;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Example;

public class Program
{
    public static int Main(string[] args)
    {
        Invoice invoice = new Invoice()
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
            Note = "This invoice has been created using InvoiceSdk. This will appear at the end of the invoice, before the footer as a highlighted remark."
        };

        invoice.Items = new List<InvoiceItem>()
        {
            new()
            {
                Id = 534,
                InvoiceId = invoice.Id,
                Description = "Subscription #1",
                Name = "Subscription",
                UnitPriceWithoutVat = 10,
                Quantity = 1,
                VatPercentage = 24
            },
            new()
            {
                Id = 5435345,
                InvoiceId = invoice.Id,
                Description = "Credits #1",
                Name = "Credits",
                UnitPriceWithoutVat = 3,
                Quantity = 5,
                VatPercentage = 24
            },
            new()
            {
                Id = 6544,
                InvoiceId = invoice.Id,
                Description = "Lorem ipsum dolor sit amet dolor amet lorem",
                Name = "Upgrade",
                UnitPriceWithoutVat = 23,
                Quantity = 1,
                VatPercentage = 24
            }
        };

        invoice.Payments = new List<Payment>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Amount = 48,
                Method = new() { Name = "PayPal", ProviderUrl = "paypal.com" },
                PaidAt = DateTime.Now,
                Status = PaymentStatus.Paid
            },
            new()
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Amount = 10,
                Method = new() { Name = "Stripe", ProviderUrl = "stripe.com" },
                PaidAt = DateTime.Now,
                Status = PaymentStatus.Paid
            }
        };


        InvoiceConfiguration configuration = InvoiceConfigurationFactory
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

        IInvoiceRenderer renderer = new InvoiceRenderer();
        IDocument document = renderer.RenderInvoice(invoice, configuration);

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "Invoice.pdf");
        document.GeneratePdf(path);

        return 0;
    }
}