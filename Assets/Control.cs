using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour {
    RaycastHit   rayinfo;
    public Vector3 Direction = new Vector3(1, 0, 0);
    public Camera cam;
    public Main Main;
    public int NeighborMines = 0;
    public Vector2 TheTargetIndex;
    public GameObject Numbers3D;
    public bool BlockInput = false;
	void Start () {
		
	}
	
	
	void Update () {
       if(BlockInput== false)
        {
            Ray rays = cam.ScreenPointToRay(/*Input.GetTouch(0).position*/Input.mousePosition);
            Physics.Raycast(rays, out rayinfo);
            if (rayinfo.collider != null)
            {
                if (rayinfo.collider.tag == "Square" && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    rayinfo.collider.GetComponent<SquareScript>().DisableMesh();
                    rayinfo.collider.GetComponent<SquareScript>().ImDead = true;
                    TheTargetIndex = rayinfo.collider.GetComponent<SquareScript>().MyOwnIndex;



                    if (rayinfo.collider.GetComponent<SquareScript>().AmIaMine == true)
                    {
                        //Call Animations HERE
                        Main.Lost();
                        BlockInput = true;
                    }
                    else
                    {
                        DoTheNumbers();
                        Main.CheckWinning();
                    }
                }
            }
        }
            
        }
    public void DoTheNumbers()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (((i + (int)TheTargetIndex.x) >= 0) && ((i + (int)TheTargetIndex.x) < Main.Dimension))
                {
                    if (((j + (int)TheTargetIndex.y) >= 0) && ((j + (int)TheTargetIndex.y) < Main.Dimension))
                    {
                        if (Main.SquaresInGame[i + (int)TheTargetIndex.x, j + (int)TheTargetIndex.y].GetComponent<SquareScript>().AmIaMine == true)
                        {
                            NeighborMines += 1;
                           
                        }
                    }
                }



            }

        }
        
        GameObject instance = Instantiate(Numbers3D, rayinfo.collider.GetComponent<Transform>().position, Quaternion.Euler(0, -90, 0));
        instance.GetComponent<TextMesh>().text = "" + NeighborMines;
        NeighborMines = 0;
    }
       
        
	public void Exit()
    {
        
       Application.Quit();
            
    }
   
	
}
