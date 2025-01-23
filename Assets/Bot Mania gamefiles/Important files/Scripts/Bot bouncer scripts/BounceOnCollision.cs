using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class BounceOnCollision : MonoBehaviour
{
    public float bounciness = 10f;
    public float mass = 1f;
    public float drag = 0f;
    public float suctionForce = 100f;
    public Toggle Bounce;
    public Toggle Suction;
    public Toggle ExtremeSuction;
    public Toggle MetalPipe;
    public Toggle VolumeToggle;
    private Rigidbody rb;
    public AudioClip Metal;
    public AudioSource Metal2;
    public AudioClip BounceSound;

    //getting the component of the object
    private void Start()
    {
        Metal2 = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;


        Bounce = GameObject.Find("Bounce toggle").GetComponent<Toggle>();
        Suction = GameObject.Find("Suck toggle").GetComponent<Toggle>();
        ExtremeSuction = GameObject.Find("Extreme Succ").GetComponent<Toggle>();
        MetalPipe = GameObject.Find("Metal Pipe").GetComponent<Toggle>();
        VolumeToggle = GameObject.Find("Volume toggle").GetComponent<Toggle>();
    }

    //when collision happens
    private void OnCollisionEnter(Collision collision)
    {
    
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1.0f);
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);

        if (Bounce.isOn)
        {
        if (MetalPipe.isOn)
        {
          if(!VolumeToggle.isOn){
          Metal2.volume = audioVolume * masterVolume;
          Metal2.PlayOneShot(Metal);
          }
        }
        if (!MetalPipe.isOn)
        {
          if(!VolumeToggle.isOn){
          Metal2.volume = audioVolume * masterVolume;
          Metal2.PlayOneShot(BounceSound);
          }
        }
            Vector3 inDirection = collision.contacts[0].normal;
            Vector3 outDirection = -inDirection;
            rb.AddForce(outDirection * bounciness, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (Suction.isOn)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mousePos - objectPos;
            direction.z = 0;
            direction = direction.normalized;
            rb.AddForce(direction * suctionForce*20 * Time.deltaTime);
        }
        if (ExtremeSuction.isOn)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mousePos - objectPos;
            direction.z = 0;
            direction = direction.normalized;
            rb.AddForce(direction * suctionForce*120 * Time.deltaTime);
        }
    }
}