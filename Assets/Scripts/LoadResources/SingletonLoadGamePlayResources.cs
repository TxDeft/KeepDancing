//**********************************************************************************************************
//
//文件名(File Name)：                              SingletonLoadGamePlayResources.cs
//
//功能描述(Description)：                          根据配置文件，动态加载游戏资源
//
//作者(Author)：                                   汪成峰
//
//日期(Create Date)：                              2014.07.27
//
//修改记录(Revision History)：
//                                  			  暂无
//
//**********************************************************************************************************
using UnityEngine;
using System.Collections;

/// <summary>
/// 动态加载游戏资源
/// </summary>
public class SingletonLoadGamePlayResources : MonoBehaviour {
	//singleton
	private static SingletonLoadGamePlayResources mInstance;
	public static SingletonLoadGamePlayResources GetInstance(){
		return mInstance;
	}

	//用户提示图片
	private Texture[] mArrayTextureUserPromptImages;
	//当前音乐资源
	private AudioClip mCurrentSong;
	//角色
	private GameObject[] mArrayCharacters;
	//主角，用于光环跟随
	private Transform mMainPlayerTransform;
	//场景
	private GameObject[] mArrayScenes;
	//特效
	private GameObject[] mArrayEffects;

	//属性们
	public Texture[] ArrayTextureUserPromptImages{
		get { return mArrayTextureUserPromptImages; }
	}

	public AudioClip CurrentSong{
		get { return mCurrentSong; }
	}

	public GameObject[] ArrayCharacters{
		get { return mArrayCharacters; }
	}

	public Transform MainPlayerTransform{
		get { return mMainPlayerTransform;}
	}

	public GameObject[] ArrayScenes{
		get { return mArrayScenes; }
	}

	public GameObject[] ArrayEffects{
		get { return mArrayEffects; }
	}

	// Use this for initialization
	void Start () {
		//Resources.Load用法
		//http://docs.unity3d.com/ScriptReference/Resources.Load.html
		//http://blog.csdn.net/mxlcl0557/article/details/11195575

		mInstance = this;

		int i = 0;
		IIterator userPromptsIter = SingletonGameData.GetInstance().CreateUserPromptsIterator();

		//加载音乐、用户提示图片和特效
		mCurrentSong = Resources.Load("Songs/" + DataBetweenScene.SongName, typeof(AudioClip)) as AudioClip;
		mArrayTextureUserPromptImages = new Texture[userPromptsIter.Count];
		for(i = 0; i < userPromptsIter.Count; i++){
			mArrayTextureUserPromptImages[i] = Resources.Load("User Prompt Images/" + DataBetweenScene.SongName + "/" + i.ToString(), typeof(Texture)) as Texture;
		}

		//加载特效
		EffectAttribute effectAttribute;
		IIterator effectsIterator=SingletonGameData.GetInstance().CreateEffectIterator();
		mArrayEffects=new GameObject[effectsIterator.Count];
		while (effectsIterator.HasNext ()){
			effectAttribute=(effectsIterator.Next ()) as EffectAttribute ;
			i=effectAttribute.EffectId ;
			if ( mArrayEffects[i]== null ){
				mArrayEffects[i]=Resources .Load ("Effect Prefabs/"+i.ToString (),typeof (GameObject )) as GameObject ;
			}
		}


		//TODO: 场景和角色，当前各有一个，均在Song的属性中，第二版改为多个场景切换，角色排列多个
		//加载单个场景和单个角色
		mArrayScenes = new GameObject[1];
		mArrayCharacters = new GameObject[1];
		mArrayScenes[0] = Instantiate(Resources.Load("Scene Prefabs/Scene 0", typeof(GameObject))) as GameObject;
		mArrayCharacters[0] = Instantiate(Resources.Load("Character Prefabs/Character 0", typeof(GameObject))) as GameObject;
		mMainPlayerTransform = GameObject.FindWithTag("Standard Torso").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
