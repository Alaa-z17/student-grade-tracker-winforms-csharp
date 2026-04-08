using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StudentGradeTracker.Models;

namespace StudentGradeTracker.Services;

public static class JsonStorageService
{
    private static readonly string FilePath;
    private static readonly byte[] Key;
    private static readonly byte[] IV;

    static JsonStorageService()
    {
        string exeDir = AppContext.BaseDirectory;
        var config = new ConfigurationBuilder()
            .SetBasePath(exeDir)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string fileName = config["AppSettings:DataFile"] ?? "students.json";
        FilePath = Path.Combine(exeDir, fileName);

        string keyBase64 = config["Encryption:Key"] ?? throw new InvalidOperationException("Encryption:Key missing");
        string ivBase64 = config["Encryption:IV"] ?? throw new InvalidOperationException("Encryption:IV missing");

        Key = Convert.FromBase64String(keyBase64);
        IV = Convert.FromBase64String(ivBase64);

        if (Key.Length != 32) throw new InvalidOperationException("Encryption key must be 32 bytes.");
        if (IV.Length != 16) throw new InvalidOperationException("Encryption IV must be 16 bytes.");
    }

    public static void SaveStudents(List<Student> students)
    {
        if (students == null) throw new ArgumentNullException(nameof(students));

        try
        {
            string? dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            byte[] encrypted = EncryptStringToBytes(json, Key, IV);
            File.WriteAllBytes(FilePath, encrypted);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to save student data to {FilePath}: {ex.Message}", ex);
        }
    }

    public static List<Student> LoadStudents()
    {
        if (!File.Exists(FilePath))
            return new List<Student>();

        try
        {
            byte[] encrypted = File.ReadAllBytes(FilePath);
            string json = DecryptStringFromBytes(encrypted, Key, IV);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
        catch (CryptographicException)
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                var students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
                SaveStudents(students);
                return students;
            }
            catch
            {
                string backupPath = FilePath + ".corrupt";
                File.Move(FilePath, backupPath);
                MessageBox.Show($"The existing student data file was corrupt or encrypted with a different key.\nIt has been backed up to {backupPath}\nStarting with an empty student list.",
                    "Data Recovery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<Student>();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load student data from {FilePath}: {ex.Message}", ex);
        }
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        using ICryptoTransform encryptor = aes.CreateEncryptor();
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        using ICryptoTransform decryptor = aes.CreateDecryptor();
        byte[] plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
}