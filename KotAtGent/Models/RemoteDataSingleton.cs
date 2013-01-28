using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KotAtGent.Models {
    public class RemoteDataSingleton {
        private static RemoteDataSingleton instance;

        public IEnumerable<KotAtGent_Room> remoteData;
        
        private RemoteDataSingleton() { }

        public static RemoteDataSingleton Instance {
            get {
                if (instance == null) {
                    instance = new RemoteDataSingleton();
                }
                return instance;
            }
        }
        
    }
}