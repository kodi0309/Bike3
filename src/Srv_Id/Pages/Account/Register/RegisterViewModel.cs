using System.ComponentModel.DataAnnotations;

namespace Srv_Id;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; } = "noemail";

    [Required]
    public string Password { get; set; } = "nopassword";

    [Required]
    public string Username { get; set; } = "noname";

    [Required]
    public string FullName { get; set; } = "nofullname";
    public string ReturnUrl { get; set; } = "undefined returnurl";
    public string Button { get; set; } = "undefined button";
}
