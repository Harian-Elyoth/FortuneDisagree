using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Demon_Rampant : MonoBehaviour
{
	public float vitesse;
	public float distance_reperage;
	
	private Vector2 posInitial;
	
	private Transform cible;
	
	public float taille_sprite;
	
	public LayerMask LayerGrimpable;
	public LayerMask LayerPlateforme;
	
	private Rigidbody2D rb;
	public float gravite;
	
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		gravite = rb.gravityScale; 
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		Physics2D.IgnoreLayerCollision(8, 12, true);
    }

    // Update is called once per frame
    void Update()
    {		
		if (Physics2D.Raycast(transform.position, Vector2.up, 0.01f, LayerGrimpable) && rb.position.y != cible.position.y-1.33)
		{
			rb.gravityScale = 0;

			
			Physics2D.IgnoreLayerCollision(10, 12, true);
			if (Vector2.Distance(transform.position, cible.position) < distance_reperage)
				{
					transform.position = Vector2.MoveTowards(transform.position, new Vector2(cible.position.x, cible.position.y), vitesse * Time.deltaTime);
				}
				else
				{
					transform.position = Vector2.MoveTowards(transform.position, new Vector2(posInitial.x, transform.position.y), vitesse * Time.deltaTime);
				}
			if (Physics2D.Raycast(transform.position, Vector2.down, 0.01f, LayerGrimpable))
			{
				//rb.velocity = new Vector2(0, 2);
			}
		}
		else
		{
			rb.gravityScale = gravite;
			Physics2D.IgnoreLayerCollision(10, 12, false);
			if (Vector2.Distance(transform.position, cible.position) < distance_reperage)
				{
					transform.position = Vector2.MoveTowards(transform.position, new Vector2(cible.position.x, transform.position.y), vitesse * Time.deltaTime);
				}
				else
				{
					transform.position = Vector2.MoveTowards(transform.position, new Vector2(posInitial.x, transform.position.y), vitesse * Time.deltaTime);
				}
		}	
    }
}
