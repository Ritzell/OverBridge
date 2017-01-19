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
        StartCoroutine(ChangeBreathePower());
	}

	private IEnumerator WindBreathe()
	{
		player = FindObjectOfType<Player> ().gameObject.GetComponent<Rigidbody> ();
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
			player.AddForce (Windpower, 0, 0, ForceMode.Force);
			source.pitch = Windpower / (power * 2) + 1 * Mathf.Clamp(Mathf.Abs(player.velocity.y) / 3,1,5f);
            foreach (Cloth cloth in FindObjectsOfType<Cloth>())
            {
                cloth.externalAcceleration = new Vector3(Windpower, 0, 0);
            }
			yield return null;
		}
	}

    private IEnumerator ChangeBreathePower()
    {
        while (true)
        {
            Windpower = (Windpower + Random.Range(-power,power)) * power;
            yield return new WaitForSeconds(0.1f);
        }
    }
}