using UnityEngine;
using System.Collections;

/// A default implementation for the UNIGUICursor.
/// 
/// This class is an implementation of the cursor which uses the input to control the cursor.
/// It uses NIGUI_X to control the x access, NIGUI_Y to control the y access and NIGUI_CLICK
/// to choose stuff. It requires the following:
/// - NIGUI_X and NIGUI_Y should return a normalized value between -0.5 and 0.5.
/// - we will respond to the UNIGestureTracker.GestureEventHandler event and therefore the click
/// will only occur at that time.
public class DefaultUNIGUICursor : UNIGUICursor 
{

    public UNIInput m_input;
	
	  /// holds the time of the last click (from Time.time)
    protected float m_lastClickTime;
    /// holds the frame of the last click 
    protected int m_lastClickFrame;
    /// holds the position of the last click 
    protected Vector2 m_lastClickPos;

    /// holds true if we are active, false otherwise
    protected bool m_active;

    /// a rect used for the GUI
    private Rect m_positionRect;

    /// a texture to create a progress bar
    private Texture2D m_texture;

    /// if this is true we already registered to receive the click.
    private bool m_registeredClick;
	
    protected override void  InternalAwake()
    {
        base.InternalAwake();
        if (m_input == null)
            m_input = FindObjectOfType(typeof(UNIInput)) as UNIInput;
        m_lastClickFrame = -1;
        m_lastClickPos = Vector2.zero;
        m_lastClickTime = -1.0f;
        int rectSize=15;
        m_positionRect = new Rect(0, 0, rectSize, rectSize);
        m_texture = new Texture2D(1, 1);
        Color[] color = new Color[1];
        color[0] = Color.green;
        m_texture.SetPixels(color);
        m_texture.Apply();
        m_active = true;
    }

    public override bool HasClickedThisFrame()
    {
        if (m_active == false)
            return false;
        return m_input.GetAxis("NIGUI_CLICK")>=1.0f;
    }

    public override float GetLastClickedTime(out int frameClicked, out Vector2 posClicked)
    {
        if (m_active == false)
        {
            frameClicked = -100;
            posClicked = Vector2.zero;
            return -100.0f;
        }

        frameClicked = m_lastClickFrame;
        posClicked = m_lastClickPos;
        return m_lastClickTime;
    }

    public override void RegisterCallbackForGesture(UNIGestureTracker.GestureEventHandler eventDelegate)
    {
        m_input.RegisterCallbackForGesture(eventDelegate, "NIGUI_CLICK");
    }

    public override void UnRegisterCallbackForGesture(UNIGestureTracker.GestureEventHandler eventDelegate)
    {
        m_input.UnRegisterCallbackForGesture(eventDelegate, "NIGUI_CLICK");
    }

    public override Vector2 Position
    {
        get 
        {
            Vector2 res = PositionNormalized;
            // transform to screen coordinates.
            res.x *= Screen.width;
            res.y *= Screen.height;
            return res;
        }
    }


    public override Vector2 PositionNormalized
    {
        get 
        { 
            Vector2 res = Vector2.zero;
            // we need to add 0.5 to change the range from -0.5 to 0.5 to a range from 0 to 1.
            res.x = m_input.GetAxis("NIGUI_X")+0.5f; 
            res.y = m_input.GetAxis("NIGUI_Y")+0.5f; // the screen y axis is opposite to the camera's
            return res;
        }
    }

    /// to make sure we unregister
    public void OnDestroy()
    {
        m_active = false;
        if (m_registeredClick)
        {
            m_input.UnRegisterCallbackForGesture(GestureEventHandler, "NIGUI_CLICK");
            m_registeredClick = false;
        }
    }

    public override void SetActive(bool state)
    {
        m_active = state;
    }

    /// responsible for showing the cursor (override this instead of defining a new OnGUI)
    protected override void InternalOnGUI()
    {
        if (m_active == false)
        {
            return; // we are invisible
        }

        if (m_registeredClick == false)
        {
            m_input.RegisterCallbackForGesture(GestureEventHandler, "NIGUI_CLICK");
            m_registeredClick = true;
        }
        m_positionRect.x = Position.x - (m_positionRect.width/2);
        m_positionRect.y = Position.y - (m_positionRect.height / 2);
        float clickVal=m_input.GetAxis("NIGUI_CLICK");
        GUI.depth = 0;
        GUI.Box(m_positionRect, "");
        if (clickVal > 0)
        {
            float width = m_positionRect.width;
            m_positionRect.width *= clickVal;
            GUI.DrawTexture(m_positionRect, m_texture,ScaleMode.StretchToFill);
            //GUI.Box(m_positionRect, m_texture);
            m_positionRect.width = width;
        }
    }

    private void GestureEventHandler(UNIPointTracker hand)
    {        
        m_lastClickTime = Time.time;
        m_lastClickFrame = Time.frameCount;
        m_lastClickPos = Position;
    }
}
