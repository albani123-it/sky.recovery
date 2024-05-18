using sky.recovery.Helper.Enum;
using sky.recovery.Insfrastructures;
using sky.recovery.Interfaces;
using sky.recovery.Responses;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using sky.recovery.Helper.Config;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using sky.recovery.Entities;
using System.Collections.Generic;
using sky.recovery.DTOs.RequestDTO;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Numerics;

namespace sky.recovery.Services
{
    public class WorkflowServices:SkyCoreConfig, IWorkflowServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        private IUserService _User { get; set; }

        public WorkflowServices(IOptions<DbContextSettings> appsetting, IUserService user) : base(appsetting)
        {
            _User = user;
        }



        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy(CallbackApprovalDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var getCallBy = await _User.GetDataUser(Entity.userid);
            var DataHistoryWF = new workflowhistory();
            var DataHistoryWF2 = new workflowhistory();

            //get spv by role
            try
            {
                var DataWorkflow = workflow.Where(es => es.Id == Entity.workflowid).FirstOrDefault();
                //update status requestheader
                var OrdersAdd = DataWorkflow.orders + 1;
                var DataNext = masterflow.Where(es => es.fiturid == Entity.fiturid && es.orders == OrdersAdd && es.roleid != null).FirstOrDefault();
                if (DataNext != null)
                {
                    if (Entity.status == 4)//APPROVE
                    {
                        //simpan history approval pertama
                        DataHistoryWF.status = 4;
                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;
                        DataHistoryWF2.status = 11;
                        //get user by role default approval
                        DataHistoryWF2.actor = null;
                        DataHistoryWF2.workflowid = Entity.workflowid;
                        DataHistoryWF2.dated = DateTime.Now;

                        DataWorkflow.status = 11;
                        DataWorkflow.actor = null;
                        DataWorkflow.orders = OrdersAdd;
                        DataWorkflow.flowid = DataNext.id;
                        DataWorkflow.modifydated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF);
                        workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 5)//REJECT
                    {
                        //simpan history approval pertama
                        DataHistoryWF.status = 5;
                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;

                        DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.workflowid = Entity.workflowid;
                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);
                        workflowhistory.Add(DataHistoryWF);


                        DataWorkflow.status = 12;
                        DataWorkflow.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataWorkflow.modifydated = DateTime.Now;
                        DataWorkflow.reason = Entity.reason;
                        //get user by role default approval

                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.status = 13;
                        DataHistoryWF.status = 13;
                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;

                        DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.workflowid = Entity.workflowid;
                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);
                        workflowhistory.Add(DataHistoryWF);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid && es.orders==0).FirstOrDefault();
                        DataWorkflow.status = 10;
                        DataWorkflow.modifydated = DateTime.Now;
                        DataWorkflow.actor = Entity.idrequestor;
                        DataWorkflow.reason = Entity.reason;
                        DataWorkflow.orders = DataAwal.orders;
                        DataWorkflow.flowid = DataAwal.id;

                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.status = 10;

