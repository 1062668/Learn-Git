using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WuDi_NaiCha.Models;

namespace WuDi_NaiCha.Controllers
{
    public class DefaultController : Controller
    {
        public List<Models.user> InitData()
        {
            List<Models.user> list = new List<Models.user>()
            {
                new Models.user(){userName="1",passWord="1"},
                new Models.user(){userName="2",passWord="2"},
                new Models.user(){userName="3",passWord="3"}
            };

            return list;
        }


        // GET: Default
        public ActionResult Index()
        {
            System.Text.StringBuilder sbHtml = new System.Text.StringBuilder(4000);
            List<Models.user> list = InitData();
            list.ForEach(d =>
            {
                sbHtml.AppendLine("<div>" + d.ToString() + "</div>");
            });
            ViewBag.HtmlStr = sbHtml.ToString();


            return View();
        }

        public ActionResult SaveRecord(MainPageModel model)
        {
            if (ModelState.IsValid)
            {
                MainPageModel JJ = new MainPageModel();
                JJ.Infor = model.Infor;
                JJ.Infor.Birthday = model.Infor.Birthday;
                JJ.Infor.Email = model.Infor.Email;
                JJ.Infor.PassWord = model.Infor.PassWord;
                JJ.Infor.FirstName = model.Infor.FirstName;
                JJ.Infor.LastName = model.Infor.LastName;
                if (model.Infor.CellPhone == null)
                    JJ.Infor.CellPhone = "";
                else
                    JJ.Infor.CellPhone = model.Infor.CellPhone;
                if (model.Infor.City == null)
                    JJ.Infor.City = "";
                else
                    JJ.Infor.City = model.Infor.City;
                string connString = "Server=192.168.0.238;DataBase=ESpace;uid=mluo;pwd=JamesPing3131";
                SqlConnection sqlConn = new SqlConnection(connString);
                sqlConn.Open();
                SqlCommand cmdGARead = new SqlCommand();
                cmdGARead.Connection = sqlConn;
                cmdGARead.CommandText = "INSERT [dbo].[WUDINAICHA] (FirstName,LastName,Birthday,Email,Password,CellPhone,CITY) VALUES (@add1,@add2,@add3,@add4,@add5,@add6,@add7)";
                cmdGARead.Parameters.AddWithValue("@add1", JJ.Infor.FirstName);
                cmdGARead.Parameters.AddWithValue("@add2", JJ.Infor.LastName);
                cmdGARead.Parameters.AddWithValue("@add3", JJ.Infor.Birthday);
                cmdGARead.Parameters.AddWithValue("@add4", JJ.Infor.Email);
                cmdGARead.Parameters.AddWithValue("@add5", JJ.Infor.PassWord);
                cmdGARead.Parameters.AddWithValue("@add6", JJ.Infor.CellPhone);
                cmdGARead.Parameters.AddWithValue("@add7", JJ.Infor.City);
                cmdGARead.ExecuteNonQuery();
                sqlConn.Close();
                TempData["savesuccess"] = "Saved Successful";
                ViewBag.IsVisible = true;

                return RedirectToAction("Index");
                
            }
            else
            {

                return View("Index");
            }
            
        }

        public ActionResult ACT(string reportName)
        {
            ViewBag.HtmlStr2 = reportName;
            return View("Index");
        }

        public ActionResult VIPCard()
        {
            return View();
        }

        public ActionResult Logins(string FirstName, string UserPwd)
        {
            string connString = "Server=192.168.0.238;DataBase=ESpace;uid=mluo;pwd=JamesPing3131";
            SqlConnection sqlConn = new SqlConnection(connString);
            sqlConn.Open();
            SqlCommand cmdGARead = new SqlCommand();
            cmdGARead.Connection = sqlConn;
            cmdGARead.CommandText = "select * from WUDINAICHA where FirstName='@add1'";
            cmdGARead.CommandText = cmdGARead.CommandText.Replace("@add1", FirstName);
            SqlDataReader myreader = cmdGARead.ExecuteReader();
            if (myreader.HasRows == false)
            {
                ViewBag.loginindex = 0;
            }
            else
            {
                if (myreader.Read())
                {
                    ViewBag.Email = myreader[3].ToString();
                    ViewBag.account = "[Account]";
                    ViewBag.logout = "[Log Out]";
                    if (myreader[4].ToString() == UserPwd && myreader[0].ToString()== FirstName)
                    {
                        ViewBag.loginindex = 1;
                    }
                    else
                    {
                        ViewBag.loginindex = 0;
                    }
                }
            }
            sqlConn.Close();

            return View("Index");



        }


    }
}

