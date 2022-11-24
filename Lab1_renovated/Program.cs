using CsvHelper;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.NetworkInformation;

//csvPath = Path("C:\\Users\\cicui\\Documents\\apartment_buildings_2019.csv");
using (var reader = new StreamReader("C:\\Users\\cicui\\Documents\\apartment_buildings_2019.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    records = csv.GetRecords<Namas>();
    //foreach () 
}
public class Namas
{
    public int Id { get; set; }
    public string adresas { get; set; }
    public string namo_valdytojas { get; set; }
    public string valdymo_forma { get; set; }
    public string paskyrimo_pagrindas { get; set; }
    public DateTime administratoriaus_pabaigos_Data { get; set; }
    public string paskirtis { get; set; }
    public string uni_nr { get; set; }
    public int bendr_plotas { get; set; }
    public int naud_plotas { get; set; }
    public DateTime renov_metai { get; set; }
    public string renovacijos_statusas { get; set; } //bool maybe
    public string energ_naudingumo_klase { get; set; }
    public int butu_skaicius { get; set; }
    public int negyvenamuju_palapu_skaicius { get; set; }
    public string korpusas { get; set; }
    public int sklypo_plotas { get; set; }
}
