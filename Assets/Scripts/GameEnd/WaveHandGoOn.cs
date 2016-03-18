//**********************************************************************************************************
//
//文件名(File Name)：                              WaveHandGoOn.cs
//
//功能描述(Description)：                          挥手继续游戏
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.31
//
//修改记录(Revision History)：                     
//           R1:
//			      修改作者：                        汪成峰
//                修改日期：                        2014.08.03
//                修改理由：						   1、挥手进度条飞过，然后才加载下一关卡；
//												   2、当按钮被选择时，透明度逐渐变化，而不是突然变亮
//           R2:
//               修改作者：                          connie
//               修改日期：                          2014.08.04
//               修改理由：                         挥手提示，提示用户向右挥手，箭头与继续游戏向右变化慢，向左变化快
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

public class WaveHandGoOn : MonoBehaviour, IObserver {
	public GameObject GoOn2;
	public GameObject GoOn;
	public GameObject Arrow;
	//两个UI透明度变化的上下界
	public float AlphaUpperBound = 50.0f / 255.0f;
	public float AlphaLowerBound = 30.0f / 255.0f;
	//public float AlphaUpperBound = 85.0f / 255.0f;
	//public float AlphaLowerBound = 110.0f / 255.0f;
	//UI透明度变化速度
	public float AlphaSpeed = 40.0f / 255.0f;
	//public float AlphaSpeed = 40.0f / 255.0f;
	//挥手以后，箭头飞的速度
	public float ArrowSpeed = 6.0f;
	public float LeftSpeed=0.05f/10000.0f;
	public float RightSpeed=3.0f;
	//挥手检测主题
	private ISubject mWaveHand;
	//角度主题
	//private ISubject mShoulderAngle;
	private float mShoulderAngle;
	//是否选中“继续游戏”
	private bool mIsGoOnSelected;
	//alpha变化的正负
	private float mAlphaDirection = 1.0f;//, mAlphaDirection = 1.0f;
	private bool  mIsDirectionLeft = true ;

	// Use this for initialization
	void Start () {
		mWaveHand=SingletonWaveHand.GetInstance();
		mWaveHand.AddObserver(this);
		//mShoulderAngle=SingletonAngle.GetInstance();
		//mShoulderAngle.AddObserver(this );
		//arrShoulderAngle= new ShoulderAngle[2] ;
	}

	// Update is called once per frame
	void Update () {
		//检测手部角度并设置状态
		mShoulderAngle=SingletonAngle.GetInstance().RightAngle;
		if( mShoulderAngle < 50.0f && mShoulderAngle > 20.0f){
			mIsGoOnSelected = true;
		}
		else{
			mIsGoOnSelected = false;

		}
		//print (shoulderAngle);

		//mIsGoOnSelected = true;
		//如果被选中，变化alpha值
		if(mIsGoOnSelected){
			//"继续游戏"
			GoOn2.guiTexture.color = new Color(
				GoOn2.guiTexture.color.r,
				GoOn2.guiTexture.color.g,
				GoOn2.guiTexture.color.b,
				mAlphaDirection * AlphaSpeed * Time.deltaTime + GoOn2.guiTexture.color.a
				);
			//箭头
			Arrow.guiTexture.color = new Color(
				Arrow.guiTexture.color.r,
				Arrow.guiTexture.color.g,
				Arrow.guiTexture.color.b,
				mAlphaDirection * AlphaSpeed * Time.deltaTime + Arrow.guiTexture.color.a
				);

			if(GoOn2.guiTexture.color.a < AlphaLowerBound){
				mAlphaDirection = 1.0f;
			}
			if(GoOn2.guiTexture.color.a > AlphaUpperBound){
				mAlphaDirection = -1.0f;
			}
			if(GoOn2.transform.position.x <= 0.77){
				mIsDirectionLeft=false  ;
			}
			if(GoOn2.transform.position.x >= 0.8) {
				mIsDirectionLeft =true  ;
			}
			if(mIsDirectionLeft){
				GoOn2.transform .Translate(Vector3.left *LeftSpeed * Time.deltaTime );
				GoOn .transform .Translate(Vector3.left *LeftSpeed * Time.deltaTime);
				Arrow.transform .Translate(Vector3.left *LeftSpeed * Time.deltaTime);
			}
			else {
				GoOn2.transform .Translate(Vector3.right  *RightSpeed * Time.deltaTime );
				GoOn .transform .Translate(Vector3.right  *RightSpeed * Time.deltaTime);
				Arrow.transform .Translate(Vector3.right *RightSpeed * Time.deltaTime);
			}


			/*if(Arrow.guiTexture.color.a < AlphaLowerBound){
				mArrowDirection = 1.0f;
			}
			if(Arrow.guiTexture.color.a > AlphaUpperBound){
				mArrowDirection = -1.0f;
			}*/
		}
		//如果未被选中，将alpha值变化到下界
		else{
			if(GoOn2.guiTexture.color.a > AlphaLowerBound){
				GoOn2.guiTexture.color = new Color(
					GoOn2.guiTexture.color.r,
					GoOn2.guiTexture.color.g,
					GoOn2.guiTexture.color.b,
					-1.0f * AlphaSpeed * Time.deltaTime + GoOn2.guiTexture.color.a
					);
			}

			if(Arrow.guiTexture.color.a > AlphaLowerBound){
				Arrow.guiTexture.color = new Color(
					Arrow.guiTexture.color.r,
					Arrow.guiTexture.color.g,
					Arrow.guiTexture.color.b,
					-1.0f * AlphaSpeed * Time.deltaTime + Arrow.guiTexture.color.a
					);
			}
		}
	
	}
	//订阅主题数据
	void IObserver.ObserverUpdate(ISubject subject,System.Object arg){
		if( mShoulderAngle <50 ){
			if((EnumWaveHand)arg == EnumWaveHand.WaveRightHandIn )
			{
				//Application.LoadLevel("Start");
				StartCoroutine(ArrowMoveLeft());
			}
		}
	}

	//箭头向左飞
	IEnumerator ArrowMoveLeft(){
		audio.Play();
		while(GoOn.transform.position.x > -0.1f){
			Arrow.transform.Translate(-1.0f * ArrowSpeed * Time.deltaTime, 0, 0);
			GoOn.transform.Translate(-1.0f * ArrowSpeed * Time.deltaTime, 0, 0);
			GoOn2.transform.Translate(-1.0f * ArrowSpeed * Time.deltaTime, 0, 0);
			ArrowSpeed += Time.deltaTime * 5.0f;
			yield return null;
		}
		Application.LoadLevel("Start");
	}
}
