using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceSdk.Renderer.Components
{
    internal class NoteComponent : IComponent
    {
        private readonly string _note;
        public NoteComponent(string note)
        {
            if (string.IsNullOrEmpty(note)) throw new ArgumentNullException(nameof(note));
            _note = note;
        }

        public void Compose(IContainer container)
        {
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Note").FontSize(14).SemiBold();
                column.Item().Text(_note);
            });
        }
    }
}
