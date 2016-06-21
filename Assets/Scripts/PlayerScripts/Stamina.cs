using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {

	public bool enabled = true;

    Rect staminaRect;
    Texture2D staminaTexture;
    float stamina = 10f;
    float maxStamina = 10f;

    public void changeStamina(float usage)
    {
        stamina = System.Math.Min(stamina - usage, maxStamina);
        stamina = System.Math.Max(0, stamina);
    }

    public float getCurrentStamina()
    {
        return stamina;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    

}
