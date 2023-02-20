using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCol : MonoBehaviour
{
    [SerializeField] GameObject Tint;
    public int health = 100;
    private void Start()
    {
        Tint.SetActive(false);
    }
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(FindObjectOfType<GhostAI>().gameObject);
        }
    }
    public void Damage(int damage)
    {
        health -= damage;
        StartCoroutine(RedTint());
    }
    IEnumerator RedTint()
    {
        Tint.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Tint.SetActive(false);
    }
}
