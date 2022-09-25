using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //When we select object we need those variables
    private GameObject selectedPlug;
    private GameObject selectedSocket;
    public bool isMoving;

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
                            BottomPlug _bottomPlug = hit.collider.GetComponent<BottomPlug>();
                            _bottomPlug.SelectedPosition(_bottomPlug.currentSocket.GetComponent<Socket>().autoMovePosition, _bottomPlug.currentSocket);

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
                            selectedPlug.GetComponent<BottomPlug>().ChangePosition(_socket.autoMovePosition, hit.collider.gameObject);

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
                            selectedPlug.GetComponent<BottomPlug>().BackToSocket(hit.collider.gameObject);

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
}
