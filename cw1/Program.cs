using System.Text.RegularExpressions;

Console.WriteLine("Podaj adres strony internetowej w formacie http(s)://www.onet.pl");
try
{
    var url = Console.ReadLine();
    if (url.Equals(""))
    {
        Console.WriteLine("Podano pustu adres");
    }
    else
    {
        using (HttpClient httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var pattern = "(?:\\(?\\?)?(?:[-\\.\\(\\)\\s]*(\\d)){9}\\)?";
            var numbers = Regex.Matches(content, pattern)
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .Distinct();
            Console.WriteLine("Znalezione numery telefoniczne: \n");
            foreach (var number in numbers)
            {
                Console.WriteLine(number + "\n");
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Wystapil wyjatek " + ex.Message);
}
Console.WriteLine("Nacisnij ENTER aby zakonczyc");
Console.ReadLine();