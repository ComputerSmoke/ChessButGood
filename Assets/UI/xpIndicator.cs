using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xpIndicator : Indicator
{
    private Piece piece;
    private Piece AttachedPiece() {
        if(piece == null && transform.parent.gameObject.TryGetComponent<Piece>(out Piece newPiece))
            piece = newPiece;
        if(piece == null)
            Object.Destroy(gameObject);
        return piece;
    }
    protected override int TargetAmount() {
        if(AttachedPiece() == null || !AttachedPiece().gameObject.GetComponent<SpriteRenderer>().enabled)
            return 0;
        return AttachedPiece().xp;
    }
    protected override void OnUpdate() {
        if(AttachedPiece() == null)
            return;
        gameObject.layer = AttachedPiece().gameObject.layer;
        MatchLayer();
        base.OnUpdate();
    }
    private void MatchLayer() {
        foreach(GameObject coin in coins)
            coin.layer = gameObject.layer;
    }
}
