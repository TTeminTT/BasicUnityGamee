using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuzeControl : MonoBehaviour
{
    Rigidbody rb;
    public ParticleSystem duman;
    public ParticleSystem ates;
    public GameObject patlama;
    public float itmeKuvveti = 150f;
    public float beklemeSuresi = 2f;
    public AudioClip fuzeSesi;
    private Vector3 baslamaPozisyonu;
    public TextMeshProUGUI skorBoard;
    private int skor;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        baslamaPozisyonu = transform.position;
        rb = GetComponent<Rigidbody>();
        audioSource= rb.GetComponent<AudioSource>();
        duman.Stop();
        ates.Stop();
        skor = 0;
        SkorBoardGuncelle();

        
    }
    private void SkorBoardGuncelle()
    {
        skorBoard.text = "Score: " + skor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.forward * itmeKuvveti);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(fuzeSesi);
            }
            duman.Play();
            ates.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            duman.Stop();
            ates.Stop();
            audioSource.Stop();
        }
        float yatay = Input.GetAxis("Horizontal");
        if (yatay != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward, -yatay * Time.deltaTime * itmeKuvveti, Space.World);
            rb.freezeRotation = false;
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Engel"))
        {
            Instantiate(patlama, transform.position, Quaternion.identity);
            transform.position = new Vector3(-100f, -100f, -100f);
            Invoke("SahneyiBaslat", 2f);
        }
        if (collision.gameObject.CompareTag("BitisRampasi"))
        {
            SceneManager.LoadScene("Level_2");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
            skor++;
            SkorBoardGuncelle();
        }
        if (other.tag == "coinengel")
        {
            Destroy(other.gameObject);
            Instantiate(patlama, transform.position, Quaternion.identity);
            transform.position = new Vector3(-100f, -100f, -100f);
            Invoke("SahneyiBaslat", 2f);
        }
    }
    private void SahneyiBaslat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    private void FixedUpdate()
    {
      
    }

}
