using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plans.Models.View
{
    public class TotalPlansByUser
    {
        public TotalPlansByUser(int USER_ID, string USER_NAME, int TOTAL_PLANS, int TOTAL_DONE_ON_TIME, int TOTAL_DONE_OUT_TIME, int TOTAL_NOT_STARTED, int TOTAL_ON_PROGRESS, int TOTAL_SUSPENDED_OR_CANCELED)
        {
            UserId = USER_ID;
            UserName = USER_NAME;
            TotalPlans = TOTAL_PLANS;
            DoneOnTime = TOTAL_DONE_ON_TIME;
            DoneOutTime = TOTAL_DONE_OUT_TIME;
            NotStarted = TOTAL_NOT_STARTED;
            OnProgress = TOTAL_ON_PROGRESS;
            SustendedAndCanceled = TOTAL_SUSPENDED_OR_CANCELED;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalPlans { get; set; }
        public int DoneOnTime { get; set; }
        public int DoneOutTime { get; set; }
        public int NotStarted { get; set; }
        public int OnProgress { get; set; }
        public int SustendedAndCanceled { get; set; }

        public override string ToString()
        {
            return $"[UserId: {UserId}\nUserName: {UserName}\nTotalPlans: {TotalPlans}\nTotalDoneOnTime: {DoneOnTime}\nTotalDoneOutTime: {DoneOutTime}\nTotalNotStarted: {NotStarted}\nTotalOnProgress: {OnProgress}\nTotalSustendedAndCanceled: {SustendedAndCanceled}]\n";
        }
    }
}
