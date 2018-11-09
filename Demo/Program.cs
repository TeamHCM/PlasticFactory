using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection conn;
            //string Path = Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "") + @"\App.config";
            //XmlElement root;
            //XmlDocument doc = new XmlDocument();
            //doc.Load(Path);
            //root = doc.DocumentElement;
            //XmlNode node = doc.DocumentElement;
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string connectstring = string.Format("metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework&quot;","abc","123","456","789");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = config.ConnectionStrings.ConnectionStrings["PlasticFactoryEntities"].ConnectionString;
            conn.Open();
            Console.WriteLine(conn.State);
            //Console.WriteLine(System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString);
            //config.ConnectionStrings.ConnectionStrings["PlasticFactoryEntities"].ProviderName = "System.Data.EntityClient";
            //config.Save(ConfigurationSaveMode.Modified);
            Console.ReadKey();
            //Console.WriteLine(node.ChildNodes.Item(2).ChildNodes.Item(0).Attributes.Item(1).Value);

    }

    }
}
