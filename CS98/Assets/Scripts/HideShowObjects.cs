using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowObjects : MonoBehaviour
{

    // Start is called before the first frame update

    private void Awake()
    {
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void Show()
    {
        gameObject.SetActive(true);

    }
}
