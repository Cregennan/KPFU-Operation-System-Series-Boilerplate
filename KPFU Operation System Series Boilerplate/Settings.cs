using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPFU_Operation_System_Series_Boilerplate
{
    public static class Settings
    {
        //true - включено,  false - выключено

        //Требуется ли проверка вставки в текстовое поле
        public static readonly bool IsKeyPastingValidationRequired = true;

        //Требуется ли проверка ввода в текстовое поле
        public static readonly bool IsKeyTypingValidationRequired = true;

        //Требуется ли фильтрация содержимого текстового поля
        public static readonly bool IsKeyFilteringRequired = true;

        //Требуется ли проверка вставки в поле ключа
        public static readonly bool IsTextPastingValidationRequired = true;

        //Требуется ли проверка ввода в  поле ключа
        public static readonly bool IsTextTypingValidationRequired = true;

        //Требуется ли фильтрация ключа
        public static readonly bool IsTextFilteringRequired = true;

        //Требуется ли дополнительная проверка текста
        public static readonly bool IsAdditionalTextValidationRequired = true;

        //Треубется ли дополнительная проверка ключа
        public static readonly bool IsAdditionalKeyValidationRequired = true;

        //Использовать ли стандартное значение для ключа
        public static readonly bool KeyDefaultValueRequired = true;

        //Стандартное значение ключа
        public static readonly String KeyDefaultValue = "0";

        //Очищать ли поле ключа при смене алфавита
        public static readonly bool ClearingKeyOnAlphabetChangedRequired = true;

        //Запрещать ли ввод пробела в поле ключа
        public static readonly bool KeySpaceInputDenied = true;


    }
}
