using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class FileService : MonoBehaviour
{
    [SerializeField] private string path;
    private List<List<string>> _histrory;
    public List<List<string>> Histrory => _histrory;
    private void Start()
    {
        TextAsset textFile = Resources.Load<TextAsset>(path);
        string str = Encoding.UTF8.GetString(textFile.bytes);
       _histrory = GetDialogs(textFile.text);
    }

    private List<List<string>> GetDialogs(string dialogString)
    {
        List<List<string>> dialogs = new List<List<string>>();
        dialogs.Add(new List<string>());
        string line = "";
        int dialogID = 0;
        bool isComment = false;
        for (int i = 0; i < dialogString.Length; i++)
        {
            if(dialogString[i] == '*')
            {
                isComment = !isComment;
                continue;
            }
            if (isComment)
            {
                continue;
            }
            bool isEndLine = dialogString[i] == '\r' && dialogString[i + 1] == '\n';
            if (isEndLine)
            {
                i += 1;
                if (line == "")
                {
                    continue;
                }
                dialogs[dialogID].Add(line);
                line = "";
                continue;
            }
            bool isEndDialoge = dialogString[i] == '/' && dialogString[i + 1] == '/' && dialogString[i + 2] == '/';
            if (isEndDialoge)
            {
                dialogID++;
                i += 4;
                dialogs.Add(new List<string>());
                continue;
            }
            line += dialogString[i]; 
        }
        dialogs[dialogID].Add(line);
        return dialogs;
    }
}
