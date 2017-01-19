using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPlate : MonoBehaviour {
	public static float lateOnWoodPosition = 0;
	[SerializeField]
	private GameObject Squeaking;

	
	void OnCollisionStay(Collision col){
		if (col.gameObject.layer == 8 && Mathf.Abs(col.contacts[0].point.z - lateOnWoodPosition) > 0.85f + Random.Range(-0.5f,0.5f) ) {
			Instantiate (Squeaking, col.contacts [0].point, Quaternion.identity);
			lateOnWoodPosition = col.contacts [0].point.z;
		}
	}
}
