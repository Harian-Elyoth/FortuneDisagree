using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tire_Basique : MonoBehaviour
{
	public float vitesse;
	
	public float temps_de_vie = 7.0f;
	private float minuteur = 0.0f;
	
	private Transform cible;
	private Vector2 POS;
	
    // Start is called before the first frame update
    void Start()
    {
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();	
		    POS = (cible.position - transform.position).normalized; 
    }

    // Update is called once per frame
    void Update()
    {
        minuteur += Time.deltaTime;
		if (minuteur>temps_de_vie)
		{
			Destroy(gameObject);
		}
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 2*POS.x, transform.position.y + 2*POS.y), vitesse * Time.deltaTime);
    }
	
	void OnCollisionEnter2D (Collision2D collision)
    {
		if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