                        workflowhistory.Add(DataHistoryWF);

                    }
                   
                }

            
                else
                {
                    if (Entity.status == 4 || Entity.status == 5)//APPROVE
                    {
                        DataWorkflow.status = 12;


                        DataHistoryWF.status = Entity.status;
                        DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = DataWorkflow.actor;

                        DataHistoryWF2.workflowid = Entity.workflowid;

                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);
                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.status = 13;
                        DataHistoryWF.status = 13;
                        DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = DataWorkflow.actor;

                        DataHistoryWF2.workflowid = Entity.workflowid;

                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        //var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid).FirstOrDefault();
                        DataWorkflow.status = 10;
                        DataWorkflow.modifydated = DateTime.Now;
                        DataWorkflow.actor = Entity.idrequestor;
                        DataWorkflow.reason = Entity.reason;
                        DataWorkflow.orders = 0;

                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.status = 10;

                        workflowhistory.Add(DataHistoryWF);

                    }
                   
                }






                Entry(DataWorkflow).State = EntityState.Modified;
                // await workflow.AddAsync(DataWorkflow);
                await SaveChangesAsync();
                wrap.Status = true;
                wrap.Message = "ok";

                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }



        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval(CallbackApprovalDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var getCallBy = await _User.GetDataUser(Entity.userid);
            var DataHistoryWF = new workflowhistory();
            var DataHistoryWF2 = new workflowhistory();

            //get spv by role
            try
            {
                var DataWorkflow = workflow.Where(es => es.Id == Entity.workflowid).FirstOrDefault();
                //update status requestheader
                var OrdersAdd = DataWorkflow.orders + 1;
                var DataNext = masterflow.Where(es =>  es.fiturid == Entity.fiturid && es.orders==OrdersAdd && es.roleid!=null).FirstOrDefault();
              if(DataNext!= null)
                {
                    DataWorkflow.status = Entity.status;
                    DataWorkflow.modifydated = DateTime.Now;
                    DataWorkflow.actor = 100;
                    DataWorkflow.reason = Entity.reason;
                    DataWorkflow.flowid = DataNext.id;
                    DataWorkflow.orders = DataNext.orders;
                }
              else
                {
                    if(Entity.status==4 || Entity.status==5)//APPROVE
                    {
                        DataWorkflow.status = 12;

                       
                    DataHistoryWF.status = Entity.status;
                    DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = DataWorkflow.actor;

                        DataHistoryWF2.workflowid = Entity.workflowid;

                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);
                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.status = 13;
                        DataHistoryWF.status = 13;
                        DataHistoryWF2.status = 12;
                        DataHistoryWF2.actor = DataWorkflow.actor;

                        DataHistoryWF2.workflowid = Entity.workflowid;

                        DataHistoryWF2.dated = DateTime.Now;
                        workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status==10)//REVISI
                    {
                        //var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid).FirstOrDefault();
                        DataWorkflow.status = 10;
                        DataWorkflow.modifydated = DateTime.Now;
                        DataWorkflow.actor = Entity.idrequestor;
                        DataWorkflow.reason = Entity.reason;
                        DataWorkflow.orders = 0;

                        DataHistoryWF.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.dated = DateTime.Now;
                        DataHistoryWF.reason = Entity.reason;
                        DataHistoryWF.workflowid = Entity.workflowid;
                        DataHistoryWF.status = 10;

                        workflowhistory.Add(DataHistoryWF);

                    }
                    DataWorkflow.modifydated = DateTime.Now;
                    DataWorkflow.actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                    DataWorkflow.reason = Entity.reason;
                }






                Entry(DataWorkflow).State = EntityState.Modified;
               // await workflow.AddAsync(DataWorkflow);
                await SaveChangesAsync();
                wrap.Status = true;
                wrap.Message = "ok";

                return (wrap.Status, wrap);
            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }

            //SERVICE YANG DIPAKAI
            //TASKLIST RESTRUKTUR V2
            public async Task<(bool? Status, GeneralResponsesV2 Returns)> SubmitWorkflowStep(SubmitWorkflowDTO Entity)
        {
            var wrap = _DataResponses.Return();

            var getCallBy = await _User.GetDataUser(Entity.userid);
            var getSPV = await _User.GetDataUser(getCallBy.Returns.Data.FirstOrDefault().spvname);

            try
            {
                //penentuan master workflow
                var GetData =  masterflow.Where(es => es.orders==1 && es.fiturid == Entity.idfitur).FirstOrDefault();
                //get user yang role 45

                var DataWorkflow = new workflow()
                {
                    submitdated=DateTime.Now,
                    orders=GetData.orders,
                    status=11,
                    fiturid=Entity.idfitur,
                    actor=getSPV.Returns.Data.FirstOrDefault().iduser,
                    flowid=GetData.id,
                    requestid=Entity.idrequest,
                    masterworkflowid=GetData.masterworkflowid
                };


                    
                    await workflow.AddAsync(DataWorkflow);
                    await SaveChangesAsync();
                

                var DataWFHistory_Req = new workflowhistory()
            {
                workflowid=DataWorkflow.Id,
             actor=getCallBy.Returns.Data.FirstOrDefault().iduser,
             status=8,
             dated=DateTime.Now
            };

                await workflowhistory.AddAsync(DataWFHistory_Req);
                await SaveChangesAsync();

                var DataWFHistory_Usr = new workflowhistory()
                {
                    workflowid = DataWorkflow.Id,
                    actor = getSPV.Returns.Data.FirstOrDefault().iduser,
                    status = 11,
                    dated = DateTime.Now
                };
                await workflowhistory.AddAsync(DataWFHistory_Usr);
                await SaveChangesAsync();
                wrap.Status = true;
                wrap.Message = "OK";
                
                return (wrap.Status, wrap);

            }
            catch (Exception ex)
            {
                wrap.Status = false;
                wrap.Message = ex.Message;

                return (wrap.Status, wrap);
            }
        }
    }
}
