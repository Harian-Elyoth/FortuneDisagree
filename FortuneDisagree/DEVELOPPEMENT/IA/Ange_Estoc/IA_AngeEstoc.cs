using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_AngeEstoc : MonoBehaviour
{
	public GameObject zoneNormale;
	public GameObject zoneCharge;
	
	private Transform cible;	
	
	private float minuteur = 0.0f;
	public float chargement = 5.0f;
	public bool peut_charge = false;
	
	private float minuteur_va_charge = 0.0f;
	public float chargement_va_charge = 1.0f;
	public bool va_charge = false;
	
	private float minuteur_charge = 0.0f;
	public float chargement_charge = 3.0f;
	public bool charge = false;
	
	private float V;
	public float vitesse = 5.0f;
	public float vitesse_va_charge = 1.0f;
	public float vitesse_charge = 25.0f;
	
	public Vector2 posInitiale;
	public Vector2 posCharge;
	
    void Start()
    {
		posInitiale = transform.position;
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if ( peut_charge == false )
		{
			minuteur += Time.deltaTime;
			
			if (minuteur > chargement)
			{
				minuteur = 0.0f;
				peut_charge = true;
			}
		}
		
		if (va_charge == false)
		{
			if ( zoneNormale.GetComponent<Zone_Estoc>().active == false && zoneCharge.GetComponent<Zone_Estoc>().active == false)
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2 (posInitiale.x, transform.position.y), vitesse * Time.deltaTime);
			}
			
			if ( zoneNormale.GetComponent<Zone_Estoc>().active && zoneCharge.GetComponent<Zone_Estoc>().active)
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2 (cible.position.x, transform.position.y), vitesse * Time.deltaTime);
			}
			
			if ( zoneNormale.GetComponent<Zone_Estoc>().active == false && zoneCharge.GetComponent<Zone_Estoc>().active)
			{	
				va_charge = true;
			}
		}
		
		if (va_charge)
		{
			if ( zoneNormale.GetComponent<Zone_Estoc>().active )
			{
				va_charge = false;
				minuteur_va_charge = 0.0f;
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, new Vector2 (cible.position.x, transform.position.y), vitesse_va_charge * Time.deltaTime);
				minuteur_va_charge += Time.deltaTime;
				if (minuteur_va_charge > chargement_va_charge)
				{
					va_charge = false;
					charge = true;
					minuteur_va_charge = 0.0f;
					posCharge = cible.position;
				}
			}
		}
		
		if (charge)
		{
			transform.position = Vector2.MoveTowards(transform.position, new Vector2 (posCharge.x, transform.position.y), vitesse_charge * Time.deltaTime);
			minuteur_charge+=Time.deltaTime;
			if (minuteur_charge > chargement_charge)
			{
				minuteur_charge = 0;
				charge = false;
				peut_charge = false;
			}
		}
    }
}
