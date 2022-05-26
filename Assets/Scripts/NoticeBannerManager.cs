using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoticeBannerManager : MonoBehaviour
{
    [SerializeField] private List<NoticeBanner> facts;
    private NoticeBanner selectedFact;

    [SerializeField] private TMP_Text factText;

    // Start is called before the first frame update
    void Start()
    {
        SelectFact();
    }

    //********************randomly choose fact**************************
    public void SelectFact()
    {
        int value = Random.Range(0, facts.Count);
        selectedFact = facts[value];

        factText.text = selectedFact.fact;
    }

}
