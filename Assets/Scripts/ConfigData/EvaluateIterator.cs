//**********************************************************************************************************
// 
//文件名(File Name)：                              EvaluateIterator.cs
//
//功能描述(Description)：                          EvaluateIterator迭代器，用于遍历当前歌曲所有评价属性
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
using System;
using System.Collections;
using System.Xml;

public class EvaluateIterator : IIterator {
	//待遍历节点链表
	private XmlNodeList mNodeList;
	//待遍历节点链表迭代器
	private IEnumerator mListEnumerator;
	//当前索引
	private int mIndex;
	//当前遍历的元素
	private EvaluationAttribute mCurrentEvaluation;
	
	//获取当前遍历的集合元素数量
	int IIterator.Count{
		get { return mNodeList.Count; }
	}

	public EvaluateIterator(XmlNodeList nodeList){
		mNodeList = nodeList;
		mListEnumerator = mNodeList.GetEnumerator ();
		mIndex = 0;
	}

	//实现IIterator的三个接口
	Object IIterator.Next(){
		if (mListEnumerator.MoveNext ()) {
			XmlNode node = (XmlNode)mListEnumerator.Current;
			try{
				mCurrentEvaluation = new EvaluationAttribute(
					Convert.ToInt32(node.Attributes["bar_num"].Value),
					Convert.ToSingle(node.Attributes["torso_pos_weight"].Value),
					Convert.ToSingle(node.Attributes["torso_rot_weight"].Value),
					Convert.ToSingle(node.Attributes["left_hip_weight"].Value),
					Convert.ToSingle(node.Attributes["right_hip_weight"].Value),
					Convert.ToSingle(node.Attributes["left_shoulder_weight"].Value),
					Convert.ToSingle(node.Attributes["right_shoulder_weight"].Value)
					);
			}
			catch{
				return null;
			}

			mIndex++;
			return mCurrentEvaluation;
		} 
		else {
			return null;
		}
	}

	bool IIterator.HasNext(){
		return mIndex < mNodeList.Count;
	}

	Object IIterator.Current{
		get { return mCurrentEvaluation; }
	}
}
