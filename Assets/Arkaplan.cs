using UnityEngine;

public class Arkaplan : MonoBehaviour
{
	public GameObject gokyuzu;
	public GameObject toprak;

	public float gokyuzuSagaKaymaHizi = 1.0f;

	private int arkaplanSayisi;

	private Vector2 kameraUnityEbatlar;
	private Vector2 gokyuzuUnityEbatlar;
	private Vector2 toprakUnityEbatlar;

	private Transform[] gokyuzuObjeleri;
	private Transform[] toprakObjeleri;
	private int bastakiGokyuzuArkaplanObjesi = 0;
	private int bastakiToprakArkaplanObjesi = 0;

	private Transform gokyuzuParent;

	void Awake()
	{
		Camera kamera = GetComponent<Camera>();

		gokyuzuUnityEbatlar = gokyuzu.GetComponent<SpriteRenderer>().sprite.rect.size / 100;
		toprakUnityEbatlar = toprak.GetComponent<SpriteRenderer>().sprite.rect.size / 100;

		kamera.orthographicSize = ( gokyuzuUnityEbatlar.y + toprakUnityEbatlar.y ) / 2;
		arkaplanSayisi = Mathf.CeilToInt( ( kamera.orthographicSize * 2 * kamera.aspect ) / gokyuzuUnityEbatlar.x ) + 1;

		kameraUnityEbatlar = new Vector2( kamera.orthographicSize * kamera.aspect, kamera.orthographicSize );

		gokyuzuObjeleri = new Transform[arkaplanSayisi];
		toprakObjeleri = new Transform[arkaplanSayisi];

		gokyuzuParent = new GameObject().transform;

		for( int i = 0; i < arkaplanSayisi; i++ )
		{
			float xKoordinati = transform.position.x - kameraUnityEbatlar.x + i * gokyuzuUnityEbatlar.x;
			gokyuzuObjeleri[i] = Instantiate( gokyuzu, new Vector3( xKoordinati, kameraUnityEbatlar.y, 0 ), Quaternion.identity ).transform;
			gokyuzuObjeleri[i].SetParent( gokyuzuParent );
			toprakObjeleri[i] = Instantiate( toprak, new Vector3( xKoordinati, kameraUnityEbatlar.y - gokyuzuUnityEbatlar.y, 0 ), Quaternion.identity ).transform;
		}
	}

	void Update()
	{
		if( transform.position.x - kameraUnityEbatlar.x >= gokyuzuObjeleri[bastakiGokyuzuArkaplanObjesi].position.x + gokyuzuUnityEbatlar.x )
		{
			gokyuzuObjeleri[bastakiGokyuzuArkaplanObjesi].Translate( arkaplanSayisi * gokyuzuUnityEbatlar.x, 0, 0 );
			bastakiGokyuzuArkaplanObjesi++;

			if( bastakiGokyuzuArkaplanObjesi == gokyuzuObjeleri.Length )
				bastakiGokyuzuArkaplanObjesi = 0;
		}

		if( transform.position.x - kameraUnityEbatlar.x >= toprakObjeleri[bastakiToprakArkaplanObjesi].position.x + toprakUnityEbatlar.x )
		{
			toprakObjeleri[bastakiToprakArkaplanObjesi].Translate( arkaplanSayisi * gokyuzuUnityEbatlar.x, 0, 0 );
			bastakiToprakArkaplanObjesi++;

			if( bastakiToprakArkaplanObjesi == gokyuzuObjeleri.Length )
				bastakiToprakArkaplanObjesi = 0;
		}

		gokyuzuParent.Translate( gokyuzuSagaKaymaHizi * Time.deltaTime, 0, 0 );
	}
}