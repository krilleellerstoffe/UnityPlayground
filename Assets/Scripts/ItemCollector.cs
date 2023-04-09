using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    private int collectedStrawberries = 0;
    [SerializeField] private Text strawberriesText = null;
    [SerializeField] private AudioSource collectSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When boxCollider is triggered, do this
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Strawberry")) 
        {
            collectSFX.Play();
            Destroy(collision.gameObject);
            collectedStrawberries++;
            Debug.Log("Strawberries collected: " + collectedStrawberries);
            strawberriesText.text = "Strawberries: " + collectedStrawberries;
        }
    }
}
