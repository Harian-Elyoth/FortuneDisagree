using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_De_Grimpage : MonoBehaviour
{
	public bool joueur = false;
	public bool plateforme = false;
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            joueur = true;
        }
		if(other.tag == "sol")
        {
            plateforme = true;
        }
    }
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
        {
            joueur = false;
        }
		if(other.tag == "sol")
        {
            plateforme = false;
        }
	}
}
