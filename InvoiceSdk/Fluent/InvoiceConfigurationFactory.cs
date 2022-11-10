using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public static class InvoiceConfigurationFactory
    {
        public static InvoiceConfigurationDefinition CreateConfiguration() => new(new InvoiceConfiguration());
    }
}
