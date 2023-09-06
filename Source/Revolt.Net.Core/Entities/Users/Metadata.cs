using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net
{
    public abstract class Metadata
    {

    }

    public sealed class FileMetadata : Metadata
    {

    }

    public sealed class TextMetadata : Metadata
    {

    }

    public class ImageMetadata : Metadata
    {
        public string Type { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
    }

    public sealed class VideoMetadata : Metadata
    {
        public int Type { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
    }

    public sealed class AudioMetadata : Metadata
    {

    }


}
