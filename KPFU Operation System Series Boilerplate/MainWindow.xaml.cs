using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KPFU_Operation_System_Series_Boilerplate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var s in Service.Operations)
            {
                OperationComboBox.Items.Add(s);
            }
            OperationComboBox.SelectedIndex = 0;
            foreach (var s in Service.AlphabetDescriptions)
            {
                AlphabetComboBox.Items.Add(s);
            }
            AlphabetComboBox.SelectedIndex = 0;
            ClearOut.Click += (object k, RoutedEventArgs t) => ResultTextBox.Clear();
        }

        public (bool, String, String) ResolveKey(String OldValue)
        {
            if (Settings.IsAdditionalKeyValidationRequired)
            {
                if (Settings.KeyDefaultValueRequired)
                {
                    if (OldValue.Length == 0)
                    {
                        return (false, "Поле для сдвига не может быть пустым", OldValue);
                    }
                    else
                    {
                        KeyField.Text = Settings.KeyDefaultValue;
                        return (true, "Всё норм", Settings.KeyDefaultValue);
                    }
                }
                if (Service.KeyFilterRegex.IsMatch(OldValue))
                {
                    return (false, "Ошибка в поле сдвига", OldValue);
                }
                //Добавьте здесь свои дополнительные проверки

            }
            return (false, "Всё норм", OldValue);
        }

        public (bool, String) ResolveMainText(String Text)
        {
            if (Settings.IsAdditionalTextValidationRequired)
            {
                if (Text.Length == 0)
                {
                    return (false, "В тексте нет символов выбранного алфавита");
                }
            }
            return (true, "Всё норм");
        }

        private void OperationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = OperationComboBox.SelectedIndex;
            OperationLabel.Content = Service.OperationLabelStates[index];
            LaunchButton.Content = Service.ButtonStates[index];

        }

        private String Cypher(String Text, String Key, int SelectedAlphabetIndex)
        {
            String Alphabet = Service.Alphabets[SelectedAlphabetIndex];
            //Введите алгоритм шифрования здесь

            return "Результат шифрования";
        }

        private String Decypher(String Text, String Key, int SelectedAlphabetIndex)
        {
            String Alphabet = Service.Alphabets[SelectedAlphabetIndex];
            //Введите алгоритм дешифрования здесь

            return "Результат дешифрования";
        }

        public String FilterKey(String key)
        {
            if (!Settings.IsKeyFilteringRequired) return key;
            return Service.KeyFilterRegex.Replace(key, String.Empty);
        }

        public String FilterText(String text, int AlphabetIndex)
        {
            if (!Settings.IsTextFilteringRequired) return text;
            //Прогон текста через регулярные выражение, приведение регистра к нижнему, убирание любых пробелов и переносов строк
            return Service.TextFilterRegices[AlphabetIndex].Replace(Regex.Replace(MainText.Text.ToLower(), @"\s+", ""), String.Empty);
        }


        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int AlphabetIndex = AlphabetComboBox.SelectedIndex;
                String CurrentAlphabet = Service.Alphabets[AlphabetIndex];

                //Фильтрация ключа и текста
                String FilteredText = FilterText(MainText.Text, AlphabetIndex);
                String FilteredKey = FilterKey(KeyField.Text);

                //Валидация поля ключа
                (bool KeyStatus, String KeyMessage, String NewKey) = ResolveKey(KeyField.Text);
                if (!KeyStatus)
                {
                    MessageBox.Show(this, KeyMessage, "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    return;
                }
                FilteredKey = NewKey;

                //Валидация поля текста
                (bool TextStatus, String TextMessage) = ResolveMainText(KeyField.Text);
                if (!TextStatus)
                {
                    MessageBox.Show(this, TextMessage, "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    return;
                }

                //Выбор операции
                String Result = OperationComboBox.SelectedIndex == 0 ? Cypher(FilteredText, FilteredKey, AlphabetIndex) : Decypher(FilteredText, FilteredKey, AlphabetIndex);

                ResultTextBox.Text = Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка в данных, перепроверьте введенную информацию", "Ошибка");
            }
        }

        private void KeyField_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (Settings.IsTextTypingValidationRequired)
            {
                e.Handled = !Service.KeyAllowed.IsMatch(e.Text);
                return;
            }
            e.Handled = false;
        }

        private void KeyField_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                TextBox tb = (TextBox)sender;
                String Text = ((TextBox)sender).Text;
                String text = (String)e.DataObject.GetData(typeof(String));

                if (Service.KeyAllowed.IsMatch(text) && Settings.IsTextPastingValidationRequired)
                {
                    e.CancelCommand();
                    return;
                }
            }
            else
            {
                e.CancelCommand();
                return;
            }
        }

        private void AlphabetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).IsLoaded && Settings.ClearingKeyOnAlphabetChangedRequired)
            {
                MainText.Clear();
            }
        }

        private void MainText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Settings.IsTextTypingValidationRequired)
            {
                e.Handled = false;
                return;
            }
            Regex currentRegex = Service.TextAllowed[AlphabetComboBox.SelectedIndex];
            e.Handled = !currentRegex.IsMatch(e.Text);
        }

        private void MainText_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!Settings.IsTextPastingValidationRequired)
            {
                return;
            }
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String TextBoxText = ((TextBox)sender).Text;
                String text = (String)e.DataObject.GetData(typeof(String));

                Regex currentRegex = Service.TextAllowed[AlphabetComboBox.SelectedIndex];
                if (!currentRegex.IsMatch(text))
                {
                    e.CancelCommand();
                    return;
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void KeyField_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !Settings.KeySpaceInputDenied)
            {
                e.Handled = true;
            }
        }
    }
}
