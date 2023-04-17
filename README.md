# InvoiceSdk

**InvoiceSdk** is a .NET 6/7 library that enables the creation of invoices in PDF format using a robust fluent API. **InvoiceSdk** also supports serializing invoices in XML, JSON or CSV format. **InvoiceSdk** is based on the [QuestPDF](https://github.com/QuestPDF/QuestPDF) library for the rendering component and [ServiceStack.Text](https://github.com/ServiceStack/ServiceStack) for serialization.

**The package is available on NuGet:**

| Package                       | NuGet                                                                                                                                                                                     |
| ----------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **InvoiceSdk**                   |       [![InvoiceSdk](https://img.shields.io/nuget/v/InvoiceSdk.svg?style=flat-square&label=InvoiceSdk)](https://www.nuget.org/packages/InvoiceSdk/)

To install the package from your NuGet console:
```
Install-Package InvoiceSdk 
```

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


**InvoiceSdk** works by allowing you to create an `Invoice` model, specify the items/services purchased by assigning `InvoiceItem` class instances to the `Invoice`. Additionally assign `Payment` model instances to let the invoice know that payments have been made to fullfil it. 

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

### Assign payments to the invoice like so:

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

## Serializing an invoice to various formats

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

## Creating configuration for the invoice renderer

The first thing you need to do in order to render an invoice is create an `InvoiceConfiguration` class with your desired settings. By default, the `InvoiceConfiguration` class has predefined settings but these can be changed easily. To create invoice configuration, you can use the library's fluent API to make your work easier.

### Create a configuration instance by using the following method:

```csharp
InvoiceConfigurationDefinition configDefinition = InvoiceConfigurationFactory
    .CreateConfiguration()
    .WithGlobalFont(new Font("Calibri"));
```
You can specify any font of your choise and customize the font size in the `WithGlobalFont` method. If you want a different font with a different font size, let's assume it is **Times New Roman** with font size **14**, you'd do it as follows:

```csharp
.WithGlobalFont(new Font("Times New Roman", 14F));
```

Now that you have an `InvoiceConfigurationDefinition`, you can customize every aspect of your PDF invoice using the following list of methods:

### `ConfigureHeader()`

Configures the header of the invoice.

```csharp 
WithTextColor(int r, int g, int b) // Overload
WithTextColor(Color color) // Overload
WithTextColor(string hexColor) // Overload
```
Sets the color of the header text.

### `ConfigureAddress()`

Configures the address components of the invoice. Those components include the beneficiary (seller) and the payer (buyer) details on the top of the invoice.

```csharp
WithHeaders(string sellerHeader, string buyerHeader)
```

Sets the headers of the seller and buyer address areas. 

----
```csharp
.ThatShowsLabels()
```

**OR**

```csharp
.ThatDoesNotShowLabels()
```
Decides weather the renderer will include a label for each field of the address component.

### `ConfigureLogo()`

Configures the logo on the top right corner of the invoice PDF document.

```csharp
WithLogoHeightCm(float logoHeight) // Default is 50f
```
Sets the logo height in centimeters.

___

```csharp
WithLogoFile(string logoFile)
```

Sets the path to the logo file in image format.

### `ConfigureItemTable()`

Configures the table of goods/services.

```csharp
ThatDisplaysItemDescriptions()
```
**OR**

```csharp
ThatDoesNotDisplayItemDescriptions()
```
Determines weather the table will display the item description below each item's name. Use one of the methods, not both. If you use both methods, the last one in the chain will be honored.
___

```csharp
WithHeader(string headerText)
```
Specifies the text of the table header. Do not use it or set the header text to `string.Empty` to display no header.

---
```csharp
WithHeaderColor(int r, int g, int b) // Overload
WithHeaderColor(Color color) // Overload
WithHeaderColor(string hexColor) // Overload
```
Sets the color of the header text.

___

```csharp
WithFont(Font font)
```
Overrides the global font for the given table.

___

```csharp
ThatShowsAlertWithoutItems(string header, string text)
```
Shows an alert instead of a table when there are no goods/services assigned to the invoice.

**OR**

```csharp
ThatDoesNotShowAlertWithoutItems()
```
Will not display anything if there are no goods/services assigned to the invoice. Instead, rendering will be skipped.

### `ConfigurePaymentTable()`

Configures the table with the list of payments.

```csharp
WithHeader(string headerText)
```
Specifies the text of the table header. Do not use it or set the header text to `string.Empty` to display no header.

---
```csharp
WithHeaderColor(int r, int g, int b) // Overload
WithHeaderColor(Color color) // Overload
WithHeaderColor(string hexColor) // Overload
```
Sets the color of the header text.

___

```csharp
WithFont(Font font)
```
Overrides the global font for the given table.

___

```csharp
ThatShowsAlertWithoutItems(string header, string text)
```
Shows an alert instead of a table when there are no payments made to fullfil the invoice.

**OR**

```csharp
ThatDoesNotShowAlertWithoutItems()
```
Will not display anything if there are no goods/services assigned to the invoice. Instead, rendering will be skipped.

### `ConfigureFooter()`

Configures the footer at the end of each page.

```csharp
WithText(string text)
```
Specifies the text of the footer. It can be something like a copyright notice or some information for the invoice receipient.

---

```csharp 
WithTextColor(int r, int g, int b) // Overload
WithTextColor(Color color) // Overload
WithTextColor(string hexColor) // Overload
```
Sets the color of the footer text.

```csharp
WithFont(Font font)
```

### Finalizing the configuration
Once you have fully configured your invoice PDF document to your desire, run the following method to build the configuration, at the end of the statement chain:

```csharp
Build() // Creates an instance of the InvoiceConfiguration model
```

### Example configuration:

```csharp
InvoiceConfiguration configuration = InvoiceConfigurationFactory
    .CreateConfiguration()
    
    .WithGlobalFont(new Font("Calibri"))
    .ConfigureHeader()
    .WithTextColor(Color.LightBlue)
    
    .ConfigureAddress()
    .WithHeaders("Beneficiary", "Receipient")
    .ThatDoesNotShowLabels()
    
    .ConfigureLogo()
    .WithLogoHeightCm(50f)
    .WithLogoFile("C:/Users/Admin/Desktop/Logo.png")
    
    .ConfigureItemTable()
    .ThatDisplaysItemDescriptions()
    .WithHeader("Purchases Goods/Services")
    .ThatShowsAlertWithoutItems("No items!", "You did not purchase any goods or services!")
    
    .ConfigurePaymentTable()
    .ThatShowsAlertWithoutItems("No payments!", "You did not make any payments for this invoice!")
    .WithHeader("Invoice Payments")
    
    .ConfigureFooter()
    .WithText("Made with InvoiceSdk!")
    .WithTextColor(Color.LightBlue)
    
    .Build();
```

## Rendering an invoice into a PDF document

Once you have created your `InvoiceConfiguration` model as shown before, you need to pass this model to an instance of `InvoiceRenderer`, alongside with your `Invoice` model which was created as seen on the first step:

```csharp
IInvoiceRenderer renderer = new InvoiceRenderer();
IDocument document = renderer.RenderInvoice(invoice, configuration);
```

### Save the document on the disk as follows:

* Generate PDF   
```csharp
document.GeneratePdf("C:/Users/Admin/Desktop/Invoice.pdf");
```

* Generate XPS   
```csharp
document.GenerateXps("C:/users/delir/desktop/Invoice.xps");
 ```
 
# Contributing

## Found an issue?

Please report any issues you have found by [creating a new issue](https://github.com/Codeh4ck/InvoiceSdk/issues). We will review the case and if it is indeed a problem with the code, I will try to fix it as soon as possible. I want to maintain a healthy and bug-free standard for our code. Additionally, if you have a solution ready for the issue please submit a pull request. 

## Submitting pull requests

Before submitting a pull request to the repository please ensure the following:

* Your code follows the naming conventions [suggested by Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
* Your code works flawlessly, is fault tolerant and it does not break the library or aspects of it
* Your code follows proper object oriented design principles. Use interfaces!

Your code will be reviewed and if it is found suitable it will be merged. Please understand that the final decision always rests with me. By submitting a pull request you automatically agree that I hold the right to accept or deny a pull request based on my own criteria.

## Contributor License Agreement

By contributing your code to Streamliner you grant Nikolas Andreou a non-exclusive, irrevocable, worldwide, royalty-free, sublicenseable, transferable license under all of Your relevant intellectual property rights (including copyright, patent, and any other rights), to use, copy, prepare derivative works of, distribute and publicly perform and display the Contributions on any licensing terms, including without limitation: (a) open source licenses like the MIT license; and (b) binary, proprietary, or commercial licenses. Except for the licenses granted herein, You reserve all right, title, and interest in and to the Contribution.

You confirm that you are able to grant us these rights. You represent that you are legally entitled to grant the above license. If your employer has rights to intellectual property that you create, You represent that you have received permission to make the contributions on behalf of that employer, or that your employer has waived such rights for the contributions.

You represent that the contributions are your original works of authorship and to your knowledge, no other person claims, or has the right to claim, any right in any invention or patent related to the contributions. You also represent that you are not legally obligated, whether by entering into an agreement or otherwise, in any way that conflicts with the terms of this license.

Nikolas Andreou acknowledges that, except as explicitly described in this agreement, any contribution which you provide is on an "as is" basis, without warranties or conditions of any kind, either express or implied, including, without limitation, any warranties or conditions of title, non-infringement, merchantability, or fitness for a particular purpose.