using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using HtmlAgilityPack;

namespace GetFileDownload
{
    class Program
    {


        static string output = System.Configuration.ConfigurationManager.AppSettings["output"].ToString();
        static string input = System.Configuration.ConfigurationManager.AppSettings["input"].ToString();
        static string quinaHtml = System.Configuration.ConfigurationManager.AppSettings["quinaHtm"].ToString(); 
        static string quinaTxt = System.Configuration.ConfigurationManager.AppSettings["quinaTxt"].ToString();

        public static void Main(string[] args)
        {


            byte[] result = null;
            byte[] buffer = new byte[4097];



            Uri uri = new Uri("http://www1.caixa.gov.br/loterias/_arquivos/loterias/D_quina.zip");

            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create(uri);
            try
            {
                webReq.CookieContainer = new CookieContainer();
                webReq.Method = "GET";
                MemoryStream oMemory = new MemoryStream();

                WebResponse response = webReq.GetResponse();

                Stream stream = response.GetResponseStream();
                    
                StreamReader reader = new StreamReader(stream);

                int count = 0;

                do
                {
                    count = stream.Read(buffer, 0, buffer.Length);
                    oMemory.Write(buffer,0,count);

                    if (count == 0)
                    {
                        break;
                    }

                } while (true);

                result = oMemory.ToArray();

                FileStream ofile = new FileStream(input, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                ofile.Write(result,0,result.Length);

                ofile.Close();

                oMemory.Close();
                stream.Close();
                extractFile();
                
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));


                excluiZip();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                lerHtmlQuinaResult();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }


        }

        public static void extractFile()
        {

            ZipFile.ExtractToDirectory(input,output);

        }

        public static void excluiZip()
        {

            bool sVerificacao = File.Exists(input);

            if (sVerificacao)
            {
                File.Delete(input);
              
            }

        }

        public static void lerHtmlQuinaResult()
        {
            List<string> listResultFile = new List<string>();
            HtmlDocument doc = new HtmlDocument();

            doc.Load(quinaHtml);
            int count = 0;

            foreach (HtmlNode itemNode in doc.DocumentNode.SelectNodes("//td"))
            {

              
                listResultFile.Add(itemNode.InnerText);

               
               
            }

            string[] array = listResultFile.ToArray();

            StreamWriter outputFile = new StreamWriter(quinaTxt);
            foreach (var item in listResultFile)
            {

               
                if (count == 19)
                {
               
                    outputFile.WriteLine();
                    count = 0;
                }
                else
                {
                  
                    outputFile.Write(item.ToString() + ";");
                    count++;
                }
               
            }

            outputFile.Close();

            Console.ReadLine();

        }
    }
}
