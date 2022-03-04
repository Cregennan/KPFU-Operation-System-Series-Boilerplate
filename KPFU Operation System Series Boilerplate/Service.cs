using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KPFU_Operation_System_Series_Boilerplate
{
    public static class Service
    {

        //Текст для окна входного текста
        public static readonly String[] OperationLabelStates = {
            "Текст для шифрования (регистр не учитывается)",
            "Текст для дешифрования (регистр не учитывается)"
        };

        //Название операций
        public static readonly String[] Operations = {
            "Шифрование",
            "Дешифрование"
        };

        //Названия алфавитов
        public static readonly String[] AlphabetDescriptions = {
            "Русский",
            "Английский"
        };

        //Надписи для кнопок
        public static readonly String[] ButtonStates = {
            "Зашифровать",
            "Расшифровать"
        };

        //Тексты для окна результата шифрования
        public static readonly String[] ResultWindowTitles = {
            "Результат шифрования",
            "Результат дешифрования"
        };
        public static readonly Regex OnlyNumbers = new Regex("^[0-9]+$");

        //Регулярные выражения для фильтрации текста в текстовом поле
        public static readonly Regex[] TextFilterRegices =
        {
             new Regex("[^а-я0-9ё]"),
             new Regex("[^a-z0-9]")
        };
        //Допустимые символы для ввода в текстовое поле
        public static readonly Regex[] TextAllowed = 
        {
            new Regex(@"^[а-яА-я0-9ёЁ \!\?\n\r.`,\!\?\-\+=()*:;{}—]+$"),
            new Regex(@"^[A-Za-z0-9 \n\r.`,\!\?\-\+=()*:;{}—]+$")
        };

        //Фильтр для поля ключа
        public static readonly Regex KeyFilterRegex = new Regex("^[0-9]+$");

        //Условия ввода в поле ключа
        public static readonly Regex KeyAllowed = new Regex("^[0-9]+$");


        //Наборы допустимых символов для каждого из алфавита
        public static readonly String[] Alphabets =
        {
            "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789",
            "abcdefghijklmnopqrstuvwxyz0123456789"
        };
        public static readonly Regex RussianLetters = new Regex(@"^[а-яё]+$");
        public static readonly Regex EnglishLetters = new Regex(@"^[a-z]+$");


    }
}

