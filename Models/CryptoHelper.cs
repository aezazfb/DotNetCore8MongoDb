using System.Security.Cryptography;

namespace testProjectApis.Models
{
    public class CryptoHelper
    {
        public static byte[] GenerateSalt(int size = 32)
        {
            // Initialize a byte array with the specified size
            byte[] salt = new byte[size];

            // Use a cryptographic random number generator to fill the array with random bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public static void Main()
        {
            // Generate a salt and print it as a base64 string
            byte[] salt = GenerateSalt();
            Console.WriteLine(Convert.ToBase64String(salt));
        }
    }
}
