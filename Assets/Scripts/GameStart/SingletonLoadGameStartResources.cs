//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonLoadGameStartResources.cs
//
//功能描述(Description)：                          根据配置文件，动态加载游戏资源
//
//作者(Author)：                                   白野
//
//日期(Create Date)：                              2014.07.29
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class SingletonLoadGameStartResources : MonoBehaviour {
	//singleton
	private static SingletonLoadGameStartResources mInstance;
	public static SingletonLoadGameStartResources GetInstance(){
		return mInstance;
	}
	
	//歌曲选择图片
	private Texture[] mArrayTextureSongChooseImages;
	//歌曲文字图片
	private Texture[] mArrayTextureSongWords;
	//当前音乐副歌部分
	private AudioClip[] mCurrentSong_chorus;
	//当前音乐名字
	private string[] mStrSongName;
	

	//属性们
	public Texture[] ArrayTextureSongChooseImages{
		get { return mArrayTextureSongChooseImages; }
	}

	public Texture[] ArrayTextureSongWords{
		get { return mArrayTextureSongWords; }
	}

	public AudioClip[] CurrentSong_chorus{
		get { return mCurrentSong_chorus; }
	}
	public string[] StrSongName{
		get { return mStrSongName;}
	}

	// Use this for initialization
	void Start () {
		mInstance = this;
	
		int i = 0;
		SongAttribute songAttr;
		IIterator songsIter = SingletonGameData.GetInstance ().CreateSongIterator ();
		mCurrentSong_chorus = new AudioClip[songsIter.Count];
		mArrayTextureSongChooseImages = new Texture[songsIter.Count];
		mArrayTextureSongWords = new Texture[songsIter.Count];
		mStrSongName = new string[songsIter.Count];
		//加载音乐副歌、歌曲提示图片、歌曲文字图片
		for (i = 0; i < songsIter.Count; i++) {
			songAttr = songsIter.Next() as SongAttribute;
			mCurrentSong_chorus[i] = Resources.Load("Songs/" + songAttr.ProgName + "_chorus",
			                                        typeof(AudioClip)) as AudioClip;
			mArrayTextureSongChooseImages[i] = Resources.Load ("Songs Image/" + songAttr.ProgName,
			                                                   typeof(Texture)) as Texture;
			mArrayTextureSongWords[i] = Resources.Load ("Songs Word/" + songAttr.ProgName,
			                                            typeof(Texture)) as Texture;
			mStrSongName[i] = songAttr.ProgName;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
