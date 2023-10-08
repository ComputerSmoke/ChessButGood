using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    private Piece piece;
    public GameObject[] options;
    private GameObject canvas;
    private List<GameObject> buttons;
    // Start is called before the first frame update
    void Start()
    {
        canvas = Instantiate(Game.initializer.levelupCanvas, AttachedPiece().transform);
        canvas.transform.position += new Vector3(1, 0, 0);
        canvas.SetActive(false);
        buttons = new();
        for(int i = 0; i < options.Length; i++) {
            GameObject button = Instantiate(Game.initializer.levelupButton, canvas.transform);
            button.transform.position += new Vector3(i%2, -((float)i)+1.5f, 0);
            UpgradeButton upgradeButton = button.GetComponent<UpgradeButton>();
            upgradeButton.piece = AttachedPiece();
            upgradeButton.upgrade = options[i];
            buttons.Add(button);
        }
        Canvas can = canvas.GetComponent<Canvas>();
        can.worldCamera = Game.initializer.mainCamera;
    }
    private Piece AttachedPiece() {
        if(piece == null)
            piece = gameObject.GetComponent<Piece>();
        return piece;
    }
    // Update is called once per frame
    void Update()
    {
        if(AttachedPiece() == null)
            return;
        canvas.layer = AttachedPiece().gameObject.layer;
        foreach(GameObject button in buttons)
            button.layer = canvas.layer;
    }
    public void Toggle() {
        bool newStatus = !canvas.activeInHierarchy && AttachedPiece().xp > 1;
        Debug.Log("toggling menu to " + newStatus);
        canvas.SetActive(newStatus);
    }
}
