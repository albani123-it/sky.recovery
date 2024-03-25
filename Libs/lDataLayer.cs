using sky.recovery.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace sky.recovery.Libs
{
    public class lDataLayer
    {
        private BaseController bc = new BaseController();
        private lDbConn dbconn = new lDbConn();
        private lConvert lc = new lConvert();
        private MessageController mc = new MessageController();
        private lData ldt = new lData();
        private lPgsql lp = new lPgsql();

        public List<dynamic> lObjectChar = new List<dynamic>();
        public List<dynamic> lObject = new List<dynamic>();

        public List<dynamic> GetlistMonitoring()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            string spname = "public.get_list_monitoring_restruktur";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject(provider, cstrname, spname);
            return retObject;
        }

        public JArray getDataUserdetail(string p_userid)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycore");

            var split = "||";
            var schema = "public";

            string spname = "usr_getuser_detail";
            string p1 = "@userid" + split + p_userid + split + "s";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public JArray getListMontoringDetail(string idusr)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");

            var split = "||";
            var schema = "public";

            string spname = "get_list_monitor_restruktur_nsb";
            string p1 = "@usrid" + split + idusr + split + "bg";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public List<dynamic> GetDetailGnrlInfo(JObject json)
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            string spname = "public.get_detail_reassign_gnrl_info";
            string p1 = "p_id," + json.GetValue("loanid").ToString() + ",bg";
            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject(provider, cstrname, spname, p1);
            return retObject;
        }

        public JArray getListMontoringDocument(string idusr)
        {
            var jaReturn = new JArray();
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");

            var split = "||";
            var schema = "public";

            string spname = "getlist_doc_restruktur";
            string p1 = "@usrid" + split + idusr + split + "bg";
            var retObject = new List<dynamic>();
            retObject = bc.ExecSqlWithReturnCustomSplit(provider, cstrname, split, schema, spname, p1);
            jaReturn = lc.convertDynamicToJArray(retObject);

            return jaReturn;
        }

        public List<dynamic> Getpolarestruktur()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            string spname = "public.get_pola_restruktur";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject(provider, cstrname, spname);
            return retObject;
        }

        public List<dynamic> Getjenispengurangan()
        {
            var provider = dbconn.sqlprovider();
            var cstrname = dbconn.constringName("skycoll");
            string spname = "public.get_jenis_pengurangan";

            var retObject = new List<dynamic>();
            retObject = bc.getDataToObject(provider, cstrname, spname);
            return retObject;
        }
    }
}
