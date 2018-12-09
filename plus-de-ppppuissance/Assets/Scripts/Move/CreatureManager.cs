using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureManager : MonoBehaviour {

    //Properties
    [Range(0.1f, 1f)]
    public float dispatchTimer;
    [Range(0.1f, 1f)]
    public float regroupTimer;
    [Range(0.05f, 0.5f)]
    public float updateAvatarHordeTimer;

    //Inputs
    [HideInInspector]
    public float input_dispatch, input_regroup;

    //External game objects and scripts
    private GameObject go_avatar;
    private GameObject go_selection;

    //Variables
    private Horde avatarHorde;
    private List<Horde> dispatchedHordes;
    private bool dispatching;
    private bool regrouping;
    private Horde currentHorde;

    void Start () {
        dispatching = false;
        regrouping = false;

        go_avatar = GameObject.FindGameObjectWithTag("Player");
        go_selection = GameObject.FindGameObjectWithTag("Selection");

        avatarHorde = new Horde();
        dispatchedHordes = new List<Horde>();

        foreach(GameObject go_creature in GameObject.FindGameObjectsWithTag("Creature"))
        {
            avatarHorde.AddCreature(go_creature.GetComponent<MoveCreature>());
        }

        StartCoroutine(UpdateAvatarHorde());
    }
	
	void Update () {
		if(input_dispatch > 0 && !dispatching)
        {
            StartCoroutine(Dispatch());
        }
        else if (input_regroup > 0 && !regrouping)
        {
            StartCoroutine(Regroup());
        }
	}

    IEnumerator UpdateAvatarHorde()
    {
        while(true)
        {
            avatarHorde.UpdateTargetPosition(go_avatar.transform.position);
            yield return new WaitForSeconds(updateAvatarHordeTimer);
        }
    }

    IEnumerator Dispatch()
    {
        if(avatarHorde.Count() > 0)
        {
            //Determining point on the ground
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ground");
            if(Physics.Raycast(go_selection.transform.position, Vector3.down, out hit, 30, layerMask))
            {
                dispatching = true;
                currentHorde = new Horde();
                dispatchedHordes.Add(currentHorde);
                currentHorde.UpdateTargetPosition(hit.point);

                while (input_dispatch > 0 && input_regroup <= 0 && avatarHorde.Count() > 0)
                {
                    currentHorde.AddCreature(avatarHorde.PopNearest(currentHorde.targetPosition));
                    yield return new WaitForSeconds(dispatchTimer);
                }

                dispatching = false;
            }
        }
    }

    IEnumerator Regroup()
    {
        if (dispatchedHordes.Count > 0)
        {
            regrouping = true;
            currentHorde = null;

            while (input_dispatch <= 0 && input_regroup > 0 && dispatchedHordes.Count > 0)
            {
                currentHorde = SelectNearestHorde();
                avatarHorde.TransferHorde(currentHorde);
                dispatchedHordes.Remove(currentHorde);

                yield return new WaitForSeconds(regroupTimer);
            }

            regrouping = false;
        }
    }

    Horde SelectNearestHorde()
    {
        float currentDistance = float.MaxValue;
        Horde currentHorde = null;

        foreach(Horde horde in dispatchedHordes)
        {
            float distance = Mathf.Abs(Vector3.ProjectOnPlane(horde.targetPosition - go_avatar.transform.position, Vector3.up).magnitude);

            if(currentDistance > distance)
            {
                currentDistance = distance;
                currentHorde = horde;
            }
        }

        return currentHorde;
    }
}
