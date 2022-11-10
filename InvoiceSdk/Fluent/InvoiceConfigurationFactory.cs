using InvoiceSdk.Renderer.Configuration;

namespace InvoiceSdk.Fluent
{
    public static class InvoiceConfigurationFactory
    {
        public static HeaderConfigurationDefinition CreateConfiguration() => new(new InvoiceConfiguration());
    }
}
