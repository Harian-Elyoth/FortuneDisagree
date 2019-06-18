using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Volant : MonoBehaviour
{
	public GameObject projectile;
	private Transform cible;
	
	public float vitesse;
	
	public float distance_reperage;
	
	public Transform Milieu1;
	public Transform Milieu2;
	public bool Va_vers_1 = true;
	public bool Va_vers_2 = false;
	
	public Transform point_de_lancement;
	
	public float delai = 10.0f;
	private float minuteur = 0.0f;
	private bool recharge = false;
	
	public bool plonge = false;
	
    // Start is called before the first frame update
    void Start()
    {
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (plonge == false)
		{
			if (recharge)
			{
				minuteur += Time.deltaTime;
			}
			
			if ( minuteur >= delai)
			{
				recharge = false;
			}
			
			if ( Vector2.Distance(transform.position, Milieu1.position) <= 0.1f)
			{
				Va_vers_1=false;
				Va_vers_2=true;
			}
			if ( Vector2.Distance(transform.position, Milieu2.position) <= 0.1f)
			{
				Va_vers_1=true;
				Va_vers_2=false;
			}
			
			if ( Va_vers_1 )
			{
				transform.position = Vector2.MoveTowards(transform.position, Milieu1.position, vitesse * Time.deltaTime);
			}
			if ( Va_vers_2 )
			{
				transform.position = Vector2.MoveTowards(transform.position, Milieu2.position, vitesse * Time.deltaTime);
			}
			
			if (recharge == false)
			{
				minuteur = 0.0f;
				if (Vector2.Distance(transform.position, cible.position) < distance_reperage)
				{
					GameObject tire;
					tire = Instantiate(projectile, point_de_lancement.position, Quaternion.identity);
					recharge = true;
				}
			}
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, cible.position, vitesse * 4 * Time.deltaTime);
		}
    }
	
	void OnCollisionEnter2D (Collision2D collision)
    {
        plonge = false;
    }
}
