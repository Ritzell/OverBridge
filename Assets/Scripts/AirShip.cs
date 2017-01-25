using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip : MonoBehaviour {
	[SerializeField]
	private float scale = 1;
	[SerializeField]
	private GameObject AirShipCamera;
	[SerializeField]
	private GameObject[] Propeller = new GameObject[2];

	private GameObject player;
	private Vector3 center;


	void Start(){
		player = FindObjectOfType<Player> ().gameObject;
		center = FindObjectOfType<WoodPlate> ().gameObject.transform.position;
		StartCoroutine (RotatePropellers ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (center, Vector3.up, scale * Time.deltaTime);
		//transform.GetComponent<Rigidbody> ().AddForce (Mathf.PerlinNoise(Time.time,Time.deltaTime) - 0.5f, 0, Mathf.PerlinNoise(Time.deltaTime,Time.time) - 0.5f,ForceMode.Acceleration);
		var StartRt = AirShipCamera.transform.rotation;
		AirShipCamera.transform.LookAt (player.transform);
		var EndRt = AirShipCamera.transform.rotation;
		AirShipCamera.transform.rotation = StartRt;
		AirShipCamera.transform.rotation = Quaternion.Lerp(StartRt,EndRt,Time.deltaTime*5);
	}

	IEnumerator RotatePropellers(){
		while (true) {
			foreach (GameObject propeller in Propeller) {
				propeller.transform.Rotate (0, 0, 36000 * Time.deltaTime);
			}
			yield return null;
		}
	}
}
