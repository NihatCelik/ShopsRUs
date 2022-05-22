namespace Core.Utilities.UserRequest
{
    public interface IUserRequestService
    {
        int RequestCompanyId
        {
            get;
        }
        bool IsAdmin
        {
            get;
        }
        bool IsCustomer
        {
            get;
        }
        int RequestUserId
        {
            get;
        }
    }
}
