//**********************************************************************************************************
//
//文件名(File Name)：                              UserPromptsAttribute.cs
//
//功能描述(Description)：                          用户提示属性，包括用户提示间隔的小节数和用户提示文字。
//											      用户提示图片根据歌曲在程序中使用的名字确定，不必写在这里。
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
/// 用户提示属性
/// </summary>
public class UserPromptsAttribute{
	private int mBarNumber;			//间隔的小节数
	private string mText;			//提示文字
	
	// 构造函数
	public UserPromptsAttribute(int barNumber, string text){
		mBarNumber = barNumber;
		mText = text;
	}

	// 属性
	public int BarNumber{
		get { return mBarNumber; }
	}
	public string Text{
		get { return mText; }
	}
}
