using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdeManagement : MonoBehaviour {

    public Creature[] creatures;
    public float herdeScopeDistance = 50;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        //TODO - Form herde with controller
        foreach (Creature creature in creatures)
        {
            List<Creature> closeCreatures = new List<Creature>();
            foreach (Creature other in creatures)
            {
                if (other == creature) continue;
                if (creature.Distance(other) < herdeScopeDistance)
                    closeCreatures.Add(other);
            }

            creature.SetHerde(closeCreatures);
        }
    }
}
