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
            
            //Colliderlara herhangi bir kablo parçası çarparsa
            if (hitColliders[i].CompareTag("CablePart"))
            {
                //Kablo parçası çarptığı için false değer gelecek
                _gameManager.CheckCollision(collisionIndex, false);
            }
            else
            {
                
                //Çarpmıyor ise true değer gelecek
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
