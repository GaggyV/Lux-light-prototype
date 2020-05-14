using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public class Controller : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField]
        float forceMultiplier;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            Vector2 forceVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.Translate(forceVector*Time.deltaTime*forceMultiplier);
        }

        private void FixedUpdate()
        {
            Vector2 forceVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Debug.Log("yoo");
            rb.AddForce(forceVector * forceMultiplier);
        }
    }
}