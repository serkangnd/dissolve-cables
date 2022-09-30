using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCablePart : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Socket"))
        {
            _gameManager.LoseCondition();
        }
    }
}
