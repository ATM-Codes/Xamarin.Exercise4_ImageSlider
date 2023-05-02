using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace Exercise4_ImageSlider
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Initally the URI images sources and URI button are selected as default
            Image_Slide.Source = new UriImageSource
            {
                Uri = new Uri(uriImages[Slider_count]),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(3, 0, 0, 0)
            };


            //Image_Slide.Source = ImageSource.FromFile(localImages[0]);

            URI.TextColor = Color.Black; 
            URI.BackgroundColor = Color.White;
            IndicatorText.Text = $"1 OF {uriImages.Length}";
            

            
        }

        //When the app starts the initial image slide source would be the URI version. Using this variable we can change
        // the image source when clicking on the Source Option buttons.

        private string Image_Slide_Source = "URI";


        //slider count - This value is used in the "1 of 3" section, in place of the "1", to indicate the current slide that is
        //being shown. This value will reset to 0 when the new source is chosen

        private int Slider_count = 0;

        //URI IMAGE SOURCE
        //Here, we use the links for the images to be used. An active internet connection is needed to display the images.
        private string[] uriImages = { "https://thumbs.dreamstime.com/b/lion-cartoon-26990518.jpg",
                "https://thumbs.dreamstime.com/b/lion-square-format-12297546.jpg",
                "https://cdn.britannica.com/55/2155-050-604F5A4A/lion.jpg", "https://images.unsplash.com/photo-1614027164847-1b28cfe1df60?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80" };

        //EMBEDDED IMAGE SOURCE
        //We need to create a folder called "images" inside this project. Once the images are added to this folder, we make
        //them as embedded resource by going to the Solution Explorer -> Images ->ImageName.jpg -> File Properties -> Advanced
        // -> Build Action -> Embedded Reource
        private string[] embeddedImages = new string[] { "Exercise4_ImageSlider.Images.eagle1.jpg",
            "Exercise4_ImageSlider.Images.eagle2.jpg",
            "Exercise4_ImageSlider.Images.eagle3.jpg" };

        //LOCAL IMAGE SOURCE
        //The images that need to be used locally are inserted in the drawable foler (Solution Explorer -> Resources -> Drawable)
        // Here, only the file names are given without the extension.
        private string[] localImages = new string[] {"scorpion1","scorpion2"};
        
        //BTN_CLICKED
        //This method is common for the Source Option button (URI, Embedded, Local Button). In this method when the button is
        //clicked it will change the Image_Slide's source, the selected button's text and background color, and set the slider count
        //to zero.
        private void Btn_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            
            switch (button.Text)
            {
                case "URI":
                    Slider_count = 0;
                    IndicatorText.Text = $"{Slider_count + 1} OF {uriImages.Length}";
                    //The URI image source retrieval method is done using the Uri method.
                    //It initializes a new instance of the System.Uri class based on the combination of
                    //a specified base System.Uri instance and a relative System.Uri instance.
                    Image_Slide.Source = new UriImageSource
                    {
                        Uri = new Uri(uriImages[Slider_count]),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(3, 0, 0, 0)
                    };

                    Image_Slide_Source = "URI";
                    URI.TextColor = Color.Black; URI.BackgroundColor = Color.White;
                    Embedded.TextColor = Color.White; Embedded.BackgroundColor = Color.Transparent;
                    Local.TextColor = Color.White; Local.BackgroundColor = Color.Transparent;
                    break;
                case "Embedded":
                    Slider_count = 0;
                    IndicatorText.Text = $"{Slider_count + 1} OF {embeddedImages.Length}";
                    //The Embedded image source retrieval method is done using the ImageSource.FromResource() method.
                    Image_Slide.Source = ImageSource.FromResource(embeddedImages[0]);
                    Image_Slide_Source = "Embedded";
                    Embedded.TextColor = Color.Black; Embedded.BackgroundColor = Color.White;
                    URI.TextColor = Color.White; URI.BackgroundColor = Color.Transparent;
                    Local.TextColor = Color.White; Local.BackgroundColor = Color.Transparent;
                    break;
                case "Local":
                    Slider_count = 0;
                    IndicatorText.Text = $"{Slider_count + 1} OF {localImages.Length}";
                    //The Embedded image source retrieval method is done using the ImageSource.FromFile() method.
                    Image_Slide.Source = ImageSource.FromFile(localImages[0]);
                    Image_Slide_Source = "Local";
                    Local.TextColor = Color.Black; Local.BackgroundColor = Color.White;
                    URI.TextColor = Color.White; URI.BackgroundColor = Color.Transparent;
                    Embedded.TextColor = Color.White; Embedded.BackgroundColor = Color.Transparent;
                    break;
            }

        }


        //This method is for the Prev Button.
        private void Prev_Clicked(object sender, EventArgs e)
        {
            int arrLength = 0;
            //Switch case is necessary to make sure the previous button works based on the source. Each image source might
            //have different number of images

            switch (Image_Slide_Source)
            {
                case "URI": arrLength = uriImages.Length; break;
                case "Embedded": arrLength = embeddedImages.Length; break;
                case "Local": arrLength = localImages.Length; break;
            }

            //This condition is to make sure that we are in the first slide then it should return the last slide.
            if (Slider_count == 0 )
            {
                Slider_count= arrLength-1;
                if(Image_Slide_Source == "URI")
                {
                    //If the Image Source is URI then the Image Slide section will show those images
                    Image_Slide.Source = new UriImageSource
                    {
                        Uri = new Uri(uriImages[Slider_count]),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(3, 0, 0, 0)
                    };
                    IndicatorText.Text = $"{Slider_count + 1} OF {uriImages.Length}";//This changes the number in the indicator text
                }

                //If the Image Source is Embedded then the Image Slide section will show those images
                if (Image_Slide_Source == "Embedded")
                {
                    Image_Slide.Source = ImageSource.FromResource(embeddedImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {embeddedImages.Length}";
                }

                //If the Image Source is Local then the Image Slide section will show those images
                if (Image_Slide_Source == "Local")
                {
                    Image_Slide.Source = ImageSource.FromFile(localImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {localImages.Length}";
                }

            }
            //This condition brings the previous slide.
            else
            {
                Slider_count--;

                if (Image_Slide_Source == "URI")
                {
                    Image_Slide.Source = new UriImageSource
                    {
                        Uri = new Uri(uriImages[Slider_count]),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(3, 0, 0, 0)
                    };

                    IndicatorText.Text = $"{Slider_count + 1} OF {uriImages.Length}";
                }

                if (Image_Slide_Source == "Embedded")
                {
                    Image_Slide.Source = ImageSource.FromResource(embeddedImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {embeddedImages.Length}";
                }

                if (Image_Slide_Source == "Local")
                {
                    Image_Slide.Source = ImageSource.FromFile(localImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {localImages.Length}";
                }

                
            }
        }


        //This method is for the Next Button.
        private void Next_Clicked(object sender, EventArgs e)
        {

            int arrLength = 0;
            //Switch case is necessary to make sure the previous button works based on the source. Each image source might
            //have different number of images

            switch (Image_Slide_Source)
            {
                case "URI": arrLength = uriImages.Length; break;
                case "Embedded": arrLength = embeddedImages.Length; break;
                case "Local": arrLength = localImages.Length; break;
            }

            //This condition is to make sure that if we are in the last slide then it should return back to the first slide.
            if (Slider_count == arrLength-1)
            {
                Slider_count = 0;
                if(Image_Slide_Source == "URI")
                {
                    Image_Slide.Source = new UriImageSource
                    {
                        Uri = new Uri(uriImages[Slider_count]),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(3, 0, 0, 0)
                    };
                    IndicatorText.Text = $"{Slider_count + 1} OF {uriImages.Length}";
                }

                if(Image_Slide_Source == "Embedded")
                {
                    Image_Slide.Source = ImageSource.FromResource(embeddedImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {embeddedImages.Length}";
                }

                if (Image_Slide_Source == "Local")
                {
                    Image_Slide.Source = ImageSource.FromFile(localImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {localImages.Length}";
                }

                
            }
            else
            //This condition brings the next slide.
            {
                Slider_count++;
                if(Image_Slide_Source == "URI")
                {
                    Image_Slide.Source = new UriImageSource
                    {
                        Uri = new Uri(uriImages[Slider_count]),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(3, 0, 0, 0)
                    };
                    IndicatorText.Text = $"{Slider_count + 1} OF {uriImages.Length}";
                }

                if (Image_Slide_Source == "Embedded")
                {
                    Image_Slide.Source = ImageSource.FromResource(embeddedImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {embeddedImages.Length}";

                }
                if (Image_Slide_Source == "Local")
                {
                    Image_Slide.Source = ImageSource.FromFile(localImages[Slider_count]);
                    IndicatorText.Text = $"{Slider_count + 1} OF {localImages.Length}";
                }


            }
        }
    }
}
