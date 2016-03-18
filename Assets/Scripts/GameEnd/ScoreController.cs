//**********************************************************************************************************
//
//文件名(File Name)：                              ScoreController.cs
//
//功能描述(Description)：                          实现结束场景分数增长的效果
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.29
//
//修改记录(Revision History)：                     暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	private float mScore = 0,score,speed,mSumScore = 0,time=0;
	//数金币结束和光线出现声音
	public AudioClip AudioLight;
	public GUIText Score;
	public GameObject PointLight1,PointLight2,Cube;

	// Use this for initialization
	void Start () {
		mScore=DataBetweenScene .Score;
		//这个条件仅在单独调试该关卡时才会触发
		if(mScore < 0.01f){
			mScore = 10000.0f;
		}
		score = 0.0f;
		Score .text=score.ToString();
		StartCoroutine(ShowScore());
	
	}
	
	// Update is called once per frame
	void Update () {
		/*if (score < mScore){
			score += del;
			Score .text=score.ToString() ;
		}
		else{
			Score .text=mScore .ToString ();
		}*/
	}

	private IEnumerator ShowScore(){
		//播放声音，不能勾选PlayOnAwake，否则声音比界面先来
		audio.Play();

		/*while (score < 10){
			score += Random.Range (1,3);
			Score .text=score.ToString() ;
			yield return null;
		}
		while (score < mScore){
			score += del;
			Score .text=score.ToString() ;
			yield return null;
		}*/
		//yield return null;
		//yield return null;
		time +=Time.deltaTime;
		while (mSumScore < mScore){
			if(mSumScore<100){
				speed=100;
			}
			else if(mSumScore>500){
				speed=5000;
			}
			else{
				speed=2000;
			}
			mSumScore += Time.deltaTime * speed;
			if(mSumScore > mScore){
				mSumScore = mScore;
//				DirectionalLight.SetActive(true);
				PointLight1 .SetActive (true );
				PointLight2.SetActive(true );
				Cube.SetActive (true);
			}
			Score .text=((int)mSumScore).ToString ();
			yield return null;
		}
		//Score .text=mScore .ToString ();
		//分数数完了，停止金币声音，播放金币结束声音
		audio.Stop();
		//audio.PlayOneShot(AudioCoinEnd);
		//播放光线的声音
		audio.PlayOneShot(AudioLight);
	}
}
