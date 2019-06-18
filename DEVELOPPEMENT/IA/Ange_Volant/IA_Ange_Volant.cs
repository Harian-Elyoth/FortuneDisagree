using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Ange_Volant : MonoBehaviour
{
	public GameObject projectile;
	private Transform cible;
	
	public float vitesse;
	
	public float distance_reperage;
	
	private float t = 0.0f;
	private bool monte = true;
	
	public float A = 5;
	public float W = 1;
	public float FI = 0;
	
	public Transform point_de_lancement;
	
	public float delai = 10.0f;
	private float minuteur = 0.0f;
	private bool recharge = false;
	
	public bool plonge = false;
	
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
		if (plonge == false)
		{
			if (monte)
			{
				t += Time.deltaTime*vitesse;
			}
			else
			{
				t -= Time.deltaTime*vitesse;
			}
			
			if (W*t+FI > 3.14)
			{
				monte = false;
			}
			if (W*t+FI < -3.14)
			{
				monte = true;
			}
			
			if (monte)
			{
				transform.position = new Vector2( t + posInitiale.x, A*Mathf.Sin(W*t+FI) + posInitiale.y);
			}
			else
			{
				transform.position = new Vector2( t + posInitiale.x, -A*Mathf.Sin(W*t+FI) + posInitiale.y);
			}
			
			if (recharge)
			{
				minuteur += Time.deltaTime;
			}
			
			if ( minuteur >= delai)
			{
				recharge = false;
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
