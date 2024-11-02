using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class EnemySentryBoss : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip EnemyAlertSound;
    public AudioClip BossDeathExplosion1;
    public AudioSource Sound;
    public AudioSource Sound2;
    public AudioSource DeathSource;
    public TextMeshPro MagText;
    public ParticleSystem SpawnAnimation;
    public ParticleSystem DeathAnimation1;
    public ParticleSystem DeathAnimation2;
    public ParticleSystem DeathAnimation3;
    public ParticleSystem Cannon1SpawnParticle;
    public GameObject[] ColorHPBar;
    public GameObject[] objectsToActivate;
    public Renderer BossBodyColor2;
    public Renderer BossBodyCeilingColor;
    public Renderer BossEyes;
    private float MagtoText = 1;
    public Slider HPslider;
    public Color HPcolor;
    public Enemydamagegiver MeleeChecker;

    public GameObject Player;
    public GameObject PlayerCamera;
    public WeaponScript WeaponScript;
    public KeyboardControlMk2 PlayerScript;
    public GameObject Bot;
    public GameObject MainObject;
    public GameObject Gun;
    public GameObject Cannon1;
    public GameObject Cannon2;
    public GameObject Cannon3;
    public GameObject AlertSign;
    public GameObject HealthBar;
    public GameObject bulletPrefab;
    public GameObject MissilePrefab;
    public GameObject TurretGrenadePrefab;
    public GameObject AntiCheeseMissile;
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint2;
    public Transform bulletSpawnPoint3;
    public Transform Cannontransformleft,Cannontransformleftspin,Cannontransformright,Cannontransformrightspin,Cannontransformtop,Cannontransformbehind,Cannontransformbehindspin,SpinOrigin,Cannontransformfront,Cannontransformfrontleft,Cannontransformfrontright;
    public Transform MissileSpawner1,MissileSpawner2;
    public GameObject Cannon1Prefab;
    public LayerMask whatIsPlayer;

    public int PatrolChance = 20;
    public int Shoot;
    public float FrameImmunity;
    public bool playerInSightRange;
    public int IsDead,IsSearching,IsPatrolling,LIFE,hascounted;

    private int ATTACKSMAX,ATTACKSMIN,CannonMode;
    public float sightrangeStart = 150;
    public float sightRange;
    public float sightRangeWhenCaught;
    public float sightRangeWhenLost;
    public float CannonSpinning = 0.5f;
    public float CannonSpinning2 = 0.7f;

    private Vector3 destination;

    //EnemyStats
    public float HPMAX = 100;
    public float HP;
    public float Mag = 1;
    public float DamageReduction = 5f;
    public float bulletSpeed = 10;
    public float DMG;
    public int ShootingSpeed;

    private void Start()
    {

        HPMAX = HPMAX*Mag;
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
        sightRangeWhenCaught = sightRange*3;
        sightRangeWhenLost = sightRange;
        LIFE = 0;
        DMG = DMG*Mag;

        ATTACKSMIN = 0;
        ATTACKSMAX = 2;

    BossBodyColor2.materials[1].color = Color.blue;
    BossBodyCeilingColor.material.color = Color.blue;
    BossEyes.materials[1].color = Color.blue;

    for (int i = 0; i < ColorHPBar.Length; i++) 
    {
    Renderer renderer = ColorHPBar[i].GetComponent<Renderer>();
    if (renderer != null)
    {
        renderer.material.color = HPcolor;
    }
    }

    Gun.SetActive(false);
    }



    private void FixedUpdate()
    {
        ///Cannon modes
        if(CannonMode == 1){
        SpinOrigin.eulerAngles = SpinOrigin.eulerAngles + new Vector3(0,CannonSpinning,0);
        Vector3 Cannon1vector = (Cannontransformleftspin.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformrightspin.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformbehindspin.position - Cannon3.transform.position).normalized;
        Cannon1.transform.position += Cannon1vector * CannonSpinning2;
        Cannon2.transform.position += Cannon2vector * CannonSpinning2;
        Cannon3.transform.position += Cannon3vector * CannonSpinning2;
        }
        else if(CannonMode == 2){
        SpinOrigin.eulerAngles = SpinOrigin.eulerAngles + new Vector3(0,-CannonSpinning,0);
        Vector3 Cannon1vector = (Cannontransformleftspin.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformrightspin.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformbehindspin.position - Cannon3.transform.position).normalized;
        Cannon1.transform.position += Cannon1vector * CannonSpinning2;
        Cannon2.transform.position += Cannon2vector * CannonSpinning2;
        Cannon3.transform.position += Cannon3vector * CannonSpinning2;
        }
        else if(CannonMode == 3){
        float distance = Vector3.Distance(Cannon1.transform.position, Cannontransformfront.transform.position);
        float distance2= Vector3.Distance(Cannon2.transform.position, Cannontransformfrontright.transform.position);
        float distance3 = Vector3.Distance(Cannon3.transform.position, Cannontransformfrontleft.transform.position);
        Vector3 Cannon1vector = (Cannontransformfront.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformfrontright.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformfrontleft.position - Cannon3.transform.position).normalized;
        if(distance > 1) {Cannon1.transform.position += Cannon1vector * 1.3f;}
        if(distance2 > 1) {Cannon2.transform.position += Cannon2vector * 1.3f;}
        if(distance3 > 1) {Cannon3.transform.position += Cannon3vector * 1.3f;}
        }
        else if(CannonMode == 4){
        float distance = Vector3.Distance(Cannon1.transform.position, Cannontransformbehind.transform.position);
        float distance2= Vector3.Distance(Cannon2.transform.position, Cannontransformright.transform.position);
        float distance3 = Vector3.Distance(Cannon3.transform.position, Cannontransformleft.transform.position);
        Vector3 Cannon1vector = (Cannontransformbehind.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformright.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformleft.position - Cannon3.transform.position).normalized;
        if(distance > 1) {Cannon1.transform.position += Cannon1vector * 1f;}
        if(distance2 > 1) {Cannon2.transform.position += Cannon2vector * 1f;}
        if(distance3 > 1) {Cannon3.transform.position += Cannon3vector * 1f;}
        }
        else if(CannonMode == 5){
        SpinOrigin.eulerAngles = SpinOrigin.eulerAngles + new Vector3(0,-0.5f,0);
        float distance = Vector3.Distance(Cannon1.transform.position, Cannontransformtop.transform.position);
        Vector3 Cannon1vector = (Cannontransformtop.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformrightspin.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformbehindspin.position - Cannon3.transform.position).normalized;
        if(distance > 1) {Cannon1.transform.position += Cannon1vector * 1f;}
        Cannon2.transform.position += Cannon2vector * 0.7f;
        Cannon3.transform.position += Cannon3vector * 0.7f;
        Vector3 Cannon1direction = Player.transform.position - Cannon1.transform.position;
        Cannon1.transform.rotation = Quaternion.LookRotation(Cannon1direction);
        }
        else if(CannonMode == 6){
        SpinOrigin.eulerAngles = SpinOrigin.eulerAngles + new Vector3(0,-0.5f,0);
        float distance3 = Vector3.Distance(Cannon3.transform.position, Cannontransformtop.transform.position);
        Vector3 Cannon1vector = (Cannontransformleftspin.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformrightspin.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformtop.position - Cannon3.transform.position).normalized;
        Cannon1.transform.position += Cannon1vector * 0.7f;
        Cannon2.transform.position += Cannon2vector * 0.7f;
        if(distance3 > 1) {Cannon3.transform.position += Cannon3vector * 1f;}
        Vector3 Cannon3direction = Player.transform.position - Cannon3.transform.position;
        Cannon3.transform.rotation = Quaternion.LookRotation(Cannon3direction);
        }
        else if(CannonMode == 7){
        SpinOrigin.eulerAngles = SpinOrigin.eulerAngles + new Vector3(0,-0.5f,0);
        float distance2 = Vector3.Distance(Cannon2.transform.position, Cannontransformtop.transform.position);
        Vector3 Cannon1vector = (Cannontransformleftspin.position - Cannon1.transform.position).normalized;
        Vector3 Cannon2vector = (Cannontransformtop.position - Cannon2.transform.position).normalized;
        Vector3 Cannon3vector = (Cannontransformbehindspin.position - Cannon3.transform.position).normalized;
        Cannon1.transform.position += Cannon1vector * 0.7f;
        if(distance2 > 1) {Cannon2.transform.position += Cannon2vector * 1f;}
        Cannon3.transform.position += Cannon3vector * 0.7f;
        Vector3 Cannon2direction = Player.transform.position - Cannon2.transform.position;
        Cannon2.transform.rotation = Quaternion.LookRotation(Cannon2direction);
        }

    }

    private void Update()
    {
        
        if(HP < HPMAX && PlayerScript.HP <= 0){HP = HPMAX;}
        //FrameImmunity
        if(FrameImmunity <= 1f){FrameImmunity += 0.4f;}
        //failsafe incase enemy heals above max
        if(HP > HPMAX){HP = HPMAX;}
        //HPBar code
        HPslider.value = HP;
        
        //CheckPlayerDistance
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            //MainStarter
            if(playerInSightRange && HP > 0 && IsDead == 0){
            Vector3 GunDirection = Player.transform.position - Gun.transform.position;
            Gun.transform.rotation = Quaternion.LookRotation(GunDirection);
            
            if(IsSearching == 1){
            
            //HeadSpawn
            if(Gun.activeSelf == false){
            SpawnAnimation.Play();
            Gun.SetActive(true);
            }

             //Alert sound
             if(GlobalData.GetEnemyCount() < 1){ float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); Sound2.volume = audioVolume * masterVolume;
             Sound2.PlayOneShot(EnemyAlertSound);}

             AlertSign.SetActive(true);
             HealthBar.SetActive(true);
             LIFE = 1;
             IsSearching = 0;
             IsPatrolling = 0;
             HP = HPMAX;
             sightRange = sightRangeWhenCaught;

             ATTACKSMIN = 0;
             ATTACKSMAX = 2;

             //EnemyCountstuff
             hascounted = 2;
             if(IsDead == 0 && GlobalData.GetEnemyCount() >= 0){
             GlobalData.SetEnemyCount(GlobalData.GetEnemyCount() + 1);
             }

             Invoke("StartEnemy", 1f);
             Invoke("AlertSignLeave", 1f);
            }
            }
            else{
                if(IsPatrolling == 0){
                    BossBodyColor2.materials[1].color = Color.blue;
                    BossBodyCeilingColor.material.color = Color.blue;
                    BossEyes.materials[1].color = Color.blue;
                    if(Cannon1.activeSelf == false){Cannon1.SetActive(true); Cannon1SpawnParticle.Play();}
                    CancelInvoke("StartEnemy");
                    CancelInvoke("Attack1A");
                    CancelInvoke("Attack2A");
                    CancelInvoke("Attack2B");
                    CancelInvoke("Attack3A");
                    CancelInvoke("Attack3B");
                    CancelInvoke("Attack4A");
                    CancelInvoke("Attack5A");
                    AlertSign.SetActive(false);
                    HealthBar.SetActive(false);
                    LIFE = 0;
                    IsPatrolling = 1;
                    IsSearching = 1;
                    sightRange = sightRangeWhenLost;

                    //EnemyCountstuff
                    if(hascounted == 2 && GlobalData.GetEnemyCount() > 0){
                    CannonMode = 1;
                    GlobalData.SetEnemyCount(GlobalData.GetEnemyCount() - 1);
                    }
                }
            }

            //Deathcode
            if(HP <= 0){

                if(IsDead == 0){
                if(Cannon1.activeSelf == false){Cannon1.SetActive(true); Cannon1SpawnParticle.Play();}
                CancelInvoke("StartEnemy");
                CancelInvoke("Attack1A");
                CancelInvoke("Attack2A");
                CancelInvoke("Attack3A");
                CancelInvoke("Attack3B");
                CancelInvoke("Attack4A");
                CancelInvoke("Attack5A");
                CancelInvoke("Attack2B");
                CancelInvoke("ChasePlayer");
                CannonMode = 1;

                LIFE = 0;
                AlertSign.SetActive(false);
                IsDead = 1;
                HealthBar.SetActive(false);
                hascounted = 1;

                Invoke("DyingAnimationStart",0.1f);
                Invoke("Remove",8f);
                }
            }




    }



    void StartEnemy()
    {

        if(HP <= HPMAX*1 && HP > HPMAX*0.7){
        ATTACKSMIN = 0;
        ATTACKSMAX = 2;
        }
        if(HP <= HPMAX*0.7 && HP > HPMAX*0.5){
        ATTACKSMIN = 1;
        ATTACKSMAX = 3;
        }
        if(HP <= HPMAX*0.5 && HP > HPMAX*0.3){
        ATTACKSMIN = 0;
        ATTACKSMAX = 4;
        }
        if(HP <= HPMAX*0.3 && HP > HPMAX*0){
        ATTACKSMIN = 0;
        ATTACKSMAX = 5;
        }

    int ATT = UnityEngine.Random.Range(ATTACKSMIN,ATTACKSMAX);
    AlertSign.SetActive(false);
     CancelInvoke("Attack1A");
     CancelInvoke("Attack2A");
     CancelInvoke("Attack2B");
     CancelInvoke("ChasePlayer");


     if(Cannon1.activeSelf == false){Cannon1.SetActive(true); Cannon1SpawnParticle.Play();}

    if(ATT == 0){
    BossBodyColor2.materials[1].color = Color.cyan;
    BossBodyCeilingColor.material.color = Color.cyan;
    BossEyes.materials[1].color = Color.blue;
        CannonMode = 1;
        Invoke("Attack1A", 0.1f);
        int StartEnemyAgain = UnityEngine.Random.Range(2,6);
        Invoke("StartEnemy", StartEnemyAgain);

    }
    else if(ATT == 1){
    BossBodyColor2.materials[1].color = Color.magenta;
    BossBodyCeilingColor.material.color = Color.magenta;
    BossEyes.materials[1].color = Color.blue;
       CannonMode = 2;
       Invoke("Attack2A", 0.7f);
       Invoke("StartEnemy", 2f);

    }
    else if(ATT == 2){
    BossBodyColor2.materials[1].color = Color.red;
    BossBodyCeilingColor.material.color = Color.red;
    BossEyes.materials[1].color = Color.blue;
       CannonMode = 5;
       Invoke("Attack3A", 1f);
       Invoke("StartEnemy", 5f);

    }
    else if(ATT == 3){
    BossBodyColor2.materials[1].color = Color.green;
    BossBodyCeilingColor.material.color = Color.green;
    BossEyes.materials[1].color = Color.blue;
       CannonMode = 6;
       Invoke("Attack4A", 0.5f); 
       Invoke("StartEnemy", 2.5f);

    }
    else if(ATT == 4){
    BossBodyColor2.materials[1].color = Color.blue;
    BossBodyCeilingColor.material.color = Color.blue;
    BossEyes.materials[1].color = Color.blue;
    CannonMode = 7;
       Invoke("Attack5A", 0.5f); 
       Invoke("StartEnemy", 5f);

    }
    }



    //Attack1
    void Attack1A()
    {
    if(Shoot < ShootingSpeed/1.5){Shoot += 1;}
    if(Shoot >= ShootingSpeed/1.5 && WeaponScript.GuardingBot == 0){Shoot = 0;}


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

    Invoke("Attack1A", 0.05f);
    }


    //Attack2
    void Attack2A()
    {
        var Missile = Instantiate(MissilePrefab, MissileSpawner1.position, MissileSpawner1.rotation);
        Missile.GetComponent<Enemydamagegiver>().Damage = DMG;
        
        Invoke("Attack2B", 0.5f);

    }
    void Attack2B()
    {
        var Missile2 = Instantiate(MissilePrefab, MissileSpawner2.position, MissileSpawner2.rotation);
        Missile2.GetComponent<Enemydamagegiver>().Damage = DMG;
    }
    //Attack3
    void Attack3A()
    {
    Invoke("Attack3B", 1f);
    }
    void Attack3B()
    {
    Cannon1.SetActive(false);
    var Cannonthrow = Instantiate(Cannon1Prefab, Cannon1.transform.position, Cannon1.transform.rotation);
    Cannonthrow.GetComponent<Enemydamagegiver>().Damage = DMG*1.5f;
    }
    //Attack4
    void Attack4A()
    {
        var TurretGrenade = Instantiate(TurretGrenadePrefab, Cannon3.transform.position, Cannon3.transform.rotation);
        TurretGrenade.GetComponent<Rigidbody>().velocity = Cannon3.transform.forward * 75;
        TurretGrenade.GetComponent<timespawner>().MAG = Mag;

        var TurretGrenade2 = Instantiate(TurretGrenadePrefab, Cannon3.transform.position, Cannon3.transform.rotation);
        TurretGrenade2.GetComponent<Rigidbody>().velocity = Cannon3.transform.forward * 100;
        TurretGrenade2.GetComponent<timespawner>().MAG = Mag;

        var TurretGrenade3 = Instantiate(TurretGrenadePrefab, Cannon3.transform.position, Cannon3.transform.rotation);
        TurretGrenade3.GetComponent<Rigidbody>().velocity = Cannon3.transform.forward * 125;
        TurretGrenade3.GetComponent<timespawner>().MAG = Mag;
    }
    //Attack5
    void Attack5A()
    {
        var AntiCheeseNuke = Instantiate(AntiCheeseMissile, Cannon2.transform.position, Cannon2.transform.rotation);
        AntiCheeseNuke.GetComponent<Enemydamagegiver>().Damage = DMG*5;
    }


    void AlertSignLeave(){AlertSign.SetActive(false);}

