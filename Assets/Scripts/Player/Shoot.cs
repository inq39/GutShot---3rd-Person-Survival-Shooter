using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bloodSplatter;

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;
            

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);
                Health health = hitInfo.collider.GetComponent<Health>();

                if (health != null)
                {
                    var startRot = Quaternion.LookRotation(hitInfo.normal);
                    var blood = Instantiate(bloodSplatter, hitInfo.point, startRot);
                    Destroy(blood, 0.2f);
                    health.GetDamage(50);
                }
            }

        }
    }
}
