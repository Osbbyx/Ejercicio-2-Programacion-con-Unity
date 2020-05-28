using UnityEngine;

public class Box : MonoBehaviour
{

    public float horizontalSpeed = 3f;

    private Rigidbody2D boxRigidbody2d;
    private bool hasCollided;

    private Vector2 pointA;
    private Vector2 pointB;

    private Transform boxSpawner;
    private bool movingRight;

    public Sound[] dropSounds;
    public Sound[] hitSounds;

    void Start()
    {
        boxRigidbody2d = GetComponent<Rigidbody2D>();
        boxSpawner = CamaraManager.Instance.boxSpawner;

        pointA = new Vector2(-2, boxSpawner.position.y);
        pointB = new Vector2(2, boxSpawner.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hasCollided && (collision.collider.CompareTag(GameTags.Box) || collision.collider.CompareTag(GameTags.Ground)))
        {
            hasCollided = true;
            AudioManage.Instance.PlaySound(hitSounds[Random.Range(0, hitSounds.Length)]);
            CheckShake();
            GameManager.Instance.SetScore();
            CamaraManager.Instance.UpdateBoxesHeight(transform.position.y);
           // Debug.Log(collision.collider.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTags.GameOverZone))
        {
            GameManager.Instance.GameOver();
        }
    }


    private void CheckShake()
    {
        if (GameManager.Instance.Score == 0)
        {
            CamaraManager.Instance.Shake();
        }
        else
        {
            int shakeRandom = Random.Range(0, 10);
            if (shakeRandom > 7)
            {
                CamaraManager.Instance.Shake();
            }
        }

       
    }

    private void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            if (boxRigidbody2d.gravityScale == 0f)
            {
                Move();
            }

            if (Input.GetMouseButtonDown(0))
            {
                AudioManage.Instance.PlaySound(dropSounds[Random.Range(0, dropSounds.Length)]);
                boxRigidbody2d.gravityScale = 1f;
            }
        }
        else
        {
            if (!hasCollided)
            {
                boxRigidbody2d.gravityScale = 1f;
            }
        }
    }


    private void Move()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB, horizontalSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA, horizontalSpeed * Time.deltaTime);
        }
        if (transform.position.x >= pointB.x)
        {
            movingRight = false;
        }
        else if (transform.position.x <= pointA.x)
        {
            movingRight = true;
        }
    }


}
