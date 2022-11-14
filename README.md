# InvoiceSdk

**InvoiceSdk** is a .NET 6 library that enables the creation of invoices in PDF format using a robust fluent API. **InvoiceSdk** also supports serializing invoices in XML, JSON or CSV format. **InvoiceSdk** is based on the [QuestPDF](https://github.com/QuestPDF/QuestPDF) library for the rendering component and [ServiceStack.Text](https://github.com/ServiceStack/ServiceStack) for serialization.

**The package is available on NuGet:**

| Package                       | NuGet                                                                                                                                                                                     |
| ----------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **InvoiceSdk**                   |       [![InvoiceSdk](https://img.shields.io/nuget/v/InvoiceSdk.svg?style=flat-square&label=InvoiceSdk)](https://www.nuget.org/packages/InvoiceSdk/)

## How it works

**InvoiceSdk** has a few prebuilt generic models that represent core aspects of an invoice.

Specifically, the core models of the SDK are:

* **`Invoice`**
	Represents an invoice document. This is the main model you'll be working with. All serializers expect an Invoice model to serialize/deserialize.
	
* **`Payment`**
Represents a payment made to fullfil the invoice. The data it holds is the payment provider name, date, amount and provider URL.

* **`InvoiceItem`**
Represents an item or service that is assigned to a specific invoice. 

* **`Address`**
Represents one of the parties involved in the invoice. An `Invoice` model requires two `Address` instances; one for the beneficiary (seller) and one for the payer (buyer). 


**InvoiceSdk** works by allowing you to create an `Invoice` model, specify the items/services purchased by assigning `InvoiceItem` class instances to the `Invoice`. Additionally assign `Payment` model instances to let the `Invoice` know that payments have been made to fullfil it. 

You can serialize/deserialize `Invoice` models to and from **JSON**, **XML** and **CSV**.


## Creating an invoice

### Create an `Invoice` using the following snippet:

```csharp
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
```

Every property of the `Invoice` model is open to modify and assign as you please. Typically, the `Number` property should be an auto-incremented number. However, the choice is still yours to make on how these fields are populated. You can retrieve the data from a database or you can build it on the fly after a successful payment has been made to your service. It's all customizable.


###  Assign purchased goods/services to the `Invoice` like so:

```csharp
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
```
As before, the `InvoiceItem` properties are also fully customizable. The only field that needs to be properly set is the `InvoiceId` property which holds the `Id` of an `Invoice`.

***Note***: The `Description` property holds the value of the text that will be rendered on an invoice should you choose to render item descriptions. They will be rendered underneath the `Name` property.

You can set different VAT percentages for each item. It will be used to calculate the total price of each item.

### Assign payments to the `Invoice` like so:

```csharp
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
```

As with the previous models, the `Payment` model is also fully customizable. `InvoiceId` should hold the `Id` of the `Invoice` the payment has been made for. Each `Payment` can have different statuses which are described in the enum below:

```csharp
public enum PaymentStatus
{
    Paid,
    Completed,
    Reverted,
    Cancelled,
    Rejected
}
```

The `Paid` and `Completed` values are interchangeable. However, if for any reason you want to differentiate when the payment has been made and when it is manually accepted, you can use these values to represent these two states.

## Serializing an `Invoice` to various formats

Currently, **InvoiceSdk** supports 3 different serialization formats; **JSON**, **XML** and **CSV**. Thanks to `ServiceStack.Text`, it is also possible to deserialize from **CSV** into an `Invoice` model. 

### First create an invoice generator:

* For JSON:
```csharp
IInvoiceGeneratorFactory factory = new InvoiceGeneratorFactory();
IInvoiceGenerator generator = factory.CreateInvoiceGenerator(InvoiceFormatType.Json);
```

* For XML:
```csharp
IInvoiceGeneratorFactory factory = new InvoiceGeneratorFactory();
IInvoiceGenerator generator = factory.CreateInvoiceGenerator(InvoiceFormatType.Xml);
```

* For CSV:
```csharp
IInvoiceGeneratorFactory factory = new InvoiceGeneratorFactory();
IInvoiceGenerator generator = factory.CreateInvoiceGenerator(InvoiceFormatType.Csv);
```
### Serialize an invoice

* Serialize to `string`:
```csharp
string serializedInvoice = generator.GenerateInvoice(invoice);
```

* Serialize directly to disk file:
```csharp
generator.GenerateInvoice(invoice, "C:/Users/Admin/Desktop/Invoice.{json/xml/csv}", true);
```
***Note: The third parameter in this overload is a `bool` which lets the invoice generator know that it should format  (ident & prettify)  the serialized string. Also make sure to use the correct extension on the path parameter.***

### Deserialize an invoice

```csharp
string serializedInvoice = File.ReadAllText("C:/Path/To/Invoice.{json/xml/csv}");
Invoice invoice = generator.GenerateInvoice(serializedInvoice);
```