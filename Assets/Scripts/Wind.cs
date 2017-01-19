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
        //StartCoroutine(ChangeBreathePower());
	}

	private IEnumerator WindBreathe()
	{
		player = FindObjectOfType<Player> ().gameObject.GetComponent<Rigidbody> ();
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
            Windpower = NaturalRandom(Random.Range(-scale, scale), 10, 0);//((Random.Range(-scale, scale) * Random.Range(-scale, scale) * Random.Range(-scale, scale) * Random.Range(-scale, scale) * Random.Range(-scale, scale)) / 5.0f) * power;
            player.AddForce (Windpower, 0, 0, ForceMode.Force);
			//source.pitch = Windpower / (scale ** 10 / 10) + 1 * Mathf.Clamp(Mathf.Abs(player.velocity.y) / 3,1,5f);
            foreach (Cloth cloth in FindObjectsOfType<Cloth>())
            {
                cloth.externalAcceleration = new Vector3(Windpower, 0, 0);
            }
			yield return null;
		}
	}

    private float NaturalRandom(float random,int max, int now)
    {
        now++;
        if (max > now)
        {
            var addRandom = random * Random.Range(-scale, scale);
            return NaturalRandom(addRandom,max,now);
        }
        else
        {
            return random / max;
        }
    }

   /* private IEnumerator ChangeBreathePower()
    {
        while (true)
        {
            Windpower = (Windpower + Random.Range(-power,power)) * power;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}