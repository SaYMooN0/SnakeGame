using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
namespace SnakeGameWpf
{
    public class ThemeContainer
    {

        public Theme curTheme;
        Theme themeDef = new Theme(
            (Color)ColorConverter.ConvertFromString("#4A752C"),
            (Color)ColorConverter.ConvertFromString("#578A34"),
            (Color)ColorConverter.ConvertFromString("#AAD751"),
            (Color)ColorConverter.ConvertFromString("#A2D149"),
            (Color)ColorConverter.ConvertFromString("#4774EA"),
            "apple");
        Theme theme2 = new Theme(
            (Color)ColorConverter.ConvertFromString("#125a57"),
            (Color)ColorConverter.ConvertFromString("#1d908c"),
            (Color)ColorConverter.ConvertFromString("#25b5af"),
            (Color)ColorConverter.ConvertFromString("#66cbc7"),
            (Color)ColorConverter.ConvertFromString("#F57170"),
            "peach");
        Theme theme3 = new Theme(
            (Color)ColorConverter.ConvertFromString("#6b4b31"),
            (Color)ColorConverter.ConvertFromString("#916643"),
            (Color)ColorConverter.ConvertFromString("#ab784e"),
            (Color)ColorConverter.ConvertFromString("#b88154"),
            (Color)ColorConverter.ConvertFromString("#331c16"),
            "avocado");
        Theme theme4 = new Theme(
            (Color)ColorConverter.ConvertFromString("#7b5d81"),
            (Color)ColorConverter.ConvertFromString("#b18f96"),
            (Color)ColorConverter.ConvertFromString("#d1b5b3"),
            (Color)ColorConverter.ConvertFromString("#e6ccc6"),
            (Color)ColorConverter.ConvertFromString("#211e31"),
            "plum");
        Theme theme5 = new Theme(
            (Color)ColorConverter.ConvertFromString("#3ca156"),
            (Color)ColorConverter.ConvertFromString("#94dd9d"),
            (Color)ColorConverter.ConvertFromString("#eee8aa"),
            (Color)ColorConverter.ConvertFromString("#fff8dc"),
            (Color)ColorConverter.ConvertFromString("#211e31"),
            "pear");
        List<Theme> allThemes = new List<Theme> ();

