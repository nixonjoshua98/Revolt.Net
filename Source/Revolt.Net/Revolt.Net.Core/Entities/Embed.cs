using Revolt.Net.Core.Common;

namespace Revolt.Net.Core.Entities
{
    public sealed class Embed
    {
        public string IconUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Media { get; set; }
        public RevoltColour Colour { get; set; }
    }
}
