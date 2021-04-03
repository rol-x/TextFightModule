using System;
using System.Collections.Generic;
using System.Text;

namespace TheAmuletsOfCamembert
{
    /*enum TextType
    {
        Info,
        Neutral,
        Danger,
        Consumable,
        Fight,
        Quest,
        Sub,
        Equipment,
        Backpack
    }*/

    class TextColor
    {
        public void SetColor(string colorCode)
        {
            switch (colorCode)
            {
                case "info":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "neutral":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "danger":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "consumable":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "fight":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "quest":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "sub":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "equipment":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "backpack":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        public void WriteColor(string message, string colorCode)
        {
            ConsoleColor textColor = Console.ForegroundColor;
            SetColor(colorCode);
            Console.Write(message);
            Console.ForegroundColor = textColor;
        }
    }
}
