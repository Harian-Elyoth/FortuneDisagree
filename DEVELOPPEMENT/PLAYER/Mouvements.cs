using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvements : MonoBehaviour
{
	public Rigidbody2D rb_Joueur;
	
	//Variables pour les déplacements horizontaux
	private float vitesse = 0.0f;
	private float vitesse_min = 1.0f;
	private float vitesse_max = 30f;
	
	private float augmentation_vitesse = 1.5f;
	
	private bool cours_gauche = false;
	private bool cours_droite = false;
	
	//Variables pour les déplacements verticaux
	//REQUIERT UNE GRAVITE = 10
	private float force_saut = 50.0f;	
	private bool est_au_sol = false;
	private float multiplicateur_second_saut = 1.5f;
	private int nb_sauts = 0;
	
	private float minuteur = 0.0f;
	private float temps_entre_deux_sauts = 1.5f;
	private float deltaTransformPosY = 2.95f;
	private float deltaPhysRayLastArg = 0.1f;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Code pour les mouvements horizontaux
		if ( (Input.GetKey ("q") || Input.GetKey ("d")) )
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
						vitesse = vitesse_min;
				}
				
				rb_Joueur.velocity = new Vector2(-vitesse, rb_Joueur.velocity.y);
		
				cours_gauche = true;
				cours_droite = false;
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
						vitesse = vitesse_min;
				}
				rb_Joueur.velocity = new Vector2( vitesse, rb_Joueur.velocity.y);
				
				cours_gauche = false;
				cours_droite = true;
			}
			
			if (Input.GetKey ("q") && Input.GetKey ("d"))
			{
				vitesse = vitesse_min;
				rb_Joueur.velocity = new Vector2 (0.0f, rb_Joueur.velocity.y);
			}
		}
		else
		{
			rb_Joueur.velocity = new Vector2(0, rb_Joueur.velocity.y);
			vitesse = vitesse_min;
		}
		
		//Code pour les sauts
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - deltaTransformPosY), -Vector2.up, deltaPhysRayLastArg);
		
		if (hit.collider != null)
		{
			est_au_sol = true;
		}
		else
		{
			est_au_sol = false;
		}
		
		if (est_au_sol)
		{
			nb_sauts = 1;
			if ( Input.GetKeyDown ("z") || Input.GetKeyDown ("space") )
			{
				rb_Joueur.velocity = new Vector2( rb_Joueur.velocity.x, force_saut);
			}
		}
		else
		{
			minuteur += Time.deltaTime;
			if ( ( Input.GetKeyDown ("z") || Input.GetKeyDown ("space") ) && nb_sauts != 0 && minuteur > temps_entre_deux_sauts)
			{
				rb_Joueur.velocity = new Vector2( rb_Joueur.velocity.x, force_saut * multiplicateur_second_saut);
				nb_sauts = 0;
				minuteur = 0.0f;
			}
		}
    }
}
