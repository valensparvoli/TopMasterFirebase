﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    SpriteRenderer Sprite;
    public GameObject Bullet;
    public Transform Spawn;
    // Start is called before the first frame update
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
    }

    void Aim()
    {
        Vector3 MousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(MousePos.x - screenPoint.x, MousePos.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        Sprite.flipY = (MousePos.x < screenPoint.x);

    }
    void Shoot ()
    {
    if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, Spawn.position,Spawn.rotation);
        }
    }
}
