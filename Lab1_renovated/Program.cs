using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.NetworkInformation;

var path = @"C:\Users\cicui\Documents\\apartment_buildings_2019.csv";
using (TextFieldParser csvParser = new TextFieldParser(path))
{
    csvParser.SetDelimiters(new string[] { ";" });
    csvParser.ReadLine();

    while (!csvParser.EndOfData)
    {
        string[] sarasas = csvParser.ReadFields();
        string Id = sarasas[0];
        string adresas = sarasas[1];
        string namo_valdytojas = sarasas[2];
        string valdymo_forma = sarasas[3];
        string paskyrimo_pagrindas = sarasas[4];
        string administratoriaus_pabaigos_Data = sarasas[5];
        string paskirtis = sarasas[6];
        string uni_nr = sarasas[7];
        string bendr_plotas = sarasas[8];
        string naud_plotas = sarasas[9];
        string build_year = sarasas[10];
        string renov_metai = sarasas[11];
        string renovacijos_statusas = sarasas[12];
        string energ_naudingumo_klase = sarasas[13];
        string butu_skaicius = sarasas[14];
        string negyvenamuju_palapu_skaicius = sarasas[15];
        string korpusas = sarasas[16];
        string sklypo_plotas = sarasas[17];
    }
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
    public DateTime build_year { get; set; }
    public DateTime renov_metai { get; set; }
    public string renovacijos_statusas { get; set; } 
    public string energ_naudingumo_klase { get; set; }
    public int butu_skaicius { get; set; }
    public int negyvenamuju_palapu_skaicius { get; set; }
    public string korpusas { get; set; }
    public int sklypo_plotas { get; set; }
}
