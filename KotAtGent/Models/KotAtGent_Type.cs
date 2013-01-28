using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KotAtGent.Models {
    public partial class KotAtGent_Type {
        //public static IEnumerable<KotAtGent_Type> GetEnumType() {
            //return KotAtGent_Kot.db.KotAtGent_Tpyes;
        //}
        public static IEnumerable<SelectListItem> GetEnumTypeListItem() {
            List<SelectListItem> typeListItem = new List<SelectListItem>();
            typeListItem.Add(new SelectListItem {
                                Text="All",
                                Value="All",
                                Selected = true
                            });
            foreach (KotAtGent_Type t in KotAtGent_Room.db.KotAtGent_Types) {
                typeListItem.Add(new SelectListItem {
                    Text = t.Type,
                    Value = t.Type
                });
            }
            return typeListItem;
        }
    }
}