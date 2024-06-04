using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bougerPlatforme : MonoBehaviour
{
    public float speed = 2.0f; // Vitesse de la plateforme
    public float distance = 3.0f; // Distance à parcourir depuis la position de départ
    private Vector3 startPosition;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            
        // Calculer la position cible en fonction de la position de départ, de la distance et de la direction
        Vector3 targetPosition = startPosition + (movingRight ? Vector3.right : Vector3.left) * distance;

        // Déplacer la plateforme vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Vérifier si la plateforme a atteint la position cible
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Changer de direction
            movingRight = !movingRight;
        }
    }
    
}
