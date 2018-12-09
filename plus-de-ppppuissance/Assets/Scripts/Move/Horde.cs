using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horde {

    //Inputs
    [HideInInspector]
    public Vector3 targetPosition;

    //Variables
    public List<MoveCreature> creatures;

    public Horde()
    {
        targetPosition = Vector3.zero;
        creatures = new List<MoveCreature>();
    }

    public void UpdateTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        foreach(MoveCreature creature in creatures)
            creature.UpdateTargetPosition(targetPosition);
    }
    
    public int Count()
    {
        return creatures.Count;
    }

    public void TransferHorde(Horde horde)
    {
        creatures.AddRange(horde.creatures);
        horde.creatures.Clear();
        UpdateTargetPosition(targetPosition);
    }

    public void AddCreature(MoveCreature creature)
    {
        creatures.Add(creature);
        creature.UpdateTargetPosition(targetPosition);
    }

    public void RemoveCreature(MoveCreature creature)
    {
        if (creatures.Contains(creature))
            creatures.Remove(creature);
    }

    public MoveCreature PopCreature ()
    {
        if (creatures.Count <= 0)
        {
            return null;
        }

        MoveCreature creature = creatures[creatures.Count - 1];
        creatures.RemoveAt(creatures.Count - 1);
        return creature;
    }
}
