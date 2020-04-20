using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPlayerMovment : MonoBehaviour
{

    Vector3 moving = new Vector3(0,0,0);
    public float speed;

    public TagMananger TM;

    Rigidbody2D myRB;
    public Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (TM.paused)
        {
            myRB.velocity = new Vector2(0, 0);
            moving = new Vector3(0, 0, 0);
        }
        else
        {

            this.transform.Translate(moving * Time.deltaTime * speed);


        }
    }

    public void Move(int whichWay)
    {

        transform.localScale = new Vector3(1, 1, 1);
        
        switch (whichWay)
        {
            case 0:
                moving = Vector3.up;
                m_Animator.SetInteger("Direction", 1);
                break;
            case 1:
                moving = Vector3.left;
                m_Animator.SetInteger("Direction", 2);
                break;
            case 2:
                moving = Vector3.down;
                m_Animator.SetInteger("Direction", 3);
                break;
            case 3:
                moving = Vector3.right;
                m_Animator.SetInteger("Direction", 2);
                transform.localScale = new Vector3(-1,1,1);
                break;
        }

       // Debug.Log("hit");

    }
    public void StopMove()
    {
        m_Animator.SetInteger("Direction", 0);

        moving = new Vector3(0, 0, 0);
        myRB.velocity = new Vector2(0, 0);
    }

    public bool GameOver()
    {
        return TM.EndGame();
    }
    
        
    

private void OnTriggerEnter2D(Collider2D collision)
    { 
        Star hit = collision.GetComponent<Star>();
        if (hit != null)
        {
            TM.NumberOfStars++;
            Destroy(hit.gameObject);
        }
    }
}
