//**********************************************************************************************************
//
//文件名(File Name)：                              BeatWaveCube.cs
//
//功能描述(Description)：                          显示随着音乐跳动的波形
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.08.02
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// 显示随着音乐跳动的波形
/// </summary>
public class BeatWaveCube : MonoBehaviour {
	//显示波形的单个小方块（GUITexture）
	public GameObject SingleBeatWaveCube;
	//有AudioSource的那个GameObject
	public GameObject AudioSourceGameObject;
	//声音数据放大比例
	public float Rate = 120.0f;
	//AudioSource
	private AudioSource mAudioSource;
	//显示波形的所有小方块
	private GameObject[,] mBeatWaveCubes;
	//波形的数量
	private int mRow = 8, mCol = 8;
	//小方块初始偏移量（像素）
	private float mOriginalHorizontalOffset = 9.0f, mOriginalVerticalOffset = 10.0f;
	//小方块间隔（像素）
	private float mSpaceHorizontal = 4.0f, mSpaceVertical = 4.0f;
	//显示刷新波形抖动的时间间隔
	private float mFlashDeltaTime = 0.08f;
	//累计时间
	private float mSumTime = 0.0f;
	//获取音频信息
	private float[] mSpectrum;

	// Use this for initialization
	void Start () {
		mAudioSource = AudioSourceGameObject.audio;
		mSpectrum = new float[128];
		mBeatWaveCubes = new GameObject[mRow, mCol];
		int i, j;
		//动态生成方块
		for(i = 0; i < mRow; i++){
			for(j = 0; j < mCol; j++){
				//动态生成波形方块，并设置其为该GameObject的子GameObject
				mBeatWaveCubes[i, j] = Instantiate(SingleBeatWaveCube) as GameObject;
				mBeatWaveCubes[i, j].transform.parent = gameObject.transform;
				mBeatWaveCubes[i, j].guiTexture.enabled = false;
				//设置每个方块位置
				float width, height;
				width = mBeatWaveCubes[i, j].guiTexture.pixelInset.width;
				height = mBeatWaveCubes[i, j].guiTexture.pixelInset.height;
				mBeatWaveCubes[i, j].guiTexture.pixelInset = new Rect(
					mOriginalHorizontalOffset + j * (mSpaceHorizontal + width), //初始偏移量 + 间隔 + 自身大小
					mOriginalVerticalOffset + i * (mSpaceVertical + height),
					width,
					height
					);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//根据声音大小，决定哪些方块是可以看见的
		if(mSumTime > mFlashDeltaTime){
			mSumTime -= mFlashDeltaTime;
			mAudioSource.GetSpectrumData(mSpectrum, 0, FFTWindow.BlackmanHarris);
			for(int i = 0; i < mRow; i++){
				for(int j = 0; j < mCol; j++){
					if(Rate * mSpectrum[j] < i + 1)
						mBeatWaveCubes[i, j].guiTexture.enabled = false;
					else
						mBeatWaveCubes[i, j].guiTexture.enabled = true;
				}
			}
		}
		//累计时间
		mSumTime += Time.deltaTime;
	}
}
