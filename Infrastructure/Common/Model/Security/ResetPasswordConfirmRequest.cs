namespace Infrastructure.Model.Security
{
    public class ResetPasswordConfirmRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public bool ForcePasswordChangeOnNextSignIn { get; set; }
    }
}
