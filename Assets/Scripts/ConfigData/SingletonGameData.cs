//**********************************************************************************************************
// 
//文件名(File Name)：                              SingletonGameData.cs
//
//功能描述(Description)：                          提供六个迭代器，用于获取游戏配置数据
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
using System;
using System.Xml;
using System.IO ;

public class SingletonGameData : MonoBehaviour {
	//singleton
	public TextAsset Config;
	//根节点
	private XmlNode mRoot;
	private static SingletonGameData mInstance;
	public  static SingletonGameData GetInstance(){
		return mInstance;
	}

	// Use this for initialization
	void Start () {
		mInstance = this;
		//获取配置文件根节点
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml(Config.text);
		XmlElement rootElem = doc.DocumentElement;
		mRoot = (XmlNode)rootElem;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//
	//以下六个函数获取迭代器，用于遍历游戏配置文件数据
	//
	public IIterator CreateEachEvalScoreIterator (){
		XmlNodeList nodelist = mRoot.SelectNodes ("/Configuration/EachEvalScore/ScoreLowerBound");
		return new EachEvalScoreIterator (nodelist);
	}
	public IIterator CreateTotalEvalScoreIterator(){
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/TotalEvalScore/ScoreLowerBound");
		return new TotalEvalScoreIterator(nodeList);
	}
	public IIterator CreateEvaluateIterator(){
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']/Evaluations/Evaluation");
		return new EvaluateIterator(nodeList);
	}
	public IIterator CreateSongIterator(){
		//XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']");
		//获得所有的歌曲，用于选择歌曲的场景，而不是仅获取当前歌曲
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song");
		return new SongIterator (nodeList);
	}
	public IIterator CreateBarIterator(){
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']/Bars/SameBars");
		return new BarIterator (nodeList);
	}
	public IIterator CreateUserPromptsIterator(){
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']/UserPrompts/Prompt");
		return new UserPromptsIterator (nodeList);
	}

	public IIterator CreateEffectIterator(){
		XmlNodeList nodeList = mRoot.SelectNodes ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']/Effects/Effect");
		return new EffectsIterator (nodeList);
	}

	/// <summary>
	/// 获得当前选择的歌曲的属性
	/// </summary>
	/// <returns>返回歌曲属性对象</returns>
	public SongAttribute GetCurrentSongAttribute(){
		XmlNode node =  mRoot.SelectSingleNode ("/Configuration/Songs/Song[@prog_name='" + DataBetweenScene.SongName + "']");
		SongAttribute song;
		if(node == null){
			return null;
		}
		try{
			song = new SongAttribute(
				Convert.ToString (node.Attributes ["real_name"].Value),
				Convert.ToString (node.Attributes ["prog_name"].Value),
				Convert.ToInt32 (node.Attributes ["scene_id"].Value),
				Convert.ToInt32 (node.Attributes ["character_id"].Value),
				Convert.ToInt32 (node.Attributes ["BPM"].Value)
				);
		}
		catch{
			return null;
		}	
		return song;
	}
}
