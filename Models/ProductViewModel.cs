using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
namespace Estore.Models
{
    [Serializable]
    public class ProductInCart : Product
    {
        public uint Quantity { get; set; }
        public ProductInCart(DataRow Row) : base(Row){}
    }
    [Serializable]
    public class ProductInPage : Product
    {
        public string DealerDisplayName{get;set;}
        public string ProducterName { get; set; }
        public string Note { get; set; }
        public string[] Images { get; set; }
        public Catergory[] Catergories{get;set;}
        public Tag[] Tags{get;set;}
        public string ShortNote { get; set;}
        public static ProductInPage GetProductInPageByID(uint ID)
        {
            DataTable table = DataProvider.ExcuteQuery("select tProduct.shortNote, tProduct.name as productName, tUser.displayName, tProducter.name as producterName,tProduct.note as note,tProduct.remainderQuantity as remainderQuantity,tProduct.avatar as avatar,oldPrice, tProduct.price,tProduct.isSale from tUser, tProduct, tProducter WHERE tProduct.idProducter = tProducter.id and tUser.id = tProduct.idUser and tProduct.id = @ID",ID);
            if (table.Rows.Count == 0)
            {
                return null;
            }
            ProductInPage product = new ProductInPage()
            {
                Name = table.Rows[0]["productName"].ToString(),
                DealerDisplayName = table.Rows[0]["displayName"].ToString(),
                ProducterName = table.Rows[0]["producterName"].ToString(),
                Note = table.Rows[0]["note"].ToString(),
                RemainderQuantity = (uint)table.Rows[0]["remainderQuantity"],
                Avatar = table.Rows[0]["avatar"].ToString(),
                OldPrice = (uint)table.Rows[0]["oldPrice"],
                Price = (uint)table.Rows[0]["price"],
                IsSale = (bool)table.Rows[0]["isSale"],
                ShortNote = table.Rows[0]["shortNote"].ToString()
            };
            product.Catergories = Catergory.GetCatergoriesByProductID(ID);
            product.Tags = Tag.GetTagsByProductID(ID);
            table = DataProvider.ExcuteQuery("SELECT tProductImage.filePath FROM tProductImage WHERE idProduct = @id", ID);
            product.Images = new string[table.Rows.Count];
            for (int i = 0; i < product.Images.Length; i++)
            {
                product.Images[i] = table.Rows[i][0].ToString();
            }
            return product;
        }
    }
    [Serializable]
    public class Product
    {
        private static int pageLimit = 21;
        public uint ID { get; set; }
        public string Name { get; set; }
        public uint RemainderQuantity { get; set; }
        public string Avatar { get; set; }
        public uint OldPrice { get; set; }
        public uint Price { get; set; }
        public bool IsSale { get; set; }
        public Product(DataRow Row)
        {
            this.ID = (uint)Row["id"];
            this.Name = Row["name"].ToString();
            this.RemainderQuantity = (uint)Row["remainderQuantity"];
            this.Avatar = Row["avatar"].ToString();
            this.OldPrice = (uint) Row["oldPrice"];
            this.Price = (uint) Row["price"];
            this.IsSale = (bool) Row["isSale"];
        }
        public Product(){}
        public static Product GetProductByID(uint ID)
        {
            return new Product(DataProvider.ExcuteQuery("select * from tProduct where id = @id", ID).Rows[0]);
        }
        public static Product[] GetProductsByPageIndex(int PageIndex)
        {
            DataTable table = DataProvider.ExcuteQuery("Select * from tProduct limit @pageIndex , @Limit", PageIndex, pageLimit);
            Product[] products = new Product[table.Rows.Count];
            for (int i = 0; i < products.Length; i++)
            {
                products[i] = new Product(table.Rows[i]);
            }
            return products;
        }
        public static List<ProductInCart> GetProductsInCartByUIntList(List<uint> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }
            int count =0; 
            List<ProductInCart> products = new List<ProductInCart>();
            StringBuilder query = new StringBuilder("select * from tProduct where id =");
            foreach (uint item in list)
            {
                query.Append(" @ID"+count++ +" or id =");
            }
            query.Remove(query.Length -7, 7);
            DataTable productsTable = DataProvider.UIntExecuteQuery(query.ToString(), list);
            foreach (DataRow item in productsTable.Rows)
            {
                ProductInCart productInCart = new ProductInCart(item);
                productInCart.Quantity = (uint)(list.FindAll(ID => ID == productInCart.ID)).Count;
                products.Add(productInCart);
            }
            return products;
        }
        public static Product[] SearchProduct(string KeyWord, int PageIndex)
        {
            if (KeyWord == null)
            {
                return GetProductsByPageIndex(PageIndex);
            }
            DataTable table = DataProvider.ExcuteQuery("select * from tProduct where name like concat('%', @Key ,'%') limit @Page , @Limit", KeyWord, PageIndex, pageLimit);
            Product[] products = new Product[table.Rows.Count];
            for (int i = 0; i < products.Length; i++)
            {
                products[i] = new Product(table.Rows[i]);
            }
            return products;
        }
        public static Product[] GetFavorateProductsByUserID(uint ID, uint pageIndex)
        {
            DataTable table = DataProvider.ExcuteQuery("SELECT * from tFavorateProduct WHERE idUser = @ID limit @pageindex , 21", ID, pageIndex);
            Product[] products = new Product[table.Rows.Count];
            int i =0;
            foreach (DataRow item in table.Rows)
            {
                products[i++] = new Product(item);
            }
            return products;
        }
    }
}