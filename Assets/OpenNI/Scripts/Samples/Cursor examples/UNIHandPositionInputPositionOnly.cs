using UnityEngine;
using System.Collections;

public class UNIHandPositionInputPositionOnly : MonoBehaviour {

	/// the input which controls us
    protected UNIInput m_input;
    /// the initialization (mono-behavior)
    public void Start()
    {
        m_input = FindObjectOfType(typeof(UNIInput)) as UNIInput;
    }

    /// the mono-behavior update
    public void OnGUI()
    {       
		
        float x = m_input.GetAxis("NI_X");
        float y = m_input.GetAxis("NI_Y");
        // since the axes are between -0.5 and 0.5 we add 0.5 to get a value between 0 and 1.
        x += 0.5f;
        y += 0.5f;
        // The value is multiplied by the screen width (minus 20 for the cursor size) to decide
        // where to place it
        x *= (Screen.width-20);
        y *= (Screen.height-20);
        // draw the cursor
        GUI.Box(new Rect(x,y,20,20),"");
    }
}
