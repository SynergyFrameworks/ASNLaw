using System;
using System.Collections.Generic;
using Infrastructure.Common.Persistence;
using Infrastructure.Common.Domain;
using Infrastructure.Query;

namespace Infrastructure
{ 
    public interface IActivityManager:IBaseDataReadManager
    {
        IList<Activity> GetActivitys();
        Activity GetActivityByActivityId(Guid activityId);
        IList<ActivityByObject> GetActivitysByObjectId(Criteria criteria);
        IList<Activity> GetActivitysByObjectIds(IList<Guid> objectIds);
        Activity SaveActivity(Activity activity);
        Activity CreateActivityObject(Guid objectId, string text);
        Activity CreateActivityObjectAndSave<T>(Guid objectId, string text, T objectPrevious, string[] ignoreFieldNameList = null, string[] ignoreTypeNameList = null);
        Guid DeleteActivity(Guid activityId);
    }
}
