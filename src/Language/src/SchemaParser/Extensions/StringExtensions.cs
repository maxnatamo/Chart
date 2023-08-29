namespace Chart.Language
{
    public static class StringExtensions
    {
        public static int IndexNotOf(this string str, char value)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] != value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexNotOfAny(this string str, char[] values)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(!values.ToList().Exists(v => v == str[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}