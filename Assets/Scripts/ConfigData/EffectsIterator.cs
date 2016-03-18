//**********************************************************************************************************
//
//文件名(File Name)：                              EffectsIterator.cs
//
//功能描述(Description)：                          特效迭代器
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.08.02
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using System;
using System.Collections;
using System.Xml;

public class EffectsIterator :IIterator {
	//待遍历节点链表
	private XmlNodeList mNodeList;
	//待遍历节点链表迭代器
	private IEnumerator mListEnumerator;
	//当前索引
	private int mIndex;
	//当前遍历的元素
	private EffectAttribute mCurrentEffect;
	
	//获取当前遍历的集合元素数量
	int IIterator.Count{
		get { return mNodeList.Count; }
	}
	
	public EffectsIterator(XmlNodeList nodeList){
		mNodeList = nodeList;
		mListEnumerator = mNodeList.GetEnumerator ();
		mIndex = 0;
	}
	
	//实现IIterator的三个接口
	Object IIterator.Next(){
		if (mListEnumerator.MoveNext ()) {
			XmlNode node = (XmlNode)mListEnumerator.Current;
			try{
				mCurrentEffect = new EffectAttribute(
					Convert.ToInt32(node.Attributes["effect_id"].Value),
					Convert.ToInt32(node.Attributes["bar_num"].Value),
					Convert.ToInt32(node.Attributes["beat"].Value),
					Convert.ToBoolean(node.Attributes["flag_pos"].Value),
					Convert.ToSingle(node.Attributes["pos_x"].Value),
					Convert.ToSingle(node.Attributes["pos_y"].Value),
					Convert.ToSingle(node.Attributes["pos_z"].Value)
					);
			}
			catch{
				return null;
			}
			
			mIndex++;
			return mCurrentEffect;
		} 
		else {
			return null;
		}
	}
	
	bool IIterator.HasNext(){
		return mIndex < mNodeList.Count;
	}
	
	Object IIterator.Current{
		get { return mCurrentEffect; }
	}
}
