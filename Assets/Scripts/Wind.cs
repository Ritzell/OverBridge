using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{

	[SerializeField]
	private float Windpower;
	[SerializeField]
	private float scale;
	[SerializeField]
	private float power = 1;
	[SerializeField]
	private ParticleSystem windParticle;

	private void Start()
	{
		StartCoroutine (WindBreathe ());
	}

	private IEnumerator WindBreathe()
	{
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
			Windpower = Mathf.PerlinNoise (Windpower * scale, Time.time * scale) * power;
			foreach (GameObject ob in Object.FindObjectsOfType(typeof(GameObject))) {
				if (ob.GetComponent<Rigidbody> ()) {
					ob.GetComponent<Rigidbody> ().AddForce (Windpower, 0, 0, ForceMode.Force);
				}
			}
			source.pitch = Windpower / (power * 2) + 1;
			yield return null;
		}
	}
}