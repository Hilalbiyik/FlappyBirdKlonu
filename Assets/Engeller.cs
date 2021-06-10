using UnityEngine;

public class Engeller : MonoBehaviour
{
	public GameObject ustEngel;
	public GameObject altEngel;
	public GameObject skorCollider;

	public float altUstEngelArasiBosluk = 0.8f;
	public float ikiEngelArasiMesafe = 2f;

	private int engelSayisi;

	private Vector2 kameraUnityEbatlar;
	private Vector2 engelUnityEbatlar;

	private Transform[] ustEngelObjeleri;
	private Transform[] altEngelObjeleri;
	private int bastakiEngelObjesi = 0;

	void Start()
	{
		Camera kamera = GetComponent<Camera>();

		engelUnityEbatlar = ustEngel.GetComponent<SpriteRenderer>().sprite.rect.size / 100;
		kameraUnityEbatlar = new Vector2( kamera.orthographicSize * kamera.aspect, kamera.orthographicSize );

		engelSayisi = Mathf.CeilToInt( ( kamera.orthographicSize * 2 * kamera.aspect ) / ( engelUnityEbatlar.x + ikiEngelArasiMesafe ) ) + 1;

		ustEngelObjeleri = new Transform[engelSayisi];
		altEngelObjeleri = new Transform[engelSayisi];

		for( int i = 0; i < engelSayisi; i++ )
		{
			float xKoordinati = transform.position.x + kameraUnityEbatlar.x + i * ( engelUnityEbatlar.x + ikiEngelArasiMesafe );
			float yKoordinati = Random.Range( -kameraUnityEbatlar.y + altUstEngelArasiBosluk + 0.6f, kameraUnityEbatlar.y - 0.6f );
			ustEngelObjeleri[i] = Instantiate( ustEngel, new Vector3( xKoordinati, yKoordinati, 0 ), Quaternion.identity ).transform;
			altEngelObjeleri[i] = Instantiate( altEngel, new Vector3( xKoordinati, yKoordinati - altUstEngelArasiBosluk, 0 ), Quaternion.identity ).transform;

			EdgeCollider2D temasAlani = Instantiate( skorCollider, new Vector3( xKoordinati + engelUnityEbatlar.x / 2, kameraUnityEbatlar.y, 0 ), Quaternion.identity ).GetComponent<EdgeCollider2D>();
			Vector2[] cizgi = new Vector2[2];
			cizgi[0] = new Vector2( 0, 0 );
			cizgi[1] = new Vector2( 0, -2 * kameraUnityEbatlar.y );
			temasAlani.points = cizgi;
			temasAlani.transform.SetParent( ustEngelObjeleri[i] );
		}
	}

	void Update()
	{
		if( transform.position.x - kameraUnityEbatlar.x >= ustEngelObjeleri[bastakiEngelObjesi].position.x + engelUnityEbatlar.x )
		{
			float yKoordinati = Random.Range( -kameraUnityEbatlar.y + altUstEngelArasiBosluk + 0.6f, kameraUnityEbatlar.y - 0.6f );

			Vector3 ustEngelPosition = ustEngelObjeleri[bastakiEngelObjesi].position;
			Vector3 altEngelPosition = altEngelObjeleri[bastakiEngelObjesi].position;

			ustEngelPosition.x += engelSayisi * ( engelUnityEbatlar.x + ikiEngelArasiMesafe );
			altEngelPosition.x += engelSayisi * ( engelUnityEbatlar.x + ikiEngelArasiMesafe );

			ustEngelPosition.y = yKoordinati;
			altEngelPosition.y = yKoordinati - altUstEngelArasiBosluk;

			ustEngelObjeleri[bastakiEngelObjesi].position = ustEngelPosition;
			altEngelObjeleri[bastakiEngelObjesi].position = altEngelPosition;

			bastakiEngelObjesi++;

			if( bastakiEngelObjesi == ustEngelObjeleri.Length )
				bastakiEngelObjesi = 0;
		}
	}
}