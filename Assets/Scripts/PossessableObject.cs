using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessableObject : MonoBehaviour
{
    [SerializeField] GameObject UnCrackedObject;
    [SerializeField] GameObject CrackedObject;
    public string choiceName;
    int height;
    public bool cracked;
    public int weight;
    public bool chosen;
    Vector3 pos;
    IEnumerator Start1() { yield return new WaitForSeconds(0.3f); Hit(1); cracked = true; }
    private void Start()
    {/*StartCoroutine("Start1");*/
        if (cracked == false)
        {
            height = (int)((gameObject.transform.GetComponent<Collider>().bounds.size.y) * 100);
            weight = height;
        }
        else
        {
            weight = 1;
        }
        
        print(weight);
    
    }
    private void Update()
    {
        if (cracked) { if (GetComponent<Rigidbody>().useGravity == false) { GetComponent<Rigidbody>().useGravity = true; } }
        if (cracked && gameObject.transform.GetChild(0).position != gameObject.transform.position){ transform.position = transform.GetChild(0).position; }
        if (chosen == true && weight == height) { weight *= 5; }
        if (chosen == false && weight != height) { weight /= 5; }
        //if (cracked) { if (transform.position != Center()) { pos = transform.position = Center(); } }
    }
    public PossessableObject(string newChoiceName, int newChoiceWeight)
    {
        choiceName = newChoiceName;
        weight = newChoiceWeight;
    }
    public void Hit(float damage)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        GameObject ICracked = Instantiate(CrackedObject, gameObject.transform);
        cracked = true;
        gameObject.layer = 2; 
    }
    public void UnHit()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        GameObject ICracked = Instantiate(UnCrackedObject, gameObject.transform);
        cracked = false;
        gameObject.layer = 6;
    }
    public void UnHit2(int a)
    {
        for (int i = 0; i < a; i++)
        {
            GameObject ICracked = Instantiate(UnCrackedObject);
            if (ICracked.GetComponent<Renderer>()) { ICracked.gameObject.GetComponent<Renderer>().material = FindObjectOfType<GhostAI>().CloneMat; } else {foreach (Transform child in ICracked.transform){child.gameObject.GetComponent<Renderer>().material = FindObjectOfType<GhostAI>().CloneMat;}}
            ICracked.transform.position = gameObject.transform.position;
            ICracked.AddComponent<Rigidbody>(); ICracked.AddComponent<BoxCollider>();
            ICracked.GetComponent<Rigidbody>().useGravity = false;
            ICracked.GetComponent<BoxCollider>().isTrigger = true; ICracked.GetComponent<BoxCollider>().center = GetComponent<BoxCollider>().center; ICracked.GetComponent<BoxCollider>().size = GetComponent<BoxCollider>().size;
            ICracked.transform.LookAt(Camera.main.transform); ICracked.GetComponent<Rigidbody>().AddForce(ICracked.transform.forward * 75);
            ICracked.AddComponent<CloneCollision>(); ICracked.AddComponent<Attack>();
        }
    }
    //Vector3 Center()
    //{
    //    Vector3 pos = new Vector3(0, 0, 0);
    //    foreach (Transform child in transform.GetChild(0).transform)
    //    {
    //        pos += child.position;
    //    }
    //    float x = transform.GetChild(0).transform.childCount;
    //    Vector3 pos2 = new Vector3(pos.x / x, pos.y / x, pos.z / x);
    //    return pos2;
    //}
}
