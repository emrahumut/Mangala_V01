using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    public int energy;
    public Text energyText;
    public Text timer;
    private int gainTime = 5;
    public void Awake()
    {
        if (PlayerPrefs.HasKey("Energy"))
        {
            energy = PlayerPrefs.GetInt("Energy");
        }
        else
        {
          
            PlayerPrefs.SetInt("Energy",energy);
        }

        StartCoroutine(CheckEnergyGain());
        UIUpdateEnergy();
    }

    public IEnumerator CheckEnergyGain()
    {
        while (PlayerPrefs.HasKey("EnergyGainTime"))
        {
            if (DateTime.Now > DateTime.Parse(PlayerPrefs.GetString("EnergyGainTime"),CultureInfo.InvariantCulture))
            {
                AddEnergy(1);
                if (!ChechEnergyMax())
                {
                    PlayerPrefs.SetString("EnergyGainTime", DateTime.Now.AddSeconds(gainTime).ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    PlayerPrefs.DeleteKey("EnergyGainTime");
                }
            }

            UIUpdateEnergy();
            yield return new WaitForSeconds(1);
        }
    }
    public void UseEnergy(int count)
    {
        if (!CheckEnergyMin())
        {
            energy -= count;
        }
        PlayerPrefs.SetInt("Energy",energy);
        if (!PlayerPrefs.HasKey("EnergyGainTime"))
        {
            PlayerPrefs.SetString("EnergyGainTime", DateTime.Now.AddSeconds(gainTime).ToString(CultureInfo.InvariantCulture));
        }
        UIUpdateEnergy();
    }

    public void AddEnergy(int count)
    {
        if (!ChechEnergyMax())
        {
            energy += count;
            UIUpdateEnergy();
        }
        PlayerPrefs.SetInt("Energy",energy);
    }

    public bool CheckEnergyMin()
    {
        return energy == 0;
    }

    public bool ChechEnergyMax()
    {
        return energy == 5;
    }

    public void UIUpdateEnergy()
    {
        if(energyText) energyText.text = "Enerjin: " + energy + " / " + 5;
        if (timer)
        {
            if (PlayerPrefs.HasKey("EnergyGainTime") && !ChechEnergyMax())
            {
                timer.text = "Yeni enerji: " + Math
                    .Ceiling((System.DateTime.Parse(PlayerPrefs.GetString("EnergyGainTime"),
                        CultureInfo.InvariantCulture) - DateTime.Now).TotalSeconds)
                    .ToString(CultureInfo.InvariantCulture);
            }

            if (timer.text == "Yeni enerji: 0")
            {
                timer.text = "";
            }
        }
    }
    
}
