namespace FitnessManager.BusinessLogic.Common
{
    public enum BusinessLogicResponseResult
    {
        Ok,
        ConflictOccured,
        UserIsNotAuthorized,
        AccessDenied,
        ResourceDoesntExist,
        Created,
        Updated,
        Deleted   
    }
}