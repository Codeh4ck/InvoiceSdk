using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using InvoiceSdk.Models.Payments;

namespace InvoiceSdk.Renderer.Components;

internal class AddressComponent : IComponent
{
    private readonly Address _address;
    private readonly string _title;
    private readonly bool _showLabels;

    public AddressComponent(Address address, string title, bool showLabels = false)
    {
        _address = address ?? throw new ArgumentNullException(nameof(address));

        _title = title;
        _showLabels = showLabels;
    }

    public void Compose(IContainer container)
    {
        container.ShowEntire().Column(column =>
        {
            column.Spacing(2);

            column.Item().Text(_title).SemiBold();
            column.Item().PaddingBottom(5).LineHorizontal(1);

            if (!_showLabels)
            {
                column.Item().Text(_address.CompanyName);
                column.Item().Text($"{_address.Street}, {_address.ZipCode}");
                column.Item().Text($"{_address.City}, {_address.State} ({_address.Country})");
                column.Item().Text(_address.Email);
                column.Item().Text(_address.Phone);
            }
            else
            {
                column.Item().Text(name =>
                {
                    name.Span("Name:").Underline().Bold();
                    name.Span($" {_address.CompanyName}");
                });

                column.Item().Text(street =>
                {
                    street.Span("Street:").Underline().Bold();
                    street.Span($" {_address.Street}, {_address.ZipCode}");
                });

                column.Item().Text(location =>
                {
                    location.Span("Location:").Underline().Bold();
                    location.Span($" {_address.City}, {_address.State} ({_address.Country})");
                });

                column.Item().Text(email =>
                {
                    email.Span("E-mail:").Underline().Bold();
                    email.Span($" {_address.Email}");
                });

                column.Item().Text(phone =>
                {
                    phone.Span("Phone:").Underline().Bold();
                    phone.Span($" {_address.Phone}");
                });
            }
        });
    }
}