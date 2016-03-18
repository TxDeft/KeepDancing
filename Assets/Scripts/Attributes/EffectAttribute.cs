//**********************************************************************************************************
//
//文件名(File Name)：                              EffectAttribute.cs
//
//功能描述(Description)：                          特效属性
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.08.02
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************

using UnityEngine;
using System.Collections;

public class EffectAttribute{
	private int mEffectId;			
	private int mBarNum;			
	private int mBeat;				
	private bool mFlagPos;			
	private float mPosX;
	private float mPosY;
	private float mPosZ;
	
	// 构造函数
	public EffectAttribute(int effect_id, int bar_num, int beat, bool flag_pos, float pos_x,float pos_y,float pos_z){
		mEffectId = effect_id;
		mBarNum = bar_num;
		mBeat = beat;
		mFlagPos = flag_pos;
		mPosX = pos_x;
		mPosY = pos_y;
		mPosZ = pos_z;
	}
	
	// 属性
	public int EffectId{
		get { return mEffectId; }
	}
	public int BarNum{
		get { return mBarNum; }
	}
	public int Beat{
		get { return mBeat; }
	}
	public bool FlagPos{
		get { return mFlagPos; }
	}
	public float PosX{
		get { return mPosX; }
	}
	public float PosY{
		get { return mPosY;}
	}
	public float PosZ{
		get { return mPosZ;}
	}
}
