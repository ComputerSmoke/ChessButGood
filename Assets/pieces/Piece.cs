using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Square square;
    protected virtual void Move(Square square) {
        this.square = square;
        square.Arrive(this);
    }
    public virtual void Die(Piece killer) {
        MultiSession.Destroy(this.gameObject);
    }
    public Movement Movement() {
        return this.gameObject.GetComponent<Movement>();
    } 
}
