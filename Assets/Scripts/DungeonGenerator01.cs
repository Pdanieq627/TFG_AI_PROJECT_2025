using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Tamaño de la mazmorra")]
    public int width = 15;
    public int height = 15;

    [Header("Tiles y referencias")]
    public Tilemap dungeonTilemap;         // Referencia al Tilemap de la escena
    public Tile floorTile;                 // Tile de suelo
    public Tile wallTile;                  // Tile de muro
    public Tile stairUpTile;               // Tile de escalera hacia arriba
    public Tile stairDownTile;             // Tile de escalera hacia abajo

    private int[,] grid;                   // Matriz interna: 0 = muro, 1 = suelo

    // Datos para las salas
    private List<RectInt> rooms;           // Lista de rectángulos (salas)
    private List<Vector2Int> roomCenters;  // Centros de cada sala

    void Start()
    {
        GenerateDungeon();
    }

    /// <summary>
    /// Genera la mazmorra completa: salas, pasillos y pinta los tiles en el Tilemap.
    /// </summary>
    public void GenerateDungeon()
    {
        // 1) Inicializar la matriz de muros
        grid = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0; // 0 = muro
            }
        }

        rooms = new List<RectInt>();
        roomCenters = new List<Vector2Int>();

        // 2) Generar salas (5 salas de tamaño aleatorio 3x3 a 5x5)
        int maxRooms = 5;
        for (int i = 0; i < maxRooms; i++)
        {
            // Tamaño aleatorio de sala
            int roomWidth = Random.Range(3, 6);  // 3, 4 o 5
            int roomHeight = Random.Range(3, 6);

            // Posición aleatoria garantizando que la sala quede dentro del grid
            int roomX = Random.Range(1, width - roomWidth - 1);
            int roomY = Random.Range(1, height - roomHeight - 1);

            RectInt newRoom = new RectInt(roomX, roomY, roomWidth, roomHeight);

            // Verificar que no se solape con ninguna sala previa (dejamos un margen de 1 tile)
            bool overlaps = false;
            foreach (RectInt other in rooms)
            {
                RectInt expandedOther = new RectInt(other.xMin - 1, other.yMin - 1, other.width + 2, other.height + 2);

                if (newRoom.Overlaps(expandedOther))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                CreateRoom(newRoom);
                rooms.Add(newRoom);
                // Calcular centro:  
                int centerX = newRoom.x + newRoom.width / 2;
                int centerY = newRoom.y + newRoom.height / 2;
                roomCenters.Add(new Vector2Int(centerX, centerY));
            }
            // Si solapó, descartamos esta iteración (no agregamos sala)
        }

        // 3) Conectar centros de salas con pasillos en L-shape
        for (int i = 1; i < roomCenters.Count; i++)
        {
            Vector2Int prevCenter = roomCenters[i - 1];
            Vector2Int currCenter = roomCenters[i];
            CreateCorridor(prevCenter, currCenter);
        }

        // 4) Pintar la cuadrícula final en el Tilemap
        PaintTiles();

        // 5) Colocar escaleras: arriba en la primera sala, abajo en la última
        if (roomCenters.Count > 0)
        {
            Vector2Int firstCenter = roomCenters[0];
            Vector2Int lastCenter = roomCenters[roomCenters.Count - 1];

            dungeonTilemap.SetTile(new Vector3Int(firstCenter.x, firstCenter.y, 0), stairUpTile);
            dungeonTilemap.SetTile(new Vector3Int(lastCenter.x, lastCenter.y, 0), stairDownTile);
        }
    }

    /// <summary>
    /// Marca todos los tiles dentro de los límites de la sala como suelo (1).
    /// </summary>
    private void CreateRoom(RectInt room)
    {
        for (int x = room.x; x < room.x + room.width; x++)
        {
            for (int y = room.y; y < room.y + room.height; y++)
            {
                grid[x, y] = 1; // 1 = suelo
            }
        }
    }

    /// <summary>
    /// Crea un pasillo en forma de L entre dos centros de sala.
    /// </summary>
    private void CreateCorridor(Vector2Int from, Vector2Int to)
    {
        int x = from.x;
        int y = from.y;

        // Primero, mover horizontalmente hasta la x de destino
        while (x != to.x)
        {
            grid[x, y] = 1;
            if (to.x > x) x++;
            else x--;
        }
        // Luego, mover verticalmente hasta la y de destino
        while (y != to.y)
        {
            grid[x, y] = 1;
            if (to.y > y) y++;
            else y--;
        }
        // Finalmente, marcar la última posición como suelo
        grid[x, y] = 1;
    }

    /// <summary>
    /// Recorre la matriz y coloca Floor o Wall tiles según corresponda.
    /// </summary>
    private void PaintTiles()
    {
        // Limpia cualquier tile previo
        dungeonTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (grid[x, y] == 1)
                {
                    // Suelo
                    dungeonTilemap.SetTile(pos, floorTile);
                }
                else
                {
                    // Muro
                    dungeonTilemap.SetTile(pos, wallTile);
                }
            }
        }
    }
}