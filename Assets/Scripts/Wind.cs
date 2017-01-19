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

	private Rigidbody player;

	void Start()
	{
		StartCoroutine (WindBreathe ());

	}

	private IEnumerator WindBreathe()
	{
		player = FindObjectOfType<Player> ().gameObject.GetComponent<Rigidbody> ();
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
			Windpower = Mathf.PerlinNoise (Windpower * scale, Time.time * scale) * power;
			/*foreach (GameObject ob in Object.FindObjectsOfType(typeof(GameObject))) {
				if (ob.GetComponent<Rigidbody> ()) {
					ob.GetComponent<Rigidbody> ().AddForce (Windpower, 0, 0, ForceMode.Force);
				}
			}*/
			player.AddForce (Windpower, 0, 0, ForceMode.Force);
			source.pitch = Windpower / (power * 2) + 1 * Mathf.Clamp(Mathf.Abs(player.velocity.y) / 3,1,5f);
            foreach (Cloth cloth in FindObjectsOfType<Cloth>())
            {
                cloth.externalAcceleration = new Vector3(Windpower, 0, 0);
            }

			yield return null;
		}
	}
}