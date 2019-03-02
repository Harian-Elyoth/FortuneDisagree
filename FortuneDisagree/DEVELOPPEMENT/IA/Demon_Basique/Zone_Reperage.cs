using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_Reperage : MonoBehaviour
{
	public bool active = false;
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            active = true;
        }
    }
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
        {
            active = false;
        }
	}
}
