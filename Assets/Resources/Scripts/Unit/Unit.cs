﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private int unitID = 0;
    public string unitName;
    public Offense fireball1;
    public Offense fireball2;
    public Offense fireball3;

    public Defense shield1;
    public Defense shield2;
    public Defense shield3;

    public Transform fireballLocation;
    public Transform shieldLocation;
    public Direction facing;

    public float maxHp;
    public float maxMp;
    [SerializeField]
    private float _hp;
    [SerializeField]
    private float _mp;
    public float hpRegenRate = 0;
    public float mpRegenRate;

    public float maxSp = 10;
    public float sp = 10;
    [SerializeField]
    private VitalsUI vitalsUI; //Still keep this public for Player, but not for Enemy

    public bool usingShield; //not sure if necessary. Currently is being used in code, but that itself may not be necessary
    public bool canUseShield = true;
    public float shieldGeneralCooldownTime = 0.1f; //Cooldown time when you run out of MP or when you let go of one shield 
    public float shieldOverpoweredCooldownTime = 0.1f; //cooldown time when an offense is higher than your shield
    public float shieldBreakCooldownTime = 0.2f; //cooldown time for when you can use shield again when shield is broken
    public Defense currentShield;
    public Defense lastShield;
    public TurnType currentTurnType = TurnType.Attack;



    private Offense collidedFireball;
    private bool _spawning = false;
    [SerializeField]
    private bool _preventFireballs = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Entrance _entrance;
    public Entrance entrance{
        get{
            return _entrance;
        }
    }
    public float mp
    {
        get
        {
            return _mp;
        }
        //set{
        //    _spawning = value; 
        //}

    }
    public float hp
    {
        get
        {
            return _hp;
        }
        //set{
        //    _spawning = value; 
        //}

    }

    public bool spawning{
        get{ 
            return _spawning; 
        }
        //set{
        //    _spawning = value; 
        //}

    }
    public bool preventFireballs
    {
        get
        {
            return _preventFireballs;
        }
        set
        {
            _preventFireballs = value;
        }

    }

    public float currentDrainRate = 0.5f;
	// Use this for initialization
	void Start () {
//        print("Happens 1");
        InitializeUnit();
	}
	
    //This sets the direction the fireball will move as well as its ID
    public void InitFireballProperties(Offense fireball){
        fireball.InitProperties(facing,unitID);
        //fireball.GetComponent<Mover>().direction = facing;
        //fireball.GetComponent<Mover>().InitDirection();
        //fireball.unitID = unitID;
    }

    public void InitShieldProperties(Defense shield)
    {
        if(!GameplayController.instance.useObjectPooling){
            shield.transform.SetParent(this.transform);
        }
        shield.GetComponent<Mover>().direction = facing;
        shield.GetComponent<Mover>().InitDirection();
        shield.Initialize(this);
    }

    public void shootFireball(int number){
//        print("Got here at least10 " +preventFireballs);

        if(preventFireballs){
            return;
        }
//        print("Got here at least20");
        Offense fireball = null;
        string poolName = null;
        switch(number){
            case 1:
                poolName = PoolNames.pFireball1;
                fireball = fireball1;
                break;
            case 2:
                poolName = PoolNames.pFireball2;
                fireball = fireball2;
                break;
            case 3:
                poolName = PoolNames.pFireball3;
                fireball = fireball3;
                break;
            default:
                Debug.LogError("Invalid number for fireball. Must be an integer between 1 and 3.");
                break;
        }
        if(fireball!=null && fireball.mpCost <= mp){
            animator.SetTrigger(ParameterNames.Fire);
            GameObject obj = null;
            if(GameplayController.instance.useObjectPooling){
                
                obj = ObjectPooler.instance.SpawnFromPool(poolName, fireballLocation.position, fireballLocation.rotation);
            }
            else{
                obj = (GameObject)Instantiate(fireball.gameObject, fireballLocation.position, fireballLocation.rotation);
            }

            //obj.GetComponent<Offense>().unitID = unitID;
            InitFireballProperties(obj.GetComponent<Offense>());
            addMP(-fireball.mpCost);
        }
    }

    //Put a negative value to remove
    public void addMP(float val){
        _mp = Mathf.Clamp(mp+val,0,maxMp);
//        print(mp);
        if(vitalsUI != null){
            vitalsUI.UpdateVitals(VitalName.MP,mp,maxMp);
        }
        else{
            Debug.LogWarning("There is no UI element for this Unit and its vital update. Please check if this was intentional");
        }
        if(mp <= 0.1 & usingShield){
            stopShield();
        }
    }

    //Put a negative value to remove
    public void addHP(float val)
    {
        if(val < 0){
            animator.SetTrigger(ParameterNames.Hurt);
        }
        _hp = Mathf.Clamp(_hp + val, 0, maxHp);
        if(vitalsUI!=null){
            vitalsUI.UpdateVitals(VitalName.HP, _hp, maxHp);
            if(_hp <=0){
                Die();
            }
        }
        else{
            Debug.Log("There is no UI element for this Unit and its vital update. Please check if this was intentional");
        }
    }

    //This is called once button is pressed
    public void startShield(int number){
        print("shielding"+canUseShield);
        if(canUseShield){
            if(currentTurnType == TurnType.Defense){ //Just as a precaution. This check is not required to have
                //If a shield already exists, destroy that one, and then create a new one
                if(usingShield){
                    if(currentShield.numberValue == number){
                        return;
                    }
                    else{
                        stopShield();
                    }
                }
                Defense shield = null;
                string poolName = null;//used for object pooling
                switch (number)
                {
                    case 1:
                        poolName = PoolNames.pShield1;
                        shield = shield1;
                        break;
                    case 2:
                        poolName = PoolNames.pShield2;
                        shield = shield2;
                        break;
                    case 3:
                        poolName = PoolNames.pShield3;
                        shield = shield3;
                        break;
                    default:
                        Debug.LogError("Invalid number for shield. Must be an integer between 1 and 3.");
                        break;
                }


                if (shield != null && shield.mpCost <= _mp)
                {
                    usingShield = true;
                    animator.SetBool(ParameterNames.Defend, true);
                    if(GameplayController.instance.useObjectPooling){
                        lastShield = currentShield = (ObjectPooler.instance.SpawnFromPool(poolName, shieldLocation.position, shieldLocation.rotation)).GetComponent<Defense>();
                    }
                    else{
                        lastShield = currentShield = (Instantiate(shield.gameObject, shieldLocation.position, shieldLocation.rotation)).GetComponent<Defense>();
                    }
                    InitShieldProperties(currentShield);
                    addMP(-shield.mpCost);
                    // Substracting MP here isn't enough. After creating the shield object,
                    //it  will deal with constantly draining your MP. The shield will also call stopShield
                    //once you run out MP
                }
            }
        }
    }
    //General code used in all methods that destroy shield
    public void removeShield()
    {
        if(animator){
            animator.SetBool(ParameterNames.Defend, false);
        }
        usingShield = false;
        if(currentShield){
            if(GameplayController.instance.useObjectPooling){
                currentShield.destroySelf();
            }
            else{
                currentShield.destroySelf();
            }

        }
        currentShield = null;
    }
    //This is called once button is released or if you run out of MP, or when your defense turn ends, since
    //the player may theoretically still be holding the button down even after turn ends.
    public void stopShield()
    {
        //canUseShield = false; //can't do this here, because this would mess up the flow of switching shields
        print("stopped shield "+canUseShield);
        removeShield();
        //Shield fading animation or something.
//        Invoke("makeShieldUsable", shieldGeneralCooldownTime);
    }

    public void shieldOverpowered()
    {
        canUseShield = false;
        removeShield();
        //Instantiate shield overpowered particle effect.
        Invoke("makeShieldUsable",shieldOverpoweredCooldownTime);
    }
    public void shieldBroken()
    {
        canUseShield = false;
        removeShield();
        //Instantiate shield break particle effect which should be more dramatic than overpowered.
        Invoke("makeShieldUsable", shieldBreakCooldownTime);
    }
    public void makeShieldUsable()
    {
        canUseShield = true;
    }

    public int GetUnitID(){
        return unitID;
    }

    public void Die(){
        //TODO: Need to do more. Have to check if this is main player, probably with checking player
        //tag is easiest way
        removeShield();
        //TODO probably isn't good hard code constants.
        float waitTime = 0.2f;
        if(lastShield){
            lastShield.destroySelf();
        }
        if (GameplayController.instance.entranceOn)
        {
            this.vitalsUI.delayedHide(0.5f);
        }
        else{
            this.vitalsUI.delayedHide(0.1f);
        }
        Destroy(this.gameObject, waitTime);

        //Even though the above happens .2 seconds later. spawnNextEnemy won't happen instantly either
        //because levelController itself has a wait time variable that it will use to wait before
        //actualy spawning.
        if(!(gameObject.CompareTag(TagNames.Player))){
            GameplayController.instance.player.preventFireballs = true;
            GameplayController.instance.SpawnNextEnemy();
        }
        else{
            GameplayController.instance.Invoke("GameOver",1f);
        }

    }


	// Update is called once per frame
	void Update () {
        //Don't need to update vitals here, because addMP already takes care of that       
        addMP(mpRegenRate*Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagNames.Fireball)){
            collidedFireball = collision.GetComponent<Offense>();
            if(collidedFireball 
               && collidedFireball.unitID != unitID
              ){
                addHP(-collidedFireball.damage);
                collidedFireball.Explode(true);
            }
        }
    }

    public void LoadEnemyData(EnemyData enemyData){
        animator.runtimeAnimatorController = enemyData.animatorController;
        spriteRenderer.sprite = enemyData.sprite;
        spriteRenderer.color = enemyData.color;
        spriteRenderer.flipX = true;
        AIController ai = GetComponent<AIController>();
        this.maxHp = enemyData.MaxHP;
        this.maxMp = enemyData.MaxMP;
        this.maxSp = enemyData.MaxSP;
        this._hp = maxHp;
        this._mp = maxMp;
        this.mpRegenRate = enemyData.MPRegenRate;
        this.hpRegenRate = enemyData.HPRegenRate;
        ai.attackThinker = enemyData.AttackThinker;
        ai.defenseThinker = enemyData.DefenseThinker;
        this.shieldBreakCooldownTime = enemyData.ShieldBreakCooldown;
        this.shieldGeneralCooldownTime = enemyData.ShieldGeneralCooldown;
        this.shieldOverpoweredCooldownTime = enemyData.ShieldOverpoweredCooldown;
        vitalsUI.InitializeVitals(maxHp, maxMp);

    }
    public void InitializeUnit(){
        if (GameplayController.instance.entranceOn && GameplayController.instance.lightPause)
        {

            _entrance = GetComponent<Entrance>();
            if (!_entrance)
            {
                Debug.LogError("You need an entrance script on this unit " + gameObject);
            }
            else
            {
                    currentTurnType = TurnType.Standby;
                    _entrance.BeginEntrance();
            }
        }
        else{
            if (GameplayController.instance.entranceOn && GameplayController.instance.player.currentTurnType == TurnType.Attack)
            {
                GameplayController.instance.player.preventFireballs = true;
            }
        }
        unitID = GameplayController.instance.GetNextUnitID();
        if (gameObject.CompareTag(TagNames.Player))
        {
            vitalsUI = GameplayController.instance.playerVitalsUI;
        }
        else
        {
            vitalsUI = GameplayController.instance.enemyVitalsUI;
        }
        animator = Utility.GetChildByTag(transform, TagNames.UnitAnimator).GetComponent<Animator>();
        spriteRenderer = Utility.GetChildByTag(transform, TagNames.UnitAnimator).GetComponent<SpriteRenderer>();
        if (!animator)
        {
            Debug.LogError("You need an animator on this unit");
        }
        _hp = maxHp;
        _mp = maxMp;
        vitalsUI.InitializeVitals(maxHp, maxMp);
    }
    public void showVitals(){
        vitalsUI.show();
    }
}
