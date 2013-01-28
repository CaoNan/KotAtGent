using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using KotAtGent.Models;

namespace KotAtGent.Controllers
{
    public class OverzichtController : Controller
    {
        private IEnumerable<KotAtGent_Room> kotInfoList;
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            kotInfoList = KotAtGent_Room.GetAllKotInfo();
            ViewBag.Count = kotInfoList.Count();
            ViewBag.KotInfoList = this.kotInfoList;
            ViewBag.ZoneList = KotAtGent_Zone.GetEnumZone();
            ViewBag.ZoneList = KotAtGent_Zone.GetEnumZoneListItem();
            ViewBag.TypeList = KotAtGent_Type.GetEnumTypeListItem();
            return View();
        }
        [System.Web.Http.HttpPost]
        public ActionResult Search(string selectZone, string selectType, string selectPrice) {
            ViewBag.ZoneList = KotAtGent_Zone.GetEnumZoneListItem();
            ViewBag.TypeList = KotAtGent_Type.GetEnumTypeListItem();
            //search
            kotInfoList = KotAtGent_Room.GetSearchResult(selectZone, selectType, selectPrice);
            ViewBag.Count = kotInfoList.Count();
            ViewBag.KotInfoList = kotInfoList;
            //ViewBag.ZoneList = KotAtGent_Zone.GetEnumZone();
            
            return View("Index");
        }
        /// <summary>
        /// Details page
        /// </summary>
        /// <returns></returns>
        public ViewResult ShowDetails(string id) {
            kotInfoList = KotAtGent_Room.GetAllKotInfo();
            ViewBag.KotInfoList = this.kotInfoList;
            try {
                int idint = Int32.Parse(id);
                var entry = KotAtGent_Room.GetKotInfoByID(idint);
                return View(entry);
            }
            catch (HttpResponseException httpe) {
                return View("Error", new HandleErrorInfo(new Exception("The given id is not exist"), "Overzicht", "Error"));
            }
            catch (Exception e) {
                return View("Error",new HandleErrorInfo(new Exception("Illegal argument error"),"Overzicht","Error"));
            }
        }
    }
}
