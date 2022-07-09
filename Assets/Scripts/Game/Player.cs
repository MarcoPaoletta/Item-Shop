using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public static Color spriteRendererColor;

    private int speed = 10;
    private float horizontal;
    private float vertical;

    void Start()
    {
        SetSkin();
        SetMoneyCountText();
    }

    void SetSkin()
    {
        spriteRenderer.color = spriteRendererColor;
    }

    void SetMoneyCountText()
    {
        GameObject.Find("MoneyCountText").GetComponent<Text>().text = GameManager.LoadMoneyCount() + "$";
    }

    void Update()
    {
        Movement();      
        ClampPosition();  
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        vertical = Input.GetAxisRaw("Vertical") * speed; 
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        pos.y = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Destroy(other.gameObject);
        GameManager.moneyCount += 10;
        GameManager.SaveMoneyCount();
    }
}
