using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cache_using_sample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult homepage()
        {
            HttpContext.Cache.Add("name", "alican", null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 10), System.Web.Caching.CacheItemPriority.High, CacheItemExpired);


            return View();
        }

        private void CacheItemExpired(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
        {
            //Mail atabilirsin
            //Veri tabanına yazabilirsin
            //uyarı mesajı verdirebilirsin
            //biz basit bir sekilde txt olusturup solution altındaki proje içerisinde oraya yazacağız birseyler.
            System.IO.File.WriteAllText(Server.MapPath("~/cache-expired-note.txt"), $"{key} cache item, {value.ToString()} cache item value are expired.Reason : {reason.ToString()}.");
        }//Cache silindikten sonra proje içinde txt oluşuyor.
        public ActionResult samplepage()
        {
            return View();
        }

        public ActionResult delete() //otomatik silebileceğin (TimeSpan veya NoAbsoluteExpiration)  gibi bu Action ilede silsen yine txt oluşur CacheItemExpired çağırılır.
        {
            HttpContext.Cache.Remove("name");

            return View();
        }
    }
}