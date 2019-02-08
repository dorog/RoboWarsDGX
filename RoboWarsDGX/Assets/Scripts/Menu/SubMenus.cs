using UnityEngine;

public class SubMenus : MonoBehaviour
{
    private GameObject choosedMenu = null;

    public void ShowSubMenu(GameObject subMenu)
    {
        if(choosedMenu == subMenu)
        {
            choosedMenu.SetActive(false);
            return;
        }
        else if(choosedMenu != null)
        {
            choosedMenu.SetActive(false);
        }
        choosedMenu = subMenu;
        subMenu.SetActive(true);
    }
}
