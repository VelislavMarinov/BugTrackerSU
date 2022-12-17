namespace BugTrackerSU.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "BugTrackerSU";

        public const string AdministratorRoleName = "Administrator";

        public const string ProjectManagerRoleName = "Project Manager";

        public const string SubmitterRoleName = "Submitter";

        public const string DeveloperRoleName = "Developer";

        public const string AdminManagerSubmiterRolesAuthorization = $"{AdministratorRoleName},{ProjectManagerRoleName}";
    }
}
