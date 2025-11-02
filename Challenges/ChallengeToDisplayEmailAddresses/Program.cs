// the username for all employees is the first two characters of their first name, followed by their full last name.
/*
    robavin@contoso.com
    sibright@contoso.com
    kisinclair@contoso.com
    aakamath@contoso.com
    sadelucchi@contoso.com
    siali@contoso.com
    viashton@hayworth.com
    codysart@hayworth.com
    shlawrence@hayworth.com
    davaldes@hayworth.com
*/
Console.WriteLine("Challenge to display email addresses");

string[,] corporate = {
		{"Robert", "Bavin"}, {"Simon", "Bright"},
		{"Kim", "Sinclair"}, {"Aashrita", "Kamath"},
		{"Sarah", "Delucchi"}, {"Sinan", "Ali"}
};

string[,] external ={
		{"Vinnie", "Ashton"}, {"Cody", "Dysart"},
		{"Shay", "Lawrence"}, {"Daren", "Valdes"}
	};

string externalDomain = "hayworth.com";

for (int i = 0; i < corporate.GetLength(0); i++)
{
	DisplayEmail(first: corporate[i, 0], last: corporate[i, 1]);
}

for (int i = 0; i < external.GetLength(0); i++)
{
	DisplayEmail(first: external[i, 0], last: external[i, 1], domain: externalDomain);
}

void DisplayEmail(string first, string last, string domain = "contoso.com")
{
	string email = first.Substring(0, 2) + last;
	email = email.ToLower();
	Console.WriteLine($"{email}@{domain}");
}