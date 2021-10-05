using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float SpeedB;
    [SerializeField] ParticleSystem effect;

   
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * SpeedB);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
