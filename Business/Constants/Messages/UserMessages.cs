using Business.Helpers;

namespace Business.Constants
{
    public static partial class Messages
    {
        public static string UserNotFound => TranslatorHelper.GetByKey("UserNotFound");
        public static string UserIsNotActive => TranslatorHelper.GetByKey("UserIsNotActive");
        public static string UserAlreadyExist => TranslatorHelper.GetByKey("UserAlreadyExist");
        public static string PhoneAlreadyExist => TranslatorHelper.GetByKey("PhoneAlreadyExist");
        public static string EmailAlreadyExist => TranslatorHelper.GetByKey("EmailAlreadyExist");
        public static string AuthorizationsDenied => TranslatorHelper.GetByKey("AuthorizationsDenied");
        public static string AuthenticationDenied => TranslatorHelper.GetByKey("AuthenticationDenied");
        public static string ResetPasswordMailSent => TranslatorHelper.GetByKey("ResetPasswordMailSent");
        public static string EmailNotFound => TranslatorHelper.GetByKey("EmailNotFound");
    }
}
