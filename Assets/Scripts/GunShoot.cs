using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShoot : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;
    [SerializeField] GameObject GunTip; 

    [SerializeField] private InputActionProperty shootActivate;

    [SerializeField] GameObject Hand;

    private void Start()
    {
        GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0.125f, range));
        HideL();
    }
    void OnEnable() { shootActivate.action.performed += OnShoot;}
    void OnDisable() { shootActivate.action.performed -= OnShoot; }

    void OnShoot(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(GunTip.transform.position, GunTip.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.GetComponent<PossessableObject>())
            {
                hit.transform.gameObject.GetComponent<PossessableObject>().Hit(damage);
            }
            if (hit.transform.gameObject.GetComponent<GhostAI>())
            {
                hit.transform.gameObject.GetComponent<GhostAI>().Hit(damage, GunTip);
            }
        }
    }
    public void HideL()
    {
        
        GetComponent<LineRenderer>().enabled = false;
        if (Hand.GetComponent<XRInteractorLineVisual>().enabled == false)
        {
            Hand.GetComponent<XRInteractorLineVisual>().enabled = true;
        }
    }
    public void ShowL()
    {
        
        GetComponent<LineRenderer>().enabled = true;
        if (Hand.GetComponent<XRInteractorLineVisual>().enabled == true)
        {
            Hand.GetComponent<XRInteractorLineVisual>().enabled = false;
        }
    }
}
