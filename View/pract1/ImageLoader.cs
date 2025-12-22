using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace View
{
    public class ImageLoader
    {
        /// <summary>
        /// Загружает изображение из ресурсов по имени.
        /// </summary>
        /// <param name="resourceName">Имя ресурса (без расширения).</param>
        /// <returns>Объект Image или заглушка No_image, если не найдено.</returns>
        public static Image GetImageFromFile(string fileName)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pictures", fileName);
                return File.Exists(path) ? Image.FromFile(path) : Properties.Resources.No_image;
            }
            catch
            {
                return Properties.Resources.No_image;
            }
        }
    }
}
