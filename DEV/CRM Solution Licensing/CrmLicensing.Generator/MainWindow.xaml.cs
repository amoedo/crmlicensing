using Org.BouncyCastle.Crypto.Signers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
using System.Xml;

namespace CrmLicensing.Generator
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string fileName = "keys.xml";
        private string key;
        private RSACryptoServiceProvider csp;
        //private ECDsaCng csp;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Generate(object sender, RoutedEventArgs e)
        {
            
            byte[] code = csp.SignData(System.Text.Encoding.UTF8.GetBytes(TextToSign.Text.ToCharArray(), 0, TextToSign.Text.Length), "SHA256");

            LicenseKeyText.Text = Convert.ToBase64String(code);           
        }

        private void Button_Click_LoadKey(object sender, RoutedEventArgs e)
        {
            if (File.Exists(fileName))
            {
                string Key = File.ReadAllText(fileName);
                csp = new RSACryptoServiceProvider();
                csp.FromXmlString(Key);
            }
            else            
            {
                csp = new RSACryptoServiceProvider(2048);
                key = csp.ToXmlString(true);
                File.WriteAllText(fileName, key);
            }

            PublicKey.Text = csp.ToXmlString(false);
        }

        private void Button_Click_Validate(object sender, RoutedEventArgs e)
        {
            var tempCsp = new RSACryptoServiceProvider();
            tempCsp.FromXmlString(PublicKey.Text);


            if (tempCsp.VerifyData(
                System.Text.Encoding.UTF8.GetBytes(TextToSign.Text.ToCharArray(), 0, TextToSign.Text.Length)
                , "SHA256"
                , Convert.FromBase64String(LicenseKeyText.Text)))
            {
                ResultText.Text = "Valid";
                ResultText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ResultText.Text = "Not Valid";
                ResultText.Foreground = new SolidColorBrush(Colors.Red);
            }

        }

    }

    //public static class Extensions
    //{
    //    public static byte[] SignData(this ECDsa csp, byte[] secret, string algorithm)
    //    {
    //        var hash = HashAlgorithm.Create(algorithm);
    //        var hashedSecret = hash.ComputeHash(secret);

    //        return csp.SignHash(hashedSecret);

    //    }

    //    public static bool VerifyData(this ECDsa csp, byte[] secret, string algorithm, byte[] signature)
    //    {
    //        var hash = HashAlgorithm.Create(algorithm);
    //        var hashedSecret = hash.ComputeHash(secret);

    //        return csp.VerifyHash(hashedSecret,signature);

    //    }

    //}
}
