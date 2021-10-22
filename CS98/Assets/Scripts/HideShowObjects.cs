using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUnhideObject : MonoBehaviour
{
    public string ObjectName;
    // Start is called before the first frame update

    private void Awake()
    {
        Hide();
    }
    void Hide()
    {
        GameObject.Find(ObjectName).SetActive(false);

    }

    // Update is called once per frame
    void Show()
    {
        GameObject.Find(ObjectName).SetActive(true);

    }
}
