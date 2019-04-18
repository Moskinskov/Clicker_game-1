using System;
using System.IO;
using System.Xml;
using UnityEngine;

namespace MosMos
{
    public class XMLData
    {
        /// <summary>
        /// the path of Save\Load file
        /// </summary>
        public string SaveLoadPath { get; } = Path.Combine(Application.dataPath, "SaveGame");

        /// <summary>
        /// 'Save' - method.(XML)
        /// </summary>
        /// <param name="data"></param>
        public void Save(SaveData data)
        {
            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("SaveGame");
            xmlDoc.AppendChild(rootNode);

            var element = xmlDoc.CreateElement("Score");
            element.SetAttribute("value", String.Format(data.score.ToString()));
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("ActiveBonusLvl");
            element.SetAttribute("value", data.activeBonusLvl.ToString());
            rootNode.AppendChild(element);

            element = xmlDoc.CreateElement("PassiveBonusLvl");
            element.SetAttribute("value", data.passiveBonusLvl.ToString());
            rootNode.AppendChild(element);

            xmlDoc.Save(SaveLoadPath);
        }
        /// <summary>
        /// 'Load' - method.(XML)
        /// </summary>
        /// <returns></returns>
        public SaveData Load()
        {
            var result = new SaveData();

            using (XmlTextReader reader = new XmlTextReader(SaveLoadPath))
            {
                while (reader.Read())
                {
                    var key = "Score";
                    if (reader.IsStartElement(key))
                        result.score = float.Parse(reader.GetAttribute("value"));

                    key = "ActiveBonusLvl";
                    if (reader.IsStartElement(key))
                        result.activeBonusLvl = Convert.ToInt32(reader.GetAttribute("value"));

                    key = "PassiveBonusLvl";
                    if (reader.IsStartElement(key))
                        result.passiveBonusLvl = Convert.ToInt32(reader.GetAttribute("value"));
                }

                return result;
            }
        }
    }
}