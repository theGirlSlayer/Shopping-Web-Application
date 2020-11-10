using System.Data;
using System;
namespace Estore.Models
{
    [Serializable]
    public class Tag
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public Tag(uint ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public Tag(){}
        public static Tag[] GetTagsByProductID(uint ID)
        {
            DataTable table = DataProvider.ExcuteQuery("SELECT tTag.id,tTag.name from tProduct, tProductRefTag, tTag WHERE tProductRefTag.idProduct = tProduct.id and tTag.id = tProductRefTag.idTag and tProduct.id = @ID", ID);
            Tag[] tags = new Tag[table.Rows.Count];
            for (int i = 0; i < tags.Length; i++)
            {
                tags[i] = new Tag(){
                    ID = (uint) table.Rows[i]["id"],
                    Name = table.Rows[i]["name"].ToString()
                };
            }
            return tags;
        }
    }
}