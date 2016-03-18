//**********************************************************************************************************
// 
//文件名(File Name)：                              SongIterator.cs
//
//功能描述(Description)：                          SongIterator迭代器，用于遍历所有歌曲属性
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.07.19
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.27
//                 修改理由：                       加入Count属性
//
//**********************************************************************************************************
using System.Collections;
using System.Xml;
using System;

public class SongIterator : IIterator {
	//待遍历节点链表
	private XmlNodeList mNodeList;
	//待遍历节点链表迭代器
	private IEnumerator mListEnumerator;
	//当前索引
	private int mIndex;
	//当前遍历的元素
	private SongAttribute mCurrentSong;
	
	//获取当前遍历的集合元素数量
	int IIterator.Count{
		get { return mNodeList.Count; }
	}

	public SongIterator(XmlNodeList nodeList){
		mNodeList = nodeList;
		mListEnumerator = mNodeList.GetEnumerator ();
		mIndex = 0;
	}

	//实现IIterator的三个接口
	Object IIterator.Next(){
		if (mListEnumerator.MoveNext ()) {
			XmlNode node = (XmlNode)mListEnumerator.Current;
			try{
				mCurrentSong = new SongAttribute(
			    Convert.ToString (node.Attributes ["real_name"].Value),
				Convert.ToString (node.Attributes ["prog_name"].Value),
				Convert.ToInt32 (node.Attributes ["scene_id"].Value),
				Convert.ToInt32 (node.Attributes ["character_id"].Value),
				Convert.ToInt32 (node.Attributes ["BPM"].Value));
			}
			catch{
				return null;
			}	

			mIndex++;
			return mCurrentSong;
		} 
		else {
			return null;
		}
	}

	bool IIterator.HasNext(){
		return mIndex < mNodeList.Count;
	}

	Object IIterator.Current{
		get { return mCurrentSong; }
	}
}