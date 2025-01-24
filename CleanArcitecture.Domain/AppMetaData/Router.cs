namespace CleanArcitecture.Domain.AppMetaData
{
    public static class Router
    {
        public const string Root = "Api";
        public const string Version = "V1";
        public const string Rule = Root + "/" + Version + "/";

        public static class StudentRouting
        {
            public const string Prefix = "Student";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/GetById/" + "{Id}";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{Id}";
        }
        public static class UserRouting
        {
            public const string Prefix = "User";
            public const string List = Prefix + "/List";
            public const string GetById = Prefix + "/GetById/" + "{Id}";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{Id}";
            public const string ChangePassword = Prefix + "/ChangePassword";
        }
        public static class AuthenticationRouting
        {
            //public const string Prefix = "Authentication";
            public const string SignIn = "SignIn";
            public const string RefreshToken = "RefreshToken";
            public const string ValidateToken = "ValidateToken";
            public const string SendResetPasswordCode = "SendResetPasswordCode";
            public const string ConfirmOTP = "ConfirmOTP";
            public const string ResetPassword = "ResetPassword";

        }
        public static class RoleRouting
        {
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string List = "List";
            public const string GetById = "GetById/" + "{Id}";
        }
        public static class AuthorizationRouting
        {
            public const string UserRoles = "UserRoles/" + "{userId}";
            public const string UpdateUserRoles = "UpdateUserRoles";
            public const string UserClaims = "UserClaims/" + "{userId}";
            public const string UpdateUserClaims = "UpdateUserClaims";
        }
        public static class EmailRouting
        {
            public const string SendEmail = "SendEmail";
        }

    }
}
