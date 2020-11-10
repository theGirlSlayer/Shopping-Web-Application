using System.Data;
using System;
namespace Estore.Models
{
    [Serializable]
    public class Catergory
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public Catergory(uint ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public Catergory(){}

        public static Catergory[] GetCatergoriesByProductID(uint ID)
        {
            DataTable table = DataProvider.ExcuteQuery("SELECT tCatergory.id,tCatergory.name from tProduct, tProductRefCatergory, tCatergory WHERE tProductRefCatergory.idProduct = tProduct.id and tCatergory.id = tProductRefCatergory.idCatergory and tProduct.id = @ID", ID);
            Catergory[] catergories = new Catergory[table.Rows.Count];
            for (int i = 0; i < catergories.Length; i++)
            {
                catergories[i] = new Catergory(){
                    ID = (uint) table.Rows[i]["id"],
                    Name = table.Rows[i]["name"].ToString()
                };
            }
            return catergories;
        }
    }
}