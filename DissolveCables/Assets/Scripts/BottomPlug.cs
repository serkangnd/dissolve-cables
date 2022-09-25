using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlug : MonoBehaviour
{
    //Getting the clicked plug's socket
    public GameObject currentSocket;
    //Game will check if is true not, with socket color - plug color is matched.
    public string socketColor;
    [SerializeField] private GameManager _gameManager;

    //When the player select the plug, isSelected will trigger
    private bool isSelected;
    //If player change plug position, it will trigger
    private bool isPositionChanged;
    //This bool variable need for when the plug go to position it will insert to socket
    private bool isInsertToSocket;

    GameObject movementPosition;
    GameObject targetSocket;


    /* ------------ UNREFACTORED VERSION
    //transform object which is after selection, the object will go to that place
    //Socket is, where the plug taken from
    public void SelectedPosition(GameObject transformObjectPosition, GameObject socket)
    {
        movementPosition = transformObjectPosition;
        isSelected = true;
    }

    public void ChangePosition(GameObject transformObjectPosition, GameObject socket)
    {
        targetSocket = socket;
        movementPosition = transformObjectPosition;
        isPositionChanged = true;
    }

    public void BackToSocket(GameObject socket)
    {
        targetSocket = socket;
        isInsertToSocket = true;
    }
    ------------ UNREFACTORED VERSION
    */

    //REFACTORED VERSION
    public void MoveStart(string process, GameObject socket, GameObject transformObjectPosition = null)
    {
        switch (process)
        {
            //transform object which is after selection, the object will go to that place
            //Socket is, where the plug taken from
            case "SelectedPosition":
                movementPosition = transformObjectPosition;
                isSelected = true;
                break;
            case "ChangePosition":
                targetSocket = socket;
                movementPosition = transformObjectPosition;
                isPositionChanged = true;
                break;
            case "BackToSocket":
                targetSocket = socket;
                isInsertToSocket = true;
                break;
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            //After selection our game object will automatically go to movementPosition
            transform.position = Vector3.Lerp(transform.position, movementPosition.transform.position, .040f);
            //If the distance between our plug and the position we send is less than .10f, the if block here will work.
            if (Vector3.Distance(transform.position,movementPosition.transform.position) < .10)
            {
                isSelected = false;
            }
        }

        if (isPositionChanged)
        {
            //After selection our game object will automatically go to movementPosition
            transform.position = Vector3.Lerp(transform.position, movementPosition.transform.position, .040f);
            //If the distance between our plug and the position we send is less than .10f, the if block here will work.
            if (Vector3.Distance(transform.position, movementPosition.transform.position) < .10)
            {
                isPositionChanged = false;
                isInsertToSocket = true;
            }
        }

        if (isInsertToSocket)
        {
            //After selection our game object will automatically go to movementPosition
            transform.position = Vector3.Lerp(transform.position, targetSocket.transform.position, .040f);
            //If the distance between our plug and the position we send is less than .10f, the if block here will work.
            if (Vector3.Distance(transform.position, targetSocket.transform.position) < .10)
            {
                isInsertToSocket = false;
                _gameManager.isMoving = false;
                //now our current plug's socket need to be changed with selectedSocket
                currentSocket = targetSocket;

                //Checking cable collision function all plug insert situatio
                //It goes to GameManager.cs's method
                _gameManager.CheckPlugs();
            }
        }
    }
}
