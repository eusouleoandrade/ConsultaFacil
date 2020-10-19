using System;
using System.Text.RegularExpressions;

namespace INTELECTAH.ConsultaFacil.Service.Common
{
    public static class TelefoneCommonService
    {
        public static bool IsValid(string telefone)
        {
            try
            {
                return Regex.IsMatch(telefone,
                    @"^1\d\d(\d\d)?$|^0800 ?\d{3} ?\d{4}$|^(\(0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d\) ?|0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d[ .-]?)?(9|9[ .-])?[2-9]\d{3}[ .-]?\d{4}$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
