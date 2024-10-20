using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public static CollidableObject Instance;
    private Collider2D Collider2D;
    public bool isCollided = false;

    [SerializeField] private ContactFilter2D contactFilter;
    public List<Collider2D> objectCollided = new List<Collider2D>(1);

    public void Awake()
    {
        if (Instance == null)
        { 
            Instance = this; 
        }
    }
    protected virtual void Start()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Collider2D.OverlapCollider(contactFilter, objectCollided);
        foreach (var o in objectCollided)
        {
            OnCollided(o.gameObject);
        }
    }
    protected virtual void OnCollided(GameObject collidedObj)
    {
        if (collidedObj.CompareTag("Player")) 
        { 
            Debug.Log("Collided with" + collidedObj.name);
            isCollided = true;
        }
    }
}
