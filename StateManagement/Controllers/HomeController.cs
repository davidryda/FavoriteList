using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        int counter = 0;

        public ActionResult About()
        {
            if (Session["counter"] == null)
            {
                Session.Add("Counter", 0);
            }

            //fetch the value of the counter

            counter = (int)Session["counter"];

            counter += 1;

            ViewBag.Message = $"Counter value = {counter}";

            Session["counter"] = counter; //save the counter value

            return View();
        }

        public ActionResult Contact()
        {

            HttpCookie c;
            if (Request.Cookies["counter"] == null)
            {
                c = new HttpCookie("counter");
                c.Value = "0";
                c.Expires = new DateTime(2018, 1, 1);
            }
            else //fetch the cookie from the request
            {
                c = Request.Cookies["counter"];
            }

            int counter = int.Parse(c.Value);
            counter++;

            c.Value = counter.ToString(); //saving the value in the cookie

            Response.Cookies.Add(c); //sending the cookie to the clien09


            ViewBag.Message = $"Counter = {counter}";

            return View();
        }

        // this will take of the action:
        //Home/AddToFavList?Name=songname
        public ActionResult AddToFavList(string Name)
        {
            //set up the session
            if (Session["FavList"] == null)
            {
                Session.Add("FavList", new List<string>());
            }

            // fetch the list from the session
            List<string> FavList = (List<string>)Session["FavList"];

            if (!FavList.Contains(Name))
            {
                FavList.Add(Name);
            }

            //save list back in session
            Session["FavList"] = FavList;

            ViewBag.FavList = FavList; //sending the favlist to the view

            return View("About");
        }

        public ActionResult RemoveFromFavList()
        {
            if (Session["FavList"] != null)
            {
                ((List<string>)Session["FavList"]).Clear();

                //Session["FavList"] = null;
            }

            return View("About");
        }
    }
}