namespace GeekBurguer_Promotion_Service.Promotion.Utils
{
    public class Util
    {
        public static List<int> ConverterStringToListInt(string value)
        {
            List<int> list = new List<int>();
            string[] numbers = value.Split(',');

            foreach (string numero in numbers)
            {
                int number;
                if (int.TryParse(numero, out number))
                    list.Add(number);
            }

            return list;
        }
    }
}
