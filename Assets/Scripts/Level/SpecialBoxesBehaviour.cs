using SuperAbilityMario.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpecialBoxesBehaviour : MonoBehaviour, IInteractable
{

    [Serializable]
    private struct TileData
    {
        public Vector3Int pos;
        public PowerUp content;
    }

    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private TileBase UsedSpecialBoxTile;
    [SerializeField] private float paddingSpace;
    [SerializeField] private float animatedYPos = 0.1f;
    [SerializeField] private float animatedSpeed;

    [SerializeField] private List<TileData> tileData = new List<TileData>();

    public void Interact(Character character, Vector2 pos)
    {
        if (character.transform.position.y < transform.position.y)
        {
            pos.y += paddingSpace;
            Vector3Int cellPos = Tilemap.WorldToCell(pos);
            TileBase cellTile = Tilemap.GetTile(cellPos);
            
            // Check if there is no tile where we're interacting
            if (cellTile == null || cellTile == UsedSpecialBoxTile)
            {
                return;
            }
            
            Tilemap.SetTile(cellPos, UsedSpecialBoxTile);
            StartCoroutine(AnimateTile(cellPos));
        }
    }

    private IEnumerator AnimateTile(Vector3Int pos)
    {
        float yAnim = 0;
        Matrix4x4 matrix;
        while (yAnim < animatedYPos)
        {
            yAnim += Time.deltaTime * animatedSpeed;
            yAnim = Mathf.Clamp(yAnim, 0, animatedYPos);
            matrix = Matrix4x4.TRS(new Vector2(0, yAnim), Quaternion.identity, Vector3.one);
            Tilemap.SetTransformMatrix(pos, matrix);
            yield return null;
        }

        SpawnBoxContent(pos);

        while (yAnim > 0)
        {
            yAnim -= Time.deltaTime * animatedSpeed;
            yAnim = Mathf.Clamp(yAnim, 0, animatedYPos);
            matrix = Matrix4x4.TRS(new Vector2(0, yAnim), Quaternion.identity, Vector3.one);
            Tilemap.SetTransformMatrix(pos, matrix);
            yield return null;
        }
        yAnim = 0;
        matrix = Matrix4x4.TRS(new Vector2(0, yAnim), Quaternion.identity, Vector3.one);
        Tilemap.SetTransformMatrix(pos, matrix);
    }

    private void SpawnBoxContent(Vector3Int cell)
    {
        for (int i = 0; i < tileData.Count; i++)
        {
            if (tileData[i].pos == cell)
            {
                Instantiate(tileData[i].content, Tilemap.CellToWorld(cell) + new Vector3(0.08f, 0.08f), Quaternion.identity);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        string tileDataName;
        for(int i = 0; i < tileData.Count; i++)
        {
            Handles.color = Color.yellow;
            tileDataName = "(" + i + ") - ";
            if (tileData[i].content == null)
                tileDataName += "N/A";
            else
                tileDataName += tileData[i].content.gameObject.name.ToString();
            Handles.Label(Tilemap.CellToWorld(tileData[i].pos), tileDataName);
        }
    }

    [ContextMenu("Refresh Data")]
    private void OnValidate()
    {
        // Find all tiles in the map
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            bool tileExists = Tilemap.HasTile(localPlace);

            // Check if we don't have the tile yet
            if (tileData.Exists(x => x.pos == pos))
            {
                // Skip existing tiles
                if (tileExists) continue;

                // Remove non-existing tile
                tileData.RemoveAll(x => x.pos == pos);
            }
            else
            {
                // Skip non-existing tiles
                if (!tileExists) continue;

                // Add new tile
                var tile = new TileData
                {
                    pos = pos
                };
                tileData.Add(tile);
            }
        }

        // Delete duplicate tile data
        for(int i = 0; i < tileData.Count; i++)
        {
            for(int x = 0; x < tileData.Count; x++)
            {
                if (x == i)
                    continue;

                if (tileData[i].Equals(tileData[x]))
                {
                    x--;
                    tileData.RemoveAt(x);
                }
            }
        }
    }
}
