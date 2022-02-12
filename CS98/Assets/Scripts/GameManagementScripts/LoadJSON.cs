using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJSON : MonoBehaviour
{

    public static void load_JSON(TextAsset file, BaseGame baseGame)
    {
        baseGame.qh = JsonUtility.FromJson<BaseGame.QuestHolder>(file.text);
    }
}
