using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonEffect : MonoBehaviour {
	[SerializeField]
	private GameObject balloonEffect;
	void OnParticleCollision(GameObject obj){
		//処理内容
		Instantiate(balloonEffect,obj.transform.position,Quaternion.identity);
	}
}
