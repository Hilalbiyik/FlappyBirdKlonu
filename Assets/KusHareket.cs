using UnityEngine;

public class KusHareket : MonoBehaviour
{
	public float yercekimi = 4f;
	public float ziplamaGucu = 2.5f;
	public float yatayHiz = 1.5f;
	private float dikeyHiz = 0f;

	public AudioClip kanatSes;
	public AudioClip skorSes;
	public AudioClip olumSes;
	public AudioClip dusmeSes;

	public UnityEngine.UI.Text skorText;

	private Vector2 kameraUnityEbatlar;

	private int skor = 0;
	private int yuksekSkor = 0;

	private bool oyunBitti = false;
	private bool engeleCarptim = false;

	private void Start()
	{
		Camera kamera = Camera.main;
		kameraUnityEbatlar = new Vector2( kamera.orthographicSize * kamera.aspect, kamera.orthographicSize );
		yuksekSkor = PlayerPrefs.GetInt( "YuksekSkor" );
	}

	void Update()
	{
		if( !oyunBitti )
		{
			dikeyHiz -= yercekimi * Time.deltaTime;

			if( Input.GetMouseButtonDown( 0 ) && !engeleCarptim )
			{
				dikeyHiz = ziplamaGucu;
				GetComponent<AudioSource>().PlayOneShot( kanatSes );
			}

			float egim = 90 * dikeyHiz / yatayHiz;

			if( egim < -50 ) egim = -50;
			else if( egim > 50 ) egim = 50;

			transform.eulerAngles = new Vector3( transform.eulerAngles.x, transform.eulerAngles.y, egim );
			transform.Translate( new Vector3( 0, dikeyHiz, 0 ) * Time.deltaTime, Space.World );
			transform.parent.Translate( yatayHiz * Time.deltaTime, 0, 0 );

			if( transform.position.y > kameraUnityEbatlar.y )
			{
				Destroy( Camera.main.GetComponent<Arkaplan>() );
				Destroy( GetComponent<KusAnimasyon>() );
				yatayHiz = 0;

				if( !engeleCarptim )
				{
					GetComponent<AudioSource>().PlayOneShot( olumSes );
					Invoke( "DusmeSesiCal", 0.25f );
					engeleCarptim = true;
				}
			}
		}
		else
		{
			if( Input.GetMouseButtonDown( 0 ) )
				UnityEngine.SceneManagement.SceneManager.LoadScene( "Oyun" );
		}

		skorText.text = "SKOR: " + skor + "\nYÜKSEKSKOR: " + yuksekSkor;

		if( Input.GetKeyDown( KeyCode.Escape ) )
			Application.Quit();
	}

	void OnTriggerEnter2D( Collider2D temas )
	{
		if( temas.CompareTag( "SkorTemasAlani" ) )
		{
			if( !engeleCarptim && !oyunBitti )
			{
				skor++;
				GetComponent<AudioSource>().PlayOneShot( skorSes );
			}
		}
		else
		{
			Destroy( Camera.main.GetComponent<Arkaplan>() );
			Destroy( GetComponent<KusAnimasyon>() );
			yatayHiz = 0;

			if( !engeleCarptim )
			{
				GetComponent<AudioSource>().PlayOneShot( olumSes );
				Invoke( "DusmeSesiCal", 0.25f );
				engeleCarptim = true;
			}
		}
	}

	void OnCollisionEnter2D( Collision2D temas )
	{
		Destroy( Camera.main.GetComponent<Arkaplan>() );
		Destroy( GetComponent<KusAnimasyon>() );
		yatayHiz = 0;
		oyunBitti = true;

		if( skor > yuksekSkor )
		{
			yuksekSkor = skor;

			PlayerPrefs.SetInt( "YuksekSkor", yuksekSkor );
			PlayerPrefs.Save();
		}

		if( !engeleCarptim )
			GetComponent<AudioSource>().PlayOneShot( olumSes );
	}

	void DusmeSesiCal()
	{
		GetComponent<AudioSource>().PlayOneShot( dusmeSes );
	}
}