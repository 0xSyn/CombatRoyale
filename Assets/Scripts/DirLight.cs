using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLight : MonoBehaviour {
    private Light dlight;
    private Color endColor = Color.black;
    float fadeTime = 2.5f;
    private Color newColor;
    private Color curColor;
    public Color[] lightcolour;
    private float tColor;

    void Start() {
        dlight = GetComponent<Light>();
        curColor= lightcolour[Random.Range(0, lightcolour.Length)];
        SetColor();
    }

    void SetColor() {
        newColor = lightcolour[Random.Range(0, lightcolour.Length)];
        tColor = 0; // reset timer
    }

    void Update() {
        if (tColor <= 1) { // if end color not reached yet...
            tColor += Time.deltaTime / fadeTime; // advance timer at the right speed
             dlight.color = Color.Lerp(curColor, newColor, tColor);
        }
        else {
            fadeTime = Random.Range(0, 2.5f);
            curColor = newColor;
            SetColor();
        }
    }

}
