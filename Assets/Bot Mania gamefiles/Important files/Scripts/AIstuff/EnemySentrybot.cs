using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class EnemySentrybot : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip Clip4;
    public AudioClip EnemyAlertSound;
    public AudioSource Sound;
    public AudioSource Sound2;
    public TextMeshPro MagText;
    public ParticleSystem DeathAnimation;
    public GameObject[] ColorHPBar;
    public GameObject[] objectsToActivate;
    private float MagtoText = 1;
    public Slider HPslider;
    public Color HPcolor;

    public GameObject Player;
    public GameObject PlayerCamera;
    public WeaponScript WeaponScript;
    public KeyboardControlMk2 PlayerScript;
    public GameObject Bot;
    public GameObject MainObject;
    public GameObject Gun;
    public GameObject AlertSign;
    public GameObject HealthBar;
    public GameObject bulletPrefab;
    public GameObject[] cosmetics;
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint2;
    public Transform bulletSpawnPoint3;
    public LayerMask whatIsPlayer;

    public int PatrolChance = 20;
    public int Shoot;
    public float FrameImmunity;
    public bool playerInSightRange;
    public int IsDead,IsSearching,IsPatrolling,InPatrolMode,LIFE,BotType,hascounted,EnemyStartDelay;

    private int ATTACKSMAX,ATTACKSMIN;
    public float sightrangeStart = 150;
    public float sightRange;
    public float sightRangeWhenCaught;
    public float sightRangeWhenLost;

    private Vector3 destination;

    //EnemyStats
    public float HPMAX = 100;
    public float HP;
    public float Mag = 1;
    public float StartEnemyAndRefresh;
    public float DamageReduction = 5f;
    public float bulletSpeed = 10;
    public float DMG;
    public bool IsSmart;
    public int ShootingSpeed;
    public float StoppingDistanceStart;
    public bool IsStationary;

    private void Start()
    {
        Invoke("EnemyStarter",0.2f);
        HPMAX = HPMAX * Mag;
        HPslider.maxValue = HPMAX;
        HP = HPMAX;

        Player = GameObject.Find("Player");
        PlayerCamera = GameObject.Find("CameraArea223");
        WeaponScript = PlayerCamera.GetComponent<WeaponScript>();
        PlayerScript = Player.GetComponent<KeyboardControlMk2>();

        


        IsSearching = 1;
        IsPatrolling = 0;

        MagtoText = 1*Mag;
        MagText.text = MagtoText.ToString();
        sightRange = sightrangeStart;
        sightRangeWhenCaught = sightRange;
        sightRangeWhenLost = sightRange;
        LIFE = 0;
        DMG = DMG*Mag;

        ATTACKSMIN = 0;
        ATTACKSMAX = 2;


    int randomCosmeticIndex = UnityEngine.Random.Range(0, cosmetics.Length);

    for (int i2 = 0; i2 < cosmetics.Length; i2++) 
    {
    cosmetics[i2].SetActive(i2 == randomCosmeticIndex);
    }


    for (int i = 0; i < ColorHPBar.Length; i++) 
    {
    Renderer renderer = ColorHPBar[i].GetComponent<Renderer>();
    if (renderer != null)
    {
        renderer.material.color = HPcolor;
    }
    }
    }








    private void Update()
    {
        if(HP < HPMAX && PlayerScript.HP <= 0){HP = HPMAX;}
        
        if(FrameImmunity <= 1f){
        FrameImmunity += 0.4f;
        }

        if(HP > HPMAX){
        HP = HPMAX;
        }


        HPslider.value = HP;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);


            //MainStarter
            if(playerInSightRange && HP > 0 && EnemyStartDelay == 1){
            if(IsSearching == 1){

             if(GlobalData.GetEnemyCount() < 1){
             float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
             float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
             Sound2.volume = audioVolume * masterVolume;
             Sound2.PlayOneShot(EnemyAlertSound);
             }

             Invoke("StartEnemy", StartEnemyAndRefresh);
             Invoke("AlertSignLeave", 1f);
             AlertSign.SetActive(true);
             HealthBar.SetActive(true);
             LIFE = 1;
             IsSearching = 0;
             IsPatrolling = 0;
             HP = HPMAX;
             sightRange = sightRangeWhenCaught;
             hascounted = 2;
             if(IsDead == 0 && GlobalData.GetEnemyCount() >= 0){
             GlobalData.SetEnemyCount(GlobalData.GetEnemyCount() + 1);
             }
            }
            }
            else{
                if(IsPatrolling == 0 && EnemyStartDelay == 1){
                    CancelInvoke("StartEnemy");
                    CancelInvoke("Attack2A");
                    CancelInvoke("ChasePlayer");
                    
                    
                    AlertSign.SetActive(false);
                    HealthBar.SetActive(false);
                    LIFE = 0;
                    IsPatrolling = 1;
                    IsSearching = 1;
                    sightRange = sightRangeWhenLost;
                    if(hascounted == 2 && GlobalData.GetEnemyCount() > 0){
                    GlobalData.SetEnemyCount(GlobalData.GetEnemyCount() - 1);
                    }
                }
                    int InPatrolMode = UnityEngine.Random.Range(1, PatrolChance);
                if(InPatrolMode == 1){
                    Invoke("PatrolGo", 1f);
                }
            }

            //Deathcode
            if(HP <= 0){
                gameObject.GetComponent<Collider>().enabled = false;
                
                CancelInvoke("StartEnemy");
                CancelInvoke("Attack2A");
                CancelInvoke("ChasePlayer");

                if(IsDead == 0){
                LIFE = 0;
                AlertSign.SetActive(false);
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.Play();
                IsDead = 1;
                DeathAnimation.Play();
                HealthBar.SetActive(false);
                hascounted = 1;
                Invoke("Remove",2f);
                }

            foreach (GameObject obj in objectsToActivate)
            {
                obj.GetComponent<Renderer>().enabled = false;
            }

            for (int i = 0; i < cosmetics.Length; i++) 
            {
            cosmetics[i].SetActive(false);
            }

            }


    }

    private void EnemyStarter(){EnemyStartDelay = 1;}

    void StartEnemy()
    {
    int ATT = UnityEngine.Random.Range(ATTACKSMIN, ATTACKSMAX);
    AlertSign.SetActive(false);
     CancelInvoke("Attack2A");
     CancelInvoke("ChasePlayer");


    if(ATT <= 1){

        if(BotType == 0){
        Gun.SetActive(true);
        Invoke("Attack2A", 1f);
        Invoke("StartEnemy", 3);
        Invoke("ChasePlayer",3f);
        navMeshAgent.stoppingDistance = StoppingDistanceStart;
        }

    }
    }



    //Attack1
    void Attack2A()
    {
    if(Shoot < ShootingSpeed/1.5){
    Shoot += 1;
    }
    if(Shoot >= ShootingSpeed/1.5 && WeaponScript.GuardingBot == 0){
        Shoot = 0;
    }


    if(Shoot == 1){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip2);
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bullet.GetComponent<BulletScript>().Damage = DMG;

        var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
        bullet2.GetComponent<Rigidbody>().useGravity = false;
        bullet2.GetComponent<Rigidbody>().velocity = bulletSpawnPoint2.forward * bulletSpeed;
        bullet2.GetComponent<BulletScript>().Damage = DMG;

        var bullet3 = Instantiate(bulletPrefab, bulletSpawnPoint3.position, bulletSpawnPoint3.rotation);
        bullet3.GetComponent<Rigidbody>().useGravity = false;
        bullet3.GetComponent<Rigidbody>().velocity = bulletSpawnPoint3.forward * bulletSpeed;
        bullet3.GetComponent<BulletScript>().Damage = DMG;
    }

    if(IsSmart){
        CharacterController playerController = Player.GetComponent<CharacterController>();
        Vector3 predictedPos = playerController.transform.position + playerController.velocity * 0.1f;
        Vector3 inputMovement = Input.GetAxis("Horizontal") * playerController.transform.right + Input.GetAxis("Vertical") * playerController.transform.forward;
        Vector3 predictedMovement = inputMovement * 25;
        predictedPos += predictedMovement;
        Vector3 GunDirection = predictedPos - Gun.transform.position;
        Gun.transform.rotation = Quaternion.LookRotation(GunDirection);
    }
    else if (!IsSmart){
        Vector3 GunDirection = Player.transform.position - Gun.transform.position;
        Gun.transform.rotation = Quaternion.LookRotation(GunDirection);
    }

    Invoke("Attack2A", 0.05f);
    }









    void AlertSignLeave(){
    AlertSign.SetActive(false);
    }

    void PatrolGo(){
    Vector3 randomDirection = Random.insideUnitSphere * 100f;
    randomDirection += transform.position;
    NavMeshHit hit;
    if(!IsStationary){
    if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
    {
        destination = hit.position;
        navMeshAgent.SetDestination(destination);
    }
    }
    }

    void ChasePlayer()
    {
     Invoke("ChasePlayer",3f);
     if(!IsStationary){
     float heightDifference = Player.transform.position.y - transform.position.y;
     //if(heightDifference < 30){
     navMeshAgent.SetDestination(Player.transform.position);
     //}
     }
    }


    void Remove(){Destroy(MainObject);}









void OnCollisionEnter(Collision other)
{
    if (other.gameObject.CompareTag("Bot") && other.rigidbody.velocity.magnitude > 5 && FrameImmunity > 1f && LIFE == 1)
    {

        HP -= HPMAX/DamageReduction/Mag;
        FrameImmunity = 0;
    }

    if (other.gameObject.CompareTag("PlayerBulletBot") && FrameImmunity > 1f && LIFE == 1)
    {
            GameObject PlayerBullet = other.gameObject;
            int DamageBullet = Mathf.RoundToInt(PlayerBullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet;
            FrameImmunity = 0;
            //Destroy(PlayerBullet);
    }

    if (other.gameObject.CompareTag("MeleeProjectileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);

            HP -=  50;
            FrameImmunity = 0;
    }
    if (other.gameObject.CompareTag("PlayerMissileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);

            HP -=  50;
            FrameImmunity = 0;
    }
}
}