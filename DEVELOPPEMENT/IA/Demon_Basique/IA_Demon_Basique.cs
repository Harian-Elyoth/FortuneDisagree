using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Demon_Basique : MonoBehaviour
{
	public GameObject projectile;
	private Transform cible;
	public Transform point_de_lancement_gauche;
	public Transform point_de_lancement_droite;	
	
	private bool recharge = false;
	public float temps_rechargement = 2.0f;
	private float minuteur = 0.0f;
	
	
	public float vitesse;
	public float distance_reperage;
	public float distance_tire = 50.0f;
	
	public bool en_mouvement = false;
	public bool inatteignable = false;
	private float tmps_inatteignable = 0.0f;
	
	private Vector2 posInitiale;
	
	
    // Start is called before the first frame update
    void Start()
    {
		posInitiale = transform.position;
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if ( Mathf.Abs(cible.position.y - transform.position.y)>3.0f )
		{
			tmps_inatteignable += Time.deltaTime;
			if (tmps_inatteignable> 2.0f)
			{
				inatteignable = true;
			}
		}
		else
		{
			inatteignable = false;
			tmps_inatteignable=0;
		}

		if (Vector2.Distance(transform.position, cible.position) < distance_reperage && inatteignable == false)
		{
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(cible.position.x, transform.position.y), vitesse * Time.deltaTime);
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, posInitiale, vitesse * Time.deltaTime);
		}
		
		if ( transform.position.x < posInitiale.x+0.5 && transform.position.x > posInitiale.x-0.5)
		{
			en_mouvement = false;
		}
		else
		{
			en_mouvement = true;
		}

		if ( Vector2.Distance(transform.position, cible.position) < distance_tire && en_mouvement == false)
		{
			if (recharge == true)
			{
				minuteur += Time.deltaTime;
				if (minuteur > temps_rechargement)
				{
					recharge = false;
				}
			}
			else
			{
				GameObject tire;
				recharge = true;
				minuteur = 0;
				if (transform.position.x-cible.position.x >0)
				{
					tire = Instantiate(projectile, point_de_lancement_gauche.position, Quaternion.identity);
				}
				else
				{
					tire = Instantiate(projectile, point_de_lancement_droite.position, Quaternion.identity);
				}
			}
		}
		
    }
}
