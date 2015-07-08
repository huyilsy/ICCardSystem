using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using ICCardSystem.Utility;
using ICCardSystem.Models;
using ICCardSystem.Models.VModels;


namespace ICCardSystem.Controllers
{
    public class ReadCardController : Controller
    {

        public int icdev; // 通讯设备标识符
        public Int16 st;

        public int sec;
        //
        // GET: /ReadCard/

        public ActionResult Index()
        {

            st = 0;

            Int16 port = 1;
            int baud = 9600;
            byte[] ver = new byte[30];
            byte[] hard_ver = new byte[30];

            st = IC.lib_ver(ver);
            icdev = IC.ic_init(port, baud);
            if (icdev > 0)
            {
                ViewBag.com = "打开串口成功！";
                byte[] status = new byte[5];
                st = IC.get_status(icdev, status);
                if (st != 0)
                    ViewBag.com = "获取设备状态失败！";
                st = IC.srd_ver(icdev, 18, hard_ver);
                if (st == 0)
                {

                    ViewBag.SoftVer = System.Text.Encoding.ASCII.GetString(ver);
                    ViewBag.HardVer = System.Text.Encoding.ASCII.GetString(hard_ver);
                }
                else
                    ViewBag.com = "读取硬件版本号失败！";
            }
            else
                ViewBag.com = "打开串口失败！";
            return View();
        }

        [HttpPost]
        public string Connect()
        {
            st = 0;

            Int16 port = 1;
            int baud = 9600;
            byte[] ver = new byte[30];
            byte[] hard_ver = new byte[30];

            st = IC.lib_ver(ver);
            icdev = IC.ic_init(port, baud);
            if (icdev > 0)
            {
                ViewBag.com = "打开串口成功！";
                byte[] status = new byte[5];
                st = IC.get_status(icdev, status);
                if (st != 0)
                {  return "连接失败"; }
                st = IC.srd_ver(icdev, 18, hard_ver);
                if (st == 0)
                {

                    ViewBag.SoftVer = System.Text.Encoding.ASCII.GetString(ver);
                    ViewBag.HardVer = System.Text.Encoding.ASCII.GetString(hard_ver);
                }
                else
                 return "连接失败"; 
            }
            else
              return "连接失败"; 

            return "连接成功";
        }
        public string Disconnect()
        {
            string lbResult;
            st = IC.ic_exit(icdev);
            if (st == 0)
                lbResult = "断开连接成功！";
            else
                lbResult = "断开连接失败！";
            return lbResult;
        }


        [HttpPost]
        public ActionResult ReadData()
        {
            RWCardView rw = new RWCardView();              
            rw.UserType = LYCard_util.ReadUserType();
            rw.UserId = LYCard_util.ReadUserId();
            rw.RemainQL = LYCard_util.ReadRemainQL();
            rw.DQBH = LYCard_util.ReadDQBH();
            return Json(rw);

        }
          [HttpPost]
        public string WriteData(RWCardView rw)
        {
            string lbResult="";
            string data ;         
             data = rw.UserId;
             if (data == null) lbResult += "输入用户ID错误";
             else if (data.Length % 2 == 1) lbResult += "输入用户ID错误";
             else { lbResult +="用户ID："+ LYCard_util.WriteUserId(data); }
             data = rw.RemainQL;
             if (data == null) lbResult += "输入剩余气量错误";
            // else if (data.Length % 2 == 1) lbResult += "输入剩余气量错误";
             else { lbResult += "剩余气量：" + LYCard_util.WriteRemainQL(data); }
             data = rw.DQBH;
             if (data == null) lbResult += "输入地区编号错误";
           //  else if (data.Length % 2 == 1) lbResult += "输入地区编号错误";
             else { lbResult += "地区编号：" + LYCard_util.WriteDQBH(data); }

            return lbResult;
          }

         [HttpPost]
          public string VerifyData(SearchConditionView search)
          {
              byte[] key1 = new byte[20];
              byte[] key2 = new byte[20];
              byte[] ver = new byte[30];
              byte[] status = new byte[5];
              int i = 0;
              st = IC.lib_ver(ver);
              st = IC.get_status(icdev, status);
              icdev = IC.ic_init(1, 9600);
              string lbResult;
              string skey = search.Condition1;
              
              if (skey == null)
              {
                  lbResult = "请正确输入数据！";
                  return lbResult;
              }
              int keylen = skey.Length;
              if (keylen != 6)
              {
                  lbResult = "请正确输入密码，密码长度不对！";
                  return lbResult;
              }

              for (i = 0; i < keylen; i++)
              {
                  if (skey[i] >= '0' && skey[i] <= '9')
                      continue;
                  if (skey[i] <= 'a' && skey[i] <= 'f')
                      continue;
                  if (skey[i] <= 'A' && skey[i] <= 'F')
                      continue;
              }
              if (i != keylen)
              {
                  lbResult = "密码必须为十六进制数！";
                  return lbResult;
              }

              key1 = System.Text.Encoding.ASCII.GetBytes(skey);
              IC.asc_hex(key1, key2, 6);
              st = IC4442.csc_4442(icdev, 3, key2);
              if (st == 0)
                  lbResult = "密码校验成功！";
              else
                  lbResult = "密码校验失败！";
              return lbResult;
          }
    }
}