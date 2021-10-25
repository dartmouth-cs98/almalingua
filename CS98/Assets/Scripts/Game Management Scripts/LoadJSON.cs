using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJSON : MonoBehaviour
{

    public TextAsset questFile;
    public TextAsset xFile;
    public BaseGame baseGame;


    // Start is called before the first frame update
    void Start()
    {
        load_JSON();
        TMPro.TextMeshProUGUI txt = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Debug.Log(baseGame.qh.quests[0].questname);
        txt.SetText(baseGame.qh.quests[0].description);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void load_JSON()
    {
        baseGame.qh = JsonUtility.FromJson<BaseGame.QuestHolder>(questFile.text);
    }
}
