//**********************************************************************************************************
//
//文件名(File Name)：                              BarAttribute.cs
//
//功能描述(Description)：                          小节属性
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
/// 小节属性
/// </summary>
public class BarAttribute{
	private EnumBarType mBarType;		//小节类型
	private int mSameBarsNumber;		//与该小节相同的小节数量
	
	// 构造函数
	public BarAttribute(EnumBarType barType, int sameBarsNumber){
		mBarType = barType;
		mSameBarsNumber = sameBarsNumber;
	}

	// 属性
	public EnumBarType BarType{
		get { return mBarType; }
	}
	public int SameBarsNumber{
		get { return mSameBarsNumber; }
	}
}


/// <summary>
/// 小节类型，只包含常见节拍类型
/// </summary>
public enum EnumBarType{
	BEAT_1_4,		//1/4拍
	BEAT_2_4,		//2/4拍
	BEAT_3_4,		//3/4拍
	BEAT_4_4,		//4/4拍
	BEAT_3_8,		//3/8拍
	BEAT_6_8,		//6/8拍
	OTHER_BEAT
};