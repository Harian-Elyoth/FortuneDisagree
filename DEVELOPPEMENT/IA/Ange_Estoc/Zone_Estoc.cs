using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone_Estoc : MonoBehaviour
{
	private Vector2 posJ;
	
	private Vector2 posM_gauche;
	private Vector2 posM_droite;
	
	public bool active = false;
	private bool joueur_gauche = false;
	public bool mur_gauche = false;
	
	private bool joueur_droite = false;
	public bool mur_droite = false;
	
    void OnTriggerEnter2D(Collider2D other)
    {
		//On verifie si il y a des murs et un joueur dans la zone de detection et si il est à droite ou à gauche pour
		//pouvoir gérer ces murs plus tard
        if(other.tag == "Player")
        {
			if ( transform.position.x < other.transform.position.x)
			{
				joueur_droite = true;
			}
			else
			{
				joueur_gauche = true;
			}
			posJ = new Vector2 (other.transform.position.x, transform.position.y);
        }
		if(other.tag == "mur")
        {
            if ( transform.position.x < other.transform.position.x && mur_droite == false)
			{
				mur_droite = true;
				posM_droite = new Vector2 (other.transform.position.x, transform.position.y);
			}
			if ( transform.position.x > other.transform.position.x && mur_gauche == false)
			{
				mur_gauche = true;
				posM_gauche = new Vector2 (other.transform.position.x, transform.position.y);
			}
        }
    }
	
	void OnTriggerExit2D(Collider2D other)
	{
		//Ici on actualise en cas de sortie de la zone
		if(other.tag == "Player")
        {
            if ( transform.position.x < other.transform.position.x)
			{
				joueur_droite = false;
			}
			else
			{
				joueur_gauche = false;
			}
			posJ = new Vector2 (0,0);
        }
		if(other.tag == "mur")
        {
            if ( transform.position.x < other.transform.position.x && mur_droite == true)
			{
				mur_droite = false;
				posM_droite = new Vector2 (0,0);
			}
			if ( transform.position.x > other.transform.position.x && mur_gauche == true)
			{
				mur_gauche = false;
				posM_gauche = new Vector2 (0,0);
			}
        }
	}
	
	void Update()
	{
		//Ici on fait les calculs et les conditions nécessaires pour bien comprendre si un mur est devant le joueur ou pas
		
		if (joueur_gauche && mur_gauche)
		{
			if ( Vector2.Distance(transform.position, posJ) < Vector2.Distance(transform.position, posM_gauche) )
			{
				active = true;
			}
		}
		
		if (joueur_droite && mur_droite)
		{
			if ( Vector2.Distance(transform.position, posJ) < Vector2.Distance(transform.position, posM_droite) )
			{
				active = true;
			}
		}
		
		if ( joueur_droite && !mur_droite)
		{
			active = true;
		}
		
		if ( joueur_gauche && !mur_gauche)
		{
			active = true;
		}
		
		if ( (joueur_droite || joueur_gauche) && !mur_droite && !mur_gauche)
		{
			active = true;
		}
		
		if (joueur_droite == false && joueur_gauche == false)
		{
			active = false;
		}
	}
	
}
