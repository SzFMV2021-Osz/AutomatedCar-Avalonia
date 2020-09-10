using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace AutomatedCar.Visualization {
    public class WorldObjectTransformer : IValueConverter {
        public static WorldObjectTransformer Instance { get; } = new WorldObjectTransformer ();

        private static Dictionary<string, Bitmap> _cache = new Dictionary<string, Bitmap> ();

        static Bitmap GetCachedImage (string filename) {
            if (!_cache.ContainsKey (filename)) {
                _cache.Add (filename, new Bitmap (Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"AutomatedCar.Assets.WorldObjects.{filename}")));
            }
            return _cache[filename];
        }
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture) => GetCachedImage ((string) value);

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException ();
        }
    }
}