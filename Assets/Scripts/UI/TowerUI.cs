using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    const float MIN_SCALE = 0.4f;

    Transform copy;

    public Tower prefab;
    public Transform image;
    public TMP_Text text;
    public int cost;

    void Start() => text.text = "Cost: " + cost.ToString();

    public void OnDrag(PointerEventData data)
    {
        copy.localScale = Vector3.one * GetCurrentScale();
        copy.position += new Vector3(data.delta.x, data.delta.y);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        copy = Instantiate(image, transform);
        Destroy(copy.GetComponent<TowerUI>());
        copy.transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        Destroy(copy.gameObject);
        if (GameManager.Instance.Money < cost)
            return;
        Tile tile = Grid.Instance.HoveredTile;
        if (!tile) return;
        GameManager.Instance.Money -= cost;
        tile.SetTower(prefab);
    }

    float GetCurrentScale()
    {
        float distanceTraveled = Vector2.Distance(transform.position, copy.transform.position);
        return Mathf.Lerp(1f, MIN_SCALE, distanceTraveled / 125f);
    }

    void OnValidate() => text = transform.GetComponentInChildren<TMP_Text>();
}
