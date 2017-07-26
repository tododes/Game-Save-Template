using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public class SaveStorage {

    private static BinaryFormatter bf = new BinaryFormatter();
    private static StringBuilder sb = new StringBuilder();
    private static FileStream fs;
    private static List<string> pathNames;
    private const string space = "\n";

    public static void Save<T>(string path, T so) where T : SaveableObject{
        so.ID = path;
        if (!pathNames.Contains(path))
            pathNames.Add(path);
        fs = File.Create(Application.persistentDataPath + path);
        bf.Serialize(fs, so);
        fs.Close();
    }

    public static T Load<T>(string path) where T : SaveableObject {
        fs = File.Open(Application.persistentDataPath + path, FileMode.Open);
        T result = (T) bf.Deserialize(fs);
        fs.Close();
        return result;
    }

    public static string getPathNames(){
        if (sb.Length > 0)
            sb.Remove(0, sb.Length - 1);
        foreach (string path in pathNames){
            sb.Append(path + space);
        }
        return sb.ToString();
    }
}
