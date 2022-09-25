using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //When we select object we need those variables
    private GameObject selectedPlug;
    private GameObject selectedSocket;
    public bool isMoving;

    BottomPlug _bottomPlug;

    [Header("--- Level Settings ---")]
    public GameObject[] collisionControlObjects;
    public GameObject[] plugs;
    public int levelTargetSockets;
    public bool[] collisionDetections;
    int completedSockets;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null)
                {
                    // ## BOTTOM PLUB ##

                    //If there is no selected object and if isMoving false, we can select the plugs
                    if (selectedPlug == null && !isMoving)
                    {
                        //If the raycast hits one of the plugs
                        if (hit.collider.CompareTag("BluePlug") || hit.collider.CompareTag("RedPlug") || hit.collider.CompareTag("YellowPlug"))
                        {
                            //It goes to the bottomplug script of the object it hits and calls the selectedPostion method from there.
                            //This method takes 2 parameters. One is where the clicked object will go automatically, the other is which socket the object is in.

                            /* Not Refactored
                            hit.collider.GetComponent<BottomPlug>().SelectedPosition(hit.collider.GetComponent<BottomPlug>().currentSocket.GetComponent<Socket>().autoMovePosition, hit.collider.GetComponent<BottomPlug>().currentSocket);
                            */

                            //Refacotred version
                            _bottomPlug = hit.collider.GetComponent<BottomPlug>();
                            _bottomPlug.MoveStart("SelectedPosition", _bottomPlug.currentSocket, _bottomPlug.currentSocket.GetComponent<Socket>().autoMovePosition);

                            //We adding to variable our hitted plug and sockets
                            selectedPlug = hit.collider.gameObject;
                            selectedSocket = _bottomPlug.currentSocket;

                            //We selected object so we have moving in the game
                            isMoving = true;
                        }
                    }
                    // ## BOTTOM PLUB ##

                    // ## SOCKET ##

                    if (hit.collider.CompareTag("Socket"))
                    {
                        //If selected plug is not null and our socket need to avaliable or not.
                        //If our raycast hit to another socket, different from our hitted plug's socket.
                        if (selectedPlug != null && !hit.collider.GetComponent<Socket>().isSocketFull && selectedPlug != hit.collider.gameObject)
                        {
                            //When we send the plug to another socket, we empty the isSocketFull status of the selected socket.
                            selectedSocket.GetComponent<Socket>().isSocketFull = false;

                            Socket _socket = hit.collider.GetComponent<Socket>();

                            _bottomPlug.MoveStart("ChangePosition", hit.collider.gameObject, _socket.autoMovePosition);

                            //When our plug insert to new socker, our socket need to be not avaliable
                            _socket.isSocketFull = true;

                            //We cleart variables our hitted plug and sockets
                            selectedPlug = null;
                            selectedSocket = null;

                            //We dont want to move another plug before our step is finish.
                            isMoving = true;
                        }
                        else if (selectedSocket == hit.collider.gameObject)
                        {
                            //Assaign to our hit plug's socket to move back
                            //Hit.collider.gameobject is a transform position for our plug
                            _bottomPlug.MoveStart("BackToSocket", hit.collider.gameObject);

                            //We cleart variables our hitted plug and sockets
                            selectedPlug = null;
                            selectedSocket = null;

                            //We dont want to move another plug before our step is finish.
                            isMoving = true;
                        }
                    }

                    // ## SOCKET ##

                }
            }
        }
    }

    public void CheckPlugs()
    {
        foreach (var item in plugs)
        {
            //If plugs name same with colors in PlugsARRAY we increase the compeleted sockets untill finish
            if (item.GetComponent<BottomPlug>().currentSocket.name == item.GetComponent<BottomPlug>().socketColor)
            {
                completedSockets++;
            }
        }

        //When compeletedSockets count equals to level target, this code block will work
        if (completedSockets == levelTargetSockets)
        {
            Debug.Log("All plugs in sockets");

            //When we reach to all plugs we need to control them, they are straight or not
            //We activated our CollisionDetections objects
            foreach (var item in collisionControlObjects)
            {
                item.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Plugs are not compeleted");
            
        }
        //We need to assaign 0 again because when this code block work again it need to start from 0
        completedSockets = 0;
    }

    public void CheckCollision(int collisionIndex, bool collisionDetection)
    {
        collisionDetections[collisionIndex] = collisionDetection;


        //If collisiondDetection objects situations trues
        //If any collisionDetection collide with cable part we change to false it on CollisionDetection.cs
        if (collisionDetections[0] && collisionDetections[1])
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
