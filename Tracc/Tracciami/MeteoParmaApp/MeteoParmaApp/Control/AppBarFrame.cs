using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TracciamiApp.Control
{
    public class AppBarFrame : Frame
    {
        public AppBarFrame()
        {
            this.CornerRadius = 2;
            this.VerticalOptions = LayoutOptions.Fill;
            this.HorizontalOptions = LayoutOptions.Fill;
            this.BackgroundColor = Color.Transparent;
            this.Padding = new Thickness(0);
            this.HasShadow = false;
            this.BorderColor = Color.DarkBlue;

        }
    }
}
