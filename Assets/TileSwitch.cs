using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileSwitch : MonoBehaviour {
    public Tilemap TileMap;
    public TileBase WhiteGroundTile;
    public TileBase BlackGroundTile;

    bool _showingWhite = true;
    bool _interactHeld;
    bool _interactCooldown;

    void Start() {
        UpdateTile();
    }

    void Update() {
        if (_interactCooldown) return;
        if (_interactHeld) {
            UpdateTile();
            _interactCooldown = true;
        }
    }

    void UpdateTile() {
        if (_showingWhite) {
            foreach (var position in TileMap.cellBounds.allPositionsWithin) {
                if (TileMap.GetTile(position) == BlackGroundTile)
                    TileMap.SetTile(position, WhiteGroundTile);
            }
        }

        _showingWhite = !_showingWhite;
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.started) {
            _interactHeld = true;
        } else if (context.canceled) {
            _interactHeld = false;
            _interactCooldown = false;
        }
    }
}