void DyingAnimationStart(){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); 
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); 
    DeathSource.volume = audioVolume * masterVolume; 
    DeathSource.PlayOneShot(BossDeathExplosion1); 
    DeathAnimation1.Play();
    CannonSpinning += 0.2f;
    CannonSpinning2 += 0.2f;
    Invoke("DyingAnimationStart2",0.5f);

}

void DyingAnimationStart2(){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); 
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); 
    DeathSource.volume = audioVolume * masterVolume; 
    DeathSource.PlayOneShot(BossDeathExplosion1); 
    DeathAnimation2.Play();
    CannonSpinning += 0.2f;
    CannonSpinning2 += 0.2f;
    Invoke("DyingAnimationStart3",0.5f);
}

void DyingAnimationStart3(){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); 
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); 
    DeathSource.volume = audioVolume * masterVolume; 
    DeathSource.PlayOneShot(BossDeathExplosion1); 
    DeathAnimation3.Play();
    CannonSpinning += 0.2f;
    CannonSpinning2 += 0.2f;
    Invoke("DyingAnimationStart",0.5f);
}
    void Remove(){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); 
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); 
    DeathSource.volume = audioVolume * masterVolume; 
    DeathSource.PlayOneShot(BossDeathExplosion1); 
    gameObject.GetComponent<Collider>().enabled = false;
    CancelInvoke("DyingAnimationStart");
    CancelInvoke("DyingAnimationStart2");
    CancelInvoke("DyingAnimationStart3");
    SpawnAnimation.Play();

    Cannon1.SetActive(false);
    Cannon2.SetActive(false);
    Cannon3.SetActive(false);

    foreach (GameObject obj in objectsToActivate){obj.GetComponent<Renderer>().enabled = false;}

    Invoke("Remove2",3f);
    }

    void Remove2(){Destroy(MainObject);}









