using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnyYazilim
{
    internal class CustomFontResolver : IFontResolver
    {
        public string FontFamily { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public string DefaultFontName => "Arial";

        public byte[] GetFont(string faceName)
        {
            using (var fs = new FileStream("c:\\windows\\fonts\\arial.ttf", FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo(familyName, isBold, isItalic);
        }
    }
}
