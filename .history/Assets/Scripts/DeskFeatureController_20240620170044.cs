using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskFeatureController : MonoBehaviour
{
    private GameObject materials11;
    private GameObject materials12;
    private GameObject materials2;
    private GameObject cup1;
    private GameObject glassRod1;
    private GameObject materials4;
    private GameObject materials5;
    private GameObject materials6;
    private GameObject egg;
    private GameObject pigments;
    private GameObject cup2;
    private GameObject cupColer;
    private GameObject cupLiquid;
    private GameObject cupEggs;
    private GameObject cupResin;
    private GameObject glassRod2;
    private GameObject eggFeature;
    private GameObject eggEgg1;
    private GameObject eggEgg2;
    private GameObject eggEgg3;
    private GameObject eggEgg6;
    private GameObject eggEgg7;
    private GameObject pigmentsFeature;

    // Start is called before the first frame update
    void Start()
    {
        materials11 = transform.Find("materials1").Find("materials11").gameObject;
        materials12 = transform.Find("materials1").Find("materials12").gameObject;
        materials2 = transform.Find("materials2").gameObject;
        cup1 = transform.Find("materials3").Find("cup1").gameObject;
        glassRod1 = transform.Find("materials3").Find("glass rod1").gameObject;
        materials4 = transform.Find("materials4").gameObject;
        materials5 = transform.Find("materials5").gameObject;
        materials6 = transform.Find("materials6").gameObject;
        egg = transform.Find("egg").gameObject;
        pigments = transform.Find("pigments").gameObject;
        cup2 = transform.Find("cup2").gameObject;
        cupLiquid = cup2.transform.Find("liquid").gameObject;
        cupEggs = cup2.transform.Find("eggs").gameObject;
        cupResin = cup2.transform.Find("resin").gameObject;
        cupColer = cup2.transform.Find("color").gameObject;
        glassRod2 = transform.Find("glass rod2").gameObject;
        eggFeature = egg.transform.Find("egg Feature").gameObject;
        eggEgg1 = eggFeature.transform.Find("egg1").gameObject;
        eggEgg2 = eggFeature.transform.Find("egg2").gameObject;
        eggEgg3 = eggFeature.transform.Find("egg3").gameObject;
        eggEgg6 = eggFeature.transform.Find("egg6").gameObject;
        eggEgg7 = eggFeature.transform.Find("egg7").gameObject;
        pigmentsFeature = pigments.transform.Find("pigments Feature").gameObject;
    
        materials4.SetActive(false);
        materials5.SetActive(false);
        materials6.SetActive(false);
        egg.SetActive(false);
        pigments.SetActive(false);
        cup2.SetActive(false);
        cupLiquid.SetActive(false);
        cupEggs.SetActive(false);
        cupResin.SetActive(false);
        cupColer.SetActive(false);
        glassRod2.SetActive(false);
        eggFeature.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
