using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Ange_Tourelle : MonoBehaviour
{
    public GameObject projectile;
	private Transform cible;
	
	private bool monte = true;
	public float vitesse_monte;
	public float vitesse_descend;
	
	public float distance_reperage;
	
	public Transform point_de_lancement;
	
	public float delai = 10.0f;
	private float minuteur = 0.0f;
	private bool recharge = false;
	
	private Vector2 posInitiale;
	
	public float cgm_angle;
	
    // Start is called before the first frame update
    void Start()
    {
		posInitiale = transform.position;
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
			
		if (recharge)
		{
			minuteur += Time.deltaTime;
		}
		
		if ( minuteur >= delai)
		{
			recharge = false;
		}
		
		if (monte)
		{
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, posInitiale.y + 2.0f), vitesse_monte * Time.deltaTime);
			if (transform.position.y >= posInitiale.y + 1.95f)
			{
				monte = false;
			}
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, posInitiale.y), vitesse_descend * Time.deltaTime);
			if (transform.position.y <= posInitiale.y + 0.05f)
			{
				monte = true;
			}
		}
		
		
		if (recharge == false)
		{
			minuteur = 0.0f;
			if (Vector2.Distance(transform.position, cible.position) < distance_reperage)
			{
				GameObject tire;

				Vector2 dir = cible.position - point_de_lancement.position;

				float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg + 90;
				
				tire = Instantiate(projectile, point_de_lancement.position, Quaternion.AngleAxis(angle, Vector3.forward));
				recharge = true;
			}
		}
    }
}
