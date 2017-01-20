using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPlate : MonoBehaviour {
	public static float lateOnWoodPosition = 0;
	private bool isPlayerOnWood = false;
	private Coroutine playSound;
	[SerializeField]
	private GameObject Squeaking;

	
	void OnCollisionStay(Collision col){
		if (col.gameObject.layer == 8 && Mathf.Abs(col.contacts[0].point.z - lateOnWoodPosition) > 0.85f + Random.Range(-0.5f,0.5f) ) {
			Instantiate (Squeaking, col.contacts [0].point, Quaternion.identity);
			lateOnWoodPosition = col.contacts [0].point.z;
		}
	}

	private IEnumerator PlaySqueaking(){
		while (true) {
			yield return new WaitForSeconds (Random.Range (4, 10));
			Instantiate (Squeaking, new Vector3(transform.position.x,transform.position.y,transform.position.z + Random.Range(-transform.lossyScale.z/2,transform.lossyScale.z/2)), Quaternion.identity);
			yield return null;
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.layer == 8) {
			isPlayerOnWood = true;
			playSound = StartCoroutine (PlaySqueaking());
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.layer == 8) {
			isPlayerOnWood = false;
			StopCoroutine (playSound);
		}
	}
}
