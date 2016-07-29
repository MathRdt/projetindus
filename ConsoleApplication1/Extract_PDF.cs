using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Extract_PDF
    {
        /// <summary>
        /// //fonction qui extrait un pdf en chaine de caractère
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ExtractTextFromPdf(string path)
        {
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(path);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }

        /// <summary>
        /// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        ///cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static bool SearchTitleInList(string path, string[] MetaDataList)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier

            string Name = name.ToLower(); //met le titre en minuscule

            int i = 0;
            int taille = MetaDataList.Length;
            string nametosearch = " ";
            int result = 0;
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                result = name.IndexOf(nametosearch);//regarde si le mot qu'on cherche est sous chaine du titre
                if (result > 0 || result == 0)
                {
                    cpt++;
                    break;
                }
            }
            if (cpt != 0) return true;
            else return false;
        }

        /// <summary>
        /// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        /// cherche le mot a partir de la lilste des méta-données que l'on connait
        /// </summary>
        /// <param name="path"></param>
        /// <param name="MetaDataList"></param>
        /// <returns></returns>
        public static bool SearchWordInList(string path, string[] MetaDataList)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            int i = 0;
            int taille = MetaDataList.Length;
            string nametosearch = " ";
            int result = 0;
            int cpt = 0;
            for (i = 0; i < taille; i++)
            {
                nametosearch = MetaDataList[i].ToLower();//met le mot qu'on cherche en minuscule
                result = chainepdf.IndexOf(nametosearch);//regarde si le mot qu'on cherche est sous chaine du titre
                if (result > 0 || result == 0)
                {
                    cpt++;
                    break;
                }
            }
            if (cpt != 0) return true;
            else return false;
        }

        /// <summary>
        /// fonction qui regarder si le mot que l'on cherche est dans le titre du fichier retourne true si le mot est dans le titre du pdf
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        public static bool SearchTitle(string path, string NameToSearch)
        {
            FileInfo oFileInfo = new FileInfo(path);
            string name = oFileInfo.Name; //recupere nom fichier

            string Name = name.ToLower(); //met le titre en minuscule
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule

            int result = name.IndexOf(nametosearch);//regarde si le mot qu'on cherche est sous chaine du titre

            if (result > 0 || result == 0) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
        }

        /// <summary>
        /// fonction qui va regarder si le mot qu'on cherche est dans le pdf retourne true si le mot est dans le pdf
        /// </summary>
        /// <param name="path"></param>
        /// <param name="NameToSearch"></param>
        /// <returns></returns>
        public static bool SearchWord(string path, string NameToSearch)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            string nametosearch = NameToSearch.ToLower();//met le mot qu'on cherche en minuscule

            int result = chainepdf.IndexOf(nametosearch); //regarde si le mot qu'on cherche est sous-chaine du string pdf

            if (result > 0 || result == 0) return true; //si oui retourne vrai
            else return false; //sinon retourne faux
        }

        /// <summary>
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SearchName(string path)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            int result = chainepdf.IndexOf("nom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = " ";

            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainepdf[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chainepdf[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chainepdf[result + k] == ' ' || chainepdf[result + k] == '\n' || chainepdf[result + k] == '\r' || chainepdf[result + k] == '\t') break; //trouve fin du nom
                }
                name = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

            }
            else name = "nom non trouvé";

            return name;
        }

        /// <summary>
        /// fonction qui cherche une méta-données en particulier
        /// fonction qui cherche le nom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <param name="metaData"></param>
        /// <returns></returns>
        public static string SearchMetaData(string path, string metaData)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            int result = chainepdf.IndexOf(metaData); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string name = " ";

            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainepdf[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chainepdf[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chainepdf[result + k] == ' ' || chainepdf[result + k] == '\n' || chainepdf[result + k] == '\r' || chainepdf[result + k] == '\t') break; //trouve fin du nom
                }
                name = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

            }
            else name = "nom non trouvé";

            return name;
        }

        /// <summary>
        /// fonction qui cherche le prénom dans le document
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SearchSurname(string path)
        {
            string chainePdf = ExtractTextFromPdf(path); //extrait le pdf en chaine de caractère
            string chainepdf = chainePdf.ToLower();//met le pdf en minuscule
            int result = chainepdf.IndexOf("prénom"); //regarde si le mot qu'on cherche est sous-chaine du string pdf
            string surname = " ";
            if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
            {
                int i = 0;
                for (i = 0; i < 20; i++)
                {
                    if (chainepdf[result + i] == ':') break;
                }
                int j = 0;
                for (j = i + 1; j < 20; j++)
                {
                    if (chainepdf[result + j] != ' ') break; //trouve le début du nom
                }
                int k = 0;
                for (k = j + 1; k < 20; k++)
                {
                    if (chainepdf[result + k] == ' ' || chainepdf[result + k] == '\n' || chainepdf[result + k] == '\r' || chainepdf[result + k] == '\t') break; //trouve fin du nom
                }
                surname = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

            }
            else
            {
                result = chainepdf.IndexOf("prenom"); //test si prénom est écrit sans accent
                if (result > 0 || result == 0) //on cherche le nom si le mot nom est présent dans le document
                {
                    int i = 0;
                    for (i = 0; i < 20; i++)
                    {
                        if (chainepdf[result + i] == ':') break;
                    }
                    int j = 0;
                    for (j = i + 1; j < 20; j++)
                    {
                        if (chainepdf[result + j] != ' ') break; //trouve le début du nom
                    }
                    int k = 0;
                    for (k = j + 1; k < 20; k++)
                    {
                        if (chainepdf[result + k] == ' ' || chainepdf[result + k] == '\n' || chainepdf[result + k] == '\r' || chainepdf[result + k] == '\t') break; //trouve fin du nom
                    }
                    surname = chainepdf.Substring(result + j, k - j + 1); //extrait le nom

                }
                else surname = "prenom non présent";

            }

            return surname;
        }
    }
}
