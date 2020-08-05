using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {


    public GameObject SquareModel;
   
    public GameObject[,] SquaresInGame=new GameObject [24,24];
    public Vector3 Position;
    public Vector3 AppliedPosition;
    public Vector3 SpacesZ=new Vector3(0,0,20);
    public Vector3 SpacesY = new Vector3(0, 20, 0);
    public Vector3 CameraOffSet = new Vector3(20, 0, 0);
    public int Dimension=6;
    private bool DimensionSpecified = false;
   
    public Text DimensionText;
    public Text LostWin;
    public GameObject[] Buttons;
    public GameObject StartButton;
    public GameObject RestartButton;
    public Vector2[] IndexesOfNonMinedSquares=new Vector2[576];
    public int NumberOfCurrentUpdateDisabledSquares;
    
    public Camera cam;
    public int[] MaxNumberOfMines;
    public int NumberOfMinesCurrentlyUsed;
   
    public int TheRandomNumber;
    public int TheRandomNumber2;
    public int t = 0;
    // Use this for initialization
    void Start () {

        

    }

    // Update is called once per frame
    void Update () {
        
        
            
        
        
            DimensionText.text = "Dimension : " + Dimension+" x "+ Dimension;
            

      

    }

    public void SpawnSquares()
    {



        for (int i = 0; i < Dimension; i++)
        {
            Position = transform.position + SpacesY * i;
            for (int j = 0; j < Dimension; j++)
            {
                AppliedPosition = Position + SpacesZ * j;
                SquaresInGame[i, j] = Instantiate(SquareModel, AppliedPosition, transform.rotation);
                SquaresInGame[i, j].GetComponent<SquareScript>().MyOwnIndex = new Vector2(i, j);
            }

        }
        for (int i = 0; i < MaxNumberOfMines[Dimension]; i++)
        {
            TheRandomNumber = Random.Range(0, Dimension);
            TheRandomNumber2 = Random.Range(0, Dimension);
            while (SquaresInGame[TheRandomNumber, TheRandomNumber2].GetComponent<SquareScript>().AmIaMine == true)
            {
                TheRandomNumber = Random.Range(0, Dimension);
                TheRandomNumber2 = Random.Range(0, Dimension);
            }
            if (SquaresInGame[TheRandomNumber, TheRandomNumber2].GetComponent<SquareScript>().AmIaMine == false)
            {
                SquaresInGame[TheRandomNumber, TheRandomNumber2].GetComponent<SquareScript>().AmIaMine = true;
                
            }
        }
        cam.transform.position = SquaresInGame[Dimension / 2, Dimension / 2].transform.position+CameraOffSet*Dimension-SpacesY/2-SpacesZ/2;
     for(int i = 0; i < Dimension; i++)
        {
            for(int j = 0; j < Dimension; j++)
            {
                if (SquaresInGame[i, j].GetComponent<SquareScript>().AmIaMine == false)
                {
                    IndexesOfNonMinedSquares[t] = new Vector2(i, j);
                    t++;
                }
               
            }
        }
    }

    public void Lost()
    {
        
        LostWin.text = "You Lost";
        DestroyTheNumber3D();
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < Dimension; j++)
            {
                if(SquaresInGame[i, j].GetComponent<SquareScript>().ImDead == false)
                {
                    //  GameObject.Destroy(SquaresInGame[i, j]);
                    SquaresInGame[i, j].GetComponent<Rigidbody>().useGravity = true;
                    SquaresInGame[i, j].GetComponent<Rigidbody>().isKinematic = false;

                   
                }
               
            }

        }
       StartCoroutine( Wait());
       


    }
    public void CheckWinning()
    {
        bool win = true;
        for(int i = 0; i <t; i++)
        {
            if(SquaresInGame[(int) IndexesOfNonMinedSquares[i].x, (int)IndexesOfNonMinedSquares[i].y].GetComponent<SquareScript>().ImDead == false)
            {
                win = false;
                
            }
        }
        if (win == true)
        {
            LostWin.text = "You Win";
            StartCoroutine(Wait());
        }
    }
    public void SetDifficulty(int a)
    {

        if (DimensionSpecified == false)
        {
            if((a>0 && Dimension < 16)||(a < 0 && Dimension > 4))
            {
                Dimension = Dimension + a;
            }
           
            

        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        RestartButton.SetActive(true);
    }
    public void StartGame()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
        StartButton.SetActive(false);
        DimensionSpecified = true;
        SpawnSquares();
        DimensionText.enabled = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void DestroyTheNumber3D()
    {
        GameObject[] instances = GameObject.FindGameObjectsWithTag("Number3D");
        for (int i = 0; i < instances.Length; i++)
        {
            GameObject.Destroy(instances[i]);
        }
       
    }
}
