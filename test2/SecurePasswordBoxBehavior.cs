using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.InteropServices;

namespace test2
{
    public static class SecurePasswordBoxBehavior
    {
        public static readonly DependencyProperty SecurePasswordProperty =
            DependencyProperty.RegisterAttached("SecurePassword", typeof(SecureString), typeof(SecurePasswordBoxBehavior), new FrameworkPropertyMetadata(null, OnSecurePasswordChanged));

        public static SecureString GetSecurePassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(SecurePasswordProperty);
        }

        public static void SetSecurePassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(SecurePasswordProperty, value);
        }

        private static void OnSecurePasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            if (passwordBox == null)
                return;

            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if (e.NewValue != null)
            {
                passwordBox.Password = SecureStringToString((SecureString)e.NewValue);
            }
            else
            {
                passwordBox.Password = string.Empty;
            }

            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                SetSecurePassword(passwordBox, StringToSecureString(passwordBox.Password));
            }
        }

        private static SecureString StringToSecureString(string password)
        {
            var secureString = new SecureString();
            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        public static string SecureStringToString(SecureString securePassword)
        {
            IntPtr ptr = Marshal.SecureStringToBSTR(securePassword);
            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
