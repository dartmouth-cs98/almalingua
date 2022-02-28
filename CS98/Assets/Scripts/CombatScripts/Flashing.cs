using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Flashing : MonoBehaviour
{
    Image _image = null;
    Coroutine currentFlashRoutine = null;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    public void StartFlash(float seconds, float maxAlpha, Color newColor) {
        _image.color = newColor;
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
        if (currentFlashRoutine != null) {
            StopCoroutine(currentFlashRoutine);
        }
        currentFlashRoutine = StartCoroutine(Flash(seconds, maxAlpha));

    }

    IEnumerator Flash(float seconds, float maxAlpha) {
        int x = 0;
        while (x < 3) {
            float flashIn = seconds/2;
            for (float t = 0; t <= flashIn; t+= Time.deltaTime)
            {
                Color colorThisFrame = _image.color;
                colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t/flashIn);
                _image.color = colorThisFrame;

                yield return null;
            }

            float flashOut = seconds/2;
            for (float t = 0; t <= flashOut; t+= Time.deltaTime)
            {
                Color colorThisFrame = _image.color;
                colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t/flashOut);
                _image.color = colorThisFrame;

                yield return null;
            }
            x+=1;
            yield return new WaitForSeconds(0.5f);

        }
        _image.color = new Color32(0,0,0,0);
        }
        
}
