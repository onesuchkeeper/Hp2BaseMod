// Hp2BaseMod 2021, By OneSuchKeeper

using System.Security.Cryptography;
using System.Text;

namespace Hp2BaseMod.Utility
{
    /// <summary>
    /// Static MD5 utilities
    /// </summary>
    public static class MD5Utility
    {
        /// <summary>
        /// Encrypts the input strig via MD5, same method unity uses
        /// </summary>
        /// <param name="input">String to be encrypted</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string input)
        {
            HashAlgorithm hashAlgorithm = System.Security.Cryptography.MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            byte[] array = hashAlgorithm.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}
