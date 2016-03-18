//**********************************************************************************************************
//
//文件名(File Name)：                              StaticMatch.cs
//
//功能描述(Description)：                          动作静态匹配算法
//
//作者(Author)：                                   康文
//
//日期(Create Date)：                              2014.07.22
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 静态匹配.
/// </summary>
public class StaticMatch:ActionMatchAlogrithm
{
	private float  mTorsoPositionWeight,
	mTorsoRotationWeight,
	mLeftHipWeight,
	mRightHipWeight,
	mLeftShoulderWeight,
	mRightShoulderWeight,
	mTorsoPositionScore,
	mTorsoRotationScore,
	mLeftHipScore,
	mRightHipScore,
	mLeftShoulderScore,
	mRightShoulderScore,
	mStdDistance,
	mRate;
	//private int mcount=0;
	
	private List<float> mListMatchScore;
	private List<MotionJointQuaternionVector> mListStd;
	private List<float> mListScore;
	
	/// <summary>
	/// 构造函数
	/// </summary>
	public StaticMatch(MotionJointQuaternionVector StdJointQuaternionVector, MotionJointQuaternionVector UserJointQuaternionVector)
	{
		mListMatchScore=new List<float>();
		mListStd=new List<MotionJointQuaternionVector>();
		mListScore=new List<float>();
		mStdDistance=Math.Abs (Vector3.Distance(StdJointQuaternionVector.TorsoPosition,UserJointQuaternionVector.TorsoPosition));
		mRate=SingletonActionMatchManager.GetInstance().StaticMatchAlogrithmScoreRate;
	}
	
	public override void Init (EvaluationAttribute evalutionAttribute)
	{
		base.Init (evalutionAttribute);
		float mWeightSum = evalutionAttribute.TorsoPositionWeight+evalutionAttribute.TorsoRotationWeight+evalutionAttribute.LeftHipWeight+evalutionAttribute.RightHipWeight+evalutionAttribute.LeftShoulderWeight+evalutionAttribute.RightSoulderWeight;
		mTorsoPositionWeight = evalutionAttribute.TorsoPositionWeight / mWeightSum;
		mTorsoRotationWeight = evalutionAttribute.TorsoRotationWeight / mWeightSum;
		mLeftHipWeight = evalutionAttribute.LeftHipWeight / mWeightSum;
		mRightHipWeight = evalutionAttribute.RightHipWeight / mWeightSum;
		mLeftShoulderWeight = evalutionAttribute.LeftShoulderWeight / mWeightSum;
		mRightShoulderWeight = evalutionAttribute.RightSoulderWeight / mWeightSum;
		mListMatchScore.Clear();
		mListStd.Clear ();
		
		
		//float mWeightSum = evalutionAttribute.TorsoPositionWeight+evalutionAttribute.TorsoRotationWeight+evalutionAttribute.LeftHipWeight+evalutionAttribute.RightHipWeight+evalutionAttribute.LeftShoulderWeight+evalutionAttribute.RightSoulderWeight;
		
		//mListMatchScore.Clear();
	}
	
	public override void Update (MotionJointQuaternionVector StdJointQuaternionVector, MotionJointQuaternionVector UserJointQuaternionVector)
	{

		if(mListStd.Count < 30){
			mListStd.Add( StdJointQuaternionVector );
		}
		else{
			mListStd.RemoveAt(0);
			mListStd.Add ( StdJointQuaternionVector );
		}
		//base.Update (StdJointQuaternionVector, UserJointQuaternionVector);
		foreach(MotionJointQuaternionVector stdJointQuaternionVector in mListStd)
		{
		float difTorsoPosition = Math.Abs (Vector3.Distance(stdJointQuaternionVector.TorsoPosition,UserJointQuaternionVector.TorsoPosition));
		float difTorsoRotation = Math.Abs (Quaternion.Angle(stdJointQuaternionVector.TorsoQuaternion,UserJointQuaternionVector.TorsoQuaternion));
		float difLeftHip = Math.Abs (Quaternion.Angle(stdJointQuaternionVector.LeftHipQuaternion,UserJointQuaternionVector.LeftHipQuaternion));
		float difRightHip = Math.Abs (Quaternion.Angle(stdJointQuaternionVector.RightHipQuaternion,UserJointQuaternionVector.RightHipQuaternion));
		float difLeftShoulder = Math.Abs (Quaternion.Angle(stdJointQuaternionVector.LeftShoulderQuaternion,UserJointQuaternionVector.LeftShoulderQuaternion));
		float difRightShoulder = Math.Abs (Quaternion.Angle(stdJointQuaternionVector.RightShoulderQuaternion,UserJointQuaternionVector.RightShoulderQuaternion));
		mTorsoPositionScore = ScoreDistance(difTorsoPosition);
		mTorsoRotationScore = ScoreAngle(difTorsoRotation);
		mLeftHipScore = ScoreAngle(difLeftHip);
		mRightHipScore = ScoreAngle(difRightHip);
		mLeftShoulderScore = ScoreAngle(difLeftShoulder);
		mRightShoulderScore = ScoreAngle(difRightShoulder);
		float totalScore = mTorsoPositionWeight * mTorsoPositionScore + mTorsoRotationWeight * mTorsoRotationScore + mLeftHipWeight * mLeftHipScore + mRightHipWeight * mRightHipScore + mLeftShoulderWeight * mLeftShoulderScore + mRightShoulderWeight * mRightShoulderScore;
		mListScore.Add(totalScore);
		}
		BestScore();

			/*if ( mcount < 10){
			mListFrameScore.Add(totalScore);
			mcount++;
		}
		else {
			mcount=0;
			BestScore();
		}*/
	}
	
	private void BestScore(){
		float bestScore=0;
		foreach(float score in mListScore)
		{
			if(score > bestScore){
				bestScore=score;
			}
		}
		mListScore.Clear();
		mListMatchScore.Add(bestScore);
	}

	public override MatchResult Evaluate ()
	{
		
		float sumScore,finalScore;
		EnumDetailMatchInfo detailInfo;
		sumScore = 0;
		int count = mListMatchScore.Count;
		foreach(float score in mListMatchScore)
		{
			sumScore = sumScore + score;
		}
		finalScore = sumScore / count ;	
		detailInfo=EnumDetailMatchInfo.NORMAL;
		MatchResult matchResult = new MatchResult(finalScore,detailInfo);
		Clean();
		//return base.Evaluate ();
		return matchResult;
		
	}
	
	public override void Clean ()
	{
		//base.Clean ();
		mListMatchScore.Clear();
	}
	
	public float ScoreAngle(float diffrence)
	{
		float score;
		if ( diffrence <= 90 ) {
			score = 100 - 0.5f * diffrence;
		}
		else {
			score = 0;
		}
		return score;
	}
	
	
	public float ScoreDistance(float diffrence)
	{
		float score,relativeDifference;
		relativeDifference=Math.Abs(mStdDistance-diffrence);
		score = 100 - mRate * relativeDifference;
		if(score<0)
		{
			score =0;
		}
		return score;
	}
}


