using MySqlConnector;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
public sealed class DataProvider
{
    public static byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);
        return ms.ToArray();
    }
    public static object ByteArrayToObject(byte[] arrBytes)
    {
        if (arrBytes == null)
        {
            return null;
        }
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        object obj = binForm.Deserialize(memStream);
        return obj;
    }
    public static DataTable ExcuteQuery(string Query, params dynamic[] Parameters)
    {
        DataTable table = new DataTable();
        using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=estore;User ID=kn;Password=nguyen11369;Allow User Variables=true"))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand(Query, con);
            if (Parameters != null)
            {
                int i = 0;
                string[] para = Query.Split(' ');
                foreach (string item in para)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, Parameters[i++]);
                    }
                }
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            con.Close();
        }
        return table;
    }
    public static DataTable UIntExecuteQuery(string Query, List<uint> Parameters)
    {
        DataTable table = new DataTable();
        using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=estore;User ID=kn;Password=nguyen11369;Allow User Variables=true"))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand(Query, con);
            if (Parameters != null)
            {
                int i = 0;
                string[] para = Query.Split(' ');
                foreach (string item in para)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, Parameters[i++]);
                    }
                }
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            con.Close();
        }
        return table;
    }
    public static int ExcuteNonQuery(string Query, params object[] Parameters)
    {
        int re = 0;
        using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;Database=estore;User ID=kn;Password=nguyen11369;Allow User Variables=true"))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand(Query, con);
            if (Parameters != null)
            {
                int i = 0;
                string[] para = Query.Split(' ');
                foreach (string item in para)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, Parameters[i++]);
                    }
                }
            }
            re = cmd.ExecuteNonQuery();
            con.Close();
        }
        return re;
    }
}