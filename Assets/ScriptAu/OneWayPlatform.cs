using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField] BoxCollider2D playerCollider;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentOneWayPlatform = null;
    }
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platfromCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platfromCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, platfromCollider, false);
    }
}
