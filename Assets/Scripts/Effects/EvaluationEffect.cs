//**********************************************************************************************************
//
//文件名(File Name)：                              EvaluationEffect.cs
//
//功能描述(Description)：                          显示评价特效，包括文字和场景的特效显示
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.08.04
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// 显示评价特效，包括文字和场景的特效显示
/// </summary>
public class EvaluationEffect : MonoBehaviour, IObserver {
	//需要显示的两组特效Prefab，在编辑器中拖进去
	public GameObject[] WordEffects;
	public GameObject[] PlayerEffects;
	//脚底灯光位置
	public Transform FootLightTranform;
	
	//不同评价等级的分数
	private float[] mLevelScore;
	// Use this for initialization
	void Start () {
		//读取迭代器，确定比较分数
		IIterator scoreIter = SingletonGameData.GetInstance().CreateEachEvalScoreIterator();
		mLevelScore = new float[scoreIter.Count];
		for(int i = 0; i < scoreIter.Count; i++){
			mLevelScore[i] = (int)scoreIter.Next();
		}
		//注册为观察者
		SingletonActionMatchManager.GetInstance().AddObserver(this);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void IObserver.ObserverUpdate(ISubject subject, System.Object arg){
		float score = (arg as MatchResult).Score;
		int i;
		//按照分数，评等级并产生特效
		for(i = 0; i < mLevelScore.Length; i++){
			//在评分范围内
			if(score > mLevelScore[i] ){
				audio.Play();
				Instantiate(WordEffects[i]);
				GameObject playerEffect = Instantiate(PlayerEffects[i], FootLightTranform.position,  Quaternion.identity) as GameObject;
				//这样才能让其跟随人物
				playerEffect.transform.parent = FootLightTranform;
				break;
			}
		}
		//到了最后一个评分范围
		if(i == mLevelScore.Length){
			audio.Play();
			Instantiate(WordEffects[i]);
			GameObject playerEffect = Instantiate(PlayerEffects[i], FootLightTranform.position,  Quaternion.identity) as GameObject;
			//这样才能让其跟随人物
			playerEffect.transform.parent = FootLightTranform;
		}
	}
}
