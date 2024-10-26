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

    private void Awake()
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

        // เช็คว่ามี Player ใน objectCollided หรือไม่
        bool playerInRange = objectCollided.Exists(c => c.CompareTag("Player"));
        if (!playerInRange)
        {
            isCollided = false; // เปลี่ยน isCollided เป็น false เมื่อ Player ออกนอกระยะ
        }
    }

    protected virtual void OnCollided(GameObject collidedObj)
    {
        if (collidedObj.CompareTag("Player")) 
        { 
            isCollided = true;
        }
    }
}