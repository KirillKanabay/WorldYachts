using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace WorldYachts.Helpers
{
    struct ColorStruct
    {
        public string Name { get; set; }
        public Brush Color { get; set; }
    }
    class ColorWorker
    {
        public static Dictionary<string, Color> ColorsDictionary => new Dictionary<string, Color>()
        {
            {"Коричневый", GetColorFromHex("#8C6354")},
            {"Белый", GetColorFromHex("#FFFFFF")},
            {"Черный", GetColorFromHex("#2A2A2A")},
            {"Зеленый", GetColorFromHex("#4CAF50")},
            {"Красный", GetColorFromHex("#F44336")},
            {"Синий", GetColorFromHex("#2196F3")},
        };
        
        /// <summary>
        /// Получение цвета по названию
        /// </summary>
        /// <param name="colorHex"></param>
        /// <returns></returns>
        public static string GetColorName(string colorHex)
        {
            Color color = GetColorFromString(colorHex);
            return ColorsDictionary.SingleOrDefault(c => c.Value == color).Key;
        }
        /// <summary>
        /// Получаем цвет типа Color из строки
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetColorFromString(string color)
        {
            if (ColorsDictionary.Keys.Any(c => c == color))
            {
                return ColorsDictionary[color];
            }
            return GetColorFromHex(color);
        }

        public static ColorStruct GetColorStructFromString(string color)
        {
            var colorDictionary = ColorsDictionary[color];
            return new ColorStruct() { Name = color, Color = new SolidColorBrush(colorDictionary)}
            ;
        }

        public static Color GetColorFromHex(string hex)
        {
            if(!IsHexColor(hex))
                throw new ArgumentException("Неверный формат цвета.");
            return (Color)ColorConverter.ConvertFromString(hex);
        }
        /// <summary>
        /// Проверка строки на цвет
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool IsHexColor(string color)
        {
            Regex regex = new Regex(@"^#((([0-9]|[a-f]|[A-F]){6})|(([0-9]|[a-f]|[A-F]){8}))$");
            return regex.IsMatch(color);
        }

        public static List<ColorStruct> GetColorsCollection()
        {
            var collection = new List<ColorStruct>();
            foreach (var color in ColorsDictionary)
            {
                collection.Add(new ColorStruct(){Name = color.Key, Color = new SolidColorBrush(color.Value)});
            }

            return collection;
        }
    }
}
