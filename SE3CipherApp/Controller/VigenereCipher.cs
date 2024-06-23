using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3CipherApp
{
    public static class VigenereCipher
    {
        // The set of characters used in the cipher
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,:\"!? ";

        // Encrypts the input text using the Vigenère cipher with the provided key
        public static string Encrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            // Iterate through each character in the input text
            foreach (char c in text)
            {
                int charIndex = Alphabet.IndexOf(c);
                if (charIndex < 0) continue; // Skip characters not in the alphabet

                int keyCharIndex = Alphabet.IndexOf(key[keyIndex]);
                // Calculate the encrypted character index
                int encryptedIndex = (charIndex + keyCharIndex) % Alphabet.Length;
                result.Append(Alphabet[encryptedIndex]);

                // Move to the next character in the key
                keyIndex = (keyIndex + 1) % key.Length;
            }
            return result.ToString();
        }

        // Decrypts the input text using the Vigenère cipher with the provided key
        public static string Decrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;

            // Iterate through each character in the encrypted text
            foreach (char c in text)
            {
                int charIndex = Alphabet.IndexOf(c);
                if (charIndex < 0) continue; // Skip characters not in the alphabet

                int keyCharIndex = Alphabet.IndexOf(key[keyIndex]);
                // Calculate the decrypted character index
                int decryptedIndex = (charIndex - keyCharIndex + Alphabet.Length) % Alphabet.Length;
                result.Append(Alphabet[decryptedIndex]);

                // Move to the next character in the key
                keyIndex = (keyIndex + 1) % key.Length;
            }
            return result.ToString();
        }
    }
}
