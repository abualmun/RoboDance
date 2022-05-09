using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftTrail : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocityToDrift;
    [SerializeField] TrailRenderer left, right;
    [SerializeField] GameObject Cinemachine;
    void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
        }
        else
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }
    }

    void Update()
    {
        bool drift = Mathf.Abs(Vector2.Dot(rb.velocity, transform.up)) > velocityToDrift;
        left.emitting = drift;
        right.emitting = drift;
    }
}
