using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

internal class Program
{

    static void Main(string[] args)
    {



        var path = @"apartment_buildings_2019.csv"; //change path if needed
        int max_metai = 0;
        int min_metai = 2022;
        int yearsort = 2;
        
        string[] renov = null;
        Dictionary<int, Namas> namai = new Dictionary<int, Namas>();

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
                
                int.TryParse(Id, out int id);
                int.TryParse(bendr_plotas, out int bendras_plotas);
                int.TryParse(naud_plotas, out int naudotas_plotas);
                int.TryParse(butu_skaicius, out int butu_skaicius_name);
                int.TryParse(negyvenamuju_palapu_skaicius, out int tusciu_butu_skaicius);
                int.TryParse(sklypo_plotas, out int sklypo_bendrasisPlotas);

                DateTime.TryParse(administratoriaus_pabaigos_Data, out DateTime pabaigos_data);
                DateTime.TryParse(build_year, out DateTime pastatymo_metai);
                string renovacijos_metai = renov_metai;



                if (renovacijos_statusas == "Renovuotas")
                {

                    renov = sarasas;
                    int x = 0;
                    AddToDictionary(namai, id, adresas, namo_valdytojas, valdymo_forma, paskyrimo_pagrindas, pabaigos_data, paskirtis, uni_nr, bendras_plotas, naudotas_plotas, pastatymo_metai, renovacijos_metai, renovacijos_statusas, energ_naudingumo_klase, butu_skaicius_name, tusciu_butu_skaicius, korpusas, sklypo_bendrasisPlotas);
                    try
                    {
                        int metai_num = Int32.Parse(renov_metai);
                        x = metai_num;
                        if (metai_num > max_metai)
                        {
                            max_metai = metai_num;
                        }
                        if (metai_num < min_metai)
                        {
                            min_metai = metai_num;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{renov[11]}'");
                    }
                    

                }
            }

            int bucket_num = ((max_metai - min_metai) / yearsort) + 1;
            
            List<string>[] buckets = new List<string>[bucket_num];
            
          
            
            DisplayInConsole( ConstructBuckets(min_metai,max_metai,yearsort, namai), min_metai, max_metai, namai);

            



            //DisplayConsole(buckets, bucket_num);

        }

    }

    public static Dictionary<int, Bucket> ConstructBuckets(int lowestYear, int highestYear, int period, Dictionary<int, Namas> list)
    {

        

        var bucketList = new Dictionary<int, Bucket>();

        // Calculations to find out, how many sorted bucket we need

        int duration = highestYear - lowestYear;

        int bucketNumber = (int)Convert.ToSingle(duration / period);  // We may get fractions, convert to integer

        int bucket_startYear = 0;

        int bucket_endYear = 0;

        // We need to find starting bucket range, below our calculation of doing that
        // n is correcting value
        int n = 0;

        
        // here we try to find n, so that by sorting our lowest year, there would be no fractions.

        while ((lowestYear-n)%period != 0)
        {
            n++; // 4
        };

        // so in case we have 1904, it went to first bucket range 1900-1905
        // Calculateing first bucket range
        bucket_startYear = lowestYear - n;

        Console.WriteLine("test " + bucketNumber);


        // Creating bucket dictionary with sorted ranges

        for (int i = 0; i <= bucketNumber; i++)
        {
            Bucket bucket = new Bucket();
            bucket.start_range = bucket_startYear;

            bucket_endYear = bucket_startYear + period;
            bucket.end_range = bucket_startYear + period;


            bucket.buidingList_id = new List<int>();
            bucketList.Add(key: i, value: bucket);
            bucket_startYear = bucket_endYear;

        }

        int buildYear = 0;

        // Assigning bulding build years into buckets

        foreach (KeyValuePair<int, Namas> building in list)
        {

            foreach (KeyValuePair<int, Bucket> buckets in bucketList)
            {
                int.TryParse(building.Value.renov_metai, out buildYear);
                if (buildYear >= buckets.Value.start_range && buildYear < buckets.Value.end_range)
                {
                    buckets.Value.buidingList_id?.Add(building.Value.Id);

                }
            }
        }

        return bucketList;

    }


    public class Bucket
    {

        public int start_range { get; set; }

        public int end_range { get; set; }
        public List<int>? buidingList_id { get; set; }

    }


    static void DisplayConsole(List<string>[] buckets, int bucket_num)
    {
        Console.WriteLine("---START WRITING---");
        //Console.WriteLine(buckets[1].ToList());
        List<string>[] buck = new List<string>[bucket_num];

        //for (int i = 0; i < bucket_num; i++)
        {
            //  buck[i] = new List<string>();
        }

        //foreach (string[] buck in buckets)
        {
            for (int j = 0; j < bucket_num; j++)
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    Console.WriteLine(buck[j][i]);
                }
            }
        }
        Console.WriteLine("");
        Console.WriteLine("---END WRITING---");


    }

     public static void DisplayInConsole(Dictionary<int,Bucket> bucketList, int lowestYear, int highestYear , Dictionary<int,Namas> buildingList ){

            string bucketKey; 

            Console.WriteLine("---START WRITING---");
            Console.WriteLine(" ");
            Console.WriteLine("The lowest renovated year in the bucket : " + lowestYear);
            Console.WriteLine("The highest renovated year in the bucket : " + highestYear);

            foreach(KeyValuePair<int,Bucket> bucket in bucketList){
                
                Console.WriteLine("Building renovated from " + bucket.Value.start_range + " to " + bucket.Value.end_range + " are " + bucket.Value.buidingList_id?.Count + " in the list , bucket number " + bucket.Key );
               
                
            }

            Console.WriteLine(" ");
            Console.WriteLine("Which bucket to display? :");
            bucketKey = Console.ReadLine();

            if(int.TryParse(bucketKey,out int key)){
                Console.WriteLine("This bucket renovated list ids: ");
                if(bucketList.ContainsKey(key)){
                    foreach(var i in bucketList[key].buidingList_id)
                    {
                         Console.WriteLine(i + " Renovation year: " + buildingList[i].renov_metai);
                        
                        
                    }
                }else
                {
                    Console.WriteLine("! Suck bucket id doesnt exist");
                }
                
            }
            else{
                Console.WriteLine("! Wrong input...");
            }

            

            
            Console.WriteLine("");
            Console.WriteLine("---END WRITING---");


        }
    


    static void AddToDictionary(Dictionary<int, Namas> elements, int Id, string address, string owner, string ownershipForm, string appointmentBasis, DateTime adminstrationEndDate, string purpose, string uniNumber, int generalSize, int usedSize, DateTime buildYear, string renovationYear, string renovationStatus, string energyClass, int houseCount, int emtyHouseCount, string corpus, int areaSize)
    {
        Namas theElement = new Namas();

        theElement.Id = Id;
        theElement.adresas = address;
        theElement.namo_valdytojas = owner;
        theElement.valdymo_forma = ownershipForm;
        theElement.paskyrimo_pagrindas = appointmentBasis;
        theElement.administratoriaus_pabaigos_Data = adminstrationEndDate;
        theElement.uni_nr = uniNumber;
        theElement.bendr_plotas = generalSize;
        theElement.naud_plotas = usedSize;
        theElement.build_year = buildYear;
        theElement.renov_metai = renovationYear;
        theElement.renovacijos_statusas = renovationStatus;
        theElement.energ_naudingumo_klase = energyClass;
        theElement.butu_skaicius = houseCount;
        theElement.negyvenamuju_palapu_skaicius = emtyHouseCount;
        theElement.korpusas = corpus;
        theElement.sklypo_plotas = areaSize;



        elements.Add(key: theElement.Id, value: theElement);

    }
    public class Namas
    {
        public int Id { get; set; }
        public string ?adresas { get; set; }
        public string ?namo_valdytojas { get; set; }
        public string ?valdymo_forma { get; set; }
        public string ?paskyrimo_pagrindas { get; set; }
        public DateTime administratoriaus_pabaigos_Data { get; set; }
        public string ?paskirtis { get; set; }
        public string ?uni_nr { get; set; }
        public int bendr_plotas { get; set; }
        public int naud_plotas { get; set; }
        public DateTime build_year { get; set; }
        public string ?renov_metai { get; set; }
        public string ?renovacijos_statusas { get; set; }
        public string ?energ_naudingumo_klase { get; set; }
        public int butu_skaicius { get; set; }
        public int negyvenamuju_palapu_skaicius { get; set; }
        public string ?korpusas { get; set; }
        public int sklypo_plotas { get; set; }
    }
}
