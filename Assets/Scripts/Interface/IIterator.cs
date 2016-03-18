//**********************************************************************************************************
//
//文件名(File Name)：                              IIterator.cs
//
//功能描述(Description)：                          迭代器接口
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.12
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.17
//                 修改理由：                       该接口要定义成public
//          R2:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.27
//                 修改理由：                       加入Count属性
//
//**********************************************************************************************************
using System;

/// <summary>
/// 迭代器接口
/// </summary>
public interface IIterator{
    /// <summary>
    /// 集合是否还有下一个被遍历的元素。
    /// </summary>
    /// <returns>若有，返回true</returns>
    bool HasNext();

    /// <summary>
    /// 遍历集合的下一个元素
    /// </summary>
    /// <returns>返回下一个元素</returns>
	Object Next();

    /// <summary>
    /// 集合当前被遍历的元素
    /// </summary>
    Object Current{
        get;
    }

	/// <summary>
	/// 集合内元素个数
	/// </summary>
	int Count{
		get;
	}
}