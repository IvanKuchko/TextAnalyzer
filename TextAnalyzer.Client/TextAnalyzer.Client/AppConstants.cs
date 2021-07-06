namespace TextAnalyzer.Client
{
    public static class AppConstants
    {
        public const string BaseAddressUrl = "https://localhost:5001/";

        public const string SignInUrl = BaseAddressUrl + "api/Account/SignIn";
        public const string SignUpUrl = BaseAddressUrl + "api/Account/SignUp";
        public const string SignOutUrl = BaseAddressUrl + "api/Account/SignOut";
        public const string GetUsersUrl = BaseAddressUrl + "api/User/GetUsers";
        public const string AddValueUrl = BaseAddressUrl + "api/Analyzer/AddValue";
        public const string CompareWithUserUrl = BaseAddressUrl + "api/Analyzer/CompareWithUser";
        public const string CompareWithUsersUrl = BaseAddressUrl + "api/Analyzer/CompareWithUsers";
<<<<<<< HEAD
        public const string CompareWithRandomUserUrl = BaseAddressUrl + "api/Analyzer/CompareWithRandomUser";
=======
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb

        public const string ErrorTitle = "Ошибка";
        public const string NotificationTitle = "Уведомление";
        public const string WarningTitle = "Предупреждение";

        public const string OkButton = "ОК";
        public const string CancelButton = "Отмена";
        public const string AcceptButton = "Да";
        public const string DenyButton = "Нет";

        public const string NoInternetConnectionMessage = "Нет подключения к интернету";
        public const string AuthorizationErrorMessage = "Произошла ошибка при авторизации";
        public const string RegistrationErrorMessage = "Произошла ошибка при регистрации";
    }
}
