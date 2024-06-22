using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SE3CipherApp.Models;

namespace SE3CipherApp.Views
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _key;
        private string _outputText;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }

        public ICommand EncryptCommand { get; }
        public ICommand DecryptCommand { get; }

        public MainViewModel()
        {
            EncryptCommand = new RelayCommand(Encrypt);
            DecryptCommand = new RelayCommand(Decrypt);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Encrypt()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Key))
                return;

            OutputText = VigenereCipher.Encrypt(InputText, Key);
            SaveToDatabase(InputText, OutputText, Key);
        }

        public void Decrypt()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Key))
                return;

            OutputText = VigenereCipher.Decrypt(InputText, Key);
            SaveToDatabase(OutputText, InputText, Key);
        }

        private void SaveToDatabase(string plainText, string cipherText, string key)
        {
            var encryption = new EncryptionModel
            {
                EncryptText = cipherText,
                DecryptText = plainText,
                Key = key,
                TimeStamp = DateTime.Now
            };
            DatabaseHelper.SaveEncryption(encryption);
        }
    }
}
