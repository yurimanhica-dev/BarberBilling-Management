namespace BarberBilling.Api.Security.Authorization;

public static class Permissions
{
    public static class Categories
    {
        public const string Read = "categories:read";
    }
    public static class Users
    {
        public const string Create = "users:create";
        public const string Read = "users:read";
        public const string Update = "users:update";
        public const string Delete = "users:delete";
    }
    public static class Authorization
    {
        public const string CreateRole = "authorization:create-role";
        public const string CreatePermission = "authorization:create-permission";
        public const string GetAllRoles = "authorization:get-all-roles";
        public const string GetAllPermissions = "authorization:get-all-permissions";
        public const string RevokePermission = "authorization:revoke-permission";
        public const string AssignPermission = "authorization:assign-permission";
    }

    public static class Services
    {
        public const string Create = "services:create";
        public const string Read = "services:read";
        public const string Update = "services:update";
        public const string Delete = "services:delete";
        // public const string Toggle = "services:toggle";
    }

    public static class Billings
    {
        public const string Create = "billings:create";
        public const string Read = "billings:read";
        public const string ReadById = "billings:read-by-id";
        public const string Update = "billings:update";
        public const string Delete = "billings:delete";
    }

    public static class Bookings
    {
        public const string Create = "bookings:create";
        public const string Read = "bookings:read";
        public const string ReadById = "bookings:read-by-id";
        public const string Update = "bookings:update";
        public const string Delete = "bookings:delete";
    }

    public static class Reports
    {
        public const string ReadBillings = "reports:billings-read";
    }
}