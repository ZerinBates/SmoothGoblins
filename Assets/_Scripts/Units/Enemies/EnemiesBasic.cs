using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBasic : UnitBasic
{
    private int targetNumber=0;
    private Tile Target;
    // Start is called before the first frame update
    private void Start()
    {
        if (!set)
        {
            Stats = new Pawn();
        }
            foreach (Trigger t in Stats.genTrigger)
            {
                addTrigger(t);
            }
            set = true;
        
    }
    private Tile selectOption(List<HeroBasic> party, int choice =0)
    {
        //pick the clossest enemy 
        if(choice == 0)
        {
            double maxDistance = 10000;
            foreach(UnitBasic u in party)
            {
                int j = u.OccupiedTile.x;
                int i = u.OccupiedTile.y;
                double distance = System.Math.Sqrt(System.Math.Pow(j - this.OccupiedTile.x, 2) + System.Math.Pow(i - this.OccupiedTile.y, 2));
                if ( distance< maxDistance)
                {
                    maxDistance = distance;
                    Target = u.OccupiedTile;
                }
            }
        }
        //pick the firt enemy on the list and attack that
        if (choice == 1)
        {
            if (targetNumber >= party.Count)
            {
                targetNumber = 0;
            }
            List<Tile> targets = new List<Tile>();
            foreach (HeroBasic H in party)
            {
                targets.Add(H.OccupiedTile);
            }

            Target = party[targetNumber].OccupiedTile;
        }
        return Target;
    }
    public Tile SelectTarget(List<HeroBasic> party)
    {
        return selectOption(party);
    }
    public void AttackTarget()
    {
        if (Detects(this.OccupiedTile, Target))
        {
            GameManager.Instance.formationmoving = false;
        }
        if (Threatens(this.OccupiedTile, Target))
        {
            Attack(Target.OccupiedUnit);

//            Debug.Log("attacked");
            return;
        }
        //Debug.Log(this + "attack");
        if (!GridManager.Instance.MoveUp(this.OccupiedTile, Target))
        {

            // Debug.Log("attack1");

            Tile basic = GridManager.Instance.getClosestTile(this.OccupiedTile, Target);
            basic.SetUnit(this);

            // Debug.Log("attack2");
            //targetNumber++;
        }


        

    }
}
