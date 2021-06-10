using UnityEngine;

public class KusAnimasyon : MonoBehaviour
{
	public int saniyedeKareSayisi = 10;
	public Sprite[] animasyonKareleri;

	private SpriteRenderer spriteRenderer;

	private float sonrakiAnimasyonDegismeAni;
	private bool animasyonYonu = true;
	private int mevcutAnimasyonKaresi = 0;

	void Start()
	{
		if( animasyonKareleri.Length < 2 )
			Destroy( this );

		spriteRenderer = GetComponent<SpriteRenderer>();
		sonrakiAnimasyonDegismeAni = Time.time + 1f / saniyedeKareSayisi;
	}

	void Update()
	{
		if( Time.time >= sonrakiAnimasyonDegismeAni )
		{
			if( animasyonYonu )
			{
				if( mevcutAnimasyonKaresi == animasyonKareleri.Length - 1 )
				{
					mevcutAnimasyonKaresi--;
					animasyonYonu = false;
				}
				else
				{
					mevcutAnimasyonKaresi++;
				}
			}
			else
			{
				if( mevcutAnimasyonKaresi == 0 )
				{
					mevcutAnimasyonKaresi++;
					animasyonYonu = true;
				}
				else
				{
					mevcutAnimasyonKaresi--;
				}
			}

			spriteRenderer.sprite = animasyonKareleri[mevcutAnimasyonKaresi];
			sonrakiAnimasyonDegismeAni = Time.time + 1f / saniyedeKareSayisi;
		}
	}
}