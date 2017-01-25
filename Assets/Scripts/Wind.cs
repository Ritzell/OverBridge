using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wind : MonoBehaviour
{
	[SerializeField]
	private float Windpower;
	[SerializeField]
	private float scale;
	[SerializeField]
	private ParticleSystem windParticle;
	[SerializeField]
	private AudioSource windSource;
	private Rigidbody playerRig;
	private Player player;
	private ParticleSystem.MinMaxCurve particleSpeed;

	private Queue<Vector2> Forecasts = new Queue<Vector2>();

	void Awake(){
		for (int i = 0; i < 3; i++) {
			AddWindpowerForecast (false);
		}
	}

	void Start()
	{
		StartCoroutine (WindBreathe ());
        StartCoroutine(ChangeBreathePower());
	}

	private IEnumerator WindBreathe()
	{
		player = FindObjectOfType<Player> ();
		playerRig = player.gameObject.GetComponent<Rigidbody> ();
		AudioSource source = GetComponent<AudioSource> ();
		while (true) {
			windSource.pitch= Mathf.Clamp(Mathf.Abs(playerRig.velocity.y) / 4,1,6);
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
			Vector2 windStatus = Forecasts.Dequeue ();
			Windpower = windStatus.y;
			var particle = windParticle.main;
			particle.startSpeed = (Windpower / scale)*-10;
			if (Mathf.Abs (Windpower) >= 80 && WoodPlate.lateOnWoodPosition >= 0.65f &&  WoodPlate.isPlayerOnWood) {
				windSource.panStereo = 0.85f * Mathf.Sign (-Windpower);
				player.StartCoroutine (player.StrongWind (Mathf.Sign (Windpower), windStatus.x));
			} else {
				windSource.panStereo = 0;
			}
			AddWindpowerForecast (true);
			yield return new WaitForSeconds(windStatus.x);
        }
    }
		

	private void AddWindpowerForecast(bool isUpdate){
		float windpower;
		var random = Mathf.FloorToInt( Random.Range (0,2.8f));
		if (random == 0) {
			windpower = NaturalRandom (Random.Range (-scale, scale), 10, 0);
		} else if (random == 2 && Mathf.Abs(Windpower) < 80 && isUpdate) {
			windpower = Random.value <= 0.5f ? -80 : 80;
		} else {
			windpower = NaturalRandom (Random.Range (-scale / 10, scale / 10), 10, 0);
		}
		var time = Random.Range (1.5f, 5f);
		if (Mathf.Abs (windpower) >= 80 && WoodPlate.lateOnWoodPosition >= 0.65f && !GameManager.IsGameOver) {
			time = Mathf.Clamp (time * Random.Range (2, 3), 3.5f, 7);
		}

		WindForecastFrame.Forecasts.Add (new Vector2(time, windpower));
		Forecasts.Enqueue(new Vector2(time,windpower));
	}
}