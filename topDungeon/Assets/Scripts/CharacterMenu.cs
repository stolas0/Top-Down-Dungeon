using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }  

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgrateClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Update The Character Info
    public void UpdateMenu()
    {
        var currLevel = GameManager.instance.GetCurrentLevel();
        //Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = currLevel.ToString();

        //Weapon
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        //xp Bar
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);
            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + "/" + diff;
        }

    }
}
