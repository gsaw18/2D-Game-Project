using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;

public class CSVTilemapLoader : MonoBehaviour
{
    [Header("Inputs")]
    public Sprite[] sprites;          // Drag all sliced sprites from the tilesheet
    public TextAsset visualCsv;       // visual_map.csv
    public TextAsset collisionCsv;    // collision_map.csv

    [Header("Tilemaps")]
    public Tilemap visualTilemap;
    public Tilemap collisionTilemap;

    [Header("Optional")]
    public Vector3Int startCell = Vector3Int.zero; // where to paint the top-left (0,0)

    void Start() => LoadNow();

    public void LoadNow()
    {
        if (!visualTilemap || sprites == null || sprites.Length == 0 || visualCsv == null)
        {
            Debug.LogError("CsvTilemapLoader: Assign sprites, visualCsv, and visualTilemap.");
            return;
        }

        // Parse CSVs
        int[,] visual = ParseCsv(visualCsv.text, out int width, out int height);
        int[,] coll = collisionCsv ? ParseCsv(collisionCsv.text, out _, out _) : new int[height, width];

        // Paint visual
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int idx = visual[y, x];
                if (idx >= 0 && idx < sprites.Length && sprites[idx] != null)
                {
                    var tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = sprites[idx];
                    visualTilemap.SetTile(startCell + new Vector3Int(x, -y, 0), tile);
                }
            }
        }

        // Paint collision (1 = wall)
        if (collisionTilemap && coll != null)
        {
            for (int y = 0; y < coll.GetLength(0); y++)
            {
                for (int x = 0; x < coll.GetLength(1); x++)
                {
                    if (coll[y, x] == 1)
                    {
                        // Use same wall sprite index as visuals (1), or blank tile with collider-only
                        var tile = ScriptableObject.CreateInstance<Tile>();
                        tile.sprite = (sprites.Length > 1) ? sprites[1] : null; // wall look
                        collisionTilemap.SetTile(startCell + new Vector3Int(x, -y, 0), tile);
                    }
                }
            }
        }
    }

    static int[,] ParseCsv(string csv, out int width, out int height)
    {
        var lines = csv.Trim().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        height = lines.Length;
        var rows = lines.Select(l => l.Split(',').Select(s => int.Parse(s.Trim())).ToArray()).ToArray();
        width = rows[0].Length;

        var data = new int[height, width];
        for (int y = 0; y < height; y++)
        {
            if (rows[y].Length != width) throw new Exception("CSV width mismatch at row " + y);
            for (int x = 0; x < width; x++) data[y, x] = rows[y][x];
        }
        return data;
    }
}
