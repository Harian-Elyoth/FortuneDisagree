using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Ange_Tourelle_Laser : MonoBehaviour
{
    private Transform cible;
	
	public float vitesse = 10.0f;
	public float augmentation_vitesse = 0.2f;	
	
	private Vector2 POS;
	
    // Start is called before the first frame update
    void Start()
    {
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		Physics2D.IgnoreLayerCollision(13, 12, true);
		
		POS = (cible.position - transform.position).normalized; 
    }

    // Update is called once per frame
    void Update()
    {
		vitesse += augmentation_vitesse;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 2*POS.x, transform.position.y + 2*POS.y), vitesse * Time.deltaTime);
    }
	
	void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(gameObject);
    }
}
