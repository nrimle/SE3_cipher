using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3CipherApp
{
    public static class VigenereCipher
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.,:\"!? ";

        public static string Encrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int charIndex = Alphabet.IndexOf(c);
                if (charIndex < 0) continue;

                int keyCharIndex = Alphabet.IndexOf(key[keyIndex]);
                int encryptedIndex = (charIndex + keyCharIndex) % Alphabet.Length;
                result.Append(Alphabet[encryptedIndex]);

                keyIndex = (keyIndex + 1) % key.Length;
            }
            return result.ToString();
        }

        public static string Decrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int charIndex = Alphabet.IndexOf(c);
                if (charIndex < 0) continue;

                int keyCharIndex = Alphabet.IndexOf(key[keyIndex]);
                int decryptedIndex = (charIndex - keyCharIndex + Alphabet.Length) % Alphabet.Length;
                result.Append(Alphabet[decryptedIndex]);

                keyIndex = (keyIndex + 1) % key.Length;
            }
            return result.ToString();
        }
    }
}
