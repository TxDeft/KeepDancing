//**********************************************************************************************************
//
//文件名(File Name)：                              StarController.cs
//
//功能描述(Description)：                          实现结束场景星星的弹出效果
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.07.28
//
//修改记录(Revision History)：                     暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarController : MonoBehaviour {
	public GameObject  star1,star2,star3,star4,star5;
	private IIterator mTotalEvalScoreIterator;
	private GameObject [] StarArr = new GameObject[5];
	private float[] mArrTotalEvalScore=new float[5]; 
	private float mAverageScore,mstars;
	//时间
	private float time=0;
	private int i=0;
	//private DataBetweenScene dataBetweenScene;

	// Use this for initialization
	void Start () {
		int i=0;
		StarArr[0] = star1;
		StarArr[1] = star2;
		StarArr[2] = star3;
		StarArr[3] = star4;
		StarArr[4] = star5;
		//dataBetweenScene=new DataBetweenScene();
		//仅在单独测试该场景时才满足
		if(DataBetweenScene.AverageScore < 0.01f){
			mAverageScore = 79.0f;
		}
		else{
			mAverageScore = DataBetweenScene.AverageScore;
		}
		//mAverageScore=79;
		mTotalEvalScoreIterator = SingletonGameData.GetInstance ().CreateTotalEvalScoreIterator ();
		//TotalEvalScoreIterator
		while (mTotalEvalScoreIterator.HasNext ()){
			mArrTotalEvalScore[i]=(int)mTotalEvalScoreIterator.Next();
			i++;
		}
		if (mAverageScore <= 100 && mAverageScore >= mArrTotalEvalScore[0]){
			mstars = 5;
		}
		else if (mAverageScore < mArrTotalEvalScore[0] && mAverageScore>= mArrTotalEvalScore[1]){
			mstars = 4;
		}
		else if (mAverageScore < mArrTotalEvalScore[1] && mAverageScore>= mArrTotalEvalScore[2]){
			mstars = 3;
		}
		else if (mAverageScore < mArrTotalEvalScore[2] && mAverageScore>= mArrTotalEvalScore[3]){
			mstars = 2;
		}
		else if (mAverageScore < mArrTotalEvalScore[3] && mAverageScore>= mArrTotalEvalScore[4]){
			mstars = 1;
		}
		else if (mAverageScore < mArrTotalEvalScore[4] ){
			mstars = 0;
		}

	}
	
	// Update is called once per frame
	void Update () {
		time +=Time.deltaTime;
		//print (time );
		if(time>=1.5f*i && i < mstars ){
			StarArr[i].SetActive (true );
			i++;
		}
		//gameObject.SetActive(false);
	}
}
