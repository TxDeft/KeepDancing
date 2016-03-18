//**********************************************************************************************************
//
//文件名(File Name)：                              MatchResult.cs
//
//功能描述(Description)：                          动作匹配结果
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.13
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

/// <summary>
/// 动作匹配结果
/// </summary>
public class MatchResult {
	private float mScore;						//动作匹配评分，0-100
	private EnumDetailMatchInfo mDetailInfo;	//动作匹配详细信息
	
	//构造函数
	public MatchResult(float score, EnumDetailMatchInfo detailInfo){
		mScore = score;
		mDetailInfo = detailInfo;
	}

	//分数和匹配详细信息属性
	public float Score {
		get { return mScore; }
		set { mScore = value; }
	}
	public EnumDetailMatchInfo DetailInfo{
		get { return mDetailInfo; }
		set { mDetailInfo = value; } 
	}
}	

/// <summary>
/// 动作匹配详细信息
/// </summary>
public enum EnumDetailMatchInfo{
	NORMAL,					//动作正常
	TOO_SLOW,				//动作太慢
	TOO_FAST				//动作太快
};
