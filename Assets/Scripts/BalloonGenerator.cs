using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BalloonGenerator : MonoBehaviour {
	[SerializeField]
	private int count = 0;
	[SerializeField]
	private Vector3 SpawnFieldScale = new Vector3(1,1,1);
	[SerializeField]
	private GameObject Balloon;
	// Use this for initialization
	void Start () {
		StartCoroutine (Generation ());
	}
	
	private IEnumerator Generation(){
		for (int i = 0; i < count; i++) {
			var randomPosition = new Vector3 (Random.Range (-SpawnFieldScale.x, SpawnFieldScale.x), Random.Range (-SpawnFieldScale.y, SpawnFieldScale.y), Random.Range (-SpawnFieldScale.z, SpawnFieldScale.z));
			Instantiate (Balloon,transform.position + randomPosition, Quaternion.identity);
			yield return null;
		}
		Destroy (gameObject);
		yield return null;
	}
}
