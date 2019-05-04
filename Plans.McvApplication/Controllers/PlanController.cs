using Plans.Api.Models;
using Plans.Api.Models.Extensions;
using Plans.McvApplication.Requests;
using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Plans.McvApplication.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            var api = new PlanApiClient();
            var resp = await api.GetPlans();
            ViewBag.Plans = resp;
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            if(id != -1)
            {
                var api = new PlanApiClient();
                var resp = await api.GetPlan(id);
                ViewBag.Plan = resp;
            } else
            {
                ViewBag.Plan = new PlanApi();
            }
            return View();
        }

        public async Task<ActionResult> Save(PlanApi planApi)
        {
            var api = new PlanApiClient();
            var convertedPlan = planApi.ToPlan();
            await api.Save(convertedPlan);
            ViewBag.Plan = convertedPlan;
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var api = new PlanApiClient();
            var resp = await api.Delete(id);
            ViewBag.Plan = resp;
            return RedirectToAction("List");
        }
    }
}