using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();
                }
            }
            return instance;
        }
    }
    private static PlayerTargeting instance;

    public bool getATarget = false;
    float currentDist = 0;
    float closestDist = 100f;
    float targetDist = 100f;
    int closeDistIndex = -1;
    int targetIndex = -1;
    public LayerMask layerMask;

    public List<GameObject> MonsterList = new List<GameObject>();

    public GameObject PlayerBolt;
    public Transform AttackPoint;

    void OnDrawGizmos()
    {
        if (getATarget)
        {
            for ( int i  = 0; i < MonsterList.Count; i++)
            {
                RaycastHit hit;
                bool isHit = Physics.Raycast (transform.position, MonsterList[i].transform.position - transform.position,
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawRay(transform.position, MonsterList[i].transform.position - transform.position);
            }
        }
    }

    void Update()
    {
        if(MonsterList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex =0;
            targetIndex = -1;


            for (int i = 0 ; i < MonsterList.Count; i++)
            {
                currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);

                RaycastHit hit;
                bool isHit = Physics.Raycast (transform.position, MonsterList[i].transform.position - transform.position,
                                            out hit, 20f, layerMask);

                if(isHit && hit.transform.CompareTag("Monster"))
                {
                    if(targetDist >= currentDist)
                    {
                        targetIndex = i;
                        targetDist = currentDist;
                    }
                }

                if(closestDist >= currentDist)
                {
                    closeDistIndex =  i;
                    closestDist = currentDist;
                }
            }

            if(targetIndex == -1)
            {
                targetIndex = closeDistIndex;
            }
            closestDist = 100f;
            targetDist = 100f;
            getATarget = true;
        }

        if ( PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "Idle" ) )
            {
                PlayerMovement.Instance.Anim.SetBool ( "Idle", false );
                PlayerMovement.Instance.Anim.SetBool ( "Walk", false );
                PlayerMovement.Instance.Anim.SetBool ( "Attack", true );
            }

        
        else if ( JoystickMovement.Instance.isPlayerMoving )
        {
            if ( !PlayerMovement.Instance.Anim.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "Walk" ) )
            {
                PlayerMovement.Instance.Anim.SetBool ( "Attack", false );
                PlayerMovement.Instance.Anim.SetBool ( "Idle", false );
                PlayerMovement.Instance.Anim.SetBool ( "Walk", true );
            }
        }
    }

    void Attack ()
    {
        Instantiate(PlayerBolt, AttackPoint.position, transform.rotation);
    }
}
