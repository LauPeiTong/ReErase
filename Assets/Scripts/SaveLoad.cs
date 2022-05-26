using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad 
{
    public static void SaveData(Username u)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/Game.dat";
        // string path = "data/data/" + Application.identifier.ToString() + "/files/reerase.dat";

        FileStream stream = new FileStream(path, FileMode.Create);

        UsernameData playerData = new UsernameData(u);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static UsernameData LoadData()
    {
        string path = Application.dataPath + "/Game.dat";
        // string path = "data/data/" + Application.identifier.ToString() + "/files/reerase.dat";

        Debug.Log(Application.dataPath);

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UsernameData data = formatter.Deserialize(stream) as UsernameData;

            stream.Close();

            return data;
        } else
        {
            
            Debug.Log("Error: Save file not found in " + path);
            return null;
        }
    }


}

