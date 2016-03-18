//**********************************************************************************************************
//
//文件名(File Name)：                              ActionMatchAlogrithm.cs
//
//功能描述(Description)：                          动作匹配算法
//
//作者(Author)：                                   康文
//
//日期(Create Date)：                              2014.07.21
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using System;
using System.Collections;

public class ActionMatchAlogrithm
{

	//private List mArrayListStdMotionJointQuaternionVector=new ArrayList();
	//private ArrayList mArrayListUserMotionJointQuaternionVector=new ArrayList();
	public virtual void Init(EvaluationAttribute evalutionAttribute)
	{
		/*float mWeightSum=torsoPositionWeight+torsoRotationWeight+leftHipWeight+rightHipWeight+leftShoulderWeight+rightShoulderWeight;
		mTorsoPositionWeight=torsoPositionWeight/mWeightSum;
		mTorsoRotationWeight=torsoRotationWeight/mWeightSum;
		mLeftHipWeight=leftHipWeight/mWeightSum;
		mRightHipWeight=rightHipWeight/mWeightSum;
		mLeftShoulderWeight=leftShoulderWeight/mWeightSum;
		mRightShoulderWeight=rightShoulderWeight/mWeightSum;
		mArrayListStdMotionJointQuaternionVector.Clear();
		mArrayListUserMotionJointQuaternionVector.Clear();*/
	}
	public virtual void Update(MotionJointQuaternionVector StdJointQuaternionVector,MotionJointQuaternionVector UserJointQuaternionVector)
	{
		/*mArrayListStdMotionJointQuaternionVector.Add(StdJointQuaternionVector);
		mArrayListUserMotionJointQuaternionVector.Add(UserJointQuaternionVector);
*/
	}
	public virtual MatchResult Evaluate()
	{
		//MatchResult mMatchResult;
		//具体算法
		return null;
		//Clean();
	}
	public virtual void Clean()
	{
		//mArrayListStdMotionJointQuaternionVector.Clear();
		//mArrayListUserMotionJointQuaternionVector.Clear ();
	}
}


