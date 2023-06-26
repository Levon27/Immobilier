using System.ComponentModel.DataAnnotations;

namespace Imobbilier.Core
{
    public static class EmailUtils
    {
        public static bool IsEmailValid(string email)
        {
            var emailAtt = new EmailAddressAttribute();
            return emailAtt.IsValid(email);
        }
    }
}