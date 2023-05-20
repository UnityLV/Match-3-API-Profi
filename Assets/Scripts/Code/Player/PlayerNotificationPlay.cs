using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerNotificationPlay : MonoBehaviour
{
    [SerializeField] private string path;

    private void Start()
    {
        TextAsset textFile = Resources.Load<TextAsset>(path);
        string str = Encoding.UTF8.GetString(textFile.bytes);
        List<string> phrases = GetPhrases(str);
    }
    private List<string> GetPhrases(string dialogString)
    {
        List<string> phrases = new();
        string line = "";
        for (int i = 0; i < dialogString.Length; i++)
        {
            bool isEndLine = dialogString[i] == '\r' && dialogString[i + 1] == '\n';
            if (isEndLine)
            {
                i += 1;
                if (line == "")
                {
                    continue;
                }
                phrases.Add(line);
                line = "";
                continue;
            }
            line += dialogString[i];
        }
        return phrases;
    }
}
