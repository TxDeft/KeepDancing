//**********************************************************************************************************
//
//文件名(File Name)：                              AccomplishData.cs
//
//功能描述(Description)：                          显示完成度
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
/// 显示完成度
/// </summary>
public class AccomplishData : MonoBehaviour, IObserver{
	//圆环、背景和节奏提示按左下边界对齐，所以需要圆环图片的一半宽度作为偏移量 
	public float OffsetPixel = 83.0f;
	//圆环的左下边界相对于屏幕的位置，即Score（GameObject）的X和Y值
	public float OffsetRelativeX = 0.065f;
	public float OffsetRelativeY = 0.25f;
	//半径上下界
	public float RadiusUpperBound = 57.0f;
	public float RadiusLowerBound = 60.0f;
	//颤抖的速度，单位为像素
	public float WaveSpeedInPixel = 10.0f;
	//两种纹理，黑和红
	public Texture BlackTexture;
	public Texture RedTexture;
	//显示分数的GameObject
	public GameObject ScoreTextGO;

	//当前分数和满分
	private float mScore = 0.0f, mFullScore = 0.0f;
	//
	private int mAccomplishPercentage = 0;
	//外圈小方块总数
	private float mCubeNum = 40;
	//方块当前的宽和高
	private float mWidth = 3.0f, mHeight = 10.0f;
	//当前半径
	private float mCurrentRadius;
	//第一个节拍。不知道为什么第一个节拍弹的特别诡异，干脆就去掉
	private bool mIsFirstBeat = true;
	//方块的中心位置
	private Vector2 mCenter;
	//绘制方块的Rect
	private Rect mCubeRect;

	// Use this for initialization
	void Start () {
		mCurrentRadius = RadiusLowerBound;
		SingletonBeatManager.GetInstance().AddObserver(this);
		SingletonActionMatchManager.GetInstance().AddObserver(this);

		mFullScore = SingletonActionMatchManager.GetInstance().ActionMatchCount * 100.0f;
		mCenter = new Vector2();
		mCubeRect = new Rect();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI ()
	{
		//每次累加的角度，注意是角度不是弧度！！！
		float deltaAngle = 360.0f / mCubeNum;
		//当前显示红色进度的方块数量
		float redNum = mScore / mFullScore * mCubeNum;
		//保存原来的变换矩阵
		Matrix4x4 matrix;
		//当前使用的纹理（Red or Black）
		Texture texture;
		//绘制完成度圈圈
		for(int i = 0; i < mCubeNum; i++){
			//保留原矩阵
			matrix = GUI.matrix;
			if(i < redNum)
				texture = RedTexture;
			else
				texture = BlackTexture;
			//计算目标中心位置（相对位置 + 像素偏移 + 半径 * 三角函数）及其对应的左上位置
			mCenter.x = Screen.width * OffsetRelativeX + OffsetPixel + mCurrentRadius * Mathf.Sin(i * deltaAngle * Mathf.Deg2Rad);
			mCenter.y = Screen.height - (Screen.height * OffsetRelativeY + OffsetPixel + mCurrentRadius * Mathf.Cos(i * deltaAngle * Mathf.Deg2Rad));

			mCubeRect.x = mCenter.x - mWidth / 2;
			mCubeRect.y = mCenter.y - mHeight / 2;
			mCubeRect.width = mWidth;
			mCubeRect.height = mHeight;

			//第一个参数不能是i * deltaAngle， 因为上一次的结果会被记录;也不能是deltaAngle，因为上一次的结果是个矩阵，不是角度标量，它还带了中心点信息
			//见http://www.unitymanual.com/4270.html
			GUIUtility.RotateAroundPivot (i * deltaAngle, mCenter);
			GUI.DrawTexture(mCubeRect, texture);
			//绘制完毕，恢复矩阵
			GUI.matrix = matrix;
		}
	}

	//节拍观察者，颤抖圆环；评价观察者，累计分数
	public void ObserverUpdate(ISubject subject, System.Object arg){
		//节拍观察者
		if(subject == SingletonBeatManager.GetInstance()){
			if(mIsFirstBeat){
				mIsFirstBeat = false;
				return;
			}
			//以防有的歌曲抖动过快，所以在每次抖动前清空数据
			mCurrentRadius = RadiusLowerBound;
			if(WaveSpeedInPixel < 0)
				WaveSpeedInPixel *= -1.0f;
			//开始颤抖圆环
			StartCoroutine(WaveCircle());
		}
		//评价观察者
		if(subject == SingletonActionMatchManager.GetInstance()){
			mScore += (arg as MatchResult).Score;
			DataBetweenScene.Score = mScore;
			//DataBetweenScene.AverageScore = mScore / SingletonActionMatchManager.GetInstance().ActionMatchCount;

			StartCoroutine(UpdateScore());
		}
	}

	//通过修改半径颤抖圆环
	IEnumerator WaveCircle(){
		while(mCurrentRadius >= RadiusLowerBound){
			mCurrentRadius += WaveSpeedInPixel * Time.deltaTime;
			yield return null;
			if(mCurrentRadius > RadiusUpperBound && WaveSpeedInPixel > 0){
				WaveSpeedInPixel *= -1.0f;
			}
		}
	}

	//更新分数
	IEnumerator UpdateScore(){
		//完成度+1的时间间隔，时间累加器
		float refreshTime = 0.08f, sumTime = 0.0f;
		while((mScore / mFullScore) * 100 > mAccomplishPercentage + 1 ){
			sumTime += Time.deltaTime;
			if(sumTime > refreshTime){
				mAccomplishPercentage++;
				ScoreTextGO.guiText.text = mAccomplishPercentage.ToString() + "%";
				sumTime = 0.0f;
				yield return null;
			}
			else{
				yield return null;
			}
		}
	}
}
