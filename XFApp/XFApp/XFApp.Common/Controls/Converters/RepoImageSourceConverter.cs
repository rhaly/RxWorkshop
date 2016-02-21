using System;
using System.Globalization;
using Xamarin.Forms;
using XFApp.Common.Model;

namespace XFApp.Common.Controls
{
    public class RepoImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (typeof(bool) != value.GetType())
                throw new ArgumentException("Expected bool as value", "value");

            bool v = (bool) value;

            if (v)
                return new OnPlatform<ImageSource>
                {
                    Android = ImageSource.FromFile("ic_grade_black_24dp.png"),
                    WinPhone = ImageSource.FromFile("Assets/ic_grade_black_24dp_2x.png")
                };

            return new OnPlatform<ImageSource>
            {
                Android = ImageSource.FromFile("ic_star_border_black_24dp.png"),
                WinPhone = ImageSource.FromFile("Assets/ic_grade_black_24dp_2x.png")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}