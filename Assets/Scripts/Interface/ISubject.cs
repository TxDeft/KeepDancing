//**********************************************************************************************************
//
//文件名(File Name)：                                 ISubject.cs
//
//功能描述(Description)：                             观察者模式——主题接口
//
//作者(Author)：                                     汪成峰
//
//日期(Create Date)：                                2014.07.12
//
//修改记录(Revision History)：
//          R1:
//                 修改作者：                       汪成峰
//                 修改日期：                       2014.07.17
//                 修改理由：                       该接口要定义成public
//
//**********************************************************************************************************
using System;

/// <summary>
/// 观察者模式的主题接口。主题是观察者模式中被观察的对象。
/// </summary>
public interface ISubject{
    /// <summary>
    /// 增加观察者
    /// </summary>
    /// <param name="observer">加入的观察者</param>
	void AddObserver(IObserver observer);

    /// <summary>
    /// 删除观察者
    /// </summary>
    /// <param name="observer">被删除的观察者</param>
	void DeleteObserver(IObserver observer);

    /// <summary>
    /// 通知所有观察者更新
    /// </summary>
    /// <param name="arg"></param>
	void NotifyObserver(Object arg);
}