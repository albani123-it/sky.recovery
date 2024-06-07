﻿using sky.recovery.Helper.Enum;
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
using Org.BouncyCastle.Asn1.Ocsp;
using System.Numerics;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using OfficeOpenXml.Drawing.Controls;
namespace sky.recovery.Services
{
    public class WorkflowServices:SkyCoreConfig, IWorkflowServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        skycollContext _RecoveryContext = new skycollContext();
        private IUserService _User { get; set; }

        public WorkflowServices(IOptions<DbContextSettings> appsetting, IUserService user) : base(appsetting)
        {
            _User = user;
        }



        //SERVICE YANG DIPAKAI
        //TASKLIST RESTRUKTUR V2
        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy(string userid,CallbackApprovalDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var getCallBy = await _User.GetDataUser(userid);
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

                //UPDATE MASTER REQUEST
                if(Entity.fiturid==9)//restrukture
                {
                    var Data = await _RecoveryContext.Restruktures.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;
                    
                    Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 10)//AYDA
                {
                    var Data = await ayda.Where(es => es.id== Entity.idrequest).FirstOrDefaultAsync();
                    Data.statusid = Entity.status;
                    Data.lastupdatedate = DateTime.Now;
                    
                    Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 15)//auction
                {
                    var Data = await _RecoveryContext.Auctions.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 16)//INSURANCE
                {
                    var Data = await _RecoveryContext.Insurances.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdateddated = DateTime.Now;

                    Entry(Data).State = EntityState.Modified;

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

        public async Task<bool> UpdateStatusRequest(int? fiturid, int? idrequest, int status)
        {
            try
            {
                if (fiturid == 9)
                {
                    var Data = await restructure.Where(es => es.id == idrequest).FirstOrDefaultAsync();
                    Data.statusid = 11;
                    Data.statusmodifydated = DateTime.Now;
                    Entry(Data).State = EntityState.Modified;


                }
                if (fiturid == 10)
                {
                    var Data = await ayda.Where(es => es.id == idrequest).FirstOrDefaultAsync();
                    Data.statusid = 11;
                    Data.lastupdatedate = DateTime.Now;
                    Entry(Data).State = EntityState.Modified;
                }
                if (fiturid == 16)
                {
                    var Data = await insurance.Where(es => es.Id == idrequest).FirstOrDefaultAsync();
                    Data.statusid = 11;
                    Data.lastupdateddated = DateTime.Now;
                    Entry(Data).State = EntityState.Modified;
                }
                if (fiturid == 15)
                {
                    var Data = await auction.Where(es => es.id == idrequest).FirstOrDefaultAsync();
                    Data.statusid = 11;
                    Data.lastupdatedate = DateTime.Now;
                    Entry(Data).State = EntityState.Modified;
                }
                await SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<(bool Status, string message, Dictionary<string, List<dynamic>> DataWorkflow)> GetDetailWorkflow(GetDetailWFDTO Entity)
        {
            var Collection = new Dictionary<string, List<dynamic>>();
            Collection["DataWorkflow"] = new List<dynamic>();
            Collection["DataWorkflowHistory"] = new List<dynamic>();
            try
            {
                var CheckingWF = await _RecoveryContext.Workflows.Where(es => es.Requestid == Entity.RequestId && es.Fiturid == Entity.FiturId).ToListAsync();
                var DataMaxId = CheckingWF.Max(es => es.Id);

                var DataWorkflow = CheckingWF.Where(es=>es.Id==DataMaxId).Select(es=>new WorkflowDetailDTO
                {
                    statusid=es.Status,
                    Status=status.Where(x=>x.sts_id==es.Status).Select(es=>es.sts_name).FirstOrDefault(),
                    actor=es.Actor,
                    CreatedDated=es.Submitdated.ToString(),
                    fiturid=es.Fiturid,
                    fitur=generalparamdetail.Where(es=>es.Id==Entity.FiturId).Select(es=>es.title).FirstOrDefault(),
                    flowid=es.Flowid,
                    Id=es.Id,
                    requestid=es.Requestid
                }).ToList();
              
                Collection["DataWorkflow"].Add(DataWorkflow);
                foreach (var x in CheckingWF)
                {
                    var DataWorkflowHistory = await _RecoveryContext.Workflowhistories.Where(es => es.Workflowid == x.Id).ToListAsync();
                    var GetDataWorkflowHistory = DataWorkflowHistory.Select(es => new WorkflowHistoryDTO
                    {
                        statusid = es.Status,
                        status = status.Where(x => x.sts_id == es.Status).Select(es => es.sts_name).FirstOrDefault(),
                        actor = es.Actor,
                        dated = es.Dated,

                        ActoredBy = null,
                        reason = es.Reason,
                        workflowid = es.Workflowid,
                        id = es.Id

                    }).ToList();
                    Collection["DataWorkflowHistory"].Add(GetDataWorkflowHistory);

                };
                return (true, "OK", Collection);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        //SERVICE YANG DIPAKAI
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

                //update status request
              //await  UpdateStatusRequest(Entity.idfitur, Entity.idrequest, 8);
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