        public Button btArrow = new Button();
        public ThemeContainer()
        {
            curTheme = themeDef;
            allThemes = new List<Theme> { themeDef, theme2, theme3, theme4, theme5 };
        }
        public Canvas ShowThemePage(System.Windows.Size w)
        {
            System.Windows.Size win = w;
            Canvas can = new Canvas
            {
                Width = win.Width,
                Height = win.Height,
                Background = new SolidColorBrush(curTheme.BackGroundMain)
            };
            double mySize;
            if (win.Height > win.Width)
                mySize = win.Width*0.75;
            else
                mySize = win.Height*0.75;

            Grid BackArrow = new Grid
            {
                Width = win.Height / 10,
                Height = win.Height /10
            };
            Image imgArrow = new Image
            {
                Source = (ImageSource)new ImageSourceConverter().ConvertFromString("images/back.png"),
                Width = BackArrow.Height,
                Height = BackArrow.Height,
                VerticalAlignment = VerticalAlignment.Center
            };
            btArrow = new Button
            {
                Width = BackArrow.Height,
                Height = BackArrow.Height,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Colors.White) { Opacity = 0 },
                BorderBrush = new SolidColorBrush(Colors.White) { Opacity = 0 }
            };
            BackArrow.Children.Add(imgArrow);
            BackArrow.Children.Add(btArrow);
            can.Children.Add(BackArrow);
            Canvas.SetRight(BackArrow, BackArrow.Width);
            Grid content = new Grid()
            {
                Width = win.Width*0.9,
                Height = win.Height * 0.75,
                Background = new SolidColorBrush(curTheme.BackGroundSecond)
            };
            content.ColumnDefinitions.Add(new ColumnDefinition {Width= new GridLength(win.Width * 0.8 / 3.8) });
            content.ColumnDefinitions.Add(new ColumnDefinition {Width= new GridLength(win.Width * 0.8 / 2) });
            content.ColumnDefinitions.Add(new ColumnDefinition {Width= new GridLength(win.Width * 0.8 / 3.7) });
            content.RowDefinitions.Add(new RowDefinition { Height= new GridLength(mySize/5)});
            content.RowDefinitions.Add(new RowDefinition { Height= new GridLength(mySize/5)});
            content.RowDefinitions.Add(new RowDefinition { Height= new GridLength(mySize/5)});
            content.RowDefinitions.Add(new RowDefinition { Height= new GridLength(mySize/5)});
            content.RowDefinitions.Add(new RowDefinition { Height= new GridLength(mySize/5)});
            Viewbox vb = new Viewbox { Height = mySize / 10, Width = mySize / 2, };
            for (int i = 0; i < allThemes.Count; i++)
            {
                RadioButton t1 = new RadioButton { Content = allThemes[i].Name, GroupName = "themes"};
                t1.Checked += SetTheme;
                t1.FontWeight = FontWeights.Bold;
                t1.FontFamily = new FontFamily("Comic Sans MS");
                if (allThemes[i] == curTheme)
                    t1.IsChecked = true;
                Viewbox vb1 = new Viewbox { Height = mySize / 10, Width = mySize / 2.6 , HorizontalAlignment= HorizontalAlignment.Left};
                vb1.Child = t1;
                content.Children.Add(vb1);
                Grid.SetColumn(vb1, 0);
                Grid.SetRow(vb1, i);
                Grid colors = new Grid { Width= win.Width * 0.8 / 2, Height= mySize / 5, Background= new SolidColorBrush(Colors.White) };
                colors.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colors.Width / 4) });
                colors.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colors.Width/ 4) });
                colors.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colors.Width / 4) });
                colors.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(colors.Width / 4) });
                Rectangle b1 = new Rectangle { Width= colors.Width / 4-10, Height=mySize/5 - 10, Fill = new SolidColorBrush(allThemes[i].BackGroundMain), RadiusX=6 , RadiusY=6  };
                Rectangle b2 = new Rectangle { Width= colors.Width / 4 - 10, Height=mySize/5 - 10, Fill = new SolidColorBrush(allThemes[i].BackGroundSecond), RadiusX = 6, RadiusY = 6 };
                Rectangle f1 = new Rectangle { Width= colors.Width / 4 - 10, Height=mySize/5 - 10, Fill = new SolidColorBrush(allThemes[i].FieldColor1), RadiusX = 6, RadiusY = 6 };
                Rectangle f2= new Rectangle { Width= colors.Width / 4 - 10, Height=mySize/5 - 10, Fill = new SolidColorBrush(allThemes[i].FieldColor2), RadiusX = 6, RadiusY = 6 };
                colors.Children.Add(b1);
                Grid.SetColumn(b1, 0);
                colors.Children.Add(b2);
                Grid.SetColumn(b2, 1);
                colors.Children.Add(f1);
                Grid.SetColumn(f1, 2);
                colors.Children.Add(f2);
                Grid.SetColumn(f2, 3);
                content.Children.Add(colors);
                Grid.SetColumn(colors, 1);
                Grid.SetRow(colors, i);
                Grid IconAndSnake = new Grid { Width = win.Width * 0.8 / 3.7, Height = mySize / 5, Background = new SolidColorBrush(Colors.White) };
                IconAndSnake.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(IconAndSnake.Width / 2) });
                IconAndSnake.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(IconAndSnake.Width / 2) });
                Rectangle snakeColor = new Rectangle { Width = IconAndSnake.Width / 2 - 10, Height = mySize / 5 - 10, Fill = new SolidColorBrush(allThemes[i].SnakeColor), RadiusX = 6, RadiusY = 6 };
                IconAndSnake.Children.Add(snakeColor);
                Grid.SetColumn(snakeColor, 1);
                Image Icon = new Image { Source = allThemes[i].CurrIcon };
                IconAndSnake.Children.Add(Icon);
                Grid.SetColumn(Icon, 0);
                Grid.SetRow(Icon, i);
                content.Children.Add(IconAndSnake);
                Grid.SetColumn(IconAndSnake, 2);
                Grid.SetRow(IconAndSnake, i);

            }
            can.Children.Add(content);
            Canvas.SetLeft(content,win.Width/2-content.Width/2 );
            Canvas.SetTop(content,win.Height/2-content.Height/1.9 );
            return can;
        }
        private void SetTheme(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            for (int i = 0; i < allThemes.Count; i++)
            {
                if (pressed.Content == allThemes[i].Name)
                { curTheme = allThemes[i]; return; }
            }    
        }
    }
}
