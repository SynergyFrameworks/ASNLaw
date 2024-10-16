using log4net;
using Infrastructure.Common.Persistence;
using Infrastructure.Query;
using System;
using System.Collections.Generic;


namespace Infrastructure
{
    public class ActivityManager : BaseDataReadManager, IActivityManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ActivityManager));
        public IEntityManager EntityManager { get; set; }
        public IReadOnlyDataManager ReadOnlyDataManager { get; set; }
        //public IChangeReportManager ChangeReportManager { get; set; }

        public IList<Activity> GetActivitys()
        {
            Log.InfoFormat("Retrieving All Activities");
            return EntityManager.FindAll<Activity>();
        }

        public Activity GetActivityByActivityId(Guid activityId)
        {
            Log.InfoFormat("Finding activity {0}", activityId);
            try
            {
                return EntityManager.Find<Activity>(activityId);
            }
            catch (Exception)
            {
                Log.InfoFormat("Unable to find activity {0}", activityId);
                return null;
            }
        }

        public IList<ActivityByObject> GetActivitysByObjectId(Criteria criteria)
        {
            IList<ActivityByObject> results = ReadOnlyDataManager.FindAll<ActivityByObject>(criteria);
            return results;
        }

        public IList<Activity> GetActivitysByObjectIds(IList<Guid> objectIds)
        {
            throw new NotImplementedException();
        }

        // We currently don't listen for this update so disabling for now.
        //[UpdateClient("activityUpdateHandler")]
        public Activity SaveActivity(Activity activity)
        {
            Log.DebugFormat("Saving activity {0}", activity.Id);
            try
            {
                return EntityManager.SaveOrUpdate(activity);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error Saving activity {0}. {1}", activity.Id, ex);
                throw;
            }
        }

        public Activity CreateActivityObject(Guid objectId, string text)
        {
            Activity activity = new Activity
            {
                ObjectId = objectId,
                ActivityText = text
            };
            return activity;
        }


        public Activity CreateActivityObjectAndSave<T>(Guid objectId, string text, T objectPrevious, string[] ignoreFieldNameList = null, string[] ignoreTypeNameList = null)
        {


            T objectAfter = EntityManager.Find<T>(objectId);

            //var changeReport = ChangeReportManager.CompareChange(objectPrevious, objectAfter, ignoreFieldNameList, ignoreTypeNameList);
            //changeReport.ChangeReportName = text;
            Activity activity = new Activity
            {
                ObjectId = objectId,
                ActivityText = text,
                //ChangeReportJson = changeReport == null ? null : JsonConvert.SerializeObject(changeReport)
            };

            return SaveActivity(activity);
        }

        public Guid DeleteActivity(Guid activityId)
        {
            Log.InfoFormat("Deleting activity {0}", activityId);
            try
            {
                Activity activity = GetActivityByActivityId(activityId);
                EntityManager.Delete(activity);
                return activityId;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Error Deleting Activity {0}. {1}", activityId, ex);
                throw;
            }
        }
    }
}
