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
    // View model that handles the logic for the main application view
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _key;
        private string _outputText;
        private ObservableCollection<EncryptionModel> _cipherRecords;

        // Property for binding to the input text in the UI
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        // Property for binding to the encryption key in the UI
        public string Key
        {
            get => _key;
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        // Property for binding to the output text in the UI
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }

        // Property for binding to the collection of recent encryption records in the UI
        public ObservableCollection<EncryptionModel> CipherRecords
        {
            get { return _cipherRecords; }
            set
            {
                _cipherRecords = value;
                OnPropertyChanged(nameof(CipherRecords));
            }
        }

        // Commands for encrypting/decrypting the input text
        public ICommand EncryptCommand { get; }
        public ICommand DecryptCommand { get; }

        public MainViewModel()
        {
            EncryptCommand = new RelayCommand(Encrypt);
            DecryptCommand = new RelayCommand(Decrypt);
            CipherRecords = new ObservableCollection<EncryptionModel>(DatabaseHelper.GetLastFiveRecords());
        }

        // Event that notifies when a property value changes
        public event PropertyChangedEventHandler PropertyChanged;

        // Invokes the PropertyChanged event to notify of property changes
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Encrypts the input text using the Vigenère cipher and updates output and records
        public void Encrypt()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Key))
                return;

            OutputText = VigenereCipher.Encrypt(InputText, Key); // Perform encryption
            AddCipherRecord(InputText, OutputText, Key); // Add encryption record to database and update UI
        }

        // Decrypts the input text using the Vigenère cipher and updates output and records
        public void Decrypt()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Key))
                return;

            OutputText = VigenereCipher.Decrypt(InputText, Key); // Perform decryption
            AddCipherRecord(InputText, OutputText, Key); // Add decryption record to database and update UI
        }

        // Saves an encryption record to the database
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

        // Adds an encryption record to the database and updates the recent records collection
        private void AddCipherRecord(string inputText, string outputText, string key)
        {
            SaveToDatabase(inputText, outputText, key); // Save encryption record to database

            // Update ObservableCollection with the latest five entries
            var lastFiveRecords = DatabaseHelper.GetLastFiveRecords();
            CipherRecords.Clear();
            foreach (var record in lastFiveRecords)
            {
                CipherRecords.Add(record);
            }
        }
    }
}
