using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    public class Conf
    {
        public string url;
        public string repository;
        public List<Application> applications;
        public List<TypeExtracteurs> typesExtracteurs;

        public Conf()
        {
            url = "";
            repository = "";
            applications = new List<Application>();
        }

        public Conf(string url, string repository, List<Application> applications, List<TypeInfos> mandatoryMetadatas, List<TypeExtracteurs> typesExtracteurs)
        {
            this.url = url;
            this.repository = repository;
            this.applications = applications;
            this.typesExtracteurs = typesExtracteurs;
        }

        public Conf(string url, string repository)
        {
            this.url = url;
            this.repository = repository;
            this.applications = new List<Application>();
            this.typesExtracteurs = new List<TypeExtracteurs>();
        }



        /// <summary>
        /// Enregistre l'état courant de la classe dans un fichier au format XML.
        /// </summary>
        /// <param name="chemin">chemin pour enregistrer le fichier XML (jusqu'au nom du fichier .xml)</param>
        /// <example> 
        /// Mise en oeuvre : 
        /// <code> 
        ///   Metadatas metadatas1 = new Metadatas();
        ///   ...
        ///   metadatas1.Enregistrer("C:\\tmp\\test.xml");
        /// </code> 
        /// </example>
        public void Enregistrer(string chemin)
        {
            //if (!File.Exists(chemin))
            //{
            //    throw new NoPathFoundException();
            //}
            XmlSerializer serializer = new XmlSerializer(typeof(Conf));
            StreamWriter writer = new StreamWriter(chemin);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// déserialise un fichier XML, re donne le fichier XML sous forme de classes C#
        /// </summary>
        /// <param name="chemin">chemin du fichier XML à déserialiser</param>
        /// <returns>retourne la liste des méta données du fichier observé</returns>
        public static Conf Charger(string chemin)
        {
            if (!File.Exists(chemin))
            {
                throw new NoPathFoundException();
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(Conf));
            StreamReader reader = new StreamReader(chemin);
            Conf conf = (Conf)deserializer.Deserialize(reader);
            reader.Close();

            return conf;
        }
        
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
            }
            globalmetadatas.Enregistrer(fileName);
        }

        public string[] extractorsByType (string mimeType)
        {
            string[] extractors = null;
            for (int i = 0; i < this.typesExtracteurs.Count; i++)
            {
                if( this.typesExtracteurs[i].name == mimeType)
                {
                    extractors = new string[typesExtracteurs[i].extracteurs.Count];
                    for (int j = 0; j < typesExtracteurs[i].extracteurs.Count; j++)
                    {
                        extractors[j] = typesExtracteurs[i].extracteurs[j].ToString();
                    }
                    break;
                }
            }
            return extractors;
        }


    }
}
