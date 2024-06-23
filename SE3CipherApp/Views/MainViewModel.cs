using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<EncryptionModel> _cipherRecords;

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

        public ObservableCollection<EncryptionModel> CipherRecords
        {
            get { return _cipherRecords; }
            set
            {
                _cipherRecords = value;
                OnPropertyChanged(nameof(CipherRecords));
            }
        }

        public ICommand EncryptCommand { get; }
        public ICommand DecryptCommand { get; }

        public MainViewModel()
        {
            EncryptCommand = new RelayCommand(Encrypt);
            DecryptCommand = new RelayCommand(Decrypt);
            CipherRecords = new ObservableCollection<EncryptionModel>(DatabaseHelper.GetLastFiveRecords());
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
            AddCipherRecord(InputText, OutputText, Key);
        }

        public void Decrypt()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Key))
                return;

            OutputText = VigenereCipher.Decrypt(InputText, Key);
            AddCipherRecord(InputText, OutputText, Key);
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

        private void AddCipherRecord(string inputText, string outputText, string key)
        {
            SaveToDatabase(inputText, outputText, key);

            // Aktualisieren der ObservableCollection mit den letzten 5 Einträgen
            var lastFiveRecords = DatabaseHelper.GetLastFiveRecords();
            CipherRecords.Clear();
            foreach (var record in lastFiveRecords)
            {
                CipherRecords.Add(record);
            }
        }
    }
}
