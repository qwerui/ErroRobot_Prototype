using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONParser
{
    public static void SaveJSON<T>(string path, T target)
    {
        string json = JsonUtility.ToJson(target);
        File.WriteAllTextAsync(path, json, System.Text.Encoding.UTF8);
    }

    public static T ReadJSON<T>(string path) where T : class
    {
        var taskResult = File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);
        
        if(taskResult.IsFaulted)
            return null;
        
        string json = taskResult.Result;
        if(json == null)
            return null;

        T target = JsonUtility.FromJson<T>(json);
        return target;
    }
}
