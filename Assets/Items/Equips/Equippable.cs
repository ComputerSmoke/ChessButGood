using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : MonoBehaviour
{
    protected Piece holder;
    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer holderRenderer;
    //TODO: droppable on ranged attacks (when add ranged attacks)
    public virtual void Equip(Piece piece) {
        holder = piece;
        piece.Equips().Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        holderRenderer = holder.GetComponent<SpriteRenderer>();

        gameObject.transform.parent = holder.gameObject.transform;
        float colorAdjust = 1 - holder.color*2;
        gameObject.transform.position = holder.gameObject.transform.position + (Offset() * colorAdjust);
        gameObject.transform.rotation = holder.gameObject.transform.rotation * Quaternion.Euler(new Vector3(0, 0, Rotation()));
        gameObject.transform.localScale = Scale();

        spriteRenderer.sortingLayerName = "Equips";
    }
    public virtual bool Counter(Piece piece) {
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.layer = holder.gameObject.layer;
        if(!RotateWithParent()) 
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Rotation()*((float)(1 - holder.color*2))));
    }
    protected virtual Vector3 Scale() {
        return new Vector3(1, 1, 1);
    } 
    protected virtual Vector3 Offset() {
        return new Vector3(0, 0, 0);
    }
    protected virtual float Rotation() {
        return 0;
    }
    protected virtual bool RotateWithParent() {
        return true;
    }
}

