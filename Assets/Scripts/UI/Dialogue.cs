using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public struct DialogueData
    {
        //public List<string> dialogues;
        public Dictionary<string, List<string>> dialogues;
    }
    public DialogueData data;
    [SerializeField]
    private string path;
    [SerializeField]
    private string fileName;
    private string language;
    [SerializeField]
    private string fileExtension;
    private string fullPath;
    //Save data to json
    private void Start()
    {
        fileExtension = fileExtension.ToLower();
        language = GlobalData.gObjects.Language;
        fullPath = @path + fileName + "_" + language + fileExtension;
        if(System.IO.File.Exists(@fullPath))
        {
            Load();
        }
    }
    public void Save()
    {
        string jsonData = JsonConvert.SerializeObject(this.data, Formatting.Indented);
        System.IO.File.WriteAllText(@fullPath, jsonData);
        Debug.Log("Content saved at (" + fullPath + ").");
    }

    //Load data from json
    public void Load(string jsonPath)
    {
        string jsonData = System.IO.File.ReadAllText(jsonPath);
        data = JsonConvert.DeserializeObject<DialogueData>(jsonData);
    }

    public void Load()
    {
        string jsonData = System.IO.File.ReadAllText(@fullPath);
        data = JsonConvert.DeserializeObject<DialogueData>(jsonData);
    }

    public void SetLanguage(string lang)
    {
        lang = lang.ToUpper();
        if (System.IO.File.Exists(@path + fileName + lang + fileExtension))
        {
            language = lang;
            fullPath = @path + fileName + language + "_"  + fileExtension;
            Load();
        }
        else
        {
            Debug.Log("File for " + lang +  "does not exist.");
        }
    }
}
