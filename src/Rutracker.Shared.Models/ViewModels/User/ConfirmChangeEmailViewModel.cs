﻿using System.ComponentModel.DataAnnotations;

namespace Rutracker.Shared.Models.ViewModels.User
{
    public class ConfirmChangeEmailViewModel
    {
        [Required] public string UserId { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Token { get; set; }
    }
}