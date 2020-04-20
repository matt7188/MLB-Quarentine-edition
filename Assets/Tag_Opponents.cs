using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag_Opponents : MonoBehaviour
{
    public float speed;
    public float DistanceSpeed=10;
    public bool It=false;

    Rigidbody2D myRB;

    TagMananger TM;

    TagPlayerMovment Player;

    public Animator m_Animator;

    public Vector2 StartingPoint;

    public bool CurrentlyOut;

    SpriteRenderer spriteRender;

        void  OnCollisionEnter2D(Collision2D col)
    {

        Tag_Opponents hold = col.transform.gameObject.GetComponent<Tag_Opponents>();
        if (hold != null)
        {
            if (It&& !hold.It)
            {
                m_Animator.SetTrigger("Tagging");
                hold.It = true;
                hold.m_Animator.SetTrigger("Tagged");

                if (TM.TimedTag)
                {
                    StartCoroutine(UnIt());
                }

            }
        }
        else
        {
            TagPlayerMovment Player = col.transform.gameObject.GetComponent<TagPlayerMovment>();
            if (Player != null)
            {
                if (It)
                {

                    m_Animator.SetTrigger("Tagging");
                    Player.m_Animator.SetTrigger("Tagged");

                    if (!Player.GameOver())
                        It = false;
                }
            }
        }

        while (myRB.velocity.SqrMagnitude() < 1)
            myRB.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
    }

    private void Start()
    {
        Player = FindObjectOfType<TagPlayerMovment>();
            m_Animator = gameObject.GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        StartingPoint = this.transform.position;
         TM = FindObjectOfType<TagMananger>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (TM.paused)
        {
            myRB.velocity = new Vector2(0, 0);
        }
        else
        {
            var direction = Vector3.zero;

            if (It && Vector2.Distance(transform.position, Player.transform.position) < DistanceSpeed)
            {
                //Debug.Log("hit");
                direction = Player.transform.position - transform.position;
                myRB.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
            }

            while (myRB.velocity.SqrMagnitude() < 1)
                myRB.velocity = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));


            if (TM.Color && It)
            {
                spriteRender.color = Color.green;
            }
            else
                spriteRender.color = Color.white;




            transform.localScale = new Vector3(1, 1, 1);

            if (Mathf.Abs(myRB.velocity.y) > Mathf.Abs(myRB.velocity.x))
            {
                if (myRB.velocity.y > 0)
                {
                    m_Animator.SetInteger("Direction", 1);
                }
                else
                {
                    m_Animator.SetInteger("Direction", 3);
                }
            }
            else
            {
                if (myRB.velocity.x > 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                m_Animator.SetInteger("Direction", 2);

            }
        }
    }

    IEnumerator UnIt()
    {

        yield return new WaitForSeconds(5);
        It = false;
    }


    }
