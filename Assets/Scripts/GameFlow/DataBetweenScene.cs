//**********************************************************************************************************
//
//文件名(File Name)：                              DataBetweenScene.cs
//
//功能描述(Description)：                          场景跳转需要的数据，利用static变量保存。
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.13
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.18
//                 修改理由：                       三个属性修改为static
//          R2:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.27
//                 修改理由：                       加入平均分
//
//**********************************************************************************************************

/// <summary>
/// 场景跳转传递数据
/// </summary>
public static class DataBetweenScene{
	private static string mSongName;		//歌曲名字（程序中的名字）
	private static float mScore;			//跳舞得分
	private static float mAverageScore;		//跳舞平均分（完成度）
	private static bool mSingle;			//是否单人模式

	// 属性
	public static string SongName{
		get { return mSongName; }
		set { mSongName = value; }
	}
	public static float Score{
		get { return mScore; }
		set { mScore = value; }
	}
	public static float AverageScore{
		get { return mAverageScore; }
		set { mAverageScore = value; }
	}
	public static bool Single{
		get { return mSingle; }
		set { mSingle = value; }
	}
}
