using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Rigidbody2D spikeRigidbody;
    [SerializeField] float speed;
    private void Start()
    {
        spikeRigidbody= GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        spikeRigidbody.velocity = new Vector2(-speed, 0);
    }
}
