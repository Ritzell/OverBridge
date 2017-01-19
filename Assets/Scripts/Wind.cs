using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour
{

	[SerializeField]
	private float Windpower;
	[SerializeField]
	private float scale;
	[SerializeField]
	private ParticleSystem windParticle;

	private Rigidbody playerRig;
	private Player player;
	private ParticleSystem.MinMaxCurve particleSpeed;

	void Start()
	{
		StartCoroutine (WindBreathe ());
        StartCoroutine(ChangeBreathePower());
	}

	private IEnumerator WindBreathe()
	{
		player = FindObjectOfType<Player> ();
		//playerRig = player.gameObject.GetComponent<Rigidbody> ();
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
			//playerRig.AddForce (Windpower, 0, 0, ForceMode.Force);
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
            var addRandom = random + Random.Range(-scale, scale);
            return NaturalRandom(addRandom,max,now);
        }
        else
        {
            return random / max;
        }
    }

   private IEnumerator ChangeBreathePower()
    {
        while (true)
        {
			var random = Mathf.FloorToInt( Random.Range (0,2.6f));
			if (random == 0) {
				Windpower = NaturalRandom (Random.Range (-scale, scale), 10, 0);
			} else if (random == 2 && Mathf.Abs(Windpower) < 80) {
				Windpower = Random.value <= 0.5f ? -80 : 80;
			} else {
				Windpower = NaturalRandom (Random.Range (-scale / 10, scale / 10), 10, 0);
			}
			var particle = windParticle.main;
			particle.startSpeed = (Windpower / scale)*-10 ;
			var time = Random.Range (1.5f, 5f);
			if (Mathf.Abs(Windpower) >= 80 && WoodPlate.lateOnWoodPosition >= 0.65f && !GameManager.IsGameOver) {
				time = Mathf.Clamp (time * Random.Range (2, 3), 3.5f, 7);
				player.StartCoroutine (player.StrongWind (Mathf.Sign (Windpower), time));
			}
			yield return new WaitForSeconds(time);
        }
    }
}