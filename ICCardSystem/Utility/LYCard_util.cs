using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using ICCardSystem.Utility;
using ICCardSystem.Models;
using ICCardSystem.Models.VModels;



namespace ICCardSystem.Utility
{
    public class LYCard_util
    {
           
         static  Int16 port = 1;
         static int baud = 9600;
         static Int16 st = 0;
         static int icdev = 0;         
           
            //数据解析
         //用户卡标志区 存储区地址：20H（32）数据说明：若IC卡20H存储单元数值为DDH，则表明此卡为用户卡；否则为非用户卡。
           public static string ReadUserType()
            {
               
                byte[] data = new byte[1];
                byte[] databuff = new byte[2];
                string lbResult;
                icdev = IC.ic_init(port, baud);
               
                st = IC4442.srd_4442(icdev, 32, 1, data);
                if (st == 0)
                {
                    st = IC.hex_asc(data, databuff, 1);
                    lbResult = System.Text.Encoding.ASCII.GetString(databuff);
                    if (lbResult[0] == 'D' && lbResult[1] == 'D') { lbResult += "是用户卡"; }
                    else lbResult += "非用户卡";
                }
                else
                    lbResult = "读数据失败！";
            
                return lbResult;
            }
            //用户编号区 21H—24H（33 – 36）当存储在IC卡上时，8位数从低位到高位，每两位为1组，每组存入1个字节中。
            //例如：若从卡上用户编号区读出的数据为：0CH，22H，38H，4EH，则用户编号为12345678。
           public static string ReadUserId()
            {               
                string s = "";                
                byte[] data = new byte[4];
                byte[] databuff = new byte[8];
                string lbResult;
                icdev = IC.ic_init(port, baud);
                st = IC4442.srd_4442(icdev, 33, 4, data);
                if (st == 0)
                {
                    st = IC.hex_asc(data, databuff, 4);
                    lbResult = System.Text.Encoding.ASCII.GetString(databuff);               
                    s = LConvert.hex_int(lbResult);
                    s += LConvert.int_hex(s);
                    lbResult = s +lbResult;
                }
                else
                    lbResult = "读数据失败！";
                return lbResult;
            }


           /*      表内剩余气量回写区
          存储区地址：3DH—3FH（61 – 63）数据说明：此存储区数据为燃气表对用户卡回写的表内剩余气量值。
             剩余气量 = （3DH单元数值） X 100 + （3EH单元数值） X 1+ （3FH单元数值） X 0.1。
             表内剩余气量最大数值为999.9立方米，其中，3DH单元数值范围为：0—9，3EH单元数值范围为：0—99，3FH单元数值范围为0 -- 9。
             例如：若用户表内剩余气量为123.4立方米，则IC卡3DH—3FH存储单元应为：01H，17H，04H。*/
           public static string ReadRemainQL()
           {
               string s = "";
               byte[] data = new byte[3];
               byte[] databuff = new byte[6];
               int[] sum=new int[8];
               string lbResult;
               icdev = IC.ic_init(port, baud);
               st = IC4442.srd_4442(icdev, 61, 3, data);
               if (st == 0)
               {
                   st = IC.hex_asc(data, databuff, 3);
                   lbResult = System.Text.Encoding.ASCII.GetString(databuff);
                   s = LConvert.hex_int(lbResult);
                   int lll=s.Length;
                   byte[] intbuff = new byte[lll];
                   byte[] key=new byte[2];
                   int k = 0;
                   double d = 0;
                   intbuff = System.Text.Encoding.ASCII.GetBytes(s);
                   for (int j = 0; j < 6; j = j + 2)
                   {
                       key[0] = intbuff[j];
                       key[1] = intbuff[j+1];
                       string x = System.Text.Encoding.ASCII.GetString(key);
                       sum[k] = Convert.ToInt32(x);
                       k++;
                   }
                   d = 100*sum[0] + sum[1] + 0.1 * sum[2];
                   lbResult = "剩余气量"+d.ToString() + lbResult;
               }
               else
                   lbResult = "读数据失败！";
               return lbResult;
           }
         
           /* 地区编号记录区 
              标识名：DQBH 存储区地址：47H（71）数据说明：此存储区数据为公司客户下的地区编号，
            * 如“武汉市天然气公司”在武汉市的用户可按地区分为三大片，其地区编号分别为：
            * “汉口地区”— 1、“武昌地区”— 2、“汉阳地区”-- 3。 测试：”武汉理工大学“-255*/
           public static string ReadDQBH() {

               byte[] data = new byte[1];
               byte[] databuff = new byte[2];
               string lbResult;
               icdev = IC.ic_init(port, baud);
               st = IC4442.srd_4442(icdev, 71, 1, data);
               if (st == 0)
               {
                   st = IC.hex_asc(data, databuff, 1);
                   lbResult = System.Text.Encoding.ASCII.GetString(databuff);
                   if (lbResult[0] == '0' && lbResult[1] == '1') { lbResult += "汉口地区"; }
                   else if (lbResult[0] == '0' && lbResult[1] == '2') { lbResult += "武昌地区"; }
                   else if (lbResult[0] == '0' && lbResult[1] == '3') { lbResult += "汉阳地区"; }
                   else if (lbResult[0] == 'F' && lbResult[1] == 'F') { lbResult += "武汉理工大学"; }
                   else lbResult += "未知地区";
               }
               else
                   lbResult = "读数据失败！";

               return lbResult;
           }


           public static string WriteUserId(string str)
           {
              
               byte[] databuff = new byte[4];
               byte[] buff = new byte[8];
               string data = "";
               data = LConvert.int_hex(str);
               icdev = IC.ic_init(1, 9600);
               buff = System.Text.Encoding.ASCII.GetBytes(data);
               IC.asc_hex(buff, databuff, 8);
               st = IC4442.swr_4442(icdev, 33, 4, databuff);
               if (st == 0)
                   return "写数据成功！"+data;
               else
                   return "写数据失败！"+data;
           }

           public static string WriteRemainQL(string str)
           {

               byte[] databuff = new byte[3];
               byte[] buff = new byte[6];
               string f_i = "";
               string data = "";
               f_i = LConvert.float_int(str);
               data = LConvert.int_hex(f_i);
               icdev = IC.ic_init(1, 9600);
               buff = System.Text.Encoding.ASCII.GetBytes(data);
               IC.asc_hex(buff, databuff, 6);
               st = IC4442.swr_4442(icdev, 61, 3, databuff);
               if (st == 0)
                   return "写数据成功！" + data;
               else
                   return "写数据失败！" + data;
           }

           public static string WriteDQBH(string str)
           {

               byte[] databuff = new byte[1];
               byte[] buff = new byte[2];
               string data = "";
               data = LConvert.int_hex(str);
               icdev = IC.ic_init(1, 9600);
               buff = System.Text.Encoding.ASCII.GetBytes(data);
               IC.asc_hex(buff, databuff, 2);
               st = IC4442.swr_4442(icdev, 71, 1, databuff);
               if (st == 0)
                   return "写数据成功！" + data;
               else
                   return "写数据失败！" + data;
           }
    }
}