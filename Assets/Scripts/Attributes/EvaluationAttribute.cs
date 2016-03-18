//**********************************************************************************************************
// 
//文件名(File Name)：                              EvaluationAttribute.cs
//
//功能描述(Description)：                          用户评价属性，包括评价时机和权重
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.13
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.18
//                 修改理由：                       构造函数忘了public
//
//**********************************************************************************************************

/// <summary>
/// 评价属性 
/// </summary>
public class EvaluationAttribute{
	private int mBarNumber;                 //若干小节后，对用户进行一次评价
    private float mTorsoPositionWeight;     //根骨位置权重
    private float mTorsoRotationWeight;     //根骨旋转权重
    private float mLeftHipWeight;           //左臀部旋转权重
    private float mRightHipWeight;          //右臀部旋转权重
    private float mLeftShoulderWeight;      //左肩部旋转权重
	private float mRightShoulderWeight;      //右肩部旋转权重
	
	// 构造函数
    public EvaluationAttribute(int barNumber, float torsoPositionWeight, float torsoRotationWeight, float leftHipWeight, float rightHipWeight, float leftShoulderWeight, float rightShoulderWeight) {
        mBarNumber = barNumber;
        mTorsoPositionWeight = torsoPositionWeight;
        mTorsoRotationWeight = torsoRotationWeight;
        mLeftHipWeight = leftHipWeight;
        mRightHipWeight = rightHipWeight;
        mLeftShoulderWeight = leftShoulderWeight;
		mRightShoulderWeight = rightShoulderWeight;
	}

	// 属性
    public int BarNumber{
        get { return mBarNumber; }
    }
    public float TorsoPositionWeight{
        get { return mTorsoPositionWeight; }
    }
	public float TorsoRotationWeight{
		get { return mTorsoRotationWeight; }
	}
	public float LeftHipWeight{
		get { return mLeftHipWeight; }
	}
	public float RightHipWeight{
		get { return mRightHipWeight; }
	}
	public float LeftShoulderWeight{
		get { return mLeftShoulderWeight; }
	}
	public float RightSoulderWeight{
		get { return mRightShoulderWeight; }
	}
}