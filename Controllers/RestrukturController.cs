using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sky.recovery.Libs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Newtonsoft.Json;
using sky.recovery.Model;
using sky.recovery.Services;

namespace sky.recovery.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyrecovery/[controller]")]

    public class RestrukturController : Controller
    {
        private BaseController bc = new BaseController();
        private BasetrxController bcx = new BasetrxController();
        private lDbConn dbconn = new lDbConn();
        private MessageController mc = new MessageController();
        private TokenController tc = new TokenController();
        private lConvert lc = new lConvert();
        private lDataLayer ldl = new lDataLayer();
        private readonly RestrukturServices restructureService;

        public RestrukturController(RestrukturServices restructureService)
        {
            this.restructureService = restructureService;
        }

        // [HttpPost]
        // [Route("/dokumen/upload")]
        // public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentRestructure bean)
        // {
        //     var data = new JObject();
        //     try
        //     {
        //         data = new JObject();
        //         (bool result, string message) = await this.restructureService.UploadDokumenRestruktur(bean);
        //
        //         data.Add("status", mc.GetMessage("api_output_ok"));
        //         data.Add("message", mc.GetMessage("process_success"));
        //         data.Add("data", mc.GetMessage(""));
        //
        //         data.Add("status", mc.GetMessage("api_output_ok"));
        //         data.Add("message", mc.GetMessage(message));
        //         data.Add("data", mc.GetMessage(""));
        //     }
        //     catch (Exception ex)
        //     {
        //         data = new JObject();
        //         data.Add("status", mc.GetMessage("api_output_not_ok"));
        //         data.Add("message", ex.Message);
        //     }
        //
        //     return data;
        // }
        
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromForm] CreateRestructure param)
        {
            var data = new JObject();
            try
            {
                data = new JObject();
                (bool result, string message) = await this.restructureService.Create(param);
        
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", mc.GetMessage(""));

                if (result == true)
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage(message));
                    data.Add("data", mc.GetMessage(""));
                }
                else
                {
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage(message));
                    data.Add("data", mc.GetMessage(""));
                }
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
        
            return Ok(data);
        }

        [HttpGet("monitoring/list")]
        public JObject GetLisMonitoring()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();
            try
            {
                data = new JObject();
                retObject = ldl.GetlistMonitoring();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }
        
        [HttpGet("monitoring/aprroval/list")]
        public JObject GetListTugasSaya()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();
            try
            {
                data = new JObject();
                retObject = ldl.GetlistTugasSaya();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("monitoring/list/detail")]
        public JObject GetLisMonitoringDetail([FromBody] JObject json)
        {

            var data = new JObject();
            var jFormDetData = new JObject();
            var jaFormDet = new JArray();
            try
            {
                data = new JObject();

                var userid = json.GetValue("userid").ToString();
                var dtretrunusr = ldl.getDataUserdetail(userid);
                if (dtretrunusr.Count > 0)
                {
                    var idusr = dtretrunusr[0]["usrid"].ToString();

                    var dtretrun = ldl.getListMontoringDetail(idusr);

                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("process_success"));
                    data.Add("data", dtretrun);
                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "users not found");
                    data.Add("data", new JArray());
                }


            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("monitoring/detail")]
        public JObject Getdatamonitoringdetail([FromBody] JObject json)
        {
            var retgnrlinfo = new List<dynamic>();
            var retcollateral = new List<dynamic>();
            var retlog = new List<dynamic>();

            var data = new JObject();
            try
            {
                data = new JObject();
                var jaFormDet = new JArray();
                var jFormCont = new JObject();
                retgnrlinfo = ldl.GetDetailGnrlInfo(json);
                if (retgnrlinfo.Count > 0)
                {
                   
                    for (int i = 0; i < retgnrlinfo.Count; i++)
                    {
                        var jFormDetData = new JObject();
                        jFormDetData.Add("id", retgnrlinfo[0].id);
                        jFormDetData.Add("name", retgnrlinfo[0].name);
                        //jFormDetData.Add("no_cif", retgnrlinfo[0].no_cif);
                        //jFormDetData.Add("norek", retgnrlinfo[0].norek);
                        jFormDetData.Add("no_ktp", retgnrlinfo[0].ktp);
                        jFormDetData.Add("tgl_lahir", retgnrlinfo[0].tgl_lahir);
                        jFormDetData.Add("alamat", retgnrlinfo[0].alamat);
                        jFormDetData.Add("city", retgnrlinfo[0].city);
                        jFormDetData.Add("no_tlpn", retgnrlinfo[0].no_tlpn);
                        jFormDetData.Add("no_hp", retgnrlinfo[0].no_hp);
                        jFormDetData.Add("pekerjaan", retgnrlinfo[0].pekerjaan);
                        jFormDetData.Add("startdate", retgnrlinfo[0].startdate);
                        jFormDetData.Add("segement", retgnrlinfo[0].segement);
                        jFormDetData.Add("product", retgnrlinfo[0].product);
                        jFormDetData.Add("jumlah_angsuaran", retgnrlinfo[0].jumlah_angsuaran);
                        jFormDetData.Add("tgl_mulai", retgnrlinfo[0].tgl_mulai);
                        jFormDetData.Add("tgl_jatuh_tempo", retgnrlinfo[0].tgl_jatuh_tempo);
                        jFormDetData.Add("tenor", retgnrlinfo[0].tenor);
                        jFormDetData.Add("plafond", retgnrlinfo[0].plafond);
                        jFormDetData.Add("outstanding", retgnrlinfo[0].outstanding);
                        jFormDetData.Add("kolektabilitas", retgnrlinfo[0].kolektabilitas);
                        jFormDetData.Add("dpd", retgnrlinfo[0].dpd);
                        jFormDetData.Add("tglbayarterakhir", retgnrlinfo[0].tglbayarterakhir);
                        jFormDetData.Add("tunggakanpokok", retgnrlinfo[0].tunggakanpokok);
                        jFormDetData.Add("tunggakanbunga", retgnrlinfo[0].tunggakanbunga);
                        jFormDetData.Add("tunggakandenda", retgnrlinfo[0].tunggakandenda);
                        jFormDetData.Add("tunggakantotal", retgnrlinfo[0].tunggakantotal);
                        jFormDetData.Add("kewajibantotal", retgnrlinfo[0].kewajibantotal);
                        jaFormDet.Add(jFormDetData);

                    }


                    jFormCont.Add("name", retgnrlinfo[0].name);
                    jFormCont.Add("no_account", retgnrlinfo[0].norek);


                    jFormCont.Add("genearlinfo", jaFormDet);
           
                    jFormCont.Add("fasilitas", new JArray());
                    jFormCont.Add("agunan", new JArray());
             /*       jFormCont.Add("callrecord", dtretrun1);
                    jFormCont.Add("actionhistory", dtretrun2);
*/
                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("process_success"));
                    data.Add("data", new JArray(jFormCont));
                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "data not found");
                    data.Add("data", new JArray());
                }

            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpPost("monitoring/list/doct")]
        public JObject GetLisMonitoringDocument([FromBody] JObject json)
        {

            var data = new JObject();
            var jFormDetData = new JObject();
            var jaFormDet = new JArray();
            try
            {
                data = new JObject();

                var userid = json.GetValue("userid").ToString();
                var dtretrunusr = ldl.getDataUserdetail(userid);
                if (dtretrunusr.Count > 0)
                {
                    var idusr = dtretrunusr[0]["usrid"].ToString();

                    var dtretrun = ldl.getListMontoringDocument(idusr);

                    data.Add("status", mc.GetMessage("api_output_ok"));
                    data.Add("message", mc.GetMessage("process_success"));
                    data.Add("data", dtretrun);
                }
                else
                {
                    data = new JObject();
                    data.Add("status", mc.GetMessage("api_output_not_ok"));
                    data.Add("message", "users not found");
                    data.Add("data", new JArray());
                }


            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("ddown/pola/restruktur")]
        public JObject GetDataPolaRestruktur()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();
            try
            {
                data = new JObject();
                retObject = ldl.Getpolarestruktur();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("ddown/jnspengurangan")]
        public JObject GetDatajnspengurangan()
        {
            var retObject = new List<dynamic>();
            var data = new JObject();
            try
            {
                data = new JObject();
                retObject = ldl.Getjenispengurangan();

                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", lc.convertDynamicToJArray(retObject));
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

    }
}
