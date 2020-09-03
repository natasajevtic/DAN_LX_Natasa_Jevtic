using System.Windows;

namespace Zadatak_1.Validations
{
    class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty OldIdCardNumberProperty =
        DependencyProperty.Register("OldIdCardNumber", typeof(string),
        typeof(Wrapper), new PropertyMetadata(string.Empty));

        public string OldIdCardNumber
        {
            get { return (string)GetValue(OldIdCardNumberProperty); }
            set { SetValue(OldIdCardNumberProperty, value); }
        }

        public static readonly DependencyProperty OldPhoneNumberProperty =
        DependencyProperty.Register("OldPhoneNumber", typeof(string),
        typeof(Wrapper), new PropertyMetadata(string.Empty));

        public string OldPhoneNumber
        {
            get { return (string)GetValue(OldPhoneNumberProperty); }
            set { SetValue(OldPhoneNumberProperty, value); }
        }

        public static readonly DependencyProperty OldJmbgProperty =
        DependencyProperty.Register("OldJmbg", typeof(string),
        typeof(Wrapper), new PropertyMetadata(string.Empty));

        public string OldJmbg
        {
            get { return (string)GetValue(OldJmbgProperty); }
            set { SetValue(OldJmbgProperty, value); }
        }
    }
}
