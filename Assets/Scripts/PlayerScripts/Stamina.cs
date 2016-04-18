using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {

    Rect staminaRect;
    Texture2D staminaTexture;
    float stamina = 10f;
    float maxStamina = 10f;

    public void changeStamina(float usage)
    {
        stamina = System.Math.Min(stamina - usage, maxStamina);
    }

    // Use this for initialization
    void Start () {
        staminaRect = new Rect((Screen.width / 20), (Screen.height / 10) * 9.5f, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, new Color(236 / 256f, 208 / 256f, 120 / 256f, 1f));
        staminaTexture.Apply();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio * Screen.width / 3;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);

    }

}
