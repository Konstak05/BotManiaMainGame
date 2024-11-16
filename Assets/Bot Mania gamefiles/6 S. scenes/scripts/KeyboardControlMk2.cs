using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardControlMk2 : MonoBehaviour
{
    //Player Controller
    public CharacterController controller;

    public Vector2 newcontrolls;
    public PlayerInput PlayerInputStarter;
    public InputAction MovingAction;
    public InputAction SprintAction;
    public InputAction JumpAction;
    public InputAction DashAction;
    public InputAction SlideAction;
    public bool sprintKey;
    public bool JumpKey;
    public bool SlidingKey;
    public bool DashKey;

    //Weapon scripts
    public Gunscript Gunscript;
    public WeaponScript WeaponScript;
    //ToggleUI
    public ToggleUI ToggleUI;
    //MainCamera
    public Camera MainCamera;
    public Vector3 velocity;
    public Rigidbody FallingRigidBody;
    public GameObject CameraUI;
    public GameObject CameraUI2;
    public GameObject ScreenEffect;
    //Indicator Stuff
    public GameObject HurtIndicatorCanvas;
    public Image HurtIndicatorImage;
    public Color HurtIndicatorColor;


    public MouseLook scriptA;
    public HPbar HeartbeatStarter;
    public AudioClip Clip;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioClip Clip4;
    public AudioClip Clip5;
    public AudioClip SlidingSound;
    public AudioClip Keysound;
    public AudioClip JetpackStart;
    public AudioClip DashSound;
    public AudioClip DashRecharge;
    public AudioSource Sound;
    public AudioSource footstepsSound;
    public AudioSource jetpackSound;
    public AudioSource SlidingSource;
    public AudioSource Keysoundclicking;
    public Vector3 move;
    public Vector3 move2;

    public GameObject playerBulletPrefab;

    public Transform groundCheck;
    public Transform BodyPos;
    public PlayerSpawner PlayerspawnerScene;
    public TextMeshPro CurrentLocationText;
    public TextMeshPro CurrentGoalText;
    public int currentTextIndex = 0;

    public GameObject[] CreativeModethings;
    public GameObject[] TutorialThings;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 1f;
    public float groundDistance = 0.4f;
    public float FrameImmunity = 0f;
    public float AnimationSmoother;
    public float CanFall,CanJump = 1f;
    public float CanDash = 100f;
    private float fallDamageThreshold = 100f;
    private float fallDamageMultiplier = 1f;
    public float hasjumped = 0;
    public float fallVelocity;

    public int HP = 100;
    public int Dead = 0;
    public int StartWalkSound,StartSprintSound,JetpackStarted;
    public int RechargedSound = 1;

    public bool isGrounded;
    public bool isGrounded2;
    public bool isSprinting;
    public bool IsDashing;
    public bool SlidingSourceStarter;
    public bool DashInvince;

    public WeaponScript IsHealing;
    public int IsAlreadyHealing;

    public int Sliding;
    private Vector3 velocityDash;


    public Animator SlidingAnimator;
    public float SlidingRight,SlidingFront,SlidingCooldown;
    public int SlidingDirection;

    public LayerMask Ground;
    void Start()
    {
    //InputControls
    PlayerInputStarter = GetComponent<PlayerInput>();
    SetupInputControls();
    //Playerspawnerforhelping
    PlayerspawnerScene = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
    CurrentLocationText.text = "";
    CurrentGoalText.text = "";
    Invoke("WriteNextCharacter",1.5f);  

    Vector3 newPosition = PlayerspawnerScene.SpawningPos;
    controller.enabled = false;
    transform.position = newPosition;
    controller.enabled = true;
    
    HP = 100;
    Debug.Log("Current Health = " + HP);
    FallingRigidBody.isKinematic = true;
    CameraUI.SetActive(true);
    CameraUI2.SetActive(false);
    ScreenEffect.SetActive(true);
    RechargedSound = 1;

     
    ///CreativeStuff
    if(PlayerspawnerScene.IsCreativeMode == true){
        for (int CreativemodeIndex = 0; CreativemodeIndex < CreativeModethings.Length; CreativemodeIndex++)
        {
            CreativeModethings[CreativemodeIndex].SetActive(true);
        }
    }
    else{
        for (int CreativemodeIndex = 0; CreativemodeIndex < CreativeModethings.Length; CreativemodeIndex++)
        {
            CreativeModethings[CreativemodeIndex].SetActive(false);
        }       
    }

    if(PlayerspawnerScene.IsTutorialMode == true){
        for (int TutorialthingsIndex = 0; TutorialthingsIndex < TutorialThings.Length; TutorialthingsIndex++)
        {
           TutorialThings[TutorialthingsIndex].SetActive(false);
        }
    }
    }
    ///////////////////
    //Update Function//
    ///////////////////
    void Update()
    {
        //Keys
        sprintKey = SprintAction.IsPressed();
        JumpKey = JumpAction.IsPressed();
        SlidingKey = SlideAction.IsPressed();
        DashKey = DashAction.IsPressed();
    
        if(hasjumped > 0){hasjumped -= 0.1f;}
        if(velocity.x > 0 && isGrounded && FrameImmunity >= 1f){velocity.x = 0;}
        if(velocity.z > 0 && isGrounded && FrameImmunity >= 1f){velocity.z = 0;}
        if(velocity.x < 0 && isGrounded && FrameImmunity >= 1f){velocity.x = 0;}
        if(velocity.z < 0 && isGrounded && FrameImmunity >= 1f){velocity.z = 0;}
      
        SlidingAnimator.SetFloat("Forward", SlidingFront); 
        SlidingAnimator.SetFloat("Right", SlidingRight); 

        //SprintingBool
        if (sprintKey && Sliding == 0){isSprinting = true;}
        if (!sprintKey){isSprinting = false;}
        
        //Walking&Running Sounds
        if(newcontrolls.y > 0 & Sliding == 0 || newcontrolls.x < 0 & Sliding == 0 || newcontrolls.y < 0 & Sliding == 0 || newcontrolls.x > 0 & Sliding == 0){

            if (sprintKey && WeaponScript.GuardingBot == 0){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                footstepsSound.volume = audioVolume * masterVolume;
                 
                if(StartWalkSound == 1 && controller.isGrounded){footstepsSound.Play(); StartWalkSound = 0;}
                else if(StartWalkSound == 0 && !controller.isGrounded){StartWalkSound = 1;}
                if (footstepsSound.time >= footstepsSound.clip.length/1.9 && controller.isGrounded && HP > 0){footstepsSound.Play();}
            }
            else{
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                footstepsSound.volume = audioVolume * masterVolume;

                if(StartWalkSound == 1 && controller.isGrounded && HP > 0){footstepsSound.Play(); StartWalkSound = 0;}
                else if(StartWalkSound == 0 && !controller.isGrounded){StartWalkSound = 1;}
                if (footstepsSound.time >= footstepsSound.clip.length/1.05 && controller.isGrounded && HP > 0){footstepsSound.Play();}
            }
        }
        else
        {
        if(StartWalkSound == 0){StartWalkSound = 1;}
        }

        //HealingFunctions
        if(IsHealing.IsHealingPlayer == 1 && HP > 0 && HP < 100 && IsAlreadyHealing == 0){
        Invoke("HealingInterval",0.2f);
        IsAlreadyHealing = 1;
        }
        else if(IsHealing.IsHealingPlayer == 0){
        CancelInvoke("HealingInterval");
        IsAlreadyHealing = 0;
        }
    }

    void FixedUpdate()
    {

    //Intervals&Values
    if(CanDash < 100f){CanDash += 0.30f;}
    if(CanFall < 10f){CanFall += 0.1f;}
    if(FrameImmunity < 1f){FrameImmunity += 0.1f;}
    if(CanJump < 10f){CanJump += 0.1f;}
    if(SlidingCooldown < 10f){SlidingCooldown += 0.1f;}

    if(SlidingDirection == 1){
       if(SlidingFront > 0){SlidingFront -= 0.1f;}
       if(SlidingFront < 0){SlidingFront += 0.1f;}
       if(SlidingRight > 0){SlidingRight -= 0.1f;}
       if(SlidingRight < 0){SlidingRight += 0.1f;}
    }
    if(SlidingDirection == 2){
       if(SlidingFront < 1){SlidingFront += 0.1f;}
       if(SlidingRight > 0){SlidingRight -= 0.1f;}
       if(SlidingRight < 0){SlidingRight += 0.1f;}  
    }
    if(SlidingDirection == 3){
       if(SlidingFront > -1){SlidingFront -= 0.1f;}
       if(SlidingRight > 0){SlidingRight -= 0.1f;}
       if(SlidingRight < 0){SlidingRight += 0.1f;}  
    }
    if(SlidingDirection == 4){
       if(SlidingFront > 0){SlidingFront -= 0.1f;}
       if(SlidingFront < 0){SlidingFront += 0.1f;}
       if(SlidingRight < 1){SlidingRight += 0.1f;}  
    }
    if(SlidingDirection == 5){
       if(SlidingFront > 0){SlidingFront -= 0.1f;}
       if(SlidingFront < 0){SlidingFront += 0.1f;}
       if(SlidingRight > -1){SlidingRight -= 0.1f;} 
    }

    ///////////////////////
    //MOVEMENT WHEN ALIVE//
    ///////////////////////
    if(HP > 0){

        isGrounded = controller.isGrounded;
        isGrounded2 = Physics.CheckSphere(groundCheck.position, groundDistance);

        if (!SlidingKey){

        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        newcontrolls = MovingAction.ReadValue<Vector2>();
        move = transform.right * newcontrolls.x + transform.forward * newcontrolls.y;

        if(Sliding == 1){
            SlidingSource.Stop();
            SlidingSourceStarter = false;
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            SlidingDirection = 1;
            Sliding = 0;
        }
        }
        else if (newcontrolls.y > 0 | newcontrolls.x < 0 | newcontrolls.y < 0 | newcontrolls.x > 0 && SlidingKey && isGrounded && !IsDashing && SlidingCooldown >= 5f){

            if(isGrounded && !SlidingSourceStarter){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                SlidingSource.volume = audioVolume * masterVolume;
                SlidingSource.Play();
                SlidingSourceStarter = true;
            }           
            if(Sliding == 0 && newcontrolls.y > 0){ //W
                move2 = transform.forward;
                SlidingDirection = 2;
                Sliding = 1;
            }
            if(Sliding == 0 && newcontrolls.y < 0){ //S
                move2 = -transform.forward;
                SlidingDirection = 3;
                Sliding = 1;
            }
            if(Sliding == 0 && newcontrolls.x < 0){ //A
                move2 = -transform.right;
                SlidingDirection = 4;
                Sliding = 1;
            }
            if(Sliding == 0 && newcontrolls.x > 0){ //D
                move2 = transform.right;
                SlidingDirection = 5;
                Sliding = 1;
            }
            SlidingCooldown = 0;
            move = move2 * 2f;
        }
        else if (newcontrolls.y == 0 | newcontrolls.x == 0 | newcontrolls.y == 0 | newcontrolls.x == 0 && SlidingKey && isGrounded && !IsDashing && SlidingCooldown >= 5f){
            if(isGrounded && !SlidingSourceStarter){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                SlidingSource.volume = audioVolume * masterVolume;
                SlidingSource.Play();
                SlidingSourceStarter = true;
            }           
            if(Sliding == 0){
                move2 = transform.forward;
                SlidingDirection = 2;
                Sliding = 1;
            }
            SlidingCooldown = 0;
            move = move2 * 2f;
        }



        if (!isGrounded && SlidingSourceStarter){
            SlidingSource.Stop();
            SlidingSourceStarter = false;
        }


        if (isSprinting && WeaponScript.GuardingBot == 0 && !IsDashing)
        {
            controller.Move(move * speed * 1.5f * Time.deltaTime);
        }
        else if (!isSprinting && WeaponScript.GuardingBot == 0 && !IsDashing)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        
        if (WeaponScript.GuardingBot == 0 && DashKey && CanDash >= 100f && Sliding == 0)
        {
            if(velocity.x > 0){velocity.x = 0;}
            if(velocity.z > 0){velocity.z = 0;}
            if(velocity.x < 0){velocity.x = 0;}
            if(velocity.z < 0){velocity.z = 0;}         
            Invoke("Dash",0);
            Invoke("DashStop",0.3f);
            IsDashing = true;
            CanDash = 0;
            velocityDash = move * 125;          
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(DashSound);
        }
        if(CanDash >= 100f && RechargedSound == 0){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(DashRecharge);
            RechargedSound = 1;
        }
        if (WeaponScript.GuardingBot == 1)
        {
            controller.Move(move * speed/3 * Time.deltaTime);
        }

        if(JumpKey && isGrounded && CanJump >= 5f && WeaponScript.GuardingBot == 0 && !IsDashing)

        {
            if(Gunscript.GunEquipped != 9){
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            hasjumped = 1;
            }
            if(Gunscript.GunEquipped == 9){
            velocity.y = Mathf.Sqrt(jump * -4f * gravity);
            hasjumped = 1;
            }

            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            jetpackSound.volume = audioVolume * masterVolume;
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(Clip);
            CanJump = 0f;
            jetpackSound.Stop();
            JetpackStarted = 0;
        }
    
        if(velocity.y > -200f && !JumpKey && !IsDashing)
        {
            velocity.y += gravity * Time.deltaTime;
            JetpackStarted = 0;
            jetpackSound.Stop();
        }
        else if(velocity.y > -16f && JumpKey && !IsDashing){
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y > -50f && velocity.y < -15f && JumpKey && !IsDashing){
            velocity.y -= 0.1f;
            if(velocity.x > 0){velocity.x -= 0.5f;}
            if(velocity.z > 0){velocity.z -= 0.5f;}
            if(velocity.x < 0){velocity.x += 0.5f;}
            if(velocity.z < 0){velocity.z += 0.5f;}         

            if(JetpackStarted == 0 && !isGrounded){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                jetpackSound.volume = audioVolume * masterVolume;
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(JetpackStart);
                jetpackSound.Play();
                JetpackStarted = 1;
            }
        }

        if(isGrounded && velocity.y < 0){velocity.y = -30f; jetpackSound.Stop();}

        controller.Move(velocity * Time.deltaTime);

        if(ToggleUI.PauseMenu == 0){Cursor.lockState = CursorLockMode.Locked;}else{Cursor.lockState = CursorLockMode.None;}

        //Animation
        if(move != Vector3.zero && isGrounded && !isSprinting && !IsDashing && Sliding == 0){
            if(AnimationSmoother < 15){AnimationSmoother += 0.25f;}
            if(AnimationSmoother > 15){AnimationSmoother -= 0.25f;}
        }
        else if(move != Vector3.zero && isGrounded && isSprinting && WeaponScript.GuardingBot == 0 && !IsDashing && Sliding == 0){
            if(AnimationSmoother < 30){AnimationSmoother += 0.25f;}
        }
        else if(move != Vector3.zero && isGrounded && isSprinting && WeaponScript.GuardingBot == 1 && !IsDashing && Sliding == 0){
            if(AnimationSmoother < 15){AnimationSmoother += 0.25f;}
            if(AnimationSmoother > 15){AnimationSmoother -= 0.25f;}
        }
        else if(move == Vector3.zero | !isGrounded | IsDashing | Sliding == 1){
            if(AnimationSmoother > 0){AnimationSmoother -= 0.2f;}
            if ( AnimationSmoother <= 0){AnimationSmoother = 0;}
        }
        MainCamera.GetComponent<Animator>().SetFloat("Speed", AnimationSmoother); 

        ///////////////////////////////
        ///END OF HP > 0 BEING ALIVE///
        ///////////////////////////////
        }
        else if(HP <= 0){ //PlayerIsDead//
            if(Dead == 0){ //If the player is not dead. Kill him
                if(ToggleUI.PauseMenu == 0){Cursor.lockState = CursorLockMode.Locked;}else{Cursor.lockState = CursorLockMode.None;}
                jetpackSound.Stop();
                CameraUI.SetActive(false);
                CameraUI2.SetActive(true);
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(Clip4);
                Dead = 1;
                velocity.x = 0;
                velocity.z = 0;
                //HurtIndicatorCanvas
                HurtIndicatorCanvas.SetActive(false);
            }
            FallingRigidBody.isKinematic = false;
            controller.enabled = false;
            transform.rotation = Quaternion.Euler(60f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            if(Input.GetButtonDown("Jump")){RespawnOnMap();}
        }
    }

    ///////////////////////////////
    //////////////////////
    //NONE UNITY FUNCTIONS//
    //////////////////////
    ///////////////////////////////


    //MOVEMENT CONTROLS STARTER!!
    void SetupInputControls(){
        MovingAction = PlayerInputStarter.actions["Move"];
        SprintAction = PlayerInputStarter.actions["Sprint"];
        JumpAction = PlayerInputStarter.actions["Jump"];
        SlideAction = PlayerInputStarter.actions["Slide"];
        DashAction = PlayerInputStarter.actions["Dash"];
    }

    //Respawningoption
    public void RespawnOnMap(){
        Vector3 newPosition = PlayerspawnerScene.SpawningPos;
        controller.enabled = false;
         transform.position = newPosition;
        controller.enabled = true;
        HP = 100;
        velocity.y = 10f;
        Debug.Log("Current Health = " + HP);
        FallingRigidBody.isKinematic = true;
        controller.enabled = true;
        transform.rotation = PlayerspawnerScene.Rotation;
        CameraUI.SetActive(true);
        CameraUI2.SetActive(false);
        CanJump = 0;
        FrameImmunity = 0;
        Dead = 0;
        CanDash = 100f;
   
        scriptA.RespawnOnMap2();
        HeartbeatStarter.RespawnHPHeart();

        //Sound
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip3);    
    }

    //For Dashing
    void Dash(){
        controller.Move(velocityDash * Time.deltaTime);
        Invoke("Dash",0);
        RechargedSound = 0;
        velocity.y = 0f;
        DashInvince = true;
    }
    void DashStop(){
        CancelInvoke("Dash");
        IsDashing = false;
        velocity.y = 10f;
        DashInvince = false;
    }

    ///HEALINGINTERVAL
    public void HealingInterval(){
        if(HP > 0 && HP < 100){
        HP = HP + 1;
        Invoke("HealingInterval",0.2f);
        }
    }

    //For the starting Text
    void WriteNextCharacter(){
        if (currentTextIndex < PlayerspawnerScene.Location.Length){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Keysoundclicking.volume = audioVolume * masterVolume/2;
            Keysoundclicking.PlayOneShot(Keysound); 
            CurrentLocationText.text += PlayerspawnerScene.Location[currentTextIndex];
            currentTextIndex++;
            Invoke("WriteNextCharacter",0.075f);
        }
        else{
            Invoke("WriteNextCharacter2",0.5f);
            currentTextIndex = 0;
            CancelInvoke("WriteNextCharacter");
        }
    }
    void WriteNextCharacter2(){
            if (currentTextIndex < PlayerspawnerScene.Goal.Length){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Keysoundclicking.volume = audioVolume * masterVolume/2;
            Keysoundclicking.PlayOneShot(Keysound); 
            CurrentGoalText.text += PlayerspawnerScene.Goal[currentTextIndex];
            currentTextIndex++;
            Invoke("WriteNextCharacter2",0.075f);
        }
        else{
            Invoke("WriteNextCharacter3",2.5f);
            CancelInvoke("WriteNextCharacter2");    
        }   
    }   
    void WriteNextCharacter3(){
        if (CurrentLocationText.text.Length > 0){
            CurrentLocationText.text = CurrentLocationText.text.Substring(0, CurrentLocationText.text.Length - 1);
            Invoke("WriteNextCharacter3",0.025f);
        }
        else if(CurrentGoalText.text.Length > 0){
            CurrentGoalText.text = CurrentGoalText.text.Substring(0, CurrentGoalText.text.Length - 1);
            Invoke("WriteNextCharacter3",0.025f);
        }
        else{
            CancelInvoke("WriteNextCharacter3");
        }
    }

    //HurtIndicatorFunctions
    void StartHurtGameObject(){
        HurtIndicatorColor.a = 1f;
        HurtIndicatorImage.color = HurtIndicatorColor;
        HurtIndicatorCanvas.SetActive(true);
        CancelInvoke("RemoveHurtGameObject");
        CancelInvoke("EndHurtGameObject");
        Invoke("RemoveHurtGameObject",0.5f);
    }
    void RemoveHurtGameObject(){
        HurtIndicatorColor.a -= 0.1f;
        HurtIndicatorImage.color = HurtIndicatorColor;
        Invoke("RemoveHurtGameObject",0.01f);
        Invoke("EndHurtGameObject",1f);
    }
    void EndHurtGameObject(){
        HurtIndicatorCanvas.SetActive(false);
        CancelInvoke("RemoveHurtGameObject");
    }

    ////////////////////////
    ///Collision Functions//
    ////////////////////////
    void OnControllerColliderHit(ControllerColliderHit hit){
        float fallVelocity = -velocity.y;
        if (Ground == (Ground | (1 << hit.gameObject.layer)) && hasjumped <= 0){
            velocity.x = 0; 
            if(velocity.y >= 25){velocity.y = 0;}
            velocity.z = 0;
        }
        if(HP > 0 && fallVelocity > fallDamageThreshold && DashInvince == false){
            if (CanFall >= 10f && !isGrounded && isGrounded2){
                float Falldamage = (fallVelocity - fallDamageThreshold) * fallDamageMultiplier;
                HP -= (int)Falldamage;
                Debug.Log("Current Health = " + HP);
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(Clip2);
                Sound.PlayOneShot(Clip5);
                //scriptA.FalldamageApplier();
                CanFall = 0f;
                //HurtIndicator
                StartHurtGameObject();
            }
        }
    if (hit.gameObject.CompareTag("DEATHBARRIER")){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip2);
        HP -= HP;
    }
    //BossHitbox
    if (hit.gameObject.CompareTag("BossHitbox") && FrameImmunity >= 1f && Dead == 0 && DashInvince == false){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(Clip2);
            //Damagecaused
            FrameImmunity = 0f;
            HP -= 20;
            //ExplosionKnockback
            Vector3 direction = BodyPos.position - hit.gameObject.transform.position;
            direction.Normalize();
            velocity = direction * Mathf.Sqrt(jump * -30f * gravity);
            //HurtIndicator
            StartHurtGameObject();
    }
    }

    void OnCollisionEnter(Collision collision){
    //EnemyBulletCollision
    if (collision.gameObject.CompareTag("EnemyBullet") && FrameImmunity >= 1f && Dead == 0 && DashInvince == false){
        if(WeaponScript.GuardingBot == 0){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(Clip2);
            FrameImmunity = 0f;
            GameObject bullet = collision.gameObject;
            int DamageBullet = Mathf.RoundToInt(bullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet;
        }
        if(WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 1){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(Clip2);
            FrameImmunity = 0f;
            GameObject bullet = collision.gameObject;
            int DamageBullet = Mathf.RoundToInt(bullet.GetComponent<BulletScript>().Damage);
            HP -= DamageBullet*3;
        }
        //HurtIndicator
        StartHurtGameObject();
    }
    //EnemyMeleeCollision
    if (collision.gameObject.CompareTag("EnemyMeleeProjectile") && FrameImmunity >= 1f && Dead == 0 && WeaponScript.GuardingBot == 0 && DashInvince == false){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(Clip2);
            FrameImmunity = 0f;
            GameObject bullet = collision.gameObject;
            int DamageBullet = Mathf.RoundToInt(bullet.GetComponent<Enemydamagegiver>().Damage);
            HP -= DamageBullet;
            //HurtIndicator
            StartHurtGameObject();
    }
    //PlayerMissileCollision
    if (collision.gameObject.CompareTag("PlayerMissileBot") && FrameImmunity >= 1f && Dead == 0){
        Vector3 direction = BodyPos.position - collision.gameObject.transform.position;
        direction.Normalize();
        FrameImmunity = 0f;
        velocity = direction * Mathf.Sqrt(jump * -15f * gravity);
        JetpackStarted = 0;
    }
    //EnemyMissileCollision
    if (collision.gameObject.CompareTag("EnemyMissile") && FrameImmunity >= 1f && Dead == 0 && DashInvince == false){
            if(IsHealing.IsHealingPlayer == 0){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(Clip2);

                //Damagecaused
                FrameImmunity = 0f;
                GameObject bullet = collision.gameObject;
                int DamageBullet = Mathf.RoundToInt(bullet.GetComponent<Enemydamagegiver>().Damage);
                HP -= DamageBullet;

                //ExplosionKnockback
                Vector3 direction = BodyPos.position - collision.gameObject.transform.position;
                direction.Normalize();
                velocity = direction * Mathf.Sqrt(jump * -30f * gravity);
                JetpackStarted = 0;
            }
            if(IsHealing.IsHealingPlayer == 1){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(Clip2);

                //Damagecaused
                FrameImmunity = 0f;
                GameObject bullet = collision.gameObject;
                int DamageBullet = Mathf.RoundToInt(bullet.GetComponent<Enemydamagegiver>().Damage);
                HP -= DamageBullet*2;

                //ExplosionKnockback
                Vector3 direction = BodyPos.position - collision.gameObject.transform.position;
                direction.Normalize();
                velocity = direction * Mathf.Sqrt(jump * -50f * gravity);
                JetpackStarted = 0;
            }
            //HurtIndicator
            StartHurtGameObject();
        }
    }
}