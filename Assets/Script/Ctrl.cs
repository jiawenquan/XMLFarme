using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.UI;

public class Ctrl : MonoBehaviour
{
	string xmlSavePath = null;

    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;

    

    public Image ima;

    public Text te1;
    public Text te2;
    public Text te3;




    private Text currentText;
    bool ISADD;
	void Awake()
	{
		xmlSavePath = Application.dataPath + "/Resources/Persons.xml";
	}

	/// <summary>
	/// 向一个节点添加person节点
	/// </summary>
	/// <param name="doc"></param>
	/// <param name="parent"></param>
	/// <param name="person"></param>
	void Add(XmlDocument doc, XmlElement parent, Person person)
	{
		XmlElement e = doc.CreateElement("Person");
		e.SetAttribute("id", person.ID.ToString());
		e.SetAttribute("gender", person.Gender);
		e.InnerText = person.Name;
		parent.AppendChild(e);
	}

	void Start()
	{
        btn1.onClick.AddListener(delegate () { ToCreateItem();});
        btn2.onClick.AddListener(delegate () { ToDeleteItem(); });
        btn3.onClick.AddListener(delegate () { ToSelectItem(); });
        btn4.onClick.AddListener(delegate () { ToChangeItem(); });

     
    }
    void Update()
    {
         
            FollowIng();
            ToDoTry();   
    } 







    void ToCreateItem()
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(dec);
        //根节点
        XmlElement root = doc.CreateElement("Persons");
        doc.AppendChild(root);

        var persons = new Person[] { new Person() { ID = 1, Name = "小夏", Gender = "男" }, new Person() { ID = 2, Name = "惠芬", Gender = "女" } };

        foreach (var content in persons)
        {
            this.Add(doc, root, content);
        }

        doc.Save(xmlSavePath);
        Debug.Log("文件创建成功！");
       

    }
    void ToDeleteItem()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlSavePath);    //加载Xml文件 
        XmlElement root = doc.DocumentElement;   //获取根节点 Persons节点
        XmlNodeList personNodes = root.GetElementsByTagName("Person"); //获取Person子节点集合 
        XmlNode selectNode = root.SelectSingleNode("/Persons/Person[@id='1']"); //返回匹配的第一个节点
        if (selectNode != null)
            root.RemoveChild(selectNode);
        XmlNode selectNode2 = root.SelectSingleNode("/Persons/Person[@id='2']"); //返回匹配的第一个节点
        if (selectNode != null)
            root.RemoveChild(selectNode2);
        Debug.Log("节点删除成功");
        doc.Save(xmlSavePath);
    }
    void ToSelectItem()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlSavePath);
        XmlElement root = doc.DocumentElement;
        XmlNodeList personNodes = root.GetElementsByTagName("Person");
       
        foreach (XmlNode node in personNodes)
        {
            XmlElement ele = (XmlElement)node;
            if (ele.GetAttribute("id") == "2")
            {
                print(ele.InnerText);
                //ele.InnerText = "James";
            }
        }
        Debug.Log("节点查找成功");
        doc.Save(xmlSavePath);
    }
    void ToChangeItem()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlSavePath);
        XmlElement root = doc.DocumentElement;
        XmlNodeList personNodes = root.GetElementsByTagName("Person");
      
        foreach (XmlNode node in personNodes)
        {
            XmlElement ele = (XmlElement)node;
            if (ele.GetAttribute("id") == "1")
            {
               
                ele.InnerText = "小白";                
                Debug.Log("节点修改成功");
            }
        }
        
        doc.Save(xmlSavePath);
    }
    void FollowIng()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlSavePath);
        XmlElement root = doc.DocumentElement;
        XmlNodeList personNodes = root.GetElementsByTagName("Person");
        
        foreach (XmlNode node in personNodes)
        {
            XmlElement ele = (XmlElement)node;
            if (ele.InnerText == "惠芬")
            {
                te1.text = ele.GetAttribute("gender");
            }
            else if (ele.GetAttribute("id")=="1")
            {
                te2.text = ele.InnerText;
            }
        }     
    }
    void ToDoTry()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlSavePath);    //加载Xml文件 
        XmlElement root = doc.DocumentElement;   //获取根节点 Persons节点
        XmlNodeList personNodes = root.GetElementsByTagName("Person"); //获取Person子节点集合 
        XmlNode selectNode = root.SelectSingleNode("/Persons/Person[@id='1']"); //返回匹配的第一个节点
        if (selectNode == null)
            te1.text = "null";
        XmlNode selectNode2 = root.SelectSingleNode("/Persons/Person[@id='2']"); //返回匹配的第一个节点
        if (selectNode == null)
            te2.text = "null";
    }
}
