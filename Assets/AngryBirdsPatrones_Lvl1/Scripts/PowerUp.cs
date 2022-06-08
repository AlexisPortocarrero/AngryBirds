using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Power { explotar, dividirse };

    public Power poder;
    float expForce = 200;
    float radius = 5;
    bool canexp = true;
    GameObject a;
    GameObject b;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            if (poder == Power.explotar && canexp)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider nearyby in colliders)
                {
                    Rigidbody rigg = nearyby.GetComponent<Rigidbody>();
                    if(rigg != null)
                    {
                        rigg.AddExplosionForce(expForce, transform.position, radius);
                    }
                }

                canexp = false;
            }

            if(poder == Power.dividirse)
            {
                a = Instantiate(gameObject, new Vector2(transform.position.x, transform.position.y +1 ), Quaternion.identity);
                b = Instantiate(gameObject, new Vector2(transform.position.x, transform.position.y-1), Quaternion.identity);
                a.GetComponent<Rigidbody>().AddForce(500, 60, 1);
                b.GetComponent<Rigidbody>().AddForce(500, 60, 1);
                Invoke("Destroi", 2f);
            }
        }
    }

    void Destroi()
    {
        if(a != null)
        {
            Destroy(a);
            Destroy(b);
        }
        return;
    }
   
}
