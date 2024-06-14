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
using Org.BouncyCastle.Asn1.Ocsp;
using System.Numerics;
using sky.recovery.DTOs.WorkflowDTO;
using sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery;
using sky.recovery.Insfrastructures.Scafolding.SkyEn.Workflow;

using OfficeOpenXml.Drawing.Controls;
using sky.recovery.DTOs.RequestDTO.Workflow;
namespace sky.recovery.Services
{
    public class WorkflowServices:SkyCoreConfig, IWorkflowServices
    {
        ModellingGeneralResponsesV2 _DataResponses = new ModellingGeneralResponsesV2();
        sky.recovery.Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext _RecoveryContext = new Insfrastructures.Scafolding.SkyColl.Recovery.SkyCollRecoveryDBContext();
        SkyEnWorkflowDBContext _WorkflowEngineContext = new SkyEnWorkflowDBContext();
        
        
        private IUserService _User { get; set; }

        public WorkflowServices(IOptions<DbContextSettings> appsetting, IUserService user) : base(appsetting)
        {
            _User = user;
        }

        public async Task<(bool Status, string message)> CreateWorkflowEngine(AddWorkflowEngineDTO Entity)
        {
            try
            {
                var CheckFiturId =await  _RecoveryContext.Masterflowengine.Where(es => es.Fiturid == Entity.FiturId).AnyAsync();
                if(CheckFiturId==true)
                {
                    return (false, "Fitur Tersebut Sudah Memiliki Workflow Model");
                }
                else
                {

                
                var Data = new Masterworkflowengine()
                {
                    Fiturid = Entity.FiturId,
                    Flowid = Entity.flowid,
                    Isactive = true,
                    Wfcode = Entity.flhcode,
                    Wfname = Entity.wfname
                };
                    await _RecoveryContext.Masterworkflowengine.AddAsync(Data);
                    await _RecoveryContext.SaveChangesAsync();
                    return (true, "OK");
                }

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool status, string message, List<dynamic> Data)> CreateNodesEngine(long flowid, int FiturId)
        {
            // var Param = new list["start","QUE"];
            var CheckingFitur = await _RecoveryContext.Masterflowengine.Where(es => es.Fiturid == FiturId).AnyAsync();
           
            try
            {
                var Datas = await _WorkflowEngineContext.FlowsNodes.Where(es =>
                !es.FlnNodesId.Contains("start") && !es.FlnNodesId.Contains("QUE") &&
                es.FlnFlhId == flowid).Select(es => new
                {
                    fln_id = es.FlnId,
                    fln_flh_id = es.FlnFlhId,
                    fln_nodes_id = es.FlnNodesId,
                    fln_nodes_text = es.FlnNodesText


                })
                .OrderBy(es => es.fln_id).ToListAsync<dynamic>();
                var ListData = new List<dynamic>();
                for (var x = 0; x < Datas.Count; x++)
                {
                    var Data = new
                    {
                        fln_id = Datas[x].fln_id,
                        fln_flh_id = Datas[x].fln_flh_id,
                        fln_nodes_id = Datas[x].fln_nodes_id,
                        fln_nodes_text = Datas[x].fln_nodes_text,
                        Index = x
                    };
                    ListData.Add(Data);
                };
                if (CheckingFitur == true)
                {
                    var Datax = await _RecoveryContext.Masterflowengine.Where(es => es.Flowid == flowid).ToListAsync<dynamic>();

                    foreach(var x in ListData)
                    {
                        foreach(var p in Datax)
                        {
                            p.nodesid = x.fln_nodes_id;
                            p.title = x.fln_nodes_text;
                            p.orders = x.Index;
                            _RecoveryContext.Entry(Datax).State = EntityState.Modified;

                        };
                    };
                    await _RecoveryContext.SaveChangesAsync();

                }
                else
                {
                    foreach (var x in ListData)
                    {

                        var DataFlow = new Masterflowengine()
                        {
                            Fiturid = FiturId,
                            Flowid = flowid,
                            Nodesid = x.fln_nodes_id,
                            Orders = x.Index,
                            Title = x.fln_nodes_text
                        };
                      await  _RecoveryContext.Masterflowengine.AddAsync(DataFlow);
                        await _RecoveryContext.SaveChangesAsync();
                          

                    };
                }
                

          
               
                return (true, "OK", ListData);

            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

        public  async Task<(bool status,string message,List<dynamic>Data)> GetNodesWorkflowEngine(long flowid)
        {
            try
            {
                var Data = await _WorkflowEngineContext.FlowsNodes.Where(es => es.FlnFlhId == flowid).Select(es => new
                {
                    fln_id=es.FlnId,
                    fln_flh_id=es.FlnFlhId,
                    fln_nodes_id=es.FlnNodesId,
                    fln_nodes_text = es.FlnNodesText

                }).OrderBy(es=>es.fln_id).ToListAsync<dynamic>();
                return (true, "OK", Data);
            }
            catch(Exception ex)
            {
                return (false, ex.Message,null);
            }
        }
        public async Task<(bool Status, string message, List<dynamic?> Data)>GetListWorkflow()
        {
            try
            {
                var Data = await _WorkflowEngineContext.Flows.Where(es => es.FlnIsdelete == false
                && es.FlhType == "workflow" && es.FlhApproverStatus== "synced").Select(es=>new
                {
                  
                    FlowId = es.FlhId,
                    Name = es.FlhName,
                    Description = es.FlhDesc,
                    Type = es.FlhType,
                    Code = es.FlhCode

                }).ToListAsync<dynamic>();
                return (true, "OK", Data);
            }
            catch(Exception ex)
            {
                return (false, ex.Message,null);
            }
        }

        public async Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy_Engine(string userid, CallbackApprovalDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var getCallBy = await _User.GetDataUser(userid);
            var DataHistoryWF = new Workflowhistory();
            var DataHistoryWF2 = new Workflowhistory();
            try
            {

                var DataWorkflow = _RecoveryContext.Workflow.Where(es => es.Id == Entity.workflowid).FirstOrDefault();
                //update status requestheader
                var OrdersAdd = DataWorkflow.Orders + 1;
                var DataNext = _RecoveryContext.Masterflowengine.Where(es => es.Fiturid == Entity.fiturid && es.Orders == OrdersAdd && es.Title != "Done").FirstOrDefault();
                if (DataNext != null)
                {
                    if (Entity.status == 4)//APPROVE
                    {
                        //simpan history approval pertama
                        DataHistoryWF.Status = 4;
                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF2.Status = 11;
                        //get user by role default approval
                        DataHistoryWF2.Actor = null;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;

                        DataWorkflow.Status = 11;
                        DataWorkflow.Actor = null;
                        DataWorkflow.Orders = OrdersAdd;
                        DataWorkflow.Flowid = (int)DataNext.Id;
                        DataWorkflow.Modifydated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 5)//REJECT
                    {
                        //simpan history approval pertama
                        DataHistoryWF.Status = 5;
                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;

                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);


                        DataWorkflow.Status = 12;
                        DataWorkflow.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Reason = Entity.reason;
                        //get user by role default approval

                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.Status = 13;
                        DataHistoryWF.Status = 13;
                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;

                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        var DataAwal = _RecoveryContext.Masterflowengine.Where(es => es.Fiturid== Entity.fiturid && es.Orders== 1).FirstOrDefault();
                        DataWorkflow.Status = 10;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Actor = Entity.idrequestor;
                        DataWorkflow.Reason = Entity.reason;
                        DataWorkflow.Orders = DataAwal.Orders;
                        //DataWorkflow.Flowid = DataAwal.Flowid;

                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Status = 10;

                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }

                }


                else
                {
                    if (Entity.status == 4 || Entity.status == 5)//APPROVE
                    {
                        DataWorkflow.Status = 12;


                        DataHistoryWF.Status = Entity.status;
                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = DataWorkflow.Actor;

                        DataHistoryWF2.Workflowid = Entity.workflowid;

                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.Status = 13;
                        DataHistoryWF.Status = 13;
                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = DataWorkflow.Actor;

                        DataHistoryWF2.Workflowid = Entity.workflowid;

                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        //var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid).FirstOrDefault();
                        DataWorkflow.Status = 10;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataWorkflow.Reason = Entity.reason;
                        DataWorkflow.Orders = 0;

                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Status = 10;

                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }

                }

                //UPDATE MASTER REQUEST
                if (Entity.fiturid == 9)//restrukture
                {
                    var Data = await _RecoveryContext.Restrukture.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 10)//AYDA
                {
                    var Data = await _RecoveryContext.Ayda.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 15)//auction
                {
                    var Data = await _RecoveryContext.Auction.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 16)//INSURANCE
                {
                    var Data = await _RecoveryContext.Insurance.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdateddated = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }

                _RecoveryContext.Entry(DataWorkflow).State = EntityState.Modified;
                // await workflow.AddAsync(DataWorkflow);

                await _RecoveryContext.SaveChangesAsync();
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
            public async Task<(bool? Status, GeneralResponsesV2 Returns)> CallbackApproval_Dummy(string userid,CallbackApprovalDTO Entity)
        {
            var wrap = _DataResponses.Return();
            var getCallBy = await _User.GetDataUser(userid);
            var DataHistoryWF = new Workflowhistory();
            var DataHistoryWF2 = new Workflowhistory();

            //get spv by role
            try
            {
                var DataWorkflow = _RecoveryContext.Workflow.Where(es => es.Id == Entity.workflowid).FirstOrDefault();
                //update status requestheader
                var OrdersAdd = DataWorkflow.Orders + 1;
                var DataNext = _RecoveryContext.Masterflow.Where(es => es.Fiturid== Entity.fiturid && es.Orders== OrdersAdd && es.Roleid!= null).FirstOrDefault();
                if (DataNext != null)
                {
                    if (Entity.status == 4)//APPROVE
                    {
                        //simpan history approval pertama
                        DataHistoryWF.Status = 4;
                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF2.Status = 11;
                        //get user by role default approval
                        DataHistoryWF2.Actor = null;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;

                        DataWorkflow.Status = 11;
                        DataWorkflow.Actor = null;
                        DataWorkflow.Orders = OrdersAdd;
                        DataWorkflow.Flowid = (int)DataNext.Id;
                        DataWorkflow.Modifydated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 5)//REJECT
                    {
                        //simpan history approval pertama
                        DataHistoryWF.Status = 5;
                        DataHistoryWF.Actor= getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;

                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);


                        DataWorkflow.Status = 12;
                        DataWorkflow.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Reason = Entity.reason;
                        //get user by role default approval

                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.Status = 13;
                        DataHistoryWF.Status = 13;
                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Dated= DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;

                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor= getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF2.Workflowid = Entity.workflowid;
                        DataHistoryWF2.Dated = DateTime.Now;
                       _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid && es.orders==0).FirstOrDefault();
                        DataWorkflow.Status = 10;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Actor = Entity.idrequestor;
                        DataWorkflow.Reason = Entity.reason;
                        DataWorkflow.Orders = DataAwal.orders;
                        DataWorkflow.Flowid = DataAwal.id;

                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Status= 10;

                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }
                   
                }

            
                else
                {
                    if (Entity.status == 4 || Entity.status == 5)//APPROVE
                    {
                        DataWorkflow.Status= 12;


                        DataHistoryWF.Status = Entity.status;
                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = DataWorkflow.Actor;

                        DataHistoryWF2.Workflowid = Entity.workflowid;

                        DataHistoryWF2.Dated= DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);
                    }
                    if (Entity.status == 13)//CANCEL
                    {
                        DataWorkflow.Status = 13;
                        DataHistoryWF.Status = 13;
                        DataHistoryWF2.Status = 12;
                        DataHistoryWF2.Actor = DataWorkflow.Actor;

                        DataHistoryWF2.Workflowid= Entity.workflowid;

                        DataHistoryWF2.Dated = DateTime.Now;
                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF2);

                    }
                    if (Entity.status == 10)//REVISI
                    {
                        //var DataAwal = masterflow.Where(es => es.fiturid == Entity.fiturid).FirstOrDefault();
                        DataWorkflow.Status = 10;
                        DataWorkflow.Modifydated = DateTime.Now;
                        DataWorkflow.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataWorkflow.Reason = Entity.reason;
                        DataWorkflow.Orders = 0;

                        DataHistoryWF.Actor = getCallBy.Returns.Data.FirstOrDefault().iduser;
                        DataHistoryWF.Dated = DateTime.Now;
                        DataHistoryWF.Reason = Entity.reason;
                        DataHistoryWF.Workflowid = Entity.workflowid;
                        DataHistoryWF.Status = 10;

                        _RecoveryContext.Workflowhistory.Add(DataHistoryWF);

                    }
                   
                }

                //UPDATE MASTER REQUEST
                if(Entity.fiturid==9)//restrukture
                {
                    var Data = await _RecoveryContext.Restrukture.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;
                    
                }
                if (Entity.fiturid == 10)//AYDA
                {
                    var Data = await ayda.Where(es => es.id== Entity.idrequest).FirstOrDefaultAsync();
                    Data.statusid = Entity.status;
                    Data.lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 15)//auction
                {
                    var Data = await _RecoveryContext.Auction.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdatedate = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }
                if (Entity.fiturid == 16)//INSURANCE
                {
                    var Data = await _RecoveryContext.Insurance.Where(es => es.Id == Entity.idrequest).FirstOrDefaultAsync();
                    Data.Statusid = Entity.status;
                    Data.Lastupdateddated = DateTime.Now;

                    _RecoveryContext.Entry(Data).State = EntityState.Modified;

                }

                _RecoveryContext.Entry(DataWorkflow).State = EntityState.Modified;
                // await workflow.AddAsync(DataWorkflow);

                await _RecoveryContext.SaveChangesAsync();
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

        public async Task<(bool Status, string message,  
            List<WorkflowDetailDTO> WorkflowDetail,
            List<WorkflowHistoryDTO> WorkflowHistory)> 
            GetDetailWorkflow(GetDetailWFDTO Entity)
        {
            var DataWorkflowHistorys = new List<WorkflowHistoryDTO>();
      
            try
            {
                var CheckingWF = await _RecoveryContext.Workflow.Where(es => es.Requestid == Entity.RequestId && es.Fiturid == Entity.FiturId).ToListAsync();
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
              
                foreach (var x in CheckingWF)
                {
                    var DataWorkflowHistory = await _RecoveryContext.Workflowhistory.Where(es => es.Workflowid == x.Id).ToListAsync();
                    DataWorkflowHistorys = DataWorkflowHistory.Select(es => new WorkflowHistoryDTO
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

                };
                return (true, "OK", DataWorkflow,DataWorkflowHistorys);
            }
            catch(Exception ex)
            {
                return (false, ex.Message, null,null);
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
                var GetData = _RecoveryContext.Masterflowengine.Where(es => es.Orders==2 && es.Fiturid== Entity.idfitur).FirstOrDefault();
                var GetWorkflowId = await _RecoveryContext.Masterworkflowengine.
                    Where(es => es.Fiturid == Entity.idfitur).Select(es => es.Id).FirstOrDefaultAsync();
                //get user yang role 45

                var DataWorkflow = new Workflow()
                {
                    Submitdated=DateTime.Now,
                    Orders=GetData.Orders,
                    Status=11,
                    Fiturid=Entity.idfitur,
                    Actor=getSPV.Returns.Data.FirstOrDefault().iduser,
                    Flowid=(int)GetData.Flowid,
                    Requestid=Entity.idrequest,
                    Masterworkflowid=(int)GetWorkflowId
                };


                    
                    await _RecoveryContext.Workflow.AddAsync(DataWorkflow);
                    await _RecoveryContext.SaveChangesAsync();
                

                var DataWFHistory_Req = new Workflowhistory()
            {
                Workflowid=DataWorkflow.Id,
             Actor=getCallBy.Returns.Data.FirstOrDefault().iduser,
             Status=8,
             Dated=DateTime.Now
            };

                await _RecoveryContext.Workflowhistory.AddAsync(DataWFHistory_Req);
                await _RecoveryContext.SaveChangesAsync();

                var DataWFHistory_Usr = new Workflowhistory()
                {
                    Workflowid = DataWorkflow.Id,
                    Actor = getSPV.Returns.Data.FirstOrDefault().iduser,
                    Status = 11,
                    Dated = DateTime.Now
                };
                await _RecoveryContext.Workflowhistory.AddAsync(DataWFHistory_Usr);
                await _RecoveryContext.SaveChangesAsync();

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
