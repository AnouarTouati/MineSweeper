using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour {

    public bool AmIaMine = false;
    public MeshRenderer Material;
    public bool ImDead = false;
    public Vector2 MyOwnIndex;
	void Start () {
        Material = GetComponent<MeshRenderer>();

        if (AmIaMine == true)
        {
          //  SetMaterial();
        }
    }


    void Update () {
		
	}
    public void DisableMesh()
    {
       
        GetComponent<MeshRenderer>().enabled = false;
    }
    public void SetMaterial()
    {
        Material.material.color = Color.blue;
    }
}

