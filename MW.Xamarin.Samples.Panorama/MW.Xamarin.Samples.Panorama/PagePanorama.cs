using MW.Samples.Panorama.MyControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MW.Samples.Panorama
{
    public class PagePanorama : ContentPage
    {
        public PagePanorama()
        {
            PanoramaLayout panorama = new PanoramaLayout() { VerticalOptions = LayoutOptions.CenterAndExpand };
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.00.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.01.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.02.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.03.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.04.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.05.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.06.jpg")
            });
            panorama.Children.Add(new PanoramaCellImage()
            {
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.07.jpg")
            });

            #region Actions de navigation
            Image iActionPrecedent = new Image()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Xamarin.Forms.Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.previous.png")
            };
            iActionPrecedent.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    panorama.OnPrevious();
                })
            });

            Image iActionSuivant = new Image()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Xamarin.Forms.Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Source = ImageSource.FromResource("MW.Samples.Panorama.Images.next.png")
            };
            iActionSuivant.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    panorama.OnNext();
                })
            });

            StackLayout conteneurActions = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { iActionPrecedent, iActionSuivant } };
            #endregion Actions de navigation

            Grid conteneurPage = new Grid() { Children = { panorama, conteneurActions}, Padding = 20};
            conteneurPage.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star)});
            conteneurPage.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto});
            conteneurPage.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            Grid.SetRow(panorama, 0);
            Grid.SetColumn(panorama, 0);
            Grid.SetRow(conteneurActions, 1);
            Grid.SetColumn(conteneurActions, 0);

            this.Content = conteneurPage;
        }
    }
}
