namespace TaskScheduler.Web.Models.Enums
{
    public enum ActionOutcome
    {
        None,
        CreateSuccess,
        CreateWithLinkedShutdownSuccess,
        EditSuccess,
        EditWithLinkedShutdownSuccess,
        DeleteSuccess,
        DeleteWithLinkedShutdownSuccess
    }
}