namespace Gu.Wpf.Geometry.Demo.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ValueConversion(typeof(FrameworkElement), typeof(Rect))]
    public sealed class ElementToBoundsRectConverter : MarkupExtension, IValueConverter
    {
        public Type AncestorType { get; set; }

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var element = (FrameworkElement)value;
            DependencyObject parent = element.Parent;
            while (parent != null)
            {
                if (parent.GetType() == this.AncestorType)
                {
                    return element.TransformToVisual((Visual)parent).TransformBounds(new Rect(element.DesiredSize));
                }

                parent = element.Parent;
            }

            throw new InvalidOperationException("Did not find parent.");
        }

        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotSupportedException($"{nameof(ElementToBoundsRectConverter)} can only be used in OneWay bindings");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
