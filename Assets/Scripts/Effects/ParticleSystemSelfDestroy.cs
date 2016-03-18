using UnityEngine;
using System.Collections;

/// <summary>
/// 用于粒子系统渐变自消失。设置的时间到了以后，递归搜索所有子GameObject下的粒子系统，将其loop属性置为false
/// </summary>
public class ParticleSystemSelfDestroy : MonoBehaviour {

	public float DestroyTime = 1.0f;
	// Use this for initialization
	void Start () {
		//StartCoroutine(DoDestroy());
		RecursiveDestory(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (particleSystem != null && !particleSystem.IsAlive()) 
			Object.Destroy (gameObject);*/
	}

	/// <summary>
	/// 按照指定时间关闭粒子系统的Looping，一段时间后粒子消失
	/// </summary>
	IEnumerator DoDestroy(GameObject thisGameObject){
		yield return new WaitForSeconds(DestroyTime);
		//新版粒子系统
		if(thisGameObject.particleSystem != null){
			thisGameObject.particleSystem.loop = false;
		}
		//旧版粒子系统
		ParticleAnimator legacyParticleSystem = thisGameObject.GetComponent<ParticleAnimator>();
		if( legacyParticleSystem != null){
			legacyParticleSystem.autodestruct = true;
		}
		//10s后GameObject消失
		yield return new WaitForSeconds(10);
		Destroy(gameObject);
	}
	
    /// <summary>
    /// 递归延时销毁所有子GameObject
    /// </summary>
    /// <param name='parentGameObject'>
    /// Parent game object.
    /// </param>
	private void RecursiveDestory(GameObject parentGameObject){
        StartCoroutine(DoDestroy(parentGameObject));
		foreach (Transform child in parentGameObject.transform){
			RecursiveDestory(child.gameObject);
		}
    }         
}
