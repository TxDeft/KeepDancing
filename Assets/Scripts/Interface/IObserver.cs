//**********************************************************************************************************
//
//文件名(File Name)：                                IObserver.cs
//
//功能描述(Description)：                            观察者模式——观察者接口
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
/// 观察者模式的观察者接口。在观察者模式中，观察者可以将自己注册到主题对象中，以获得主题的更新。
/// </summary>
public interface IObserver{
    /// <summary>
    /// 主题通知观察者的更新
    /// </summary>
    /// <param name="subject">通知更新的主题</param>
    /// <param name="arg">主题传递给观察者的参数</param>
    void ObserverUpdate(ISubject subject, Object arg);
}