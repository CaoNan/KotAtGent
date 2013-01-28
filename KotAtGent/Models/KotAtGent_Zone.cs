using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KotAtGent.Models {
    public partial class KotAtGent_Zone {
        public static IEnumerable<KotAtGent_Zone> GetEnumZone() {
            return KotAtGent_Room.db.KotAtGent_Zones;
        }
        public static IEnumerable<SelectListItem> GetEnumZoneListItem() {
            List<SelectListItem> zoneListItem = new List<SelectListItem>();
            zoneListItem.Add(new SelectListItem {
                Text = "All",
                Value = "All",
                Selected = true
            });
            foreach (KotAtGent_Zone z in KotAtGent_Room.db.KotAtGent_Zones) {
                zoneListItem.Add(new SelectListItem {
                    Text = z.Zone,
                    Value = z.Zone
                });
            }
            zoneListItem.Add(new SelectListItem {
                Text = "Others",
                Value = "Others"
            });
            return zoneListItem;
        }
    }
}