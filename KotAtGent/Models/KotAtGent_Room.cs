using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace KotAtGent.Models {
    public partial class KotAtGent_Room {
        /// <summary>
        /// Connection object db
        /// </summary>
        public static KotAtGentDataClassesDataContext db = new KotAtGentDataClassesDataContext();

        public static string[] remoteApiUrl = new string[2] { "http://data.appsforghent.be/kotatgent/data.json", "http://hannesholst.ikdoeict.be/app2/api/dorm" };


        public static List<KotAtGent_Room> ListRemote = KotAtGent_Room.GetRemoteData();

        private static IEnumerable<KotAtGent_Room> List = ListRemote.Concat(GetLocalKotInfo());
        /// <summary>
        /// Get all "kot" infomation from local database
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KotAtGent_Room> GetLocalKotInfo() {
            var kots = (from kot in db.KotAtGent_Rooms
                         orderby kot.Zone
                         select kot);
            return kots;
        }
        /// <summary>
        /// Get all "kot" infomation from local & remote api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KotAtGent_Room> GetAllKotInfo() {
            return KotAtGent_Room.List.OrderBy(x=>x.Datum); 
        }
        /// <summary>
        /// Get a record by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static KotAtGent_Room GetKotInfoByID(int id) {
            var kot = List.FirstOrDefault((k) => k.ID == id);
            if (kot == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return kot;
        }

        /// <summary>
        /// Get a set of record which belongs to the certain type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<KotAtGent_Room> GetKotInfoByTpye(string type) {
            return List.Where(
                //(k) => string.Equals(k.Type, type,
                    //StringComparison.OrdinalIgnoreCase));
                    (k)=>k.Type==type).OrderBy(x=>x.Datum);
        }

        public static IEnumerable<KotAtGent_Room> GetSearchResult(string zone, string type, string price) {
            IEnumerable<KotAtGent_Room> resultList = new List<KotAtGent_Room>();
            if (zone != "All") {
                resultList = List.Where(r => r.Zone == zone);
            } if (type != "All") {
                resultList = List.Where(r => r.Type == type);
            } if (price != "All") {
                switch (price) { 
                    case "250":
                        resultList = List.Where(r => r.Min_prijs <= 250);
                        break;
                    case "500":
                        resultList = List.Where(r => r.Min_prijs >= 250 && r.Min_prijs <= 500);
                        break;
                    case "501":
                        resultList = List.Where(r => r.Min_prijs >= 500);
                        break;
                }
            }
            return resultList.OrderBy(x => x.Datum);
        }

        /// <summary>
        /// Get remote data form api
        /// </summary>
        public static List<KotAtGent_Room> GetRemoteData() {
            try {
                //get remote data from url A
                WebRequest request = WebRequest.Create(remoteApiUrl[0]);
                request.ContentType = "application/json; charset=utf-8";
                string jsontextA;
                var response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream())) {
                    jsontextA = sr.ReadToEnd();
                }
                jsontextA = "[" + jsontextA.Substring(9, jsontextA.Length - 11) + "]";
                //get remote data from url B
                request = WebRequest.Create(remoteApiUrl[1]);
                request.ContentType = "application/json; charset=utf-8";
                string jsontextB;
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream())) {
                    jsontextB = sr.ReadToEnd();
                }
                //try to integrate data close to the local data structure
                jsontextB = jsontextB.Replace("dormId", "ID");
                jsontextB = jsontextB.Replace("street", "Adres");
                jsontextB = jsontextB.Replace("latitude", "Lat");
                jsontextB = jsontextB.Replace("longitude", "Long");
                jsontextB = jsontextB.Replace("price", "Min_prijs");
                jsontextB = jsontextB.Replace("startDateTime", "Datum");
                jsontextB = jsontextB.Replace("phone", "Eigenaar_telefoon");
                //Deserialize jsontextA to object
                List<KotAtGent_Room> rawDataA = JsonConvert.DeserializeObject<List<KotAtGent_Room>>(jsontextA);
                List<KotAtGent_Room> modifiedData = new List<KotAtGent_Room>();
                foreach (KotAtGent_Room room in rawDataA) {
                    if (room.Zone == "Sterre") {
                        room.ID = room.ID + 100000;
                        modifiedData.Add(room);
                    }
                }
                //Deserialize jsontextB to object
                List<KotAtGent_Room> rawDataB = JsonConvert.DeserializeObject<List<KotAtGent_Room>>(jsontextB);
                foreach (KotAtGent_Room room in rawDataB) {
                    room.ID = room.ID + 200000;
                    modifiedData.Add(room);
                    room.Zone = "Anders/Onbekende";
                }
                return modifiedData;
            }
            catch (Exception e) {
                string s = e.Message;
                Console.Write(s);
                return new List<KotAtGent_Room>();
            }
        }
    }
}