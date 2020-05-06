using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpDeath : ScaredEnemy
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(clara))
        {
            //enemy.gameObject.GetComponent<SpriteRenderer>().sprite = Dead;
            // this.GetComponent<BoxCollider>().enabled = false;
            //Destroy(enemy.GetComponent<Rigidbody2D>());
            // Destroy(this.gameObject);
        }
    }

}
