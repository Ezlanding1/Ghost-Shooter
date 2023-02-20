using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GhostAI : MonoBehaviour
{
    public int health = 100;
    int maxHealth;
    bool hidden;
    float speed;
    [SerializeField] Slider healthBar;
    Vector4 Color;

    [SerializeField] WeightedRandom weightedRandom;
    enum Phase { Phase1 = 1, Phase2 = 2, Phase3 = 3 };
    enum Attack { Possess, Reincarnate }
    enum SubAttack { Shoot, Throw, Reincarnate1, Reincarnate2 }
    Phase phase;
    Attack attack;
    SubAttack subAttack;
    PossessableObject[] possessableObjects;
    bool phase1;
    bool phase2;
    bool phase3;
    
    float nextTimeToShoot;
    [SerializeField] GameObject SpiritShot;
    
    public GameObject PossessedObj;

    public Material CloneMat;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar.value = CalculateHealth();
        StateSwap();
    }

    // Update is called once per frame
    void Update()
    {
        //if (PossessedObj == null)
        //{
        //    StateSwap();
        //}
        if (PossessedObj != null)
        {
            if (Vector3.Distance(PossessedObj.transform.position, transform.position) >= 1.5f)
            {
                hidden = true;
                GetComponent<VisualEffect>().SetVector4("Color", new Vector4(6.89914656f, 9.41345596f, 11.1817608f, 1));
            }
            else
            {
                if (hidden == true)
                {
                    hidden = false;
                    if (phase1) { GetComponent<VisualEffect>().SetVector4("Color", new Vector4(2.10755706f, 7.43499279f, 11.1817608f, 1)); }
                    if (phase2) { GetComponent<VisualEffect>().SetVector4("Color", new Vector4(2.107557f, 3.31745f, 11.18176f, 1)); }
                    if (phase3) { GetComponent<VisualEffect>().SetVector4("Color", new Vector4(11.18176f, 2.48612f, 2.107557f, 1)); }
                }
            }
        }
        healthBar.value = CalculateHealth();

        if (PossessedObj != null)
        {


            if (gameObject.transform.position != PossessedObj.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, PossessedObj.transform.position, speed * Time.deltaTime); // rigidbody.AddForce((target.position - transform.position) * multiplicationFactor); 
            }
            else
            {


                switch (phase)
                {
                    case Phase.Phase1:
                        switch (attack)
                        {
                            case Attack.Possess:
                                switch (subAttack)
                                {
                                    case SubAttack.Shoot:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            GameObject ISpShot = Instantiate(SpiritShot, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                                            ISpShot.transform.LookAt(Camera.main.transform);
                                            ISpShot.GetComponent<Rigidbody>().AddForce(ISpShot.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Throw:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            PossessedObj.transform.LookAt(Camera.main.transform);
                                            PossessedObj.GetComponent<Rigidbody>().useGravity = false;
                                            PossessedObj.GetComponent<Rigidbody>().AddForce(PossessedObj.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                            StartCoroutine(useGrav(PossessedObj));
                                        }
                                        break;
                                }
                                break;
                            case Attack.Reincarnate:
                                switch (subAttack)
                                {
                                    case SubAttack.Reincarnate1:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit();
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Reincarnate2:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit2((int)Random.Range(1,3));
                                            StateSwap();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case Phase.Phase2:
                        switch (attack)
                        {
                            case Attack.Possess:
                                switch (subAttack)
                                {
                                    case SubAttack.Shoot:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            GameObject ISpShot = Instantiate(SpiritShot, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                                            ISpShot.transform.LookAt(Camera.main.transform);
                                            ISpShot.GetComponent<Rigidbody>().AddForce(ISpShot.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Throw:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            PossessedObj.transform.LookAt(Camera.main.transform);
                                            PossessedObj.GetComponent<Rigidbody>().useGravity = false;
                                            PossessedObj.GetComponent<Rigidbody>().AddForce(PossessedObj.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                            StartCoroutine(useGrav(PossessedObj));
                                        }
                                        break;
                                }
                                break;
                            case Attack.Reincarnate:
                                switch (subAttack)
                                {
                                    case SubAttack.Reincarnate1:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit();
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Reincarnate2:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit2((int)Random.Range(1, 3));
                                            StateSwap();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                    case Phase.Phase3:
                        switch (attack)
                        {
                            case Attack.Possess:
                                switch (subAttack)
                                {
                                    case SubAttack.Shoot:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            GameObject ISpShot = Instantiate(SpiritShot, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                                            ISpShot.transform.LookAt(Camera.main.transform);
                                            ISpShot.GetComponent<Rigidbody>().AddForce(ISpShot.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Throw:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            PossessedObj.transform.LookAt(Camera.main.transform);
                                            PossessedObj.GetComponent<Rigidbody>().useGravity = false;
                                            PossessedObj.GetComponent<Rigidbody>().AddForce(PossessedObj.transform.rotation * Vector3.forward * 100);
                                            StateSwap();
                                            StartCoroutine(useGrav(PossessedObj));
                                        }
                                        break;
                                }
                                break;
                            case Attack.Reincarnate:
                                switch (subAttack)
                                {
                                    case SubAttack.Reincarnate1:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit();
                                            StateSwap();
                                        }
                                        break;
                                    case SubAttack.Reincarnate2:
                                        if (Time.time > nextTimeToShoot)
                                        {
                                            nextTimeToShoot = Time.time + 1;
                                            PossessedObj.GetComponent<PossessableObject>().UnHit2((int)Random.Range(1, 3));
                                            StateSwap();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
    void StateSwap()
    {
        if (health <= 100 && health >= 50) { speed = 1.5f; phase1 = true;  phase = Phase.Phase1; }
        if (health < 50 && health >= 25) { speed = 3; phase2 = true; phase = Phase.Phase2; }
        if (health < 25 && health > 0) { speed = 4.5f; phase3 = true; phase = Phase.Phase3; }
        if (health <= 0) { print("death"); }
        if (phase1 == true && phase2 == true)
        {
            PhaseSwitch(1);
            phase1 = false;
        }
        if (phase2 == true && phase3 == true)
        {
            PhaseSwitch(2);
            phase2 = false;
        }
        possessableObjects = FindObjectsOfType<PossessableObject>();
        int nc = 0;
        int c = 0;
        foreach (var item in possessableObjects)
        {
            if (item.cracked == false) { nc++; } else { c++; }
        }
        if (c >= nc)
        {
            if (nc != 0)
            {
                attack = (Attack)Random.Range(0, Attack.GetNames(typeof(Attack)).Length);
            }
        }
        else
        {
            attack = Attack.Possess;
        }
        if (c >= nc && nc == 0)
        {
            attack = Attack.Reincarnate;
        }
        switch (attack)
        {
            case Attack.Possess:
                if (PossessedObj != null) { PossessedObj.GetComponent<PossessableObject>().chosen = false; }
                PossessedObj = weightedRandom.RandomWeighted(false);
                PossessedObj.GetComponent<PossessableObject>().chosen = true;
                subAttack = (SubAttack)Random.Range(0, 2);
                break;
            case Attack.Reincarnate:
                if (PossessedObj != null) { PossessedObj.GetComponent<PossessableObject>().chosen = false; }
                PossessedObj = weightedRandom.RandomWeighted(true);
                PossessedObj.GetComponent<PossessableObject>().chosen = true;
                subAttack = (SubAttack)Random.Range(2, 4);
                break;
            default:
                break;
        }
        print($"Possessed Object: {PossessedObj.GetComponent<PossessableObject>().choiceName} ");
        print($"Attack Path: {phase} => {attack} => {subAttack} ");
    }
    public void Hit(float damage, GameObject GunTip)
    {
        if (!hidden)
        {
            health -= (int)damage;
            transform.position += GunTip.transform.forward;
        }
        
    }
    float CalculateHealth()
    {
        if (health < maxHealth) { healthBar.gameObject.SetActive(true); } else { healthBar.gameObject.SetActive(false); }
        return (float)health / (float)maxHealth;
    }
    IEnumerator useGrav(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        if (obj != PossessedObj)
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    void PhaseSwitch(int phase)
    {
        switch (phase)
        {
            case 1:
                GetComponent<VisualEffect>().SetVector4("Color", new Vector4(2.107557f, 3.31745f, 11.18176f, 1));
                break;
            case 2:
                GetComponent<VisualEffect>().SetVector4("Color", new Vector4(11.18176f, 2.48612f, 2.107557f, 1));
                break;
        }
    }
}
