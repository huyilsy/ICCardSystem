using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICCardSystem.Utility
{
    public class LConvert
    {
              
        static  byte[] key1 = new byte[2];   

              //16进制转换成10进制
        public static string hex_int(string str){
              string result_int = "";
              int l = str.Length;
              if (l % 2 == 1) { l = l + 1; str = '0' + str; }
             int i = 0;
              byte[] int_databuff = new byte[l];
              int_databuff = System.Text.Encoding.ASCII.GetBytes(str);
                    for(int j=0;j<l;j=j+2)   {
                        key1[0] = int_databuff[j];
                        key1[1] = int_databuff[j + 1];
                        string x = System.Text.Encoding.ASCII.GetString(key1);
                        i = Convert.ToInt32(x, 16);//转换成10进制整数
                        string ss=i.ToString();
                        if (ss.Length == 1) { ss = '0' + ss; }
                        result_int += ss;
                    }
                    return result_int;
        }

        //10进制转换成16进制
        public static string int_hex(string str){
           string s = "";
           int l = str.Length;
           if (l % 2 == 1) { l = l + 1; str = '0' + str; }
           int i = 0;
           byte[] hex_databuff = new byte[l];
           hex_databuff = System.Text.Encoding.ASCII.GetBytes(str);
           for (int j = 0; j < l; j=j+2)
           {
               key1[0] = hex_databuff[j];
               key1[1] = hex_databuff[j + 1];
               string x = System.Text.Encoding.ASCII.GetString(key1);
               i = Convert.ToInt32(x);
               string ss = string.Format("{0:X}", i);//转换成16进制字符串
               if (ss.Length == 1) { ss='0'+ss;}
               s += ss;
           }
            return s;
        }

        public static string float_int(string str) {
            int l = str.Length;
            int f = 0;
            string s="";
            for (int i = 0; i < l; i++) {
                if (str[i] == '.') { f = i + 1; i = l; }
                else s = s+str[i];
            }
            int k = Convert.ToInt32(s);
            if (f != 0) { string c =""+ str[f]; k = 100 * k + int.Parse(c); }
            s = k.ToString();
            if (s.Length % 2 != 1) { s = "0" + s; }
                return s;
        }
    }
}