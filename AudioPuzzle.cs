using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioPuzzle : MonoBehaviour
{

    private int audioSources = 1, validPlayerPositionCount;
    const int MAX_AUDIO_SOURCES = 5;
    GameObject[] boxes = new GameObject[5];
    Rigidbody[] audioBoxRigidBody = new Rigidbody[5];
    bool[] boxesStatus = new bool[5];
    bool goodPlayerPosition = false;
    Vector3[] randomPosition = new Vector3[5];
    GameObject audioBox, Player1;
    Vector3 playerPosition;


    // Use this for initialization
    void Start()
    {


        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube); //Creating the floor
        floor.name = "Floor";                                              //Name it
        floor.transform.localScale = new Vector3(20, 0.1f, 20);            //Change the dimensions (x,y,z)
        floor.transform.position += new Vector3(0, 0, 0);                  //Change the position of the floor
        floor.GetComponent<Renderer>().material.color = Color.green;       // Set floor color to green

        for (audioSources = 1; audioSources <= MAX_AUDIO_SOURCES; audioSources++)      //audioSources is just the 
                                                                                       //counter for cubes
        {
            float randomXPosition = Random.Range(-10, 10), randomZPosition = Random.Range(-10, 10);


            randomPosition[audioSources - 1] = new Vector3(randomXPosition, 1, randomZPosition);  //Setting each random 
                                                                                                  //box to a random place
            audioBox = GameObject.CreatePrimitive(PrimitiveType.Cube);                            //create the cubes
            audioBox.name = "Box " + audioSources;                                                //name them
            audioBox.transform.localScale = new Vector3(1, 1, 1);                                 //set size
            audioBox.transform.position = new Vector3(randomXPosition, 0.5f, randomZPosition);    //position them
            audioBox.GetComponent<Renderer>().material.color = Color.red;                         //set color


            /*Rigidbody rigidBody = audioBox.GetComponent<Rigidbody>();
            audioBoxRigidBody[audioSources].transform.position = new Vector3(randomXPosition, 0.5f, randomZPosition);
            audioBoxRigidBody[audioSources].transform.localScale = new Vector3(2, 2, 2);*/
            boxes[audioSources - 1] = audioBox;                                                   //put the cube into the array

        }

        while (!goodPlayerPosition)                                                               //Spawn player where there is no boxes
        {
            float randomXPosition = Random.Range(-10, 10), randomZPosition = Random.Range(-10, 10);
            playerPosition = new Vector3(randomXPosition, 1, randomZPosition);                    //random player spawn

            goodPlayerPosition = false;

            for (validPlayerPositionCount = 0; validPlayerPositionCount < 6; validPlayerPositionCount++)
            {
                if (playerPosition != randomPosition[validPlayerPositionCount])
                {
                    goodPlayerPosition = true;
                    bool worldPositionStays = false;

                    Player1 = GameObject.CreatePrimitive(PrimitiveType.Capsule);                   //Spawn the actual player!!
                    Player1.transform.position = playerPosition;
                    playerPosition += new Vector3(0, .5f, 0);
                    Player1.name = "Player 1";
                    Player1.transform.localScale = new Vector3(1, 2, 1);
                    Player1.GetComponent<Renderer>().material.color = Color.blue;

                    Camera FPCamera = gameObject.AddComponent<Camera>();
                    FPCamera.transform.position = playerPosition;
                    FPCamera.name = "Player 1 Camera";
                    this.transform.SetParent(Player1.transform,worldPositionStays);




                    break;

                }
            }
        }


        for (int setBoxesFalse = 0; setBoxesFalse < 5; setBoxesFalse++)
            boxesStatus[setBoxesFalse] = false;

    }

    // Update is called once per frame
    void Update()
    {
        float directionX = 0, directionZ=0;

        if (Input.GetKey(KeyCode.UpArrow))
            Player1.transform.position += new Vector3(directionX+0.2f, 0, directionZ);

        if (Input.GetKey(KeyCode.DownArrow))
            Player1.transform.position += new Vector3(directionX-.2f, 0, directionZ);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Player1.transform.Rotate(0, -.1f, 0);
            directionX -= .1f;
            directionZ += .1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Player1.transform.Rotate(0, .1f, 0);
            directionX += .1f;
            directionZ -= .1f;
        }
        //if (Vector3.Distance(audioBox.transform.position, Player1.transform.position) < 1)       //I think this is the command needed to to tell if the player is close to the box
        //{
        //    for(int boxfinder=0;boxfinder<5;boxfinder++)
        //        if(onCollisionEnter)                                                             //

        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {

        //    }
        //}





    }

    private void OnCollisionEnter(Collision collision)
    {
        int i = 0, j=0;

        for (i = 0; i <= boxesStatus.Length; i++)
            ///Change box color
            if (collision.gameObject.tag == "Box " + i)
            {
                j = i;
                boxes[j].GetComponent<Renderer>().material.color = Color.green;
            }


        ///set box to true
        boxesStatus[j]= true;
        ///change the audio playing

    }



}
/* PseudoCode:
//JB - for each box in the array of boxes, check the corresponding (parallel) boxesStatus array

if the player is within a distance of 1 to any box
{
	the player can activate the box(i will do this through colliders)
	 
        if the box is false                   //JB - if(boxesStatus[i]==false)
        {                                             {
		box is red                              
                                                         if(box becomes activated)
                                                           {
                                                             boxesStatus[i]=true;
                                                             boxes[i].GetComponent<Renderer>().material.color = Color.green;
                                                           }
                                                             do other things

        box box plays audio a                 }
		can affect box '1-5'                  else
	}                                             {
                                                         
	else                                              
	{                                                do the other things

        box is green                          }
		plays audio 'b'
		can effect box '1-5'
	}

//JB - for each box in the array of boxes, check the corresponding (parallel) boxesStatus array
if(all boxes are true)

{
    player wins*/