using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Using Singleton Design Pattern to reach this script everywhere.
    public static ShopManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Variables Defined.
    public GameObject whiteBall;
    public GameObject redBall;
    public GameObject blueBall;
    public GameObject greenBall;
    public GameObject yellowBall;
    public GameObject pinkBall;
    public GameObject orangeBall;
    public Material ballMaterial;
    private bool isWhiteBall = false;
    private bool isRedBall = false;
    private bool isBlueBall = false;
    private bool isGreenBall = false;
    private bool isYellowBall = false;
    private bool isPinkBall = false;
    private bool isOrangeBall = false;
    public PlayerController player;

    public void Start()
    {
        SetBallPlayerPrefs();
    }

    // Open Shop Window OnClick
    public void ShopUI()
    {
        GameManager.instance.shopUI.SetActive(true);
    }

    // Return To Game Menu OnClick
    public void ShopBackButton()
    {
        GameManager.instance.shopUI.SetActive(false);
    }

    // Defining ball skins on shop for keeping on HDD. 
    public void SetBallPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("isWhiteBall"))
        {
            PlayerPrefs.SetInt("isWhiteBall", isWhiteBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isWhiteBall") == 1 ? true : false)
        {
            whiteBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isRedBall"))
        {
            PlayerPrefs.SetInt("isRedBall", isRedBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isRedBall") == 1 ? true : false)
        {
            redBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isBlueBall"))
        {
            PlayerPrefs.SetInt("isBlueBall", isBlueBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isBlueBall") == 1 ? true : false)
        {
            blueBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isGreenBall"))
        {
            PlayerPrefs.SetInt("isGreenBall", isGreenBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isGreenBall") == 1 ? true : false)
        {
            greenBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isYellowBall"))
        {
            PlayerPrefs.SetInt("isYellowBall", isYellowBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isYellowBall") == 1 ? true : false)
        {
            yellowBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isPinkBall"))
        {
            PlayerPrefs.SetInt("isPinkBall", isPinkBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isPinkBall") == 1 ? true : false)
        {
            pinkBall.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("isOrangeBall"))
        {
            PlayerPrefs.SetInt("isOrangeBall", isOrangeBall ? 1 : 0);
        }

        if (PlayerPrefs.GetInt("isOrangeBall") == 1 ? true : false)
        {
            orangeBall.SetActive(true);
        }
    }

    // Change the ball skin to Default (black)
    public void BuyDefaultBall()
    {
        ballMaterial.color = Color.black;
        SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
    }


    // Change the ball skin color to White 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyWhiteBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isWhiteBall") == 0 ? true : false)
            {
                isWhiteBall = true;
                PlayerPrefs.SetInt("isWhiteBall", isWhiteBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isWhiteBall") == 1 ? true : false)
        {
            whiteBall.SetActive(true);
            ballMaterial.color = new Color(1, 1, 1, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Red 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyRedBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isRedBall") == 0 ? true : false)
            {
                isRedBall = true;
                PlayerPrefs.SetInt("isRedBall", isRedBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isRedBall") == 1 ? true : false)
        {
            redBall.SetActive(true);
            ballMaterial.color = new Color(1, 0, 0, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Blue 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyBlueBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isBlueBall") == 0 ? true : false)
            {
                isBlueBall = true;
                PlayerPrefs.SetInt("isBlueBall", isBlueBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isBlueBall") == 1 ? true : false)
        {
            blueBall.SetActive(true);
            ballMaterial.color = new Color(0, 1, 1, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Green 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyGreenBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isGreenBall") == 0 ? true : false)
            {
                isGreenBall = true;
                PlayerPrefs.SetInt("isGreenBall", isGreenBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isGreenBall") == 1 ? true : false)
        {
            greenBall.SetActive(true);
            ballMaterial.color = new Color(0, 1, 0, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Yellow 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyYellowBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isYellowBall") == 0 ? true : false)
            {
                isYellowBall = true;
                PlayerPrefs.SetInt("isYellowBall", isYellowBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isYellowBall") == 1 ? true : false)
        {
            yellowBall.SetActive(true);
            ballMaterial.color = new Color(1, 1, 0, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Pink 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyPinkBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isPinkBall") == 0 ? true : false)
            {
                isPinkBall = true;
                PlayerPrefs.SetInt("isPinkBall", isPinkBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isPinkBall") == 1 ? true : false)
        {
            pinkBall.SetActive(true);
            ballMaterial.color = new Color(1, 0, 1, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }

    // Change the ball skin color to Orange 
    // Checks If The Player has already bought the skin before. If not, purchase it and keep it on HDD.
    public void BuyOrangeBall()
    {
        if (PlayerPrefs.GetInt("DiamondScore") >= 100)
        {
            if (PlayerPrefs.GetInt("isOrangeBall") == 0 ? true : false)
            {
                isOrangeBall = true;
                PlayerPrefs.SetInt("isOrangeBall", isOrangeBall ? 1 : 0);
                PlayerPrefs.SetInt("DiamondScore", PlayerPrefs.GetInt("DiamondScore") - 100);
            }
        }

        if (PlayerPrefs.GetInt("isOrangeBall") == 1 ? true : false)
        {
            orangeBall.SetActive(true);
            ballMaterial.color = new Color(1, 0.5f, 0, 1);
            SoundManager.Instance.PlaySound(SoundManager.Instance.multipleScoreSound, 1f);
        }
    }
}