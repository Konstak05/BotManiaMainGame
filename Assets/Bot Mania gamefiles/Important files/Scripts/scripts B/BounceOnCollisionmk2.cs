using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class BounceOnCollisionmk2 : MonoBehaviour
{
    public float bounciness = 10f;
    public float mass = 1f;
    public float drag = 0f;
    public float suctionForce = 100f;
    private int Bounce;
    private int Suction;
    private int ExtremeSuction;
    private int MetalPipe;
    private Rigidbody rb;
    public AudioClip Metal;
    public AudioSource Metal2;
    public AudioClip BounceSound;

    //getting the component of the object
    private void Start()
    {
        float Mul = PlayerPrefs.GetFloat("BounceMul");
        Bounce = PlayerPrefs.GetInt("BounceSetting");
        Suction = PlayerPrefs.GetInt("SuccSetting");
        ExtremeSuction = PlayerPrefs.GetInt("ExtremeSuccSetting");
        MetalPipe = PlayerPrefs.GetInt("MetalPipeSetting");

        Metal2 = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
    }

    //when collision happens
    private void OnCollisionEnter(Collision collision)
    {
    float Mul = PlayerPrefs.GetFloat("BounceMul");
    Bounce = PlayerPrefs.GetInt("BounceSetting");
    MetalPipe = PlayerPrefs.GetInt("MetalPipeSetting");
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume");

        if (Bounce == 1)
        {
        if (MetalPipe == 1)
        {
          Metal2.volume = audioVolume * masterVolume;
          Metal2.PlayOneShot(Metal);
        }
        if (MetalPipe == 0)
        {
          Metal2.volume = audioVolume * masterVolume;
          Metal2.PlayOneShot(BounceSound);
        }
            Vector3 inDirection = collision.contacts[0].normal;
            Vector3 outDirection = -inDirection;
            rb.AddForce(outDirection * bounciness * Mul, ForceMode.Impulse);
        }
    }

    private void Update()
    {

        Suction = PlayerPrefs.GetInt("SuccSetting");
        ExtremeSuction = PlayerPrefs.GetInt("ExtremeSuccSetting");
        MetalPipe = PlayerPrefs.GetInt("MetalPipeSetting");
        if (Suction == 1)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = mousePos - objectPos;
            direction.z = 0;
            direction = direction.normalized;
            rb.AddForce(direction * suctionForce*20 * Time.deltaTime);
        }
        if (ExtremeSuction == 1)
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