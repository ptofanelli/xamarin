using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace GalleryTimeline
{
    public class Post
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string ImageBase64 { get; set; }

        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public byte[] ImageBytes { get; set; }

        [JsonIgnore]
        public ImageSource Image { get { return ImageSource.FromStream(() => 
        {
            return new MemoryStream(ImageBytes);
        }); } }

        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }
    }
}
