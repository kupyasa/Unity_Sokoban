using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip CrateSlide;
    [SerializeField] AudioClip Footstep;
    private AudioSource audioSource;
    [SerializeField] private float movementSpeed = 3;
    public Transform movePoint;
    [SerializeField] private LayerMask movementStoppers;
    [SerializeField] private LayerMask crateLayerMask;
    [SerializeField] private Animator playerAnimator;
    private LogicScript LogicScript;
    Direction direction;
    enum Direction { LEFT, RIGHT, UP, DOWN }
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movementSpeed * Time.fixedDeltaTime);

        //Debug.Log(movementSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) < 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                RaycastHit2D[] WallColliders = Physics2D.CircleCastAll(movePoint.position, 0.1f, new Vector2(Input.GetAxisRaw("Horizontal"), 0f), 2.0f,movementStoppers);
                RaycastHit2D[] CrateColliders = Physics2D.CircleCastAll(movePoint.position, 0.1f, new Vector2(Input.GetAxisRaw("Horizontal"), 0f), 2.0f, crateLayerMask);

                //Debug.Log(AllColliders.Length);
                /*foreach (var item in AllColliders)
                {

                   //Debug.Log(item.transform.position);
                }*/
                bool canMove = !Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, movementStoppers) && (WallColliders.Length + CrateColliders.Length) < 2 && !LogicScript.IsLevelCompletedBool;
                if (canMove)
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);

                    if (Input.GetAxisRaw("Horizontal") == 1f)
                    {
                        //ResetTriggers();
                        playerAnimator.SetTrigger("MoveRight");
                        direction = Direction.RIGHT;
                        audioSource.PlayOneShot(Footstep);
                    }
                    else if (Input.GetAxisRaw("Horizontal") == -1f)
                    {
                        //ResetTriggers();
                        playerAnimator.SetTrigger("MoveLeft");
                        direction = Direction.LEFT;
                        audioSource.PlayOneShot(Footstep);
                    }
                }

            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                RaycastHit2D[] WallColliders = Physics2D.CircleCastAll(movePoint.position, 0.1f, new Vector2(0f, Input.GetAxisRaw("Vertical")), 2.0f, movementStoppers);
                RaycastHit2D[] CrateColliders = Physics2D.CircleCastAll(movePoint.position, 0.1f, new Vector2(0f, Input.GetAxisRaw("Vertical")), 2.0f, crateLayerMask);
                
                bool canMove = !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, movementStoppers) && (WallColliders.Length + CrateColliders.Length) < 2 && !LogicScript.IsLevelCompletedBool;
                if (canMove)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    if (Input.GetAxisRaw("Vertical") == 1f)
                    {
                        //ResetTriggers();
                        playerAnimator.SetTrigger("MoveUp");
                        direction = Direction.UP;
                        audioSource.PlayOneShot(Footstep);
                    }
                    else if (Input.GetAxisRaw("Vertical") == -1f)
                    {
                        //ResetTriggers();
                        playerAnimator.SetTrigger("MoveDown");
                        direction = Direction.DOWN;
                        audioSource.PlayOneShot(Footstep);
                    }
                }

            }
        }
        //Debug.Log(direction);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        if (collision.gameObject.CompareTag("Crate"))
        {
            switch (direction)
            {
                case Direction.LEFT:
                    collision.gameObject.transform.position =  new Vector3(collision.gameObject.transform.position.x - 1.0f, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                    break;
                case Direction.RIGHT:
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + 1.0f, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                    break;
                case Direction.UP:
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 1.0f, collision.gameObject.transform.position.z);
                    break;
                case Direction.DOWN:
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 1.0f, collision.gameObject.transform.position.z);
                    break;
                default:
                    break;
            }
            audioSource.PlayOneShot(CrateSlide);
        }
    }



    private void ResetTriggers()
    {
        playerAnimator.ResetTrigger("MoveRight");
        playerAnimator.ResetTrigger("MoveLeft");
        playerAnimator.ResetTrigger("MoveUp");
        playerAnimator.ResetTrigger("MoveDown");
    }
}
