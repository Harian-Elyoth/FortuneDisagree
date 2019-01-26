using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvements : MonoBehaviour
{
	public Rigidbody2D rb_Joueur;
	
	public float vitesse = 0.0f;
	public float vitesse_min = 1.0f;
	public float vitesse_max = 8.0f;
	public float vitesse_glisse = 5.0f;
	
	public float augmentation_vitesse = 0.05f;
	public float diminution_glissade = 0.05f;
	
	private bool cours_gauche = false;
	private bool cours_droite = false;
	public bool glisse = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if ( (Input.GetKey ("q") || Input.GetKey ("d")) && (glisse == false) )
		{
			if (Input.GetKey ("q"))
			{
				if (cours_gauche)
				{
					if ( vitesse < vitesse_max)
					{
						vitesse = vitesse + augmentation_vitesse;
					}
				}
				else
				{
					if ( vitesse < vitesse_glisse)
					{
						vitesse = vitesse_min;
					}
					else
					{
						glisse = true;
					}
				}
				
				if (glisse == false)
				{
					rb_Joueur.velocity = new Vector2(-vitesse, rb_Joueur.velocity.y);
			
					cours_gauche = true;
					cours_droite = false;
				}
			}
			
			if (Input.GetKey ("d"))
			{
				if (cours_droite)
				{
					if ( vitesse < vitesse_max)
					{
						vitesse = vitesse + augmentation_vitesse;
					}
				}
				else
				{
					if ( vitesse < vitesse_glisse)
					{
						vitesse = vitesse_min;
					}
					else
					{
						glisse = true;
					}
				}
				
				if (glisse == false)
				{
					rb_Joueur.velocity = new Vector2( vitesse, rb_Joueur.velocity.y);
				
					cours_gauche = false;
					cours_droite = true;
				}
			}
			
			if (Input.GetKey ("q") && Input.GetKey ("d") && vitesse < vitesse_glisse)
			{
				vitesse = vitesse_min;
				rb_Joueur.velocity = new Vector2 (0.0f, rb_Joueur.velocity.y);
			}
		}
		else
		{
			if (vitesse > vitesse_glisse)
			{
				glisse = true;
			}
			
			if (vitesse < vitesse_max && glisse == false)
			{
				rb_Joueur.velocity = new Vector2(0, rb_Joueur.velocity.y);
				vitesse = vitesse_min;
			}
			else
			{
				if ( cours_gauche )
				{
					vitesse = vitesse - diminution_glissade;
					rb_Joueur.velocity = new Vector2( -vitesse, rb_Joueur.velocity.y);
					
					if (vitesse <= vitesse_min)
					{
						cours_gauche = false;
						glisse = false;
					}
					if (Input.GetKey ("q"))
					{
						glisse = false;
					}
				}
				if ( cours_droite )
				{
					vitesse = vitesse - diminution_glissade;
					rb_Joueur.velocity = new Vector2( vitesse, rb_Joueur.velocity.y);
					
					if (vitesse <= vitesse_min)
					{
						cours_droite = false;
						glisse = false;
					}
					if (Input.GetKey ("d"))
					{
						glisse = false;
					}
				}
			}
		}
		
    }
}
