//**********************************************************************************************************
//
//文件名(File Name)：                              ChooseList.cs
//
//功能描述(Description)：                          选择界面
//
//作者(Author)：                                   白野
//
//日期(Create Date)：                              2014.08.1
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class ChooseList : MonoBehaviour ,IObserver {

	public GUISkin Skin;

	//读取到的数据
	public Texture LightBar;
	private Texture[] SongsPictures;
	private Texture[] SongsWords;
	private AudioClip[] mSong_chorus;
	//当前播放
	private int mCurrentSongnum = -1;
	//偏移量
	private int mIndex = 0 ;
	//当前所选的歌曲号
	private int mSongNum = 0;
	//滚轮
	public Vector2 scrollPosition;
	//总大小
	private Rect mChooseRect;
	//位置
	private Rect mItemRect;
	//翻页滞留时间
	private int stayTime;
	private bool doPageUp = false;
	private bool doPageDown = false;

	//长宽
	private float mWidth = Screen.width/2.8f;
	private float mHeight = Screen.height/12;
	//间隔
	private float mGapHeight = Screen.height / 6.5f;

	//右手角度
	private float mRightAngle;
	//左手角度
	private float mLeftAngle;

	// Use this for initialization
	void Start ()
	{
		SingletonWaveHand.GetInstance ().AddObserver (this);
		SongsPictures = SingletonLoadGameStartResources.GetInstance ().ArrayTextureSongChooseImages;
		SongsWords = SingletonLoadGameStartResources.GetInstance ().ArrayTextureSongWords;
		mSong_chorus = SingletonLoadGameStartResources.GetInstance ().CurrentSong_chorus;
		mChooseRect = new Rect (0, 0, Screen.width/3.6f, mGapHeight * (SongsWords.Length - 1) + Screen.height / 5.45f);// 总大小，这个要计算正确
		mItemRect = new Rect (Screen.width/2 + Screen.width/65, Screen.height/6 ,Screen.width/3.2f, Screen.height/1.55f); // 位置
		mIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		mRightAngle = SingletonAngle.GetInstance ().RightAngle;
		mLeftAngle = SingletonAngle.GetInstance ().LeftAngle;
		GetIndexFromPos ();
		GetSongNumFromPos ();
		StayJudge ();
		if (/*Input.mousePosition.x > Screen.width / 2 &&*/ mSongNum != mCurrentSongnum) {
			audio.Stop();
			audio.clip = mSong_chorus[mSongNum];
			mCurrentSongnum = mSongNum;
		}
		if(!audio.isPlaying /*&& Input.mousePosition.x > Screen.width / 2*/){
			audio.Play();
		}
		print (mRightAngle);

	}
	// 这里为了提高性能，实际只画了3个图
	void OnGUI ()
	{
		GUI.skin = Skin;
		scrollPosition = GUI.BeginScrollView (mItemRect, scrollPosition, mChooseRect, false, false);

		int i;
		for (i = 0; i < SongsWords.Length; i++) {
			
			GUI.DrawTexture (new Rect (0 ,Screen.height/15 + i * mGapHeight, mWidth, mHeight),
			                 SongsWords[i],ScaleMode.StretchToFill);
		}

		GUI.EndScrollView ();

		//if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y < Screen.height/6 * 5) 
		if(mRightAngle < 105) {
			GUI.DrawTexture(new Rect (Screen.width/10.58f,Screen.height/7.0f,Screen.width/2.5f,Screen.height/1.4f),
			                SongsPictures[mSongNum],ScaleMode.ScaleAndCrop);
			GUI.color = new Color (255, 255, 255, 0.5f);
			//if(Input.mousePosition.y >= Screen.height / 4.5f && Input.mousePosition.y <= Screen.height/1.2f)
			if(mRightAngle > 35){
			GUI.DrawTexture (new Rect (Screen.width/2 + Screen.width/65,
			                           Screen.height/6 + Screen.height/15 
			                           //+ (int)((-Input.mousePosition.y + Screen.height/1.25f) / (mGapHeight)) * mGapHeight,
				                       + (int)((105 - mRightAngle) / 17.5f) * mGapHeight,
			                           mWidth, mHeight),
			                 LightBar,ScaleMode.StretchToFill);
			}
		}

		if (doPageUp) {
			scrollPosition.y += mGapHeight;
			doPageUp = false;
		}
		if(doPageDown) {
			scrollPosition.y -= mGapHeight;
			doPageDown = false;
		}

	}
	
	// 计算得到当前的偏移量
	private void GetIndexFromPos () {
		mIndex = (int)(scrollPosition.y / mGapHeight);
		//print (mSongNum);
	}

	private void GetSongNumFromPos () {
		//mSongNum = (int)((-Input.mousePosition.y + Screen.height/1.25f) / (mGapHeight)) + mmIndex;
		if (mRightAngle < 35.0f || mRightAngle > 105.0f)
			return;
		mSongNum = (int)((105 - mRightAngle) / 17.5f) + mIndex;
		if (mSongNum > SongsPictures.Length) {
			mSongNum = mCurrentSongnum;
		}
		print (mSongNum);		//test
	}

	private void StayJudge (){
		//if (Input.mousePosition.x > Screen.width / 2) {
			//if(Input.mousePosition.y < Screen.height / 6) 
			if(mRightAngle < 35 && mRightAngle > 15){
				stayTime ++;
				//TODO:不能按帧数！这个会和具体运行环境相关
				if(stayTime >= 30){
					doPageUp = true;
					stayTime = 0;
				}
			}
			//else if(Input.mousePosition.y > Screen.height/6 * 5) 
			else if(mRightAngle > 110){
				stayTime ++;
				if(stayTime >= 30){
					doPageDown = true;
					stayTime = 0;
				}
			}
			else {
				stayTime = 0;
			}
		//}
	}
	void IObserver.ObserverUpdate(ISubject subject,System.Object arg){
		if ((EnumWaveHand)arg == EnumWaveHand.WaveRightHandIn) {
			SingletonStateController.CurrrentState = SingletonStateController.State_Type.ChooseFinish;
			DataBetweenScene.SongName = SingletonLoadGameStartResources.GetInstance().StrSongName[mSongNum];
		}
	}
}





