using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tire_Basique : MonoBehaviour
{
	public float vitesse;
	
	public float temps_de_vie = 7.0f;
	private float minuteur = 0.0f;
	
	private Transform cible;
	private Vector2 but;
	
    // Start is called before the first frame update
    void Start()
    {
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		but = cible.position;
    }

    // Update is called once per frame
    void Update()
    {
        minuteur += Time.deltaTime;
		if (minuteur>temps_de_vie)
		{
			Destroy(gameObject);
		}
		
		transform.position = Vector2.MoveTowards(transform.position, but, vitesse * Time.deltaTime);
    }
	
}
