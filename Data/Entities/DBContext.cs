using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lists.Data.Entities
{
    public class DBContext
    {
        public List<Entities.OnePerson> People { get; set; } = new List<OnePerson>();

        private DBContext() { }

        private static DBContext _instance;
        public static DBContext getInstance()
        {
            if (_instance == null) _instance = new DBContext();
            return _instance;
        }

        public event Action<string> onError;

        public void Load(string fileName)
        {

            try
            {
                XmlSerializer formater =
                    new XmlSerializer(typeof(List<Entities.OnePerson>));

                using (FileStream fs
                    = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    People = (List<Entities.OnePerson>)formater.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                onError(ex.Message);
            }
        }

        public void Save(string fileName)
        {
            try
            {
                XmlSerializer formater =
                    new XmlSerializer(typeof(List<Entities.OnePerson>));

                using (FileStream fs
                    = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formater.Serialize(fs, People);
                }
            }
            catch (Exception ex)
            {
                onError(ex.Message);
            }
        }
    }
}
