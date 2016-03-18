using UnityEngine;
using OpenNI;

public class UNIImagemapViewerUtility : UNIMapViewerUtility {

	protected ImageMetaData m_imageMetaDate;
	protected int m_lastProcessImageFrameId = -1;
	
	protected override bool InitTexture(out Texture2D refText,out int xSize,out int ySize)
	{
		if(base.InitTexture(out refText,out xSize,out ySize)== false)
			return false;
		if(m_settingManager.ImageValid == false)
		{
			m_settingManager.m_logger.Log("Invalid image",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.Image,UNIEventLogger.VerboseLevel.Errors);
			return false;
		}
		if(m_factor <=0)
		{
			m_settingManager.m_logger.Log("Illegal factor",UNIEventLogger.Categories.Initialization,UNIEventLogger.Sources.Image,UNIEventLogger.VerboseLevel.Errors);
			return false;		
		}
			
		//get the resolution from the image
		MapOutputMode mom = m_settingManager.Image.Image.MapOutputMode;
		//update the resolution by the factor
		xSize=mom.XRes/m_factor;
		ySize=mom.YRes/m_factor;
		//create a new texture.
		refText = new Texture2D(XRes,ySize,TextureFormat.RGB24,false);
		//create a new meta data object.
		UNIOpenNICheckVersion.Instance.ValidatePrerequisite();
		m_imageMetaDate = new ImageMetaData();
		return true;
	}
	
	protected override void CalcTexture ()
	{
		base.CalcTexture ();
		//we need to make sure everything is valid
		if(m_settingManager.Valid == false || m_settingManager.ImageValid==false)
			return;
		m_settingManager.Image.Image.GetMetaData(m_imageMetaDate);
		if(m_imageMetaDate.FrameID>=m_lastProcessImageFrameId)
		{
			m_lastProcessImageFrameId = m_imageMetaDate.FrameID;
			WriteImageTexture();
		}
	}
	
	protected void WriteImageTexture()
	{
		//the size of the target data
		int i = XRes*YRes -1;
		
		MapData<RGB24Pixel> imageData = m_imageMetaDate.GetRGB24ImageMap();
		Color colorElement = Color.black; //for initialization
		
		//loop over the target arry(x and y)
		for(int y=0;y<YRes;++y)
		{
			for(int x = 0; x<XRes;++x,i--)
			{
				int index = x*m_factor + imageData.XRes*y*m_factor;
				RGB24Pixel pixel = imageData[index];
				colorElement.r= ((float)pixel.Blue)/256.0f; 
				colorElement.g= ((float)pixel.Green)/256.0f;
				colorElement.b= ((float)pixel.Red)/256.0f;
				
				m_mapPixels[i] = colorElement;
			}
		}
		m_mapTexture.SetPixels(m_mapPixels);
		m_mapTexture.Apply();
	}
}
