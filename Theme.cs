using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeGameWpf
{
    public class Theme
    {
        public Color BackGroundMain, BackGroundSecond,FieldColor1,FieldColor2, SnakeColor;
        public ImageSource CurrIcon, MaxIcon;
        public string Name;
        public Theme(Color BackGroundMain, Color BackGroundSecond, Color FieldColor1 , Color FieldColor2, Color SnakeColor, string CurrIcon)
        { 
            this.BackGroundMain = BackGroundMain;
            this.BackGroundSecond = BackGroundSecond;
            this.FieldColor1 = FieldColor1;
            this.FieldColor2 = FieldColor2;
            this.SnakeColor = SnakeColor;
            this.CurrIcon = (ImageSource)new ImageSourceConverter().ConvertFromString($"images/{CurrIcon}.png");
            MaxIcon= (ImageSource)new ImageSourceConverter().ConvertFromString($"images/trophey.png");
            Name = string.Concat(CurrIcon[0].ToString().ToUpper(), CurrIcon.AsSpan(1));
        }
    }
}
