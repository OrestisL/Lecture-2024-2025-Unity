using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static partial class Utilities
{
    private static string _saveDataPath;
    public static string SaveDataPath
    {
        get 
        {
            if (string.IsNullOrWhiteSpace(_saveDataPath)) 
            {
                _saveDataPath = Path.Combine(Application.persistentDataPath, "Saved Data");
                if (!Directory.Exists(_saveDataPath)) 
                {
                    Directory.CreateDirectory(_saveDataPath);
                }
            }

            return _saveDataPath;
        }
    }

    public static void SaveData<T>(T data, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) 
            throw new System.Exception(string.Format("Cannot save file without a name ({0})", fileName));
        if (data == null)
            throw new System.Exception(string.Format("Data was null when trying to save file ({0})", nameof(data)));

        string path = Path.Combine(SaveDataPath, fileName);
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        using StreamWriter sw = new StreamWriter(path);
        sw.Write(json);
    }

    public static T LoadData<T>(string fileName) 
    {
        if (string.IsNullOrEmpty(fileName))
            throw new System.Exception(string.Format("Cannot load file without a name ({0})", fileName));

        string path = Path.Combine(_saveDataPath, fileName);

        if (!File.Exists(path))
            throw new System.Exception(string.Format("File at {0} does not exist", path));

        using StreamReader sr = new StreamReader(path);
        string json = sr.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(json);
    }
}
