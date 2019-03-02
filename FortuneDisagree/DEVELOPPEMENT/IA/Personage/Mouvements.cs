using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvements : MonoBehaviour
{
	public Rigidbody2D rb_Joueur;
	
	//Variables pour les déplacements horizontaux
	public float vitesse = 0.0f;
	public float vitesse_min = 1.0f;
	public float vitesse_max = 30f;
	
	public float augmentation_vitesse = 1.5f;
	
	private bool cours_gauche = false;
	private bool cours_droite = false;
	
	//Variables pour les déplacements verticaux
	//REQUIERT UNE GRAVITE = 10
	public float force_saut = 25.0f;	
	public bool est_au_sol = false;
	private float multiplicateur_second_saut = 1.5f;
	private int nb_sauts = 0;
	
	private float minuteur = 0.0f;
	private float temps_entre_deux_sauts = 0.5f;
	private float deltaTransformPosY = 1.30f;
	private float deltaPhysRayLastArg = 0.3f;
	
	//Variables pour les sauts aux murs
	public LayerMask MurLayer;
	
	public bool mur_gauche = false;
	public bool mur_droite = false;
	
	public float vitesse_propulsion_verticale = 15.0f;
	public float vitesse_propulsion_horizontale = 15.0f;
	
	public bool se_propulse = false;
	public bool sest_propulse = false;
	private float minuteur_propulsion = 0.0f;
	private float temps_propulsion = 0.2f;
	
	//Saut plateforme
	public bool traverse = false;
	private float minuteur_plateforme = 0.0f;
	private float duree_desactivation_plateforme = 0.2f;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if ( rb_Joueur.velocity.y > 3.0f )
		{
			Physics2D.IgnoreLayerCollision(8, 10, true);
			traverse = true;
		}
		
		if ( Input.GetKeyDown ("s") && est_au_sol )
		{
			Physics2D.IgnoreLayerCollision(8, 10, true);
			traverse = true;
		}
		
		if (traverse == true)
		{
			minuteur_plateforme += Time.deltaTime;
		}
		
		if (minuteur_plateforme>duree_desactivation_plateforme)
		{
			traverse = false;
			minuteur_plateforme = 0.0f;
			Physics2D.IgnoreLayerCollision(8, 10, false);
		}
		
		if (se_propulse)
		{
			minuteur_propulsion += Time.deltaTime;
			if (minuteur_propulsion > temps_propulsion)
			{
				se_propulse = false;
				sest_propulse = true;
				minuteur_propulsion=0;
			}
		}
		
		if (est_au_sol && (se_propulse || sest_propulse))
		{
			se_propulse = false;
			sest_propulse = false;
			vitesse = vitesse_min;
		}
		
		//Code pour les mouvements horizontaux
		if ( (Input.GetKey ("q") || Input.GetKey ("d")) && se_propulse == false)
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
			if ( se_propulse == false && sest_propulse == false)
			{
				rb_Joueur.velocity = new Vector2(0, rb_Joueur.velocity.y);
				vitesse = vitesse_min;
			}
		}
		
		//Code pour les sauts
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - deltaTransformPosY), -Vector2.up, deltaPhysRayLastArg);
		
		if (hit.collider != null)//Attention à limiter aux sols
		{
			est_au_sol = true;
		}
		else
		{
			est_au_sol = false;
		}
		
		if (est_au_sol)
		{
			if (mur_gauche == false && mur_droite == false)
			{
				nb_sauts = 1;
				if ( Input.GetKeyDown ("z") || Input.GetKeyDown ("space") )
				{
					rb_Joueur.velocity = new Vector2( rb_Joueur.velocity.x, force_saut);
				}
			}
		}
		else
		{
			if (mur_gauche == false && mur_droite == false && se_propulse== false)
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
		
		//Code pour les sauts au murs
		RaycastHit2D hit_gauche = Physics2D.Raycast(new Vector2(transform.position.x - 0.52f, transform.position.y + deltaTransformPosY), -Vector2.up, deltaPhysRayLastArg, MurLayer);
		RaycastHit2D hit_droite = Physics2D.Raycast(new Vector2(transform.position.x + 0.52f, transform.position.y + deltaTransformPosY), -Vector2.up, deltaPhysRayLastArg, MurLayer);
		
		//Attention à limiter aux murs
		if (hit_gauche.collider != null && est_au_sol == false)
		{
			mur_gauche = true;
		}
		else
		{
			mur_gauche = false;
		}
		
		if (hit_droite.collider != null && est_au_sol == false)
		{
			mur_droite = true;
		}
		else
		{
			mur_droite = false;
		}
		
		if (mur_gauche)
		{
			nb_sauts = 0;
			if ( Input.GetKeyDown ("z") || Input.GetKeyDown ("space") )
			{
				rb_Joueur.velocity = new Vector2( vitesse_propulsion_horizontale, vitesse_propulsion_verticale);
				se_propulse = true;
			}
		}
		if (mur_droite)
		{
			nb_sauts = 0;
			if ( Input.GetKeyDown ("z") || Input.GetKeyDown ("space") )
			{
				rb_Joueur.velocity = new Vector2( -vitesse_propulsion_horizontale, vitesse_propulsion_verticale);
				se_propulse = true;
			}
		}
    }
}