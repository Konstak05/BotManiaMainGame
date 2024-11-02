using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class EnemybotAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip Clip4;
    public AudioClip EnemyAlertSound;
    public AudioSource Sound;
    public AudioSource Sound2;
    public TextMeshPro MagText;
    public ParticleSystem DeathAnimation;
    public ParticleSystem HealingParticle;
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
    public GameObject Arm1;
    public GameObject Arm2;
    public GameObject Gun;
    public GameObject Gun2;
    public GameObject AlertSign;
    public GameObject HealthBar;
    public GameObject bulletPrefab;
    public GameObject[] cosmetics;
    public Transform bulletSpawnPoint;
    public LayerMask whatIsPlayer;

    public int PatrolChance = 20;
    public int Shoot;
    public float FrameImmunity;
    public bool playerInSightRange;
    public bool playerInMeleeRange;
    public float HealingRandomDistance = 50f;
    public int IsDead,IsSearching,IsPatrolling,InPatrolMode,IsHealing,LIFE,BotType,hascounted;

    private int ATTACKSMAX,ATTACKSMIN;
    public float sightrangeStart = 150;
    public float sightRange,MeleeRange;
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
    public float MeleeReload = 1f;
    public float DMG;
    public bool IsSmart;
    public bool CanHeal;
    public int ShootingSpeed;
    public float StoppingDistanceStart;

    private void Start()
    {

        HPMAX = HPMAX * Mag;
        HPslider.maxValue = HPMAX;
        HP = HPMAX;

        Player = GameObject.Find("Player");
        PlayerCamera = GameObject.Find("CameraArea223");
        WeaponScript = PlayerCamera.GetComponent<WeaponScript>();
        PlayerScript = Player.GetComponent<KeyboardControlMk2>();
    
        Arm1.SetActive(false);
        Arm2.SetActive(false);
        Gun.SetActive(false);
        if(Gun2 != null){
        Gun2.SetActive(false);
        }

        IsSearching = 1;
        IsPatrolling = 0;

        MagtoText = 1*Mag;
        MagText.text = MagtoText.ToString();
        sightRange = sightrangeStart;
        sightRangeWhenCaught = sightRange*3;
        sightRangeWhenLost = sightRange;
        LIFE = 0;
        DMG = DMG*Mag;


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
            if(playerInSightRange && HP > 0){
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
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
                if(IsPatrolling == 0){
                    CancelInvoke("StartEnemy");
                    CancelInvoke("Attack1A");
                    CancelInvoke("Attack1B");
                    CancelInvoke("Attack1C");
                    CancelInvoke("Attack2A");
                    CancelInvoke("ChasePlayer");
                    HealingParticle.Stop();
                    Gun.SetActive(false);
                    Arm1.SetActive(false);
                    Arm2.SetActive(false);
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
                    animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
                    int InPatrolMode = UnityEngine.Random.Range(1, PatrolChance);
                if(InPatrolMode == 1){
                    Invoke("PatrolGo", 1f);
                }
            }

            //Deathcode
            if(HP <= 0){
                gameObject.GetComponent<Collider>().enabled = false;
                Arm1.SetActive(false);
                Arm2.SetActive(false);
                Gun.SetActive(false);
                if(Gun2 != null){
                Gun2.SetActive(false);
                }
                animator.SetFloat("Speed", 0);
                CancelInvoke("StartEnemy");
                CancelInvoke("Attack1A");
                CancelInvoke("Attack1B");
                CancelInvoke("Attack1C");
                CancelInvoke("Attack2A");
                CancelInvoke("ChasePlayer");
                CancelInvoke("MeleeAttack");
                CancelInvoke("MeleeAttack2");
                HealingParticle.Stop();

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



    void StartEnemy()
    {
    if(HP < HPMAX && HP > HPMAX/5){ATTACKSMAX = 3;}
    else if(HP >= HPMAX){ATTACKSMAX = 2;}

    if(HP <= HPMAX/5){ATTACKSMIN = 2; ATTACKSMAX = 6;}
    else{ATTACKSMIN = -3;}

    int ATT = UnityEngine.Random.Range(ATTACKSMIN, ATTACKSMAX);
    AlertSign.SetActive(false);
    Gun.SetActive(false);
    if(Gun2 != null){
        Gun2.SetActive(false);
    }
     CancelInvoke("Attack2A");
     CancelInvoke("ChasePlayer");
     CancelInvoke("MeleeAttack");
     CancelInvoke("MeleeAttack2");


    if(ATT <= 1){
        if(BotType == 0){
        Gun.SetActive(true);
        Invoke("Attack2A", 1f);
        Invoke("StartEnemy", 3);
        Invoke("ChasePlayer",0.3f);
        navMeshAgent.stoppingDistance = StoppingDistanceStart;
        }
        else if(BotType == 1){
        Gun.SetActive(true);
        Invoke("ChasePlayer",0.3f);
        Invoke("StartEnemy", 5);
        Invoke("MeleeAttack", 0.3f);
        navMeshAgent.stoppingDistance = 10;
        }
        if(BotType == 2){
        Gun.SetActive(true);
        Invoke("Attack2A", 1f);
        Invoke("StartEnemy", 3);
        Invoke("ChasePlayer",0.3f);
        navMeshAgent.stoppingDistance = StoppingDistanceStart;
        }
    }
    if(ATT == 2){
        if (CanHeal){
        Vector2 randomDirection = Random.insideUnitCircle.normalized * HealingRandomDistance;
        Vector3 randomPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + Player.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, HealingRandomDistance, NavMesh.AllAreas)){navMeshAgent.SetDestination(hit.position);}
        navMeshAgent.stoppingDistance = 5;
        Invoke("Attack1A", 1f);
        }
        else{
        Invoke("StartEnemy", 0.05f);
        }
    }
    if(ATT >= 3){
        Vector2 randomDirection = Random.insideUnitCircle.normalized * HealingRandomDistance;
        Vector3 randomPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + Player.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, HealingRandomDistance*3, NavMesh.AllAreas)){navMeshAgent.SetDestination(hit.position);}
        navMeshAgent.stoppingDistance = 1;
        Invoke("StartEnemy", 1);
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


    if(Shoot == 1){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip2);
        if(BotType == 0){
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bullet.GetComponent<BulletScript>().Damage = DMG;
        }
        if(BotType == 2){
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bullet.GetComponent<Enemydamagegiver>().Damage = DMG;
        }
    }

    Invoke("Attack2A", 0.05f);
    }




    //Attack2
    void Attack1A()
    {
     Arm1.SetActive(true);
     Arm2.SetActive(false);
     Invoke("Attack1B", 1f);
    }
    void Attack1B()
    {
     var main = HealingParticle.main;
     main.duration = 2f*Mag;
     IsHealing = 1;
     Arm1.SetActive(false);
     Arm2.SetActive(true);
     HealingParticle.Play();
     Invoke("Attack1C", 2f * Mag);
    }
    void Attack1C()
    {
     if(HP < HPMAX && HP > 0){
     HP = HP + HPMAX*Mag/4f;
     }
     IsHealing = 0;
     Arm1.SetActive(false);
     Arm2.SetActive(false);
     Invoke("StartEnemy", StartEnemyAndRefresh);
    }



    //MeleeAttack
    void MeleeAttack()
    {
    Gun.SetActive(true);
    Gun2.SetActive(false);

    playerInMeleeRange = Physics.CheckSphere(transform.position, 15, whatIsPlayer);

    if(playerInMeleeRange){
    Invoke("MeleeAttack2", 0.1f);

    }


    Invoke("MeleeAttack", MeleeReload);
    }

    void MeleeAttack2()
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip4);
    Gun.SetActive(false);
    Gun2.SetActive(true);

    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    bullet.GetComponent<Enemydamagegiver>().Damage = DMG;
    }




    void AlertSignLeave(){
    AlertSign.SetActive(false);
    }

    void PatrolGo(){
    Vector3 randomDirection = Random.insideUnitSphere * 100f;
    randomDirection += transform.position;
    NavMeshHit hit;
    if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
    {
        destination = hit.position;
        navMeshAgent.SetDestination(destination);
    }
    }

    void ChasePlayer()
    {
    Invoke("ChasePlayer",0.3f);
    navMeshAgent.SetDestination(Player.transform.position);
    }


    void Remove(){Destroy(MainObject);}









