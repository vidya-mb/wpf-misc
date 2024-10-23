
namespace ImageRedef.Fluent.Helpers;

public sealed class ThemeModeToIconGlyphConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string iconGlyph = "\uF140";
        if(value is ThemeMode themeMode)
        {
            switch(themeMode.Value)
            {
                case "Light":
                    iconGlyph = "\uE706";
                    break;
                case "Dark":
                    iconGlyph = "\uE708";
                    break;
                case "System":
                    iconGlyph = "\uE793";
                    break;
                default:
                    iconGlyph = "\uF140";
                    break;
            }
        }
        return iconGlyph;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}