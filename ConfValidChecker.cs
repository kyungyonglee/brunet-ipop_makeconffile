using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Schema;


namespace ConfManager
{
    class Run
    {
        static void Main(String[] args)
        {
			bool success;
			ConfValidCheck checkConf = new ConfValidCheck();
			try{
				success = checkConf.confFileValidCheck(args[0], args[1]);
			}catch(Exception x){
				Console.WriteLine("Usage : ConfValidChecker.exe [configuration file path] [schema file path]");
				return;
			}
			
			Console.WriteLine(success);

        }
    }

    class ConfValidCheck
    {
		private bool Succeed;
		
		public bool confFileValidCheck(String confPath, String schemaPath)
		{
			XmlReader validator;

			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ValidationType = ValidationType.Schema;
	
			XmlSchemaSet schemas = new XmlSchemaSet();
			settings.Schemas = schemas;
			try{
				schemas.Add(null, schemaPath);
			}catch(Exception x){
				Console.WriteLine("Invalid schema file path.. check first");
				return false;
			}

			settings.ValidationEventHandler += ValidCheckEventHandler;
			
			try{
				 validator = XmlReader.Create(confPath, settings);
			}catch(Exception x){
				Console.WriteLine("Invalid xml file path. check first");
				return false;
			}

			try {
				while (validator.Read()) { }
			} catch (XmlException err) {
			} finally {
				validator.Close();
			}

			return Succeed;
		}
		
		private void ValidCheckEventHandler(object sender, ValidationEventArgs args) {
			Console.WriteLine("Validation error: " + args.Message);
			Succeed = false;
		}

		public ConfValidCheck(){
			Succeed = true;
		}

    }
}



