using System.Text;

namespace PromocodeService.Models
{
    /// <summary>
    /// Generates code as string
    /// </summary>
    public static class CodeGenerator
    {
        const string alphabet = "AG8FOLE2WVTCPY5ZH3NIUDBXSMQK7946";
        public static string Get(long number)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < 10; ++i)
            {
                b.Append(alphabet[(int)number & ((1 << 5) - 1)]);
                number = number >> 5;
            }
            return b.ToString();
        }
    }
}