void OnCollisionEnter(Collision other)
{
    if (other.gameObject.CompareTag("Bot") && other.rigidbody.velocity.magnitude > 5 && FrameImmunity > 1f && LIFE == 1)
    {

        HP -= 20;
        FrameImmunity = 0;
        Destroy(other.gameObject);
    }
    if (other.gameObject.CompareTag("PlayerBulletBot") && FrameImmunity > 1f && LIFE == 1)
    {
            GameObject PlayerBullet = other.gameObject;
            int DamageBullet = Mathf.RoundToInt(PlayerBullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet/DamageReduction/5;
            FrameImmunity = 0;
            Destroy(PlayerBullet);
    }

    if (other.gameObject.CompareTag("MeleeProjectileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); Sound.volume = audioVolume * masterVolume; Sound.PlayOneShot(Clip3);
            
            MeleeChecker = other.gameObject.GetComponent<Enemydamagegiver>();
            if(MeleeChecker != null){
            Debug.Log("Test Damage = " + MeleeChecker.Damage);
            HP -= MeleeChecker.Damage;
            MeleeChecker = null;
            }
            else if(MeleeChecker == null){
            Debug.Log("Test2");
            HP -=  100/DamageReduction/1;
            MeleeChecker = null;
            }
            FrameImmunity = 0;
    }
    if (other.gameObject.CompareTag("PlayerMissileBot") && FrameImmunity > 1f && LIFE == 1)
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume"); float masterVolume = PlayerPrefs.GetFloat("MasterVolume"); Sound.volume = audioVolume * masterVolume; Sound.PlayOneShot(Clip3);

        HP -=  100/DamageReduction/3;
        FrameImmunity = 0;
    }
}
}