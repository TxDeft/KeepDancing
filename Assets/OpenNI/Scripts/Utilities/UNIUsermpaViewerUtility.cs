using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenNI;

public class UNIUsermpaViewerUtility : UNIMapViewerUtility {
	
	
	public List<Color> UserColor;
	public Color m_backgroundColor;
	
	protected SceneMetaData m_metaData;
	protected int m_lastProcessedImageFrameId= -1;
	
	//int testPixel=-1;
	
	protected override bool InitTexture (out Texture2D refText, out int xSize, out int ySize)
	{
		UNIOpenNICheckVersion.Instance.ValidatePrerequisite();
		if(base.InitTexture (out refText, out xSize, out ySize)== false)
			return false;
		if(m_settingManager.UserSkeletonValid == false)
		{
			m_settingManager.m_logger.Log("Invalid users node",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.Skeleton,UNIEventLogger.VerboseLevel.Errors);
			return false;
		}
		if(m_factor <=0)
		{
			m_settingManager.m_logger.Log("Illegal factor",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.Skeleton,UNIEventLogger.VerboseLevel.Errors);
			return false;
		}
		m_metaData = m_settingManager.UserGenerator.UserNode.GetUserPixels(0);
		
		xSize = m_metaData.XRes/m_factor;
		ySize = m_metaData.YRes/m_factor;
		
		refText = new Texture2D(xSize,ySize,TextureFormat.RGB24,false);
		
		return true;
	}
	
	protected override void CalcTexture ()
	{
		base.CalcTexture ();
		if(m_settingManager.Valid== false || m_settingManager.UserSkeletonValid == false)
		{
			return; //can't do anything...
		}
		if(m_metaData.FrameID>=m_lastProcessedImageFrameId)
		{
			m_metaData.FrameID = m_lastProcessedImageFrameId;
			WriteUserTexture();
		}
	}
	protected void WriteUserTexture()
	{
		int i= XRes*YRes -1;
		
		UInt16MapData imageData = m_metaData.GetLabelMap();
		
		for(int y=0;y<YRes;y++)
		{
			for(int x=0;x<XRes; x++,i--)
			{
				int piexl = imageData[x*m_factor,y*m_factor];
				//testPixel=piexl;
				if(piexl == 0)
				{
					m_mapPixels[i] = m_backgroundColor;
				}
				else
				{
					int ind = piexl%UserColor.Count;
					m_mapPixels[i]=UserColor[ind];
				}
			}
		}
		
		m_mapTexture.SetPixels(m_mapPixels);
		m_mapTexture.Apply();
	}
	
}

