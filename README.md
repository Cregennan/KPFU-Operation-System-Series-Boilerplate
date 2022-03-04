

# КФУ: Операционные системы. Шаблон проекта

![](https://img.shields.io/badge/License-MIT-brightgreen)

##Описание
Все проекты, что делаются на парах по Операционным системам по своей сути представляют из себя  шифратор. Почему бы не создать шаблонный проект, на базе которого можно  будет собирать тот проект, который нужно сдать на этой неделе?

<center>
<img width="700" src="https://sun9-48.userapi.com/impg/ax4tJM0_Fq2anK079XXZD9vJLHyAJ88PamUfpQ/QsVnSx39fXw.jpg?size=1014x718&quality=96&sign=1b72dfb9e0db6d25550e6e6799dd9c23&type=album"  >
</center>

## Возможности
- Поддержка  мультиязычности, новый язык можно добавить всего лишь дописав нужные строки  в параметрах приложения
- Проверки  всех  полей
- Легкое добавление  собственных проверок  полей для конкретных нужд
- Настройка ввода данных в поля, белый список символов
- Автоматическая  фильтрация текста после ввода

## Кастомизация
- Весь функционал можно выключить или включить по своему усмотрению, к  примеру:
```csharp
public static class Settings
    {
		//Требуется ли проверка вставки в текстовое поле
		public static readonly bool IsKeyPastingValidationRequired = true;

		//Требуется ли проверка ввода в текстовое поле
		public static readonly bool IsKeyTypingValidationRequired = true;

		//Требуется ли фильтрация содержимого текстового поля
		public static readonly bool IsKeyFilteringRequired = true;

		//Требуется ли проверка вставки в поле ключа
		public static readonly bool IsTextPastingValidationRequired = true;
		//...
		//...
}
```
- Все определения строк,  регулярных выражений выделены в отдельный  класс. Любые фильтры или строки настраиваются под нужны конкретного проекта
```csharp
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
```
- Легкая конфигурация валидатора
```csharp
public (bool, String, String) ResolveKey(String OldValue)
        {
            if (Settings.IsAdditionalKeyValidationRequired)
            {
                if (Settings.KeyDefaultValueRequired)
                {
                    if (OldValue.Length == 0)
                    {
                        return (false, "Поле для ключа не может быть пустым", OldValue);
                    }
                    else
                    {
                        KeyField.Text = Settings.KeyDefaultValue;
                        return (true, "Всё норм", Settings.KeyDefaultValue);
                    }
                }
                if (Service.KeyFilterRegex.IsMatch(OldValue))
                {
                    return (false, "Ошибка в поле ключа", OldValue);
                }
                //Добавьте здесь свои дополнительные проверки

            }
            return (false, "Всё норм", OldValue);
        }
```

## Как добавить новый язык
В классе `Service`  добавьте:
1. Регулярное выражение для фильтрации текста в `TextFilterRegices`
2.  Регулярное выражение для валидации ввода и вставки в `TextAllowed`
3.  В массив `Alphabets` добавьте новый элемент, перечислите  все символы алфавита требуемого языка
4. В  массив `AlphabetDescriptions` добавьте название алфавита, оно появится в селекторе алфавита

## Как поменять настройки приложения
В  классе `Settings` перечислены все настройки приложения. К каждой переменной прилагается комментарий, описывающий предназначение настройки. Установите значение `true` или `false` в зависимости от того, включить или выключить функционал.
