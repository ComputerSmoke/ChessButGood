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
        spriteRenderer = GetComponent<SpriteRenderer>();
        holderRenderer = holder.GetComponent<SpriteRenderer>();

        gameObject.transform.parent = holder.gameObject.transform;
        gameObject.transform.position = holder.gameObject.transform.position + Offset();
        gameObject.transform.rotation = holder.gameObject.transform.rotation;
        gameObject.transform.localScale = Scale();

        spriteRenderer.sortingLayerName = "Equips";
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.layer = holder.gameObject.layer;
    }
    protected virtual Vector3 Scale() {
        return new Vector3(1, 1, 1);
    } 
    protected virtual Vector3 Offset() {
        return new Vector3(0, 0, 0);
    }
}
