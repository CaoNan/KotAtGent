using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KotAtGent.Models;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace KotAtGent.Controllers {
    public class HomeController : Controller {
        private IEnumerable<KotAtGent_Room> kotInfoList;
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            kotInfoList = KotAtGent_Room.GetAllKotInfo();
            ViewBag.Count = kotInfoList.Count();
            ViewBag.KotInfoList = this.kotInfoList;
            //ViewBag.ZoneList = KotAtGent_Zone.GetEnumZone();
            ViewBag.ZoneList = KotAtGent_Zone.GetEnumZoneListItem();
            ViewBag.TypeList = KotAtGent_Type.GetEnumTypeListItem();
            return View();
        }

        

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}
