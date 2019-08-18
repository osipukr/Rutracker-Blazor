﻿namespace Rutracker.Shared.Models.Resources
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ChangePasswordViewModelResource
    {
        private static global::System.Resources.ResourceManager resourceMan;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ChangePasswordViewModelResource()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new global::System.Resources.ResourceManager(
                        "Rutracker.Shared.Models.Resources.ChangePasswordViewModelResource",
                        typeof(ChangePasswordViewModelResource).Assembly);
                }

                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture { get; set; }

        /// <summary>
        ///   Looks up a localized string similar to Confirm new password.
        /// </summary>
        public static string ConfirmNewPasswordDisplayName => ResourceManager.GetString("ConfirmNewPasswordDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The password and confirmation password do not match..
        /// </summary>
        public static string ConfirmNewPasswordErrorMessage => ResourceManager.GetString("ConfirmNewPasswordErrorMessage", Culture);

        /// <summary>
        ///   Looks up a localized string similar to New password.
        /// </summary>
        public static string NewPasswordDisplayName => ResourceManager.GetString("NewPasswordDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} must be at least {2} and at max {1} characters long..
        /// </summary>
        public static string NewPasswordErrorMessage => ResourceManager.GetString("NewPasswordErrorMessage", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Old password.
        /// </summary>
        public static string OldPasswordDisplayName => ResourceManager.GetString("OldPasswordDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} must be at least {2} and at max {1} characters long..
        /// </summary>
        public static string OldPasswordErrorMessage => ResourceManager.GetString("OldPasswordErrorMessage", Culture);
    }
}