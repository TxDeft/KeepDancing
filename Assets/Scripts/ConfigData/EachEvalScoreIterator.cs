//**********************************************************************************************************
// 
//文件名(File Name)：                              EachEvalScoreIterator.cs
//
//功能描述(Description)：                          EachEvalScoreIterator迭代器，用于遍历单次评价的分数标准
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.07.19
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using System.Collections;
using System.Xml;
using System;
using System.IO;

public class EachEvalScoreIterator: IIterator {
	//待遍历节点链表
	private XmlNodeList mNodeList;
	//待遍历节点链表迭代器
	private IEnumerator mListEnumerator;
	//当前索引
	private int mIndex;
	//当前遍历的元素
	private int mCurrent;

	//获取当前遍历的集合元素数量
	int IIterator.Count{
		get { return mNodeList.Count; }
	}

	public EachEvalScoreIterator(XmlNodeList nodeList){
		mNodeList = nodeList;
		mListEnumerator = mNodeList.GetEnumerator ();
		mIndex = 0;
	}

	//实现IIterator的三个接口
	Object IIterator.Next(){
		if (mListEnumerator.MoveNext ()) {
			XmlNode node = (XmlNode)mListEnumerator.Current;
			try{
				mCurrent = Convert.ToInt32 (node.InnerText);
			}
			catch{
				return null;
			}

			mIndex++;
			return mCurrent;
		} 
		else {
			return null;
		}
	}

	bool IIterator.HasNext(){
		return mIndex < mNodeList.Count;
	}

	Object IIterator.Current{
		get { return mCurrent; }
	}
}
