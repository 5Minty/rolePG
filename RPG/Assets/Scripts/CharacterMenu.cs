using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text field
    public Text healthText, levelText, headText, weaponUpgradeAmt, xpText;

    private int currentCharacterSelection;
    public Image weaponSelection;
    public Image characterSelectionImage;
    public RectTransform xpBarLevel;

    //Character selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;

            if(currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChange();
        }
    }

    private void OnSelectionChange() //Why private?
    {
        characterSelectionImage.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryWeaponUpgrade())
        {
            UpdateCharacterMenu();
        }
    }

    public void UpdateCharacterMenu()
    {
        healthText.text = GameManager.instance.player.hp.ToString(); //Check
        levelText.text = GameManager.instance.GetCurrentLvl().ToString();
        headText.text = GameManager.instance.head.ToString();
        
        if(GameManager.instance.weapon.weaponLvl == GameManager.instance.weaponPrices.Count) //if the weapon level is equal to the number of weapon prices
        {
            weaponUpgradeAmt.text = "MAX";
        }
        else
        {
            weaponUpgradeAmt.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLvl].ToString();
        }
        
        weaponSelection.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLvl];

        //xp bar
        int currLvl = GameManager.instance.GetCurrentLvl();

        if (currLvl == GameManager.instance.xpTable.Count)
        {
            xpText.text = "MAX";
            xpBarLevel.localScale = Vector3.one;
        }
        else
        {
            int prevXp = GameManager.instance.GetReqXp(currLvl - 1);
            int currXp = GameManager.instance.GetReqXp(currLvl);

            int diff = currXp - prevXp;
            int currXpIntoLvl = GameManager.instance.xp - prevXp;

            float currLvlRatio = (float)currXpIntoLvl / (float)diff;

            xpBarLevel.localScale = new Vector3(currLvlRatio, 1, 1);
            xpText.text = currXpIntoLvl.ToString() + "/" + diff.ToString();
        }

    }
}
