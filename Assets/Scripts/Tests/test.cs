//**********************************************************************************************************
// 
//文件名(File Name)：                              test.cs
//
//功能描述(Description)：                          测试6个迭代器
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.07.19
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class test : MonoBehaviour {
	private IIterator mTestEachEvalScoreIterator;
	private IIterator mTestTotalEvalScoreIterator;
	private IIterator mTestEvaluateIterator;
	private IIterator mTestSongIterator;
	private IIterator mTestBarIterator;
	private IIterator mTestUserPromptsIterator;
	private EvaluationAttribute mEvaluationAttributeTest;
	private SongAttribute mSongAttributeTest;
	private BarAttribute mBarAttributeTest;
	private UserPromptsAttribute mUserPromptsAttributeTest;
	// Use this for initialization
	void Start () {
		/*mTestEachEvalScoreIterator = SingletonGameData.GetInstance ().CreateEachEvalScoreIterator ();//测试EachEvalScoreIterator
		while (mTestEachEvalScoreIterator.HasNext ()){
			print (mTestEachEvalScoreIterator.Next());
		}*/
	   

		/*mTestTotalEvalScoreIterator = SingletonGameData.GetInstance ().CreateTotalEvalScoreIterator ();//测试TotalEvalScoreIterator
		while (mTestTotalEvalScoreIterator.HasNext ()){
			print (mTestTotalEvalScoreIterator.Next ());
		}*/

		/*mTestEvaluateIterator = SingletonGameData.GetInstance ().CreateEvaluateIterator ();//测试EvaluationsIterator
		while(mTestEvaluateIterator.HasNext()){
			mEvaluationAttributeTest = (EvaluationAttribute)mTestEvaluateIterator.Next ();
			print (mEvaluationAttributeTest.BarNumber);
			print (mEvaluationAttributeTest.TorsoPositionWeight);
			print (mEvaluationAttributeTest.TorsoRotationWeight);
			print (mEvaluationAttributeTest.LeftHipWeight);
			print (mEvaluationAttributeTest.RightHipWeight);
			print (mEvaluationAttributeTest.LeftShoulderWeight);
			print (mEvaluationAttributeTest.RightSoulderWeight);
		}*/

		/*mTestSongIterator = SingletonGameData.GetInstance ().CreateSongIterator ();//测试SongIterator
		while(mTestSongIterator.HasNext()){
			mSongAttributeTest = (SongAttribute)mTestSongIterator.Next ();
			print (mSongAttributeTest.RealName);
			print (mSongAttributeTest.ProgName);
			print (mSongAttributeTest.SceneID);
			print (mSongAttributeTest.CharacterId);
			print (mSongAttributeTest.BPM);
		}*/


		/*mTestBarIterator = SingletonGameData.GetInstance ().CreateBarIterator ();//测试BarIterator
		while(mTestBarIterator.HasNext()){
			mBarAttributeTest = (BarAttribute)mTestBarIterator.Next();
			print(mBarAttributeTest.BarType);
			print (mBarAttributeTest.SameBarsNumber);
		}*/


		/*mTestUserPromptsIterator = SingletonGameData.GetInstance ().CreateUserPromptsIterator ();//测试UserPromptsIterator
		while (mTestUserPromptsIterator.HasNext ()){
			mUserPromptsAttributeTest = (UserPromptsAttribute)mTestUserPromptsIterator.Next ();
			print (mUserPromptsAttributeTest.BarNumber);
			print (mUserPromptsAttributeTest.Text);
		}*/

		/*mSongAttributeTest = SingletonGameData.GetInstance ().GetCurrentSongAttribute();
		print (mSongAttributeTest.RealName);
		print (mSongAttributeTest.ProgName);
		print (mSongAttributeTest.SceneID);
		print (mSongAttributeTest.CharacterId);
		print (mSongAttributeTest.BPM);*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
