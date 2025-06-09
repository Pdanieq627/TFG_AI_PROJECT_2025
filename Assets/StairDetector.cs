//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class StairDetector : MonoBehaviour
//{
//    public Tilemap dungeonTilemap;     // Asignar manualmente en Inspector
//    public Tile stairDownTile;         // Asignar el tile de escalera abajo

//    void Update()
//    {
//        if (dungeonTilemap == null || stairDownTile == null) return;

//        Vector3Int tilePos = dungeonTilemap.WorldToCell(transform.position);
//        TileBase tile = dungeonTilemap.GetTile(tilePos);

//        if (tile == stairDownTile)
//        {
//            GameManager.Instance.GoToNextLevel();
//        }
//    }
//}