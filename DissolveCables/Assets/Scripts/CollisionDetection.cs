using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameManager _gameManager;
    public int collisionIndex;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        for (int i = 0; i < hitColliders.Length; i++)
        {

            //If Colliders get hit by any piece of cable
            if (hitColliders[i].CompareTag("CablePart"))
            {
                //False value will turn because the piece of cable is hit.
                _gameManager.CheckCollision(collisionIndex, false);
            }
            else
            {

                //If it does not collide, true value will be returned.
                _gameManager.CheckCollision(collisionIndex, true);

            }
        }
    }


    //Editör içerisinde objenin hatlarını görmek için gerekli olan çizim
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale / 2);
    }
}
