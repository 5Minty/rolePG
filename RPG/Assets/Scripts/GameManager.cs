using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int head;
    public int xp;

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player1 player;
    public Weapon weapon;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GetCurrentLvl();
    }

    public bool TryWeaponUpgrade()
    {
        if(weaponPrices.Count<= weapon.weaponLvl)
        {
            return false;
        }

        if (weaponPrices[weapon.weaponLvl]<= head)
        {
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public int GetCurrentLvl()
    {
        int r = 0;
        int add = 0;

        while(xp >= add)
        {
            add += xpTable[r];
            r++;
            if(r == xpTable.Count)
            {
                return r;
            }
        }

        return r;
    }

    public int GetReqXp(int lvl)
    {
        int r = 0;
        int xp = 0;

        while(r < lvl)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int experience)
    {
        int currLvl = GetCurrentLvl();
        xp += experience;

        if(currLvl < GetCurrentLvl())
        {
            OnLevelUp();
            player.OnLevelUp();
        }
        
    }
    public void OnLevelUp()
    {
        Debug.Log("LEVEL UP!");
        ShowText("LEVEL UP", 30, Color.yellow, player.transform.position, Vector3.one, 2.0f);
    }

    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Save state
    public void SaveState()
    {
        string s = "";

        // 0|11head|12xp|0
        s = "0" + "|";
        s += head.ToString() + "|";
        s += xp.ToString() + "|";
        s += weapon.weaponLvl.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        head = int.Parse(data[1]);
        xp = int.Parse(data[2]);
        if(GetCurrentLvl() != 1)
        {
            player.SetLevel(GetCurrentLvl());
        }
            
        //Change weapon level
        
        weapon.SetWeaponLevel(int.Parse(data[3])); //converts string to int

        instance = this;

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
