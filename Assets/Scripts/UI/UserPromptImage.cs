//**********************************************************************************************************
//
//文件名(File Name)：                              UserPromptImage.cs
//
//功能描述(Description)：                          切换用户提示图片
//
//作者(Author)：                                   李爽
//
//日期(Create Date)：                              2014.07.08
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

public class UserPromptImage: MonoBehaviour {
	private IIterator mUserPromptImageIterator;
	//保存用户提示图片中的小节数
	private int mBarNum;
	private int mFirstBarNum;
	//保存已过小节数
	private int mBarNumber = 0;
	//保存用户提示图片中的图片名称
	private string mText;
	//用户提示图片索引
	public int mImageIndex;
	private float mSpeed = 10;
	public int mIndex;
	public Vector3[] mVector;
	public Rect[] mRect;
	private int mUserPromptImageIndex = 1;
	public Texture mTexture1;
	public Texture mTexture2;
	public Texture mTexture3;
	public Texture mTexture4;
	private bool mIsMoved = false;
	private bool mIsMoved1 = false;
	private bool mIsMoved2 = true;
	private UserPromptsAttribute mUserPromptsAttribute;
	// Use this for initialization
	void Start () {
		mRect = new Rect[2];
		mRect [0] = new Rect (-140,0,140,80);
		mRect [1] = new Rect (-110,0,110,65);
		mVector = new Vector3[3];
		mVector[0] = new Vector3 (0.95f, 0.5f, 0);
		mVector[1] = new Vector3 (0.9f,0.3f,0);
		mVector[2] = new Vector3 (0.95f, 0.1f, 0);
		mUserPromptImageIterator = SingletonGameData.GetInstance ().CreateUserPromptsIterator ();
		if (mUserPromptImageIterator.HasNext ()) {
			mUserPromptsAttribute =(UserPromptsAttribute) mUserPromptImageIterator.Next();
		}
		mBarNum = mUserPromptsAttribute.BarNumber;
		mFirstBarNum = mBarNum;
		mText = mUserPromptsAttribute.Text;
		switch (mIndex) {
		case 1:
			guiTexture.texture = null;
			break;
		case 2:
			guiTexture.texture = mTexture1;
			break;
		case 3:
			guiTexture.texture = mTexture2;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(mIndex){
		case 1:
			if(SingletonBeatManager.GetInstance().BarIndex == mFirstBarNum-2){
				gameObject.transform.position = mVector[2];
				guiTexture.texture = mTexture3;
				guiTexture.pixelInset = mRect[1];
			}
			if(SingletonBeatManager.GetInstance().BarIndex ==mFirstBarNum-1){
				if(gameObject.transform.position.x>mVector[1].x){
					transform.Translate((mVector[1]-mVector[2])*Time.deltaTime*mSpeed);
				}
				else{
					gameObject.transform.position = mVector[1];
					guiTexture.pixelInset = mRect[0];
				}
			}
			if(SingletonBeatManager.GetInstance().BarIndex == mFirstBarNum){
				if(gameObject.transform.position.x<mVector[0].x){
					transform.Translate ((mVector [0] - mVector [1]) * Time.deltaTime * mSpeed);
				}
				else{
					gameObject.transform.position = mVector[0];
					guiTexture.pixelInset = mRect[1];
				}
			}
			break;
		case 2:
			if (SingletonBeatManager.GetInstance ().BarIndex == mFirstBarNum - 2) {
				if(gameObject.transform.position.x<mVector[0].x){
					transform.Translate ((mVector [0] - mVector [1]) * Time.deltaTime * mSpeed);
				}
				else{
					gameObject.transform.position = mVector[0];
					guiTexture.pixelInset = mRect[1];
				}
			}
			if (SingletonBeatManager.GetInstance ().BarIndex == mFirstBarNum - 1) {
				gameObject.transform.position = mVector[2];
				guiTexture.texture = mTexture4;
				guiTexture.pixelInset = mRect[1];
			}
			if(SingletonBeatManager.GetInstance().BarIndex==mFirstBarNum){
				if(gameObject.transform.position.x>mVector[1].x){
					transform.Translate((mVector[1]-mVector[2])*Time.deltaTime*mSpeed);
				}
				else{
					gameObject.transform.position = mVector[1];
					guiTexture.pixelInset = mRect[0];
				}
			}
			
			break;
			
		case 3:
			if(SingletonBeatManager.GetInstance().BarIndex == mFirstBarNum-2){
				if(gameObject.transform.position.x>mVector[1].x){
					transform.Translate((mVector[1]-mVector[2])*Time.deltaTime*mSpeed);
				}
				else{
					gameObject.transform.position = mVector[1];
					guiTexture.pixelInset = mRect[0];
				}
			}
			if(SingletonBeatManager.GetInstance().BarIndex == mFirstBarNum-1){
				if(gameObject.transform.position.x<mVector[0].x){
					transform.Translate ((mVector [0] - mVector [1]) * Time.deltaTime * mSpeed);
				}
				else{
					gameObject.transform.position = mVector[0];
					guiTexture.pixelInset = mRect[1];
				}
			}
			if(SingletonBeatManager.GetInstance().BarIndex==mFirstBarNum){
				gameObject.transform.position = mVector[2];
				guiTexture.texture = SingletonLoadGamePlayResources.GetInstance().ArrayTextureUserPromptImages[mIndex%3];
				guiTexture.pixelInset = mRect[1];
			}
			
			break;
			
			
		}
		if(SingletonBeatManager.GetInstance().BarIndex>mBarNum){
			mBarNumber = mBarNum;
			if ((gameObject.transform.position.y == mVector [0].y)&&mIsMoved2) {
				gameObject.transform.position = mVector[2];
				guiTexture.texture = SingletonLoadGamePlayResources.GetInstance().ArrayTextureUserPromptImages[mIndex%3+3*mImageIndex];
				guiTexture.pixelInset = mRect[1];
				if (mUserPromptImageIterator.HasNext ()) {
					mUserPromptsAttribute =(UserPromptsAttribute) mUserPromptImageIterator.Next();
				}
				mBarNum = mUserPromptsAttribute.BarNumber;
				mText = mUserPromptsAttribute.Text;
				mBarNum = mBarNum + mBarNumber;
				mIsMoved2 = false;
				mImageIndex++;
			}
			else if((gameObject.transform.position.x == mVector[2].x||mIsMoved)){
				if(gameObject.transform.position.x>mVector[1].x){
					mIsMoved = true;
					transform.Translate((mVector[1]-mVector[2])*Time.deltaTime*mSpeed);
					guiTexture.pixelInset = mRect[1];
				}
				else{
					gameObject.transform.position = mVector[1];
					guiTexture.pixelInset = mRect[0];
					if (mUserPromptImageIterator.HasNext ()) {
						mUserPromptsAttribute =(UserPromptsAttribute) mUserPromptImageIterator.Next();
					}
					mBarNum = mUserPromptsAttribute.BarNumber;
					mText = mUserPromptsAttribute.Text;
					mBarNum = mBarNum + mBarNumber;
					mIsMoved = false;
				}
			}
			else if((gameObject.transform.position.x == mVector [1].x||mIsMoved1)){
				if(gameObject.transform.position.y<mVector[0].y){
					mIsMoved1 = true;
					transform.Translate ((mVector [0] - mVector [1]) * Time.deltaTime * mSpeed);
					guiTexture.pixelInset = mRect[0];
					mIsMoved2 = false;
				}
				else{
					gameObject.transform.position = mVector[0];
					guiTexture.pixelInset = mRect[1];
					if (mUserPromptImageIterator.HasNext ()) {
						mUserPromptsAttribute =(UserPromptsAttribute) mUserPromptImageIterator.Next();
					}
					mBarNum = mUserPromptsAttribute.BarNumber;
					mText = mUserPromptsAttribute.Text;
					mBarNum = mBarNum + mBarNumber;
					mIsMoved1=false;
					mIsMoved2 = true;
				}
			}
		}
	}
}