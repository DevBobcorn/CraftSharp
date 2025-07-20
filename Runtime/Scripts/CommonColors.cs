using System.IO;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// 16 common dye colors in Minecraft
    /// <br/>
    /// See https://minecraft.wiki/w/Dye#Color_values
    /// </summary>
    public enum CommonColors
    {
        White,
        Orange,
        Magenta,
        LightBlue,
        Yellow,
        Lime,
        Pink,
        Gray,
        LightGray,
        Cyan,
        Purple,
        Blue,
        Brown,
        Green,
        Red,
        Black
    }

    public static class CommonColorsHelper
    {
        private static readonly Color32 WHITE      = new(249, 255, 254, 255);
        private static readonly Color32 ORANGE     = new(249, 128, 29,  255);
        private static readonly Color32 MAGENTA    = new(199, 78,  189, 255);
        private static readonly Color32 LIGHT_BLUE = new(58,  179, 218, 255);
        private static readonly Color32 YELLOW     = new(254, 216, 61,  255);
        private static readonly Color32 LIME       = new(128, 199, 31,  255);
        private static readonly Color32 PINK       = new(243, 139, 170, 255);
        private static readonly Color32 GRAY       = new(71,  79,  82,  255);
        private static readonly Color32 LIGHT_GRAY = new(157, 157, 151, 255);
        private static readonly Color32 CYAN       = new(22,  156, 156, 255);
        private static readonly Color32 PURPLE     = new(137, 50,  184, 255);
        private static readonly Color32 BLUE       = new(60,  68,  170, 255);
        private static readonly Color32 BROWN      = new(131, 84,  50,  255);
        private static readonly Color32 GREEN      = new(94,  124, 22,  255);
        private static readonly Color32 RED        = new(176, 46,  38,  255);
        private static readonly Color32 BLACK      = new(29,  29,  33,  255);
        
        public static CommonColors GetCommonColor(string colorName)
        {
            return colorName switch
            {
                "white" => CommonColors.White,
                "orange" => CommonColors.Orange,
                "magenta" => CommonColors.Magenta,
                "light_blue" => CommonColors.LightBlue,
                "yellow" => CommonColors.Yellow,
                "lime" => CommonColors.Lime,
                "pink" => CommonColors.Pink,
                "gray" => CommonColors.Gray,
                "light_gray" => CommonColors.LightGray,
                "cyan" => CommonColors.Cyan,
                "purple" => CommonColors.Purple,
                "blue" => CommonColors.Blue,
                "brown" => CommonColors.Brown,
                "green" => CommonColors.Green,
                "red" => CommonColors.Red,
                "black" => CommonColors.Black,
                
                _ => throw new InvalidDataException($"Common color with name {colorName} is not defined!")
            };
        }

        public static Color32 GetColor32(this CommonColors color)
        {
            return color switch
            {
                CommonColors.White => WHITE,
                CommonColors.Orange => ORANGE,
                CommonColors.Magenta => MAGENTA,
                CommonColors.LightBlue => LIGHT_BLUE,
                CommonColors.Yellow => YELLOW,
                CommonColors.Lime => LIME,
                CommonColors.Pink => PINK,
                CommonColors.Gray => GRAY,
                CommonColors.LightGray => LIGHT_GRAY,
                CommonColors.Cyan => CYAN,
                CommonColors.Purple => PURPLE,
                CommonColors.Blue => BLUE,
                CommonColors.Brown => BROWN,
                CommonColors.Green => GREEN,
                CommonColors.Red => RED,
                CommonColors.Black => BLACK,
                _ => throw new InvalidDataException($"Common color {color} is not defined!")
            };
        }
        
        public static string GetName(this CommonColors color)
        {
            return color switch
            {
                CommonColors.White => "white",
                CommonColors.Orange => "orange",
                CommonColors.Magenta => "magenta",
                CommonColors.LightBlue => "light_blue",
                CommonColors.Yellow => "yellow",
                CommonColors.Lime => "lime",
                CommonColors.Pink => "pink",
                CommonColors.Gray => "gray",
                CommonColors.LightGray => "light_gray",
                CommonColors.Cyan => "cyan",
                CommonColors.Purple => "purple",
                CommonColors.Blue => "blue",
                CommonColors.Brown => "brown",
                CommonColors.Green => "green",
                CommonColors.Red => "red",
                CommonColors.Black => "black",
                _ => throw new InvalidDataException($"Common color {color} is not defined!")
            };
        }
    }
}