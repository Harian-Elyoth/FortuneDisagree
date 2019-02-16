using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_AngeEstoc : MonoBehaviour
{
	private Transform cible;	
	
	public bool peut_charge = true;
	public bool prepare_charge = false;
	public bool charge = false;
	public bool a_charge = false;
	
	public float temps_prepare_charge = 1.5f;
	private float minuteur_prepare_charge = 0.0f;
	
	public float temps_charge = 1.0f;
	private float minuteur_charge = 0.0f;
	
	public float temps_rechargement = 5.0f;
	private float minuteur_rechargement = 0.0f;
	
	public float hauteur_detection_vertical = 5.0f;
	
	
	public float vitesse;
	public float vitesse_precharge;
	public float vitesse_charge;
	
	public float distance_reperage;
	public float distance_mini_charge;
	
	private Vector2 posInitiale;
	private float posCharge;
	
	private float V;
	
    void Start()
    {
		V = vitesse;
		posInitiale = transform.position;
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		V = vitesse;
		
		if (peut_charge && transform.position.y - cible.position.y < hauteur_detection_vertical && ( (transform.position.x-cible.position.x >= distance_mini_charge  && transform.position.x-cible.position.x <= distance_reperage ) || (transform.position.x-cible.position.x <= -distance_mini_charge  && transform.position.x-cible.position.x >= -distance_reperage )))
		{
			prepare_charge = true;
		}
		else
		{
			if (transform.position.y - cible.position.y < hauteur_detection_vertical && (transform.position.x-cible.position.x <= distance_reperage || transform.position.x-cible.position.x >= -distance_reperage))
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(cible.position.x, transform.position.y), V * Time.deltaTime);
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, posInitiale, V * Time.deltaTime);
			}
		}
		
		if (prepare_charge)
		{
			peut_charge = false;
			
			minuteur_prepare_charge += Time.deltaTime;
			V = vitesse_precharge;
			
			if (minuteur_prepare_charge >= temps_prepare_charge)
			{
				prepare_charge = false;
				charge = true;
				
				minuteur_prepare_charge = 0;
				
				posCharge = cible.position.x;
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(cible.position.x, transform.position.y), V * Time.deltaTime);
			}
		}
		
		if (charge)
		{
			minuteur_charge += Time.deltaTime;
			V = vitesse_charge;
			
			if (minuteur_charge >= temps_charge)
			{
				charge = false;
				a_charge = true;
				
				minuteur_charge = 0;
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(posCharge, transform.position.y), V * Time.deltaTime);
			}
		}
		
		if (a_charge)
		{
			minuteur_rechargement += Time.deltaTime;
			V = vitesse;
			
			if (minuteur_rechargement >= temps_charge)
			{
				a_charge = false;
				peut_charge = true;
				
				minuteur_rechargement = 0;
			}
		}
		
		
    }
}
