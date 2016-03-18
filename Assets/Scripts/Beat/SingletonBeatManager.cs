//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonBeatManager.cs
//
//功能描述(Description)：                          节拍管理器。根据配置文件，在合适的时间更新节拍，并通知所有需要
//												  节拍的对象。一首歌若干小节，不同小节有不同的节拍，如4/4拍的
//												  小节由4个节拍组成：强、弱、次强、弱。
//
//作者(Author)：                                   汪成峰,李爽
//
//日期(Create Date)：                              2014.07.25
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingletonBeatManager : MonoBehaviour, ISubject {
	//当前小节类型
	private EnumBarType mCurrentBarType;
	//当前节拍强弱类型
	private EnumBeatType mCurrentBeatType;
	//ArrayList记录观察者
	private ArrayList mObservers;
	private bool mIsGameOver = false;
	//相同小节个数
	private int mSameBarNumber;
	//保存每次的deltatime
	private float mDeltaTime;
	//访问次数
	private int mTimes = 1;
	//相同类型小节是否结束
	//private bool mBarNotEnd = true;
	//节拍转换时间点
	private float mBeatTurningPoint = 0;
	//小节转换时间点
	private float mBarTurningPoint = 0;
	//数组存储相同类型小节数
	private List<BarAttribute> mListSameBarsAttribute;
	//List包含的元素个数
	private int mListCount;
	//计时器
	private float mBarIimer = 0;
	//当前进行到哪类小节
	private int mCurrentBar = 0;
	//当前进行到多少小节
	private int mCurrentBarIndex = 0;
	//游戏是否开始
	private bool mIsGameStart = false;
	//创建BarIterator对象
	private IIterator mBarIterator;
	//创建SongIterator对象
	private IIterator mSongIterator;
	//一个节拍多少秒
	private float mBeatSeconds;
	//保存现在播放的音乐属性
	private SongAttribute mSongAttribute;
	//保存现在播放的音乐的小节属性
	private BarAttribute mBarAttribute;
	//singleton
	private static SingletonBeatManager mInstance;
	public static SingletonBeatManager GetInstance(){
		return mInstance;
	}
	//属性
	public EnumBarType GetBarType{
		get { return mCurrentBarType;}
	}

	public int BarIndex {
		get { return mCurrentBarIndex;}
	}

	public bool IsGameOver {
		get { return mIsGameOver; }
	}

	public bool IsGameStart{
		get { return mIsGameStart;}
	}

	public void Begin(){
		mIsGameStart = true;
		mIsGameOver = false;
	}
	// Use this for initialization
	void Start () {
		mInstance = this;
		mObservers = new ArrayList ();
		mBarIterator = SingletonGameData.GetInstance ().CreateBarIterator ();
		//mSongIterator = SingletonGameData.GetInstance ().CreateSongIterator ();
		mListSameBarsAttribute = new List<BarAttribute> ();
		while (mBarIterator.HasNext()) {
			mListSameBarsAttribute.Add(mBarIterator.Next() as BarAttribute);
		}
		mCurrentBarType = mListSameBarsAttribute[mCurrentBar].BarType;
		mSameBarNumber = mListSameBarsAttribute [mCurrentBar].SameBarsNumber;
		mListCount = mListSameBarsAttribute.Count;
		
		mSongAttribute = SingletonGameData.GetInstance ().GetCurrentSongAttribute ();
		mBeatSeconds =(float) 60/mSongAttribute.BPM;

	}
	
	// Update is called once per frame
	void Update () {
		if (mIsGameStart) {
			mDeltaTime = Time.deltaTime;
			mBarIimer = mBarIimer+mDeltaTime;
		} 
		else {
		}	
		//每过一个节拍的时间通知一次观察者
		if (mBarIimer > mBeatSeconds*mBeatTurningPoint) {
			mBeatTurningPoint++;
		switch (mCurrentBarType) {
		case EnumBarType.BEAT_1_4:
				mCurrentBeatType = EnumBeatType.DOWNBEAT;
				mTimes = 1;
				if(mBarIimer+mBeatSeconds-mBeatSeconds*mSameBarNumber-mBarTurningPoint>0){
					if(mCurrentBar<mListCount-1){
						mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
						mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
						
					}
					else{
						mIsGameOver = true;
					}
					mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
				
				}
				if(mBarIimer+mBeatSeconds-mBarTurningPoint>0){
					mCurrentBarIndex++;
				}
				break;
	
		case EnumBarType.BEAT_2_4:
				switch(mTimes){
				case 1:
					mCurrentBeatType = EnumBeatType.DOWNBEAT;
					mTimes++;
					if(mBarIimer+2*mBeatSeconds-mBarTurningPoint>0){
						mCurrentBarIndex++;
					}

					break;
				case 2:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes = 1;
					if(mBarIimer+mBeatSeconds-mBeatSeconds*2*mSameBarNumber-mBarTurningPoint>0){
						if(mCurrentBar<mListCount-1){
							mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
							mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
							
						}
						else{
							mIsGameOver = true;
						}
						mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
					}
					break;
				}
				break;

		case EnumBarType.BEAT_3_4:
				switch(mTimes){
				case 1:
					mCurrentBeatType = EnumBeatType.DOWNBEAT;
					mTimes++;
					if(mBarIimer+3*mBeatSeconds-mBarTurningPoint>0){
						mCurrentBarIndex++;
					}
					break;
				case 2:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 3:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes = 1;
					if(mBarIimer+mBeatSeconds-mBeatSeconds*3*mSameBarNumber-mBarTurningPoint>0){
						if(mCurrentBar<mListCount-1){
							mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
							mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
							
						}
						else{
							mIsGameOver = true;
						}
						mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
						
					}


					break;
				}

				break;


		case EnumBarType.BEAT_3_8:
				switch(mTimes){
				case 1:
					mCurrentBeatType = EnumBeatType.DOWNBEAT;
					mTimes++;
					if(mBarIimer+3*mBeatSeconds-mBarTurningPoint>0){
						mCurrentBarIndex++;
					}
					break;
				case 2:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 3:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes = 1;
					if(mBarIimer+mBeatSeconds-mBeatSeconds*3*mSameBarNumber-mBarTurningPoint>0){
						if(mCurrentBar<mListCount-1){
							mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
							mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
							
						}
						else{
							mIsGameOver = true;
						}
						mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
					}

					break;
				}

				break;

		case EnumBarType.BEAT_4_4:
				switch(mTimes){
				case 1:
					mCurrentBeatType = EnumBeatType.DOWNBEAT;
					mTimes++;
					if(mBarIimer+4*mBeatSeconds-mBarTurningPoint>0){
						mCurrentBarIndex++;
					}
					break;
				case 2:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 3:
					mCurrentBeatType = EnumBeatType.OFFBEAT;
					mTimes++;
					break;
				case 4:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes = 1;
					if(mBarIimer+mBeatSeconds-mBeatSeconds*4*mSameBarNumber-mBarTurningPoint>0){
						if(mCurrentBar<mListCount-1){
							mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
							mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
							
						}
						else{
							mIsGameOver = true;
						}
						mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
					}
					break;
				}
				break;

		case EnumBarType.BEAT_6_8:
				switch(mTimes){
				case 1:
					mCurrentBeatType = EnumBeatType.DOWNBEAT;
					mTimes++;
					if(mBarIimer+6*mBeatSeconds-mBarTurningPoint>0){
						mCurrentBarIndex++;
					}
					break;
				case 2:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 3:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 4:
					mCurrentBeatType = EnumBeatType.OFFBEAT;
					mTimes++;
					break;
				case 5:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes++;
					break;
				case 6:
					mCurrentBeatType = EnumBeatType.UPBEAT;
					mTimes = 1;
					if(mBarIimer+mBeatSeconds-mBeatSeconds*6*mSameBarNumber-mBarTurningPoint>0){
						if(mCurrentBar<mListCount-1){
							mCurrentBarType = mListSameBarsAttribute[++mCurrentBar].BarType;
							mSameBarNumber = mListSameBarsAttribute[mCurrentBar].SameBarsNumber;
							
						}
						else{
							mIsGameOver = true;
						}
						mBarTurningPoint = mBarIimer+mBeatSeconds-mDeltaTime;
					}

					break;
				}

				break;

	//	case EnumBarType.OTHER_BEAT:
			}
			NotifyObserver(mCurrentBeatType);
			//print (mCurrentBarIndex);
			//print (IsGameOver);
		}
	}

	public void AddObserver(IObserver observer) {
		mObservers.Add (observer);
	}

	public void DeleteObserver(IObserver observer) {
		int i = mObservers.IndexOf (observer);
		if (i >= 0) {
			mObservers.Remove(observer);	
		}
	}

	public void NotifyObserver(System.Object arg) {
		int i = mObservers.Count;
		for(int j = 0;j<i;j++){
			IObserver observer = (IObserver)mObservers[j];
			observer.ObserverUpdate(this, arg);
	    }
    }
}

/// <summary>
/// 节拍类型
/// </summary>
public enum EnumBeatType{
	DOWNBEAT,		//强拍
	UPBEAT,			//弱拍
	OFFBEAT			//次强拍
};