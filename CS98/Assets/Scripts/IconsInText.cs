using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class IconsInText : MonoBehaviour
{

    /******************   getTextWithIcons  ************************/
    /* 
     * Generate a copy of the text provided
     * where all words with icons have icons inserted, 
     * and all words with entries have links embedded.
     * 
     * Icons stored as property in our word.JSON files. Icons imported as TextMeshPro assets.
     */



    public static string GetTextWithIcons(string text)
    {
        string textWithIcons = "";
        foreach (string word in text.Split(' '))
        {
            string newWord = word;
            string cleanWord = Regex.Replace(word, "[\".,/!¡?¿;:*']", "").ToLower();
            print(cleanWord);
            string icon = "";

            //if word is a conjugated form of verb

            if (Dictionary.playerDictionary && Dictionary.verbMapping.ContainsKey(cleanWord)){
                cleanWord = ((string)Dictionary.verbMapping[cleanWord]);
            }

            print("remapped word " + cleanWord);
            if (Dictionary.playerDictionary && Dictionary.wordMap.ContainsKey(cleanWord))
            {
                icon = ((Word)Dictionary.wordMap[cleanWord]).icon;
                newWord = "<link=\"" + cleanWord + "\"><color=blue>" + newWord + "</color></link>";
            }
            textWithIcons += icon + newWord + ' ';
        }
        return textWithIcons.Substring(0, textWithIcons.Length - 1);
    }
}
