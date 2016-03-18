//**********************************************************************************************************
//
//文件名(File Name)：                              EffectsManager.cs
//
//功能描述(Description)：                          控制显示特效
//
//作者(Author)：                                   connie
//
//日期(Create Date)：                              2014.08.04
//
//修改记录(Revision History)：                     暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EffectsManager : MonoBehaviour , IObserver{
	//特效属性组
	private EffectAttribute[] mArrayEffectAttribute;
	private IIterator mEffectsIterator;
	private ISubject mEffectsManager;
	//当前进行到多少小节
	private int mCurrentBarIndex;
	int i=0;
	int j=0;
	int mCount=0;
	// Use this for initialization
	void Start () {
		//订阅主题SingletonBeatManager
		mEffectsManager =SingletonBeatManager.GetInstance ();
		mEffectsManager .AddObserver(this);
		mEffectsIterator=SingletonGameData.GetInstance().CreateEffectIterator();
		//加载特效属性,并排序
		mArrayEffectAttribute=new EffectAttribute[mEffectsIterator.Count] ;

		while (mEffectsIterator.HasNext ()){
			mArrayEffectAttribute[i]=(mEffectsIterator.Next ()) as EffectAttribute ;
			i++;
		}
		MySortClass mySortClass=new MySortClass ();
		Array.Sort (mArrayEffectAttribute,mySortClass);
		/*foreach (EffectAttribute effects in mArrayEffectAttribute){
			print (effects.BarNum+"dd" );
			print (effects .Beat+"cc");
		}*/


	
	}
	
	// Update is called once per frame
	void Update () {
		/*mCurrentBarIndex=SingletonBeatManager.GetInstance ().BarIndex;
		if(mArrayEffectAttribute[j].BarNum  == (mCurrentBarIndex-1)){
			if(mCount == mArrayEffectAttribute[j].Beat){
				if(mArrayEffectAttribute[j].FlagPos){
					GameObject Effect=	Instantiate(SingletonLoadGamePlayResources.GetInstance ().ArrayEffects[mArrayEffectAttribute[j].EffectId],new Vector3 (mArrayEffectAttribute[j].PosX,mArrayEffectAttribute[j].PosY,mArrayEffectAttribute[j].PosZ),new Quaternion ()) as GameObject ;
				j++;
				Effect.SetActive (true);
				}
				else {
					GameObject Effect=	Instantiate(SingletonLoadGamePlayResources.GetInstance ().ArrayEffects[mArrayEffectAttribute[j].EffectId]) as GameObject ;
					Effect.SetActive (true);
				}
			}
		}*/
	}
	//订阅主题数据
	void IObserver.ObserverUpdate(ISubject subject,System.Object arg){
		if( (EnumBeatType)arg ==EnumBeatType.DOWNBEAT){
			mCount =0;
		}
		else {
			mCount ++;
		}
		print ("mCount"+mCount );
		mCurrentBarIndex=SingletonBeatManager.GetInstance ().BarIndex;
		while (j < mEffectsIterator.Count && 
		       mArrayEffectAttribute[j].BarNum  == (mCurrentBarIndex-1) && 
		       (mCount == mArrayEffectAttribute[j].Beat)){
			//if(mCount == mArrayEffectAttribute[j].Beat){
			if(mArrayEffectAttribute[j].FlagPos){
				//print ("dddddd");
				Instantiate(SingletonLoadGamePlayResources.GetInstance ().ArrayEffects[mArrayEffectAttribute[j].EffectId],new Vector3 (mArrayEffectAttribute[j].PosX,mArrayEffectAttribute[j].PosY,mArrayEffectAttribute[j].PosZ),Quaternion .identity );
				//Effect.SetActive (true);
			}
			else {
				//print ("ccccc"+mCurrentBarIndex+"   "+mArrayEffectAttribute[j].EffectId);
				Instantiate(SingletonLoadGamePlayResources.GetInstance ().ArrayEffects[mArrayEffectAttribute[j].EffectId]);
				//Effect.SetActive (true);
			}
			j++;
		}
	}
}

public class MySortClass:IComparer<EffectAttribute> {

	public int Compare(EffectAttribute x, EffectAttribute y){
		if(x.BarNum >y.BarNum ){
			return 1;
		}
		else if (x.BarNum <y.BarNum ){
			return -1;
		}
		else {
			if (x.Beat>y.Beat ){
				return 1;
			}
			else if (x.Beat <y.Beat ){
				return -1;
			}
			else{
				return 0;
			}
		}
	}

}

