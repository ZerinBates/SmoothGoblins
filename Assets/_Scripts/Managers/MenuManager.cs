using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject _SelectedHeroMenu,_TileObject,_TileUnitObject,EndTurnButton,_Popup;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowPopup(bool show)
    {
        if (show)
        {
            _Popup.SetActive(true);
        }
        else
        {
            _Popup.SetActive(false);
        }
    }
    public void ShowTileInfo(Tile tile)
    {

            if (tile == null || !tile.lit|| !GameManager.Instance.menuDisplay)
            {
                _TileObject.SetActive(false);
                _TileUnitObject.SetActive(false);
                return;
            }
            _TileObject.GetComponentInChildren<Text>().text = tile.TileName;
            _TileObject.SetActive(true);
            if (tile.OccupiedUnit)
            {
                _TileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName + ": " + tile.OccupiedUnit.GetStats().Hp;
                _TileUnitObject.SetActive(true);

            }
        }
    
    public void ShowSelectedHero(UnitBasic h)
    {

            if (h == null||!GameManager.Instance.menuDisplay)
            {
                _SelectedHeroMenu.SetActive(false);
                return;
            }
            _SelectedHeroMenu.GetComponent<DiceCreator>().CreateDice(2);
            // _SelectedHeroMenu.GetComponentInChildren<Text>().text=h.UnitName + ": " + h.GetStats().Hp+"\n m:"+UnitManager.Instance.MovePoint+ " p:" + UnitManager.Instance.ActivePoint;
            _SelectedHeroMenu.GetComponentInChildren<Text>().text = h.UnitName + ": HP " + h.GetStats().Hp + "\n m:" + UnitManager.Instance.MovePoint + " p:" + UnitManager.Instance.ActivePoint +
                "\n" + "Def " + h.GetStats().Durability + "\n" + "Atk " + h.GetStats().Attack;
            _SelectedHeroMenu.SetActive(true);
        }
}
