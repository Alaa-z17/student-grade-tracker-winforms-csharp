using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Services;

/// <summary>
/// Provides encrypted JSON persistence for student data using AES-256 encryption.
/// All data is saved in an encrypted format and automatically decrypted on load.
/// </summary>
public static class JsonStorageService
{
    // File path where encrypted student data is stored
    private static readonly string FilePath = "students.json";

    // AES-256 key (32 bytes) and IV (16 bytes) - stored as Base64 strings.
    // 🔐 IMPORTANT: In a real application, never hardcode keys in source code.
    // Instead, use environment variables, user password derivation, or Windows DPAPI.
    // These values are placeholders - generate your own using the helper below.
    /*
            byte[] key = RandomNumberGenerator.GetBytes(32);
            byte[] iv = RandomNumberGenerator.GetBytes(16);
            Console.WriteLine($"Key: {Convert.ToBase64String(key)}");
            Console.WriteLine($"IV:  {Convert.ToBase64String(iv)}");
     */
    private static readonly byte[] Key = Convert.FromBase64String("oWe8XBfvhxalKadhOxhpO7N1zq9VMuE6Hk+MwLagDsI=");
    private static readonly byte[] IV = Convert.FromBase64String("O6fS7BvmzW3H1vYV8jyczg==");

    /// <summary>
    /// Saves the list of students to an encrypted JSON file.
    /// </summary>
    /// <param name="students">The list of students to save.</param>
    public static void SaveStudents(List<Student> students)
    {
        // Serialize to pretty-printed JSON
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(students, options);

        // Encrypt the JSON string
        byte[] encrypted = EncryptStringToBytes(json, Key, IV);

        // Write encrypted bytes to disk
        File.WriteAllBytes(FilePath, encrypted);
    }

    /// <summary>
    /// Loads the list of students from an encrypted JSON file.
    /// </summary>
    /// <returns>List of students, or an empty list if the file doesn't exist.</returns>
    public static List<Student> LoadStudents()
    {
        if (!File.Exists(FilePath))
            return new List<Student>();

        // Read encrypted bytes
        byte[] encrypted = File.ReadAllBytes(FilePath);

        // Decrypt back to JSON string
        string json = DecryptStringFromBytes(encrypted, Key, IV);

        // Deserialize to List<Student>, return empty list if null
        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }

    // ======================== AES Encryption Helpers ========================

    /// <summary>
    /// Encrypts a plaintext string using AES-256 (CBC mode, PKCS7 padding).
    /// </summary>
    /// <param name="plainText">The text to encrypt.</param>
    /// <param name="key">32-byte AES key.</param>
    /// <param name="iv">16-byte initialization vector.</param>
    /// <returns>Encrypted byte array.</returns>
    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
    }

    /// <summary>
    /// Decrypts a byte array back to the original plaintext string using AES-256.
    /// </summary>
    /// <param name="cipherText">Encrypted byte array.</param>
    /// <param name="key">32-byte AES key (must match encryption key).</param>
    /// <param name="iv">16-byte initialization vector (must match encryption IV).</param>
    /// <returns>Decrypted plaintext string.</returns>
    /// <exception cref="CryptographicException">Thrown if decryption fails (wrong key/IV/corrupted data).</exception>
    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor();
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }

    // ======================== Key Generation Helper (for setup) ========================
    // Uncomment and run this method ONCE to generate your own random Key and IV.
    // Then copy the Base64 strings into the static fields above.
    // After that, either comment or delete this method.
    /*
    public static void GenerateNewKeyAndIv()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32);
        byte[] iv  = RandomNumberGenerator.GetBytes(16);
        Console.WriteLine($"Key: {Convert.ToBase64String(key)}");
        Console.WriteLine($"IV:  {Convert.ToBase64String(iv)}");
    }
    */
}