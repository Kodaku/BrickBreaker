using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    private float scaledQ;
    public GameObject valueIdle;
    public GameObject valueRight;
    public GameObject valueLeft;
    private Dictionary<Vector2, GameObject> valueBreadcrumbs = new Dictionary<Vector2, GameObject>();

    public void SetColors(float currentQ, float minQValue, float maxQValue)
    {
        scaledQ = (currentQ - minQValue) / (maxQValue - minQValue + Mathf.Epsilon);
    }

    public void SpawnMove(Player player, Cell ballCell)
    {
        Action playerAction = player.GetCurrentAction();
        Vector2 ballPosition = new Vector2(ballCell.column, ballCell.row);
        
        switch(playerAction)
        {
            case Action.IDLE:
            {
                if(valueBreadcrumbs.ContainsKey(ballPosition))
                {
                    Destroy(valueBreadcrumbs[ballPosition]);
                    valueBreadcrumbs.Remove(ballPosition);
                }
                GameObject newArrow = Instantiate(valueIdle, ballPosition, Quaternion.identity);
                newArrow.transform.parent = transform;
                SpriteRenderer spriteRenderer = newArrow.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(scaledQ, scaledQ, 0.0f);
                valueBreadcrumbs.Add(ballPosition, newArrow);
                break;
            }
            case Action.LEFT:
            {
                if(valueBreadcrumbs.ContainsKey(ballPosition))
                {
                    Destroy(valueBreadcrumbs[ballPosition]);
                    valueBreadcrumbs.Remove(ballPosition);
                }
                GameObject newArrow = Instantiate(valueLeft, ballPosition, Quaternion.identity);
                newArrow.transform.parent = transform;
                SpriteRenderer spriteRenderer = newArrow.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(scaledQ, scaledQ, 0.0f);
                valueBreadcrumbs.Add(ballPosition, newArrow);
                break;
            }
            case Action.RIGHT:
            {
                if(valueBreadcrumbs.ContainsKey(ballPosition))
                {
                    Destroy(valueBreadcrumbs[ballPosition]);
                    valueBreadcrumbs.Remove(ballPosition);
                }
                GameObject newArrow = Instantiate(valueRight, ballPosition, Quaternion.identity);
                newArrow.transform.parent = transform;
                SpriteRenderer spriteRenderer = newArrow.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(scaledQ, scaledQ, 0.0f);
                valueBreadcrumbs.Add(ballPosition, newArrow);
                break;
            }
        }
    }

    public void Reset()
    {
        foreach(Vector2 value in valueBreadcrumbs.Keys)
        {
            Destroy(valueBreadcrumbs[value]);
        }
        valueBreadcrumbs.Clear();
    }
}
