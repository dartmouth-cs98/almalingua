using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildParent : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Switch() {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
}
