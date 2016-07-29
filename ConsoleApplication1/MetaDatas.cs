using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication1
{

   

    /// <summary>
    /// Strucuture d'une méta donnée
    /// le Type défini le type de la méta donnée
    /// la Valeur donne la valeur affectée à cette méta donnée
    /// </summary>
    [Serializable]
    public class MetaData
    {
        public string type { get; set; }
        public object value { get; set; }
        [XmlIgnore]
        public List<string> keyWords { get; set; }
    }


    /// <summary>
    /// donne la liste des méta données obtenues pour un fichier
    /// l'attribut Mandatory donne la liste des meta données obligatoires pour ce type de document
    /// l'attribut Optional donne la liste des meta données obligatoires pour ce type de document
    /// </summary>
    [Serializable()]
    public class MetaDatas
    {
        public List<MetaData> Mandatory { get; set; }
        public List<MetaData> Optional { get; set; }

        public MetaDatas()
        {
            this.Mandatory = new List<MetaData>();
            this.Optional = new List<MetaData>();
        }

        /// <summary>
        /// ajoute une nouvelle méta donnée à la liste des méta données déjà existante
        /// </summary>
        /// <param name="fileName">nom du fichier XML à modifier</param>
        /// <param name="typeMetaData"> le type de la nouvelle meta donnée </param>
        /// <param name="valueMetaData">la valeur affectée à cette nouvelle méta donnée</param>
        /// <param name="mandatory"> cette méta donnée est-elle obligatoire ou non</param>
        public void addMetaData(string fileName, string typeMetaData, object valueMetaData, Boolean mandatory)
        {
            GlobalMetaDatas globalmetadatas = GlobalMetaDatas.Charger(fileName);
            MetaDatas metadatas = globalmetadatas.metadatas;

            if (mandatory == true)
                metadatas.Mandatory.Add(new MetaData() { type = typeMetaData, value = valueMetaData });

            else
                metadatas.Optional.Add(new MetaData() { type = typeMetaData, value = valueMetaData });

            globalmetadatas.Enregistrer(fileName);
        }

        /// <summary>
        /// remplace la valeur d'une méta donnée déjà exsistante par une nouvelle valeur
        /// </summary>
        /// <param name="fileName">fichier XML à modifier</param>
        /// <param name="typeMetaData">la méta donnée à modifier</param>
        /// <param name="valueMetaData">la nouvelle valeur à prendre</param>
        /// <param name="mandatory">méta donnée obligatoire ou non</param>

        public void changeMetaData(string fileName, string typeMetaData, object valueMetaData, Boolean mandatory)
        {
            int i = 0;

            GlobalMetaDatas globalmetadatas = GlobalMetaDatas.Charger(fileName);
            MetaDatas metadatas = globalmetadatas.metadatas;

            if (mandatory == true)
            {
                for (i = 0; i < metadatas.Mandatory.Count; i++)
                {
                    if (metadatas.Mandatory[i].type == typeMetaData)
                    {
                        metadatas.Mandatory[i].value = valueMetaData;
                        break;
                    }
                }

                //if (i == metadatas.Mandatory.Count)
                //    throw new NoPathFoundException();
            }

            else
            {
                for (i = 0; i < metadatas.Optional.Count; i++)
                {
                    if (metadatas.Optional[i].type == typeMetaData)
                    {
                        metadatas.Optional[i].value = valueMetaData;
                        break;
                    }
                }
                //if (i == metadatas.Optional.Count)
                //    throw new NoPathFoundException();
            }

            globalmetadatas.Enregistrer(fileName);
        }
    }
}
