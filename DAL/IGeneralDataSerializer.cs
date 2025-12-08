using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IGeneralDataSerializer
    {
        /// <summary>
        /// Сериализует список в строку, разделённую запятыми.
        /// </summary>
        /// <param name="items">Список для сериализации.</param>
        /// <returns>Строка, представляющая список в формате string.</returns>
        public string SerializeList<T>(List<T> items);

        /// <summary>
        /// Десериализует строку, разделённую запятыми в список строк.
        /// </summary>
        /// <param name="inputString">Строка для десериализации.</param>
        /// <returns>Список, хранящий формат string.</returns>
        public List<string> DeserializeToListOfStrings(string inputString);
    }
}
