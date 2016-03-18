using UnityEngine;

public class UNIMapViewerUtility : MonoBehaviour
{
	public OpenNISettingsManager m_settingManager;
	public Rect m_placeToDraw = new Rect (0, 0, 160, 120);
	
	public enum ScreenSnap
	{
		UppreLeftCorner,
		UppreRightCorner,
		LowerLeftCorner,
		LowerRightCorner
	};
	
	public ScreenSnap m_snap;
	/// <summary>
	/// This is the factor in which we will reduce the received image.
	/// A factor of 1 means we use all pixels, a factor 2 means we use only the pixels in each direction.
	/// </summary>
	public int m_factor;
	
	protected bool m_valid = false;
	
	protected Texture2D m_mapTexture;
	protected Color[] m_mapPixels;
	//how many pixels in the x axis;
	protected int XRes;
	//how many pixels in the y axis;
	protected int YRes;
	
	public bool ImageValid
	{
		get{return m_valid;}
	}
			
	/// <summary>
	/// Start this instance. 
	/// Mono behavior start.
	/// </summary>
	public void Start()
	{
		InternalStart();
	}
	
	protected virtual bool InitTexture(out Texture2D refText,out int  xSize, out int ySize)
	{
		refText = null;
		xSize = -1;
		ySize = -1;
		return true;
	}
	/// <summary>
	/// Internals the start.
	/// </summary>
	protected virtual void InternalStart()
	{
		if(m_settingManager == null)
			m_settingManager = FindObjectOfType(typeof(OpenNISettingsManager)) as OpenNISettingsManager;
		if(m_settingManager == null || m_settingManager.Valid == false)
		{
			string str = "Context is invalid";
			if(m_settingManager == null)
				str = "Context is null";
			Debug.Log(str);
			m_valid = false;
			return;
		}
		if(InitTexture(out m_mapTexture,out XRes,out YRes)== false || m_mapTexture == null)
		{
			m_settingManager.m_logger.Log("Failed to init texture",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.BaseObjects,UNIEventLogger.VerboseLevel.Errors);
			m_valid = false;
			return;
		}
		m_mapPixels = new Color[XRes*YRes];
		m_valid = true;
	}
	
	/// <summary>
	///  Internal calculates the texture for the current frame and write it to the internal texture.
	/// </summary>
	protected virtual void CalcTexture()
	{
		
	}
	
	/// <summary>
	///  Update is called once per frame by mono-behavior
	/// </summary>
	public void Update()
	{
		if(m_valid)
			CalcTexture();
	}
	/// <summary>
	/// Draw the texture
	/// </summary>
	public void OnGUI()
	{
		Rect posToPut= m_placeToDraw;
		switch(m_snap)
		{
			case ScreenSnap.UppreRightCorner:
			{
				posToPut.x = Screen.width - m_placeToDraw.x - m_placeToDraw.width;
				break;
			}
			case ScreenSnap.LowerLeftCorner:
			{
				posToPut.y = Screen.height - m_placeToDraw.y - m_placeToDraw.height;
				break;
			}
			case ScreenSnap.LowerRightCorner:
			{
				posToPut.x = Screen.width - m_placeToDraw.x - m_placeToDraw.width;
				posToPut.y = Screen.height - m_placeToDraw.y - m_placeToDraw.height;
				break;
			}
		}
		if(m_valid)
		{
			GUI.DrawTexture(posToPut,m_mapTexture,ScaleMode.StretchToFill);
		}
	}
}


