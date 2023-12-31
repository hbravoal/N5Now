﻿namespace N5.Event.User;

/// <summary>
/// Event for Realty
/// </summary>
public static class EventUser
{
    #region Permission
    /// <summary>
    /// Create permission.
    /// </summary>
    public const string CreatePermission = "public.n5.user.permission.create";
    /// <summary>
    /// End process Create permission.
    /// </summary>
    public const string CreatePermissionComplete = "public.n5.user.permission.create.createpermissioncomplete";

    /// <summary>
    /// Modify permission.
    /// </summary>
    public const string ModifyPermission = "public.n5.user.permission.modify";
    /// <summary>
    /// End process Modify permission.
    /// </summary>
    public const string ModifyPermissionComplete = "public.n5.user.permission.modify.modifypermissioncomplete";

    /// <summary>
    /// Get permissions.
    /// </summary>
    public const string GetPermission = "public.n5.user.permission.get";
    /// <summary>
    /// Get permissions complete.
    /// </summary>
    public const string GetPermissionComplete = "public.n5.user.permission.getcomplete";
    #endregion Permission

}