using System.Data;
using System;
using System.Security.Cryptography;
using System.Text;
namespace Estore.Models
{
    [Serializable]
    public class AccountInfo : Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Town { get; set; }
        public string Zipcode {get;set;}
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Address { get; set; }
        public AccountInfo(DataRow Row) : base(Row)
        {
            this.Address = Row["address"].ToString();
            this.Email = Row["email"].ToString();
            this.Facebook = Row["facebook"].ToString();
            this.Instagram = Row["instagram"].ToString();
            this.PhoneNumber = "0" + Row["phoneNumber"].ToString();
            this.Twitter = Row["twiter"].ToString();
            this.FirstName = Row["firstName"].ToString();
            this.LastName = Row["lastName"].ToString();
            this.CompanyName = Row["companyName"].ToString();
            this.Town = Row["town"].ToString();
            this.Zipcode = Row["Zipcode"].ToString();
        }
        public AccountInfo(string FirstName,string LastName, string DisplayName, string PhoneNumber, string Email, string Address, string Username) : base(Username, DisplayName)
        {
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public AccountInfo(string FirstName, string LastName, string DisplayName, string PhoneNumber, string Email,string Facebook,string Twitter, string Instagram,string Address, string Username) : base(DisplayName,Username)
        {
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.Facebook = Facebook;
            this.Twitter = Twitter;
            this.Instagram = Instagram;
        }

    }
    [Serializable]
    public class Account 
    {
        public uint ID { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public Account(DataRow Row)
        {
            this.DisplayName = Row["displayName"].ToString();
            this.ID = (uint)Row["id"];
            this.Username = Row["username"].ToString();
        }
        public Account(string Username, string DisplayName)
        {
            this.Username = Username;
            this.DisplayName = DisplayName;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private static string Hash(string s)
        {
            if (s == null)
            {
                s = "";
            }
            return BitConverter.ToString(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", null).ToLower();
        }
        public static Account Login(string Username, string Password)
        {
            DataTable table = DataProvider.ExcuteQuery("select * from tUser where username = @username", Username);
            if (table.Rows.Count == 1)
            {
                if (table.Rows[0]["password"].ToString() == Hash(Username+Password))
                {
                    return new AccountInfo(table.Rows[0]);
                }
            }
            return null;
        }
        public static Account Register(string FirstName, string LastName, string DisplayName, string PhoneNumber, string Email, string Address, string Username,string Password)
        {
            DataProvider.ExcuteNonQuery("insert tUser (firstName,lastName, displayName, phoneNumber, email, address, username, password) values ( @FirstName , @LastName , @DisplayName , @PhoneNumber , @Email , @Address , @Username , @Password );", FirstName,LastName,DisplayName,PhoneNumber,Email,Address,Username,Hash(Username+Password));
            Account account = new AccountInfo(FirstName,LastName,DisplayName,PhoneNumber,Email, Address,Username);
            return account;
        }
        public static Account Update(string FirstName, string LastName, string DisplayName, string PhoneNumber, string Email,string Facebook,string Twitter, string Instagram,string Address, string Username, string Password)
        {
            if (DataProvider.ExcuteNonQuery("update tUser set firstName = @firstname ,lastName = @Lastname , displayName = @displayname , phoneNumber = @phonenumber , email = @email , facebook = @facebook , twiter = @twitter , instagram = @instagram , address = @address where username = @username and password = @password",FirstName,LastName,DisplayName,PhoneNumber,Email,Facebook,Twitter,Instagram,Address,Username, Hash(Username+Password)) == 0)
            {
                return null;
            }
            else
            {
                return new AccountInfo(FirstName,LastName,DisplayName,PhoneNumber, Email, Facebook,Twitter,Instagram,Address,Username);
            }
        }
    }
}