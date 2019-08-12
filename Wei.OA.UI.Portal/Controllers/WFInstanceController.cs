using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Data.Entity.SqlServer;

    using Wei.OA.BLL;
    using Wei.OA.IBLL;
    using Wei.OA.Model;
    using Wei.OA.Workflow;

    public class WFInstanceController : BaseController
    {
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IWF_TempService WF_TempService { get; set; }

        public IWF_InstanceService WF_InstanceService { get; set; }

        public IWF_StepService WF_StepService { get; set; }

        // GET: WFInstance
        public ActionResult Index()
        {
            return View();
        }

        #region 发起流程

        //发起流程页面 id:流程模板的id
        public ActionResult Add(int id)
        {
            var temp = WF_TempService.GetEntities(u => u.Id == id && u.DelFlag == this.delFlagNormal).FirstOrDefault();
            ViewBag.Temp = temp;
            var allUsers = UserInfoService.GetEntities(u => u.DelFlag == this.delFlagNormal).ToList();
            ViewData["flowTo"] = (from u in allUsers
                                    select new SelectListItem()
                                               {
                                                   Selected = false,
                                                   Text = u.UName,
                                                   Value = u.Id + ""
                                               })
                .ToList();
            return this.View();
        }
        //发起流程-----------------
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WF_Instance instance, int id,int flowTo)
        {
            var currentUserId = LoginUser.Id;
            //在工作流实例表添加一条数据
            instance.DelFlag = this.delFlagNormal;
            instance.StartTime = DateTime.Now;
            instance.FilePath = string.Empty;
            instance.StartBy = currentUserId;
            instance.Level = 0;
            instance.Status = (short)Wei.OA.Model.Enum.WF_InstanceEnum.Processing;
            instance.WFInstanceId = Guid.Empty;
            instance.WF_TempId = id;
            WF_InstanceService.Add(instance);
            //启动工作流
            var wfApp = WorkflowApplictionHelper.CreateWorkflowApp(new FinancialActivity(), null); //可以通过反射，更灵活的添加不同的activity
            instance.WFInstanceId = wfApp.Id;
            WF_InstanceService.Update(instance);
            //在步骤表中添加两个步骤，一个当前已经处理完的步骤
            WF_Step startStep = new WF_Step();
            startStep.WF_InstanceId = instance.Id;
            startStep.SubTime = DateTime.Now;
            startStep.StepName = "提交审批表单";
            startStep.IsEndStep = false;
            startStep.IsStartStep = true;
            startStep.ProcessBy = currentUserId;
            startStep.ProcessComment = "提交申请";
            startStep.ProcessResult = "通过";
            startStep.ProcessTime = DateTime.Now;            
            startStep.StepStatus = (short)Wei.OA.Model.Enum.WFStepEnum.Processed;

            WF_StepService.Add(startStep);

            //下一步谁审批的步骤
            WF_Step nextStep = new WF_Step();
            nextStep.WF_InstanceId = instance.Id;
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessTime = DateTime.Now;
            nextStep.ProcessComment = String.Empty;
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo;
            nextStep.ProcessResult = "";
            nextStep.StepName = "";
            nextStep.StepStatus = (short)Wei.OA.Model.Enum.WFStepEnum.UnProcess;
            WF_StepService.Add(nextStep);
            return RedirectToAction("ShowMyCheck");

        }
        #endregion

        #region 我的审批流程
        //我发起的审批请求
        public ActionResult ShowMyCheck()
        {
            var data = WF_InstanceService.GetEntities(i => i.StartBy == LoginUser.Id).ToList();
            return this.View(data);
        }
        
        //待审批（需要我审批的）
        public ActionResult ShowMyUnCheck()
        {
            var runEnum = (short)Wei.OA.Model.Enum.WFStepEnum.UnProcess;
            var instanceEnum = (short)Wei.OA.Model.Enum.WF_InstanceEnum.Processing;
            //拿到当前用户未处理的步骤
            var steps = WF_StepService.GetEntities(s => s.StepStatus == runEnum && s.ProcessBy == LoginUser.Id)
                .ToList();
            //通过未处理的步骤拿到流程实例id
            var instanceId = (from s in steps select s.WF_InstanceId).Distinct();
            //根据流程实例id、拿到所有流程
            var data = WF_InstanceService.GetEntities(i => instanceId.Contains(i.Id) && i.Status == instanceEnum)
                .ToList();
            return this.View(data);
        }
        //我已审批
        public ActionResult ShowMyChecked()
        {
            var runEnum = (short)Wei.OA.Model.Enum.WFStepEnum.UnProcess;
            var instanceEnum = (short)Wei.OA.Model.Enum.WF_InstanceEnum.Processed;


            var steps = WF_StepService.GetEntities(s => s.StepStatus == runEnum && s.ProcessBy == LoginUser.Id).ToList();

            var instanceIds = (from s in steps
                               select s.WF_InstanceId).Distinct();
            var data = WF_InstanceService.GetEntities(i => instanceIds.Contains(i.Id)).ToList();

            return View(data);
        }


        #endregion

        #region 审批详情

        public ActionResult ShowInstance(int id)
        {
            ViewData.Model = WF_InstanceService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }

        #endregion

        #region 审批

        public ActionResult ShowCheck(int id)
        {
            ViewData.Model = WF_InstanceService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData["flowTo"] =
                UserInfoService.GetEntities(u => u.DelFlag == this.delFlagNormal).ToList()
                    .Select(u => new SelectListItem() { Selected = false, Text = u.UName, Value = u.Id.ToString() });

            return View();
        }

        //审批
        public ActionResult DoCheck(int stepId, bool isPass, string Comment, int flowTo)
        {
            //stepId:id, isPass: pass, Comment: $("#Comment").val() }

            //1、更新当前步骤
            var step = WF_StepService.GetEntities(s => s.Id == stepId).FirstOrDefault();

            step.ProcessResult = isPass ? "通过" : "不通过";
            step.StepStatus = (short)Wei.OA.Model.Enum.WFStepEnum.Processed;
            step.ProcessTime = DateTime.Now;

            step.ProcessComment = Comment;
            WF_StepService.Update(step);


            //初始化下一个步骤
            WF_Step nextStep = new WF_Step();
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo; //下一个步骤处理人
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessResult = string.Empty;

            nextStep.StepStatus = (short)Wei.OA.Model.Enum.WFStepEnum.UnProcess;
            nextStep.StepName = string.Empty;
            nextStep.WF_InstanceId = step.WF_InstanceId;
            nextStep.ProcessTime = DateTime.Now;
            nextStep.ProcessComment = string.Empty;

            WF_StepService.Add(nextStep);

            //让书签继续往下执行。
            var Value = isPass ? 1 : 0;

            Wei.OA.Workflow.WorkflowApplictionHelper.ResumeBookMark(
                new FinancialActivity(), 
                step.WF_Instance.WFInstanceId,
                step.StepName,
                Value);
            return Content("ok");
        }


        #endregion

    }
}