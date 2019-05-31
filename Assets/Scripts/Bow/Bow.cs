using UnityEngine;

public class Bow : MonoBehaviour
{
    private int nbCollide = 0;
    private BowPart lastBowPart;


    public void NotifyHitCollider(BowPart bowPart)
    {
        lastBowPart = bowPart;
        ++nbCollide;
        if(nbCollide >= 2)
        {
            //if(bowPart.AttachArrow())
                //nbCollide = 0;
        }
    }

    public void TryToAttach()
    {
        if (nbCollide >= 2)
        {
            lastBowPart.AttachArrow();
        }
    }

    public void NotifyQuitCollide()
    {
        --nbCollide;
    }
}