void OnCollisionEnter(Collision other)
{
    if (other.gameObject.CompareTag("Bot") && other.rigidbody.velocity.magnitude > 5 && FrameImmunity > 1f && LIFE == 1)
    {

        if(IsHealing == 1){
        CancelInvoke("Attack1A");
        CancelInvoke("Attack1B");
        CancelInvoke("Attack1C");
        Arm1.SetActive(false);
        Arm2.SetActive(false);
        HealingParticle.Stop();
        Invoke("StartEnemy", StartEnemyAndRefresh);
        HP -= HPMAX/Mag;
        IsHealing = 0;
        }
        else{
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);
        
        HP -= HPMAX/DamageReduction/Mag;
        }

        FrameImmunity = 0;
    }

    if (other.gameObject.CompareTag("PlayerBulletBot") && FrameImmunity > 1f && LIFE == 1)
    {
            if(IsHealing == 1){
            CancelInvoke("Attack1A");
            CancelInvoke("Attack1B");
            CancelInvoke("Attack1C");
            Arm1.SetActive(false);
            Arm2.SetActive(false);
            HealingParticle.Stop();
            Invoke("StartEnemy", StartEnemyAndRefresh);
            GameObject PlayerBullet = other.gameObject;
            int DamageBullet = Mathf.RoundToInt(PlayerBullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet/DamageReduction;
            IsHealing = 0;
            FrameImmunity = 0;
            Destroy(PlayerBullet);
            }
            else{
            GameObject PlayerBullet = other.gameObject;
            int DamageBullet = Mathf.RoundToInt(PlayerBullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet;
            FrameImmunity = 0;
            Destroy(PlayerBullet);
            }
    }

    if (other.gameObject.CompareTag("MeleeProjectileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);

            if(IsHealing == 1){
            CancelInvoke("Attack1A");
            CancelInvoke("Attack1B");
            CancelInvoke("Attack1C");
            Arm1.SetActive(false);
            Arm2.SetActive(false);
            HealingParticle.Stop();
            Invoke("StartEnemy", StartEnemyAndRefresh);
            HP -=  HPMAX/Mag;
            IsHealing = 0;
            FrameImmunity = 0;
            }
            else{
            HP -=  HPMAX/Mag/2;
            FrameImmunity = 0;
            }
    }
    if (other.gameObject.CompareTag("PlayerMissileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);

            if(IsHealing == 1){
            CancelInvoke("Attack1A");
            CancelInvoke("Attack1B");
            CancelInvoke("Attack1C");
            Arm1.SetActive(false);
            Arm2.SetActive(false);
            HealingParticle.Stop();
            Invoke("StartEnemy", StartEnemyAndRefresh);
            HP -=  HPMAX/Mag;
            IsHealing = 0;
            FrameImmunity = 0;
            }
            else{
            HP -=  50;
            FrameImmunity = 0;
            }
    }
}
}