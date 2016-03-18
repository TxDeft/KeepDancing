//**********************************************************************************************************
//
//文件名(File Name)：                              SongAttribute.cs
//
//功能描述(Description)：                          音乐属性
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
/// 音乐属性
/// </summary>
public class SongAttribute{
	private string mRealName;			//歌曲名称，如“最炫民族风”
	private string mProgName;			//歌曲在程序中使用的名字，如"zuixuanmingzufeng"。图片相关命名与它有关。
	private int mSceneId;				//场景ID
	private int mCharacterId;			//角色ID
	private int mBPM;					//BPM，beat per minute，每分钟节拍数
	
	// 构造函数
	public SongAttribute(string realName, string progName, int sceneId, int characterId, int BMP){
		mRealName = realName;
		mProgName = progName;
		mSceneId = sceneId;
		mCharacterId = characterId;
		mBPM = BMP;
	}

	// 属性
	public string RealName{
		get { return mRealName; }
	}
	public string ProgName{
		get { return mProgName; }
	}
	public int SceneID{
		get { return mSceneId; }
	}
	public int CharacterId{
		get { return mCharacterId; }
	}
	public int BPM{
		get { return mBPM; }
	}
}