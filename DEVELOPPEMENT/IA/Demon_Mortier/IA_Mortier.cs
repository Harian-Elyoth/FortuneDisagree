using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Mortier : MonoBehaviour
{
	public float argument_vitesse;
	public float distance_reperage;
	public float distance_min;
	
	private Transform cible;
	
	public Rigidbody2D projectile;
	public Transform point_de_lancement;
	
	public bool recharge = false;
	public float delai_entre_coup;
	public float minuteur = 0;
	
	private bool vise = false;
	private bool a_vise = false;
	private Vector2 posTemporaire = new Vector2(0f,0f);
	public float temps_de_vise = 0.10f;
	private float minuteur_vise;
	
	private Rigidbody2D rb;
	
	public float Multiplicateur = 0.5f;
	
	public float x = 0;
	public float y = 0;
	
	private float Vx;
	private float Vy;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recharge == true)
		{
			minuteur += Time.deltaTime;
		}
		
		if (minuteur >= delai_entre_coup)
		{
			recharge = false;
		}
		
		if (recharge == false)
		{
			if (Vector2.Distance(transform.position, cible.position) >= distance_min && Vector2.Distance(transform.position, cible.position) < distance_reperage)
			{	
				float x0 = point_de_lancement.position.x;
				float y0 = point_de_lancement.position.y;
				
				float t = Multiplicateur * Vector2.Distance(transform.position, cible.position) / argument_vitesse;
				
				x = cible.position.x - x0;
				y = cible.position.y - y0;
				
				Vx = (x/t);
				Vy = y/t +0.5f*9.8f*t;

				Rigidbody2D boulet;
				boulet = (Rigidbody2D)Instantiate(projectile, point_de_lancement.position, Quaternion.identity);

				boulet.velocity = point_de_lancement.TransformDirection (new Vector2 (Vx, Vy));
					
				a_vise = false;
				minuteur = 0;
				minuteur_vise = 0;
				recharge = true;
				}
			}
		}
    }

