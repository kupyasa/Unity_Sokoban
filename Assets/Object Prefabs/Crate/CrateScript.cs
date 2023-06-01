using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    private LogicScript LogicScript;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite NormalCrate;
    [SerializeField] private Sprite GreyedOutCrate;

    // Start is called before the first frame update
    void Start()
    {
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CratePlacement"))
        {
            //spriteRenderer.sprite = GreyedOutCrate;
            LogicScript.AddToCrateInCorrectPlace();
            Debug.Log("Yerinde");
            LogicScript.IsLevelCompleted();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CratePlacement"))
        {
            LogicScript.SubstractFromCrateInCorrectPlace();
            Debug.Log("Yerinde Deðil");
            spriteRenderer.sprite = NormalCrate;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CratePlacement"))
        {
            spriteRenderer.sprite = GreyedOutCrate;
           
        }
    }
}
