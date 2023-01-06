using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.DirectoryServices.ActiveDirectory;

namespace SnakeGameWpf
{
    internal class SnakeGame
    {
        Window win;
        int MaxPoints, CurrentPoints;
        DispatcherTimer Timer = new DispatcherTimer();
        ThemeContainer Themes;
        bool isThemeShow;
        Tab myTab;
        AllTheLogic ATL;
        public void start()
        {
            Themes = new ThemeContainer();
            CurrentPoints = 1;
            MaxPoints = 0;
            myTab = new Tab(new Size(win.Width, win.Height), Themes.curTheme);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            Timer.Tick += update;
            isThemeShow = false;
            win.SizeChanged += show;
            
        }
        public void update(object sender, EventArgs e)
        {
            showAll();
            CurrentPoints+=2;
            MaxPoints += 2;
        }
        public void show(object sender, EventArgs e)
        {
            if (!isThemeShow)
                showAll();
            else toThemePage();
        }
        public void showAll(object sender, EventArgs e){showAll();}
        public void showAll()
        {
            Timer.Start();
            isThemeShow = false;
            Canvas content = new Canvas()
            {
                Width = win.Width,
                Height = win.Height,
                Background = new SolidColorBrush(Themes.curTheme.BackGroundSecond)
            };
            myTab = new Tab(new Size(win.Width, win.Height), Themes.curTheme);
            Canvas tab = myTab.ShowTab(CurrentPoints, MaxPoints);
            myTab.themeButton.Click += toThemePage;
            Grid field = showField();
            Canvas snake = ATL.showSnake();
            content.Children.Add(tab);
            content.Children.Add(field);
            content.Children.Add(snake);
            Canvas.SetLeft(field, win.Width * 0.5 - field.Width / 2);
            Canvas.SetTop(field, win.Height * 0.52 - field.Height / 2);
            win.Content = content;
        }
        public Grid showField()
        {
            double mySize;
            if (win.Height >win.Width)
                mySize = win.Width * 0.7;
            else
                mySize = win.Height * 0.7;
            Grid content = new Grid()
            {
                Width = mySize,
                Height= mySize,
                Background= new SolidColorBrush(Colors.White)
            };
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j< 16; j++)
                {
                    SolidColorBrush b;
                    if ((i + j) % 2 == 1)
                        b = new SolidColorBrush(Themes.curTheme.FieldColor1);
                    else 
                        b=new SolidColorBrush(Themes.curTheme.FieldColor2);
                    Rectangle rect = new Rectangle
                    {
                        Width = mySize / 13,
                        Height = mySize / 13,
                        Fill = b
                    };
                    content.Children.Add(rect);
                    Grid.SetColumn(rect, i);
                    Grid.SetRow(rect, j);

                    ColumnDefinition c = new ColumnDefinition();
                    c.Width = new GridLength(mySize / 13, GridUnitType.Pixel);
                    content.ColumnDefinitions.Add(c);
                    RowDefinition r = new RowDefinition();
                    r.Height = new GridLength(mySize / 13, GridUnitType.Pixel);
                    content.RowDefinitions.Add(r);
                }
            }
            return content;
        }
        public void toThemePage(object sender, EventArgs e){toThemePage();}
        public void toThemePage()
        {
            isThemeShow = true;
            Timer.Stop();
            win.Content = Themes.ShowThemePage(new Size(win.Width, win.Height));
            Themes.btArrow.Click += showAll;
        }
        public SnakeGame(Window w) { win = w; }
    }
}
