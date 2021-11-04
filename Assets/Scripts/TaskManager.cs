using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TaskManager : MonoBehaviour
{
    [SerializeField] TextAsset textJson;
    [SerializeField] GameObject tabPrefab;
    [SerializeField] GameObject contentPrefab;
    [SerializeField] GameObject topPanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] List<Button> btnIndex;
    [SerializeField] GameObject popupPanel;
    [SerializeField] int a = 0;

    [System.Serializable]
    public class Tabs
    {
        public int id;
        public string title;
        public int index;
        public bool enebled;
        public List<Content> cont;
    }

    [System.Serializable]
    public class Content
    {
        public int tabId;
        public string name;
        public string message;
        public Color color;
        
    }

    [System.Serializable]
    public class GetDatatabs
    {
        public Tabs[] tabs;
    }

    [System.Serializable]
    public class GetDatacontent
    {
        public Content[] content;
    }

    public GetDatatabs infoListTab = new GetDatatabs();
    public GetDatacontent infoListContent = new GetDatacontent();

    public void Start()
    {
        infoListTab = JsonUtility.FromJson<GetDatatabs>(textJson.text);
        infoListContent = JsonUtility.FromJson<GetDatacontent>(textJson.text);
        SpawnTabs();
        SetContenuToTab();
        Spawn();
    }

    void SpawnTabs()
    {
        for (int i = 0; i < infoListTab.tabs.Length; i++)
        {
            GameObject btn = Instantiate(tabPrefab, topPanel.transform, false) as GameObject;
            btn.name = infoListTab.tabs[i].title;
            btn.GetComponentInChildren<Text>().text = infoListTab.tabs[i].title;
        }
    }

    void SetContenuToTab()
    {
        for (var i = 0; i < infoListTab.tabs.Length; i++)
        {
            for (var k = 0; k < infoListContent.content.Length; k++)
            {
                if (infoListContent.content[k].tabId == infoListTab.tabs[i].id)
                {
                    infoListTab.tabs[i].cont.Add(infoListContent.content[k]);
                }
            }
        }
    }

    void Spawn()
    {
        GameObject[] btn = GameObject.FindGameObjectsWithTag("button");
        for (int i = 0; i < btn.Length; i++)
        {
            btnIndex.Add(btn[i].GetComponent<Button>());
            EnableDisableBtn();
            AddMethodeButton();
        }
    }

    void EnableDisableBtn()
    {
        for (int i = 0; i < btnIndex.Count; i++)
        {
            if (infoListTab.tabs[i].cont.Count == 0)
            {
                btnIndex[i].enabled = false;
            }
        }
    }
    void AddMethodeButton()
    {
        for (int i = 0; i < btnIndex.Count; i++)
        {
           btnIndex[i].onClick.AddListener(TestVerif);
        }
    }
    
     void TestVerif()
    {
        for (int i = 0; i < infoListTab.tabs.Length; i++)
        {
            if ((a < infoListTab.tabs[i].cont.Count)&&(infoListTab.tabs[i].title==btnIndex[i].name))
            {
                GameObject btn = Instantiate(contentPrefab, mainPanel.transform, false) as GameObject;
                btn.name = infoListTab.tabs[i].cont[a].name;
                btn.GetComponentInChildren<Text>().text = infoListTab.tabs[i].cont[a].name;
                btn.gameObject.GetComponent<Image>().color = infoListTab.tabs[i].cont[a].color;
                a = a + 1;
                TestVerif();
            }

        }
       
    }

     void SetContenu()
     {
        
         
     }
}


