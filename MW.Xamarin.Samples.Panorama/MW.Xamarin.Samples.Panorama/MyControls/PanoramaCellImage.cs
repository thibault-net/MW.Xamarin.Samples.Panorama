using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MW.Samples.Panorama.MyControls
{
    public class PanoramaCellImage : Image
    {
        public PanoramaCellImage()
        {
            HorizontalOptions = LayoutOptions.Fill;
            Aspect = Aspect.AspectFill;
        }

        public PanoramaLayout PanoramaView
        {
            get;
            set;
        }


        public static readonly BindableProperty IsVisiblePanoramaProperty =
         BindableProperty.Create<PanoramaCellImage, bool>(w => w.IsVisiblePanorama, false);

        public bool IsVisiblePanorama
        {
            get { return (bool)GetValue(IsVisiblePanoramaProperty); }
            set { SetValue(IsVisiblePanoramaProperty, value); }
        }

        public static readonly BindableProperty SpacingProperty =
         BindableProperty.Create<PanoramaCellImage, double>(w => w.Spacing, 10, propertyChanged: (bindable, oldValue, newValue) => { (bindable as PanoramaCellImage).PanoramaView.OnSizeChanged(); });

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly BindableProperty PreviousCellProperty =
         BindableProperty.Create<PanoramaCellImage, double>(w => w.PreviousCell, 10, propertyChanged: (bindable, oldValue, newValue) => { (bindable as PanoramaCellImage).PanoramaView.OnSizeChanged(); });

        public double PreviousCell
        {
            get { return (double)GetValue(PreviousCellProperty); }
            set { SetValue(PreviousCellProperty, value); }
        }


        public bool IsLayoutLoad
        {
            get;
            set;
        }
    }
}
