using UnityEngine;
using OpenNI;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class UNIDepthmapViewerUtility : UNIMapViewerUtility {
	protected OpenNI.DepthMetaData m_depthMetaData;
	protected int m_lastProcessDepthFrameId = -1;
	
	public Color DepthMapColor = Color.yellow;
	//the depth map before manipulation
	protected short[] rawDepthMap;
	protected float[] depthHistogramMap;
	
	protected override bool InitTexture (out Texture2D refText, out int xSize, out int ySize)
	{
		if(base.InitTexture(out refText ,out xSize, out ySize)== false)
			return false;
		if(m_settingManager.CurrentContext.Depth  == null)
		{
			m_settingManager.m_logger.Log("No depth",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.BaseObjects,UNIEventLogger.VerboseLevel.Errors);
			return false;	
		}
		if(m_factor<=0)
		{
			m_settingManager.m_logger.Log("Illegal factor",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.BaseObjects,UNIEventLogger.VerboseLevel.Errors);
			return false;
		}
		
		MapOutputMode mom= m_settingManager.CurrentContext.Depth.MapOutputMode;
		xSize = mom.XRes/m_factor;
		ySize = mom.YRes/m_factor;
		refText = new Texture2D(xSize,ySize);
		
		//depthmap data
		rawDepthMap = new short[(int)(mom.XRes*mom.YRes)];
		//histogram stuff
		int maxDepth = m_settingManager.CurrentContext.Depth.DeviceMaxDepth;
		depthHistogramMap=new float[maxDepth];
		
		UNIOpenNICheckVersion.Instance.ValidatePrerequisite();
		m_depthMetaData = new DepthMetaData();
		
		return true;
	}
	
	protected override void CalcTexture ()
	{
		base.CalcTexture ();
		
		//everything is ok
		if(m_settingManager ==null || m_settingManager.Valid == false)
			return ;
		m_settingManager.CurrentContext.Depth.GetMetaData(m_depthMetaData);
		if(m_depthMetaData.FrameID>=m_lastProcessDepthFrameId)
		{
			m_lastProcessDepthFrameId = m_depthMetaData.FrameID;
			Marshal.Copy(m_settingManager.CurrentContext.Depth.DepthMapPtr,rawDepthMap,0,rawDepthMap.Length);
			UpdateHistogram();
			WriteDepthTexture();
		}
	}
	
	/// Internal method to update the cumulative histogram.
	protected void UpdateHistogram()
	{
		int i, numOfPoints = 0;
		
		Array.Clear(depthHistogramMap, 0, depthHistogramMap.Length);
		
		int depthIndex = 0;
		for (int y = 0; y < YRes; ++y)
		{
			for (int x = 0; x < XRes; ++x, depthIndex += m_factor)
			{
				if (rawDepthMap[depthIndex] != 0)
				{
					depthHistogramMap[rawDepthMap[depthIndex]]++;
					numOfPoints++;
				}
			}
			depthIndex += (m_factor-1)*XRes; // Skip lines
		}
        if (numOfPoints > 0)
        {
            for (i = 1; i < depthHistogramMap.Length; i++)
	        {   
		        depthHistogramMap[i] += depthHistogramMap[i-1];
	        }
            for (i = 0; i < depthHistogramMap.Length; i++)
	        {
                depthHistogramMap[i] = 1.0f - (depthHistogramMap[i] / numOfPoints);
	        }
        }
	}
	
	protected void WriteDepthTexture()
	{
		int i = XRes*YRes - 1;
		int depthIndex = 0;
		
		//UInt16MapData m_mapData = m_depthMetaData.GetDepthMap();
		
		for(int y = 0;y<YRes;++y)
		{
			for(int x=0;x<XRes;++x,--i,depthIndex+=m_factor)
			{
				short pixel = rawDepthMap[depthIndex];
				//short pixel = (short)m_mapData[depthIndex];
				if(pixel == 0)
					m_mapPixels[i] = Color.black;
				else
				{
					Color c = new Color(depthHistogramMap[pixel],depthHistogramMap[pixel],depthHistogramMap[pixel],0.9f);
					m_mapPixels[i]=DepthMapColor*c;
				}
			}
			depthIndex +=(int)(m_factor - 1)*XRes*m_factor;//skip lines
		}
		m_mapTexture.SetPixels(m_mapPixels);
		m_mapTexture.Apply();
	}
}
