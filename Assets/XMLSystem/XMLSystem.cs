using System.Collections.Generic;
using System.IO;
using System.Xml;
using Bullet;
using Enemy;
using Gun;
using UnityEngine;

namespace XMLSystem
{
    public class XMLSystem : IXMLSystem
    {
        private const string PrefsKey = "User";
        
        private readonly XMLConfig _xmlConfig;
        private readonly string _savePath;

        private const string SaveNodeName = "save_file";

        public XMLSystem(XMLConfig xmlConfig)
        {
            _xmlConfig = xmlConfig;
            _savePath = GenerateSavePath();

        }

        public void CreatXMLFile()
        {
            var xmlDoc = new XmlDocument();
            var rootNode = xmlDoc.CreateElement(SaveNodeName);
            xmlDoc.AppendChild(rootNode);
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        public string LoadFromXML(string key, string value)
        {
            var loadPath = _savePath + _xmlConfig.SaveName;
            if (!File.Exists(loadPath))
            {
                Debug.LogWarning("Save file doesn't exist at load path, player data unchanged!");
                return null;
            }
            
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(loadPath);

            var node = xmlDoc.SelectSingleNode($"/{SaveNodeName}/{key}");
            if (node == null)
            {
                return null;
            }
    
            return node.Attributes[value].Value;
        }

        public void SaveHealthToXML(string health)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_savePath + _xmlConfig.SaveName);
            var rootNode = xmlDoc.DocumentElement;

            var elem = xmlDoc.CreateElement("PlayerHealth");
            elem.SetAttribute("value", health);
            rootNode.AppendChild(elem);
            
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        public void SaveEnemyCountToXML(List<EnemyView> list)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_savePath + _xmlConfig.SaveName);
            var rootNode = xmlDoc.DocumentElement;

            for (int i = 0; i < list.Count; i++)
            {
                var elem = xmlDoc.CreateElement(list[i].EnemyType.ToString()+i.ToString());
                elem.SetAttribute("health", list[i].Health.ToString());
                rootNode.AppendChild(elem);
            }

            xmlDoc.Save(_savePath + _xmlConfig.SaveName); 
        }

        public void SaveGunInBackPackToXML(List<GunView> list)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_savePath + _xmlConfig.SaveName);
            var rootNode = xmlDoc.DocumentElement;

            for (int i = 0; i < list.Count; i++)
            {
                var elem = xmlDoc.CreateElement("gun" + i.ToString());
                elem.SetAttribute("type", list[i].GetGunType().ToString());
                rootNode.AppendChild(elem);
            }
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        public void SaveBulletToXML(CellView cellView)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(_savePath + _xmlConfig.SaveName);
            var rootNode = xmlDoc.DocumentElement;
            
            var elem = xmlDoc.CreateElement("bullet");
            elem.SetAttribute("count", cellView.Count.text);
            rootNode.AppendChild(elem);
            
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        private string GenerateSavePath()
        {
            var computerPath = Application.dataPath + _xmlConfig.SourcePath;
            var phonePath = Application.persistentDataPath;
        
#if UNITY_ANDROID
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
#elif UNITY_IPHONE
        phonePath += "/";
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
#elif UNITY_EDITOR
            computerPath = Application.dataPath;
            computerPath = computerPath.Remove(computerPath.IndexOf("/Assets"), 7);
            computerPath += _xmlConfig.SourcePath;
            if (!Directory.Exists(computerPath))
            {
                Directory.CreateDirectory(computerPath);
            }
            return computerPath;
#else
            if (!Directory.Exists(computerPath))
            {
                Directory.CreateDirectory(computerPath);
            }
            return computerPath;
#endif
        }
    }
}