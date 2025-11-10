// 1️⃣ Extract the quantity → the value between <span> and </span>.
// 2️⃣ Extract the content inside <div>...</div>, replace "trade" with "reg", and output it.

Console.WriteLine("Extract, replace, and remove data from an input string");

const string input = "<div><h2>Widgets &trade;</h2><span>5000</span></div>";

string quantity = "";
string output = "";

// Extract <span>
const string openSpan = "<span>";
const string closeSpan = "</span>";
int openingSpanPosition = input.IndexOf(openSpan) + openSpan.Length;
int closingSpanPosition = input.IndexOf(closeSpan);
quantity = input.Substring(openingSpanPosition, closingSpanPosition - openingSpanPosition);

// Extract <div>
const string openDiv = "<div>";
const string closeDiv = "</div>";
int openingDivPosition = input.IndexOf(openDiv) + openDiv.Length;
int closingDivPosition = input.IndexOf(closeDiv);
output = input.Substring(openingDivPosition, closingDivPosition - openingDivPosition);
output = output.Replace("&trade;", "&reg;");

Console.WriteLine(quantity);
Console.WriteLine(output);
