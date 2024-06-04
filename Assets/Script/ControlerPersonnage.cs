using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlerPersonnage : MonoBehaviour
{
    Animator personnageAnim; // Reference composante personnage
    public bool auSol;
    float vitesseX;      // Vitesse horizontale actuelle

    public float vitesseY;
    public float vitesseMaxX;   // Vitesse horizontale maximale désirée
    public float vitesseSaut;   // Vitesse de saut désirée
    bool partieTerminee;
    bool isGrounded; // Indique si le personnage est au sol

    public int totalCoins = 0; // Nombre total de pièces à collecter
    public GameObject door; // Référence au GameObject de la porte
    public float distanceInteraction = 1.5f; // Distance requise pour interagir avec la porte

    private int coinsCollectees = 0; // Nombre de pièces collectées

    public float distanceDeChute; // Distance à partir de laquelle le changement de scène est déclenché

    void Start()
    {
        personnageAnim = GetComponent<Animator>();
        Debug.Log("Script initialisé");
    }

    void Update()
    {
        if (!partieTerminee)
        {
            // Déplacement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseMaxX;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   // Déplacement vers la droite
            {
                vitesseX = vitesseMaxX;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;  // Mémorise vitesse actuelle en X
            }

            // Sauter avec la touche "UpArrow" ou "w"
            if ((Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && Physics2D.OverlapCircle(transform.position, 0.5f))
            {
                vitesseY = vitesseSaut;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, vitesseY);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
            }
            // Applique les vitesses en X et conserve la vélocité en Y
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);

            // Gestion des animations de course et de repos
            if (Mathf.Abs(vitesseX) > 0.1f)
            {
                personnageAnim.SetBool("course", true);
            }
            else
            {
                personnageAnim.SetBool("course", false);
            }
            if (Mathf.Abs(vitesseY) == 0)
            {
                personnageAnim.SetBool("saut", false);
            }
            else
            {
                personnageAnim.SetBool("saut", true);
            }

            // Gestion de l'animation de saut
            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("saut", false);
            }

            // Vérifier si le personnage est tombé à une certaine distance
            if (transform.position.y < distanceDeChute)
            {
                Debug.Log("Le personnage est tombé à une certaine distance !");
                SceneManager.LoadScene("Scene_Intro"); // Changer de scène
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinsCollectees++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("magasin"))
        {
            VerifierEtChangerDeScene("Victoire");
        }

        if (other.gameObject.CompareTag("oiseaux"))
        {
            Debug.Log("Collision avec les oiseaux !");
            SceneManager.LoadScene("Echec");
        }
    }

    void VerifierEtChangerDeScene(string sceneName)
    {
        GameObject[] remainingCoins = GameObject.FindGameObjectsWithTag("Coin");
        if (remainingCoins.Length == 0)
        {
            // Charger la scène suivante
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Il reste encore des pièces à collecter !");
        }
    }
}
