using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IGameDataSerializer : IGeneralDataSerializer
    {

        /// <summary>
        /// Десериализует строку, разделённую запятыми в enum игровых платформ.
        /// </summary>
        /// <param name="platforms">Строка для десериализации.</param>
        /// <returns>Список, хранящий формат EnumPlatforms.</returns>
        public List<EnumPlatforms> DeserializeToEnumOfPlatforms(string platforms);
    }
}
