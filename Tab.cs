using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace SnakeGameWpf
{
    internal class Tab
    {
        Size win;
        Theme curTheme;
        Canvas icons;
        Image img1, img2, brushImg;
        TextBlock txtCur, txtMax;
        Grid toThemePage;
        public Button themeButton= new Button();

        public Tab(Size winSize, Theme curTheme) { 
            this.win = winSize;
            this.curTheme = curTheme;
        }
        public Canvas ShowTab(int CurrentPoints, int MaxPoints)
        {
            icons = new Canvas()
            {
                Width = win.Width,
                Height = win.Height * 0.12,
                Background = new SolidColorBrush(curTheme.BackGroundMain)
            };
            img1 = new Image
            {
                Source = curTheme.CurrIcon,
                Width = win.Height * 0.08,
                Height = win.Height * 0.08,
                VerticalAlignment = VerticalAlignment.Center
            };
            img2 = new Image
            {
                Source = curTheme.MaxIcon,
                Width = win.Height * 0.08,
                Height = win.Height * 0.08,
                VerticalAlignment = VerticalAlignment.Center
            };
            txtCur = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Width = win.Width * 0.1,
                Height = win.Height * 0.08,
                FontSize = win.Height * 0.065,
                Text = CurrentPoints.ToString(),
                Foreground = new SolidColorBrush(Colors.White)
            };
            txtMax = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Width = win.Width * 0.1,
                Height = win.Height * 0.08,
                FontSize = win.Height * 0.065,
                Text = MaxPoints.ToString(),
                Foreground = new SolidColorBrush(Colors.White)
            };
            toThemePage = new Grid
            {
                Width = win.Height * 0.09,
                Height = win.Height * 0.09,
                VerticalAlignment = VerticalAlignment.Center,
            };
            brushImg = new Image
            {
                Source = (ImageSource)new ImageSourceConverter().ConvertFromString("images/brush.png"),
                Width = win.Height * 0.09,
                Height = win.Height * 0.09,
                VerticalAlignment = VerticalAlignment.Center
            };
            themeButton= new Button
            {
                Width = win.Height * 0.09,
                Height = win.Height * 0.09,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Colors.White) { Opacity=0},
                BorderBrush = new SolidColorBrush(Colors.White) { Opacity = 0 }
            };
            toThemePage.Children.Add(brushImg);
            toThemePage.Children.Add(themeButton);
            icons.Children.Add(img1);
            icons.Children.Add(img2);
            icons.Children.Add(txtCur);
            icons.Children.Add(txtMax);
            icons.Children.Add(toThemePage);
            Canvas.SetRight(toThemePage, win.Width / 18);
            Canvas.SetLeft(img1, win.Width / 100);
            Canvas.SetLeft(txtCur, win.Width / 100 + win.Height * 0.08 + win.Width * 0.005);
            Canvas.SetLeft(img2, win.Width / 100 + win.Height * 0.08 + win.Width * 0.005 + win.Width / 8.5);
            Canvas.SetLeft(txtMax, win.Width / 100 + win.Height * 0.08 + win.Width * 0.005 + win.Width / 8.5 + win.Height * 0.08);
            return icons;
        }
    }
}
