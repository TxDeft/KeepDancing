//**********************************************************************************************************
//
//文件名(File Name)：                              ActionMatchAlogrithm.cs
//
//功能描述(Description)：                          动作匹配算法管理器
//
//作者(Author)：                                   白野
//
//日期(Create Date)：                              2014.07.24
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.26
//                 修改理由：                       加入延时，加入ActionMatchCount属性
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingletonActionMatchManager : MonoBehaviour, ISubject, IObserver {
	//singleton
	private static SingletonActionMatchManager mInstance;
	public static SingletonActionMatchManager GetInstance(){
		return mInstance;
	}
	#region field

	//静态动作匹配法需要传递一个参数，方便调试
	public float StaticMatchAlogrithmScoreRate = 10.0f;
	//体感获取数据延时帧数，按每秒50帧来算
	public int CameraDataDelayFrame = 5;

	//动作匹配算法
	private ActionMatchAlogrithm mActionMatchAlogrithm;
	//观察者列表
	private List<IObserver> mListObservers;
	//迭代器
	private IIterator mEvaluateIterator;
	private EvaluationAttribute mEvaluationAttribute;
	//动作评价List
	private List<EvaluationAttribute> mListEvaluationAttribute;
	//当前节拍
	private EnumBeatType mCurrentBeatType;
	//节拍主题
	private ISubject mBeatManager;
	//节拍计数
	private int mBeatCount;
	//小节计数
	private int mBarCount;
	//评价小节数
	private List<int>	mListEvaluationBar;
	//评价次数
	private int mEvaluationTimes;
	//评价总开关
	private bool mIsEvaluationAllEnd;
	//评价开关
	private bool mIsEvaluationEnd;
	//第一次评价开关
	private bool mIsFirstEvaluation;
	//存储因为延时已经过掉的动画数据
	private Queue<MotionJointQuaternionVector> mQueueStdMotion;

	//关节动作四元素和根骨向量封装类
	private MotionJointQuaternionVector mStdJointQuaternionVector,
										mUserJointQuaternionVector;
	private Transform mStdTransformTorso,
		mStdTransformLeftShoulder,mStdTransformRightShoulder,
		mStdTransformLeftHip,mStdTransformRightHip,
		mUserTransformTorso,
		mUserTransformLeftShoulder,mUserTransformRightShoulder,
		mUserTransformLeftHip,mUserTransformRightHip;

	#endregion

	public int ActionMatchCount{
		get { return mListEvaluationAttribute.Count; }
	}

	// Use this for initialization
	void Start () {
		mInstance = this;
		mBeatCount = 0;
		mBarCount = 0;
		mEvaluationTimes = 0;
		mIsEvaluationAllEnd = false;
		mIsEvaluationEnd = true;
		mIsFirstEvaluation = false;
		mBeatManager = SingletonBeatManager.GetInstance();
		mBeatManager.AddObserver (this);
		mListObservers = new List<IObserver>();
		mListEvaluationAttribute = new List<EvaluationAttribute> ();
		//通过迭代器读入数据
		ReadEvaluationAttribute ();
		//记录哪个小节进行评价
		mListEvaluationBar = new List<int> ();
		CalEvaluationBar ();
		//初始化标准动画队列
		mQueueStdMotion = new Queue<MotionJointQuaternionVector>();
		//初始化四元组
		InitTransform ();
		InitQuaternionVector ();
		//算法
		mActionMatchAlogrithm = new StaticMatch(mStdJointQuaternionVector,mUserJointQuaternionVector);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MatchResult mMatchResult;
		if (mIsFirstEvaluation) {
			mActionMatchAlogrithm.Init(mListEvaluationAttribute [mEvaluationTimes]);
			mIsEvaluationEnd = false;
			mIsFirstEvaluation = false;
		}
		if (!mIsEvaluationAllEnd && mBarCount == mListEvaluationBar [mEvaluationTimes] + 1) {
			mMatchResult = mActionMatchAlogrithm.Evaluate();
			mActionMatchAlogrithm.Init(mListEvaluationAttribute [mEvaluationTimes]);
			mIsEvaluationEnd = false;
			//print(mMatchResult.Score);
			//print(mBarCount);
			NotifyObserver(mMatchResult);
			mEvaluationTimes++;
			//结束评价
			if(mEvaluationTimes == mListEvaluationBar.Count){
				mEvaluationTimes = mListEvaluationBar.Count-1;
				mIsEvaluationAllEnd = true;
			}
		}
		if (!mIsEvaluationEnd) {
			InitQuaternionVector ();
			mActionMatchAlogrithm.Update (mStdJointQuaternionVector, mUserJointQuaternionVector);
		}
	}

	//通过findtag找到四元组数据
	void InitTransform() {
		//Std
		mStdTransformTorso = GameObject.FindGameObjectWithTag ("Standard Torso").transform;
		mStdTransformLeftShoulder = GameObject.FindGameObjectWithTag ("Standard Left Shoulder").transform;
		mStdTransformRightShoulder = GameObject.FindGameObjectWithTag ("Standard Right Shoulder").transform;
		mStdTransformLeftHip = GameObject.FindGameObjectWithTag ("Standard Left Hip").transform;
		mStdTransformRightHip = GameObject.FindGameObjectWithTag ("Standard Right Hip").transform;
		//User
		mUserTransformTorso = GameObject.FindGameObjectWithTag ("Player1 Torso").transform;
		mUserTransformLeftShoulder = GameObject.FindGameObjectWithTag ("Player1 Left Shoulder").transform;
		mUserTransformRightShoulder = GameObject.FindGameObjectWithTag ("Player1 Right Shoulder").transform;
		mUserTransformLeftHip = GameObject.FindGameObjectWithTag ("Player1 Left Hip").transform;
		mUserTransformRightHip = GameObject.FindGameObjectWithTag ("Player1 Right Hip").transform;
	}

	//给四元组赋值
	void InitQuaternionVector() {
		//标准数据入队
		mQueueStdMotion.Enqueue(new MotionJointQuaternionVector (
			mStdTransformTorso.position, 
		 	mStdTransformTorso.rotation,
		 	mStdTransformLeftHip.rotation,
			mStdTransformRightHip.rotation,
		 	mStdTransformLeftShoulder.rotation, 
			mStdTransformRightShoulder.rotation));

		/*mStdJointQuaternionVector = new MotionJointQuaternionVector 
			(mStdTransformTorso.position, mStdTransformTorso.rotation,
			 mStdTransformLeftHip.rotation, mStdTransformRightHip.rotation,
			 mStdTransformLeftShoulder.rotation, mStdTransformRightShoulder.rotation);*/
		//赋值
		mStdJointQuaternionVector = mQueueStdMotion.Peek();
		mUserJointQuaternionVector = new MotionJointQuaternionVector
			(mUserTransformTorso.position, mUserTransformTorso.rotation,
			 mUserTransformLeftHip.rotation, mUserTransformRightHip.rotation,
			 mUserTransformLeftShoulder.rotation, mUserTransformRightShoulder.rotation);
		//若队列已到延时的数据，则出队
		if(mQueueStdMotion.Count > CameraDataDelayFrame){
			mQueueStdMotion.Dequeue();
		}
		//print(mQueueStdMotion.Count);
	}

	//通过迭代器读取评价数据
	void ReadEvaluationAttribute(){
		mEvaluateIterator = SingletonGameData.GetInstance ().CreateEvaluateIterator ();
		while(mEvaluateIterator.HasNext()){
			EvaluationAttribute mEvaluationAttribute = (EvaluationAttribute)mEvaluateIterator.Next();
			mListEvaluationAttribute.Add(mEvaluationAttribute);
		}
	}

	//计算评价小节
	void CalEvaluationBar(){
		mListEvaluationBar.Add(mListEvaluationAttribute[0].BarNumber);
		for(int i = 1; i <= mListEvaluationAttribute.Count - 1; i++) {
			mListEvaluationBar.Add(mListEvaluationAttribute[i].BarNumber + mListEvaluationBar[i-1]);
			//print (mListEvaluationBar[i]);				//test
		}
	}

	//添加观察者
	public void AddObserver(IObserver observer) {
		mListObservers.Add (observer);
	}

	//删除观察者
	public void DeleteObserver(IObserver observer) {
		mListObservers.Remove (observer);
	}

	//遍历通知观察者
	public void NotifyObserver(System.Object arg) {
		foreach (IObserver o in mListObservers) {
			o.ObserverUpdate(this, arg);
		}
	}

	//订阅主题数据
	void IObserver.ObserverUpdate(ISubject subject,System.Object arg){
		if (mBarCount == 0) {
			mIsFirstEvaluation = true;
		}

		mCurrentBeatType =(EnumBeatType) arg;
		mBeatManager = subject;
		mBeatCount ++;
		if (mCurrentBeatType == EnumBeatType.DOWNBEAT) {
			mBarCount++;
		}
		//print (mCurrentBeatType);			//test
	}
}


