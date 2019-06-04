using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
namespace DaruDarNotification.Helper
{
    class DaruDarProvider
    {
        public class City
        {
            public string name;
            public string city_id;
            public City(string city_id_, string name_)
            {
                name = name_;
                city_id = city_id_;
            }
            public override string ToString()
            {
                return name;
            }
            public string GetCityID()
            {
                return city_id;
            }
        }
        public class Region
        {
            public string name;
            public string region_id;
            public Region(string region_id_, string name_ )
            {
                name = name_;
                region_id = region_id_;
            }
            public override string ToString()
            {
                return name;
            }
            public string GetRegionID()
            {
                return region_id;
            }
        }
        public class Country
        {
            public string name;
            public string country_id;

            public Country (string country_id_, string name_)
            {
                name = name_;
                country_id = country_id_;
            }
            public override string ToString()
            {
                return name;
            }
            public string GetCountryID()
            {
                return country_id;
            }
        }
        public class Category
        {
            public string name;
            public string alias;
            public Category (string alias_, string name_)
            {
                alias = alias_;
                name = name_;
            }
            public override string ToString()
            {
                return name;
            }
            public string GetCategoryAlias()
            {
                return alias;
            }
        }
        public class Gift
        {
            public string title;
            public string description;
            public string date;
            public string author;
            public string link;
            public string image;
        }
        public List<Gift> GetGifts(string country, string region, string city, string category)
        {
            string link = String.Format(@"http://darudar.org/search/rss/?q=&country={0}&region={1}&city={2}&category={3}&status%5B0%5D=0&status%5B1%5D=1&rel=0&ln=", country, region, city, category);

            HttpWebRequest rew = (HttpWebRequest)WebRequest.Create(link);
            HttpWebResponse resp = (HttpWebResponse)rew.GetResponse();

            Stream giftsStream = resp.GetResponseStream();
            StreamReader giftsStreamReader = new StreamReader(giftsStream, Encoding.UTF8);

            string xml = giftsStreamReader.ReadToEnd();
            return ParseGifts(xml);
        }
        private List<Gift> ParseGifts(string xml_)
        {
            XDocument xml = XDocument.Parse(xml_);
            int i = 0;
           
            List<Gift> gifts = new List<Gift>();
            foreach (XElement el in xml.Root.Elements())
            {
                Console.WriteLine("{0}", el.Name);

                foreach (XElement element in el.Elements())
                {
                    if (element.Name == "item")
                    {
                        foreach (XElement miniElement in element.Elements())
                        {
                            if (miniElement.Name == "title")
                            {
                                gifts.Add (new Gift());
                                gifts[i].title = miniElement.Value;
                            }
                            if (miniElement.Name == "link")
                            {
                                gifts[i].link = miniElement.Value;
                            }
                            if (miniElement.Name == "description")
                            {
                                gifts[i].description = miniElement.Value;
                            }
                            if (miniElement.Name == "pubDate")
                            {
                                gifts[i].date = miniElement.Value; 
                            }
                            if (miniElement.Name == "author")
                            {
                                gifts[i].author = miniElement.Value;

                            }

                        }
                        i++;
                        
                    }
                }
                    
            }
            return gifts;
        }
        public List <Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category("computer", "Компьютер"));
            categories.Add(new Category("phones", "Телефоны и КПК"));
            categories.Add(new Category("electronic", "Электроника"));
            categories.Add(new Category("games", "Компьютерные игры"));
            categories.Add(new Category("photo", "Фото и видео"));
            categories.Add(new Category("0", "Любая"));
            categories.Add(new Category("drive", "Авто-Мото-Вело"));
            categories.Add(new Category("health", "Гигиена и медицина"));
            categories.Add(new Category("baby", "Детям"));
            categories.Add(new Category("home", "Дом и офис"));
            categories.Add(new Category("fauna", "Животные и растения"));
            categories.Add(new Category("toys", "Игрушки и сувениры"));
            categories.Add(new Category("internet", "Инвайты и интернет"));
            categories.Add(new Category("movie", "Кино и мультфильмы"));
            categories.Add(new Category("books", "Книги и журналы"));
            categories.Add(new Category("collect", "Коллекционирование"));
            categories.Add(new Category("counpons", "Купоны и скидки"));
            categories.Add(new Category("music", "Музыка"));
            categories.Add(new Category("clothes", "Одежда и аксессуары"));
            categories.Add(new Category("beauty", "Парфюмерия и косметика"));
            categories.Add(new Category("handmade", "Рукоделие"));
            categories.Add(new Category("sport", "Спорт и отдых"));
            categories.Add(new Category("jewellery", "Украшения и бижутерия"));
            categories.Add(new Category("skills", "Услуги и помощь"));
            categories.Add(new Category("other", "Другое"));
            return categories;
        }
        public List <Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            countries.Add(new Country("168", "Россия (Russia)"));
            countries.Add(new Country("22", "Беларусь (Belarus)"));
            countries.Add(new Country("216", "Україна (Ukraine)"));
            countries.Add(new Country("1", "Australia"));
            countries.Add(new Country("2", "Österreich (Austria)"));
            countries.Add(new Country("3", "Azərbaycan (Azerbaijan)"));
            countries.Add(new Country("4", "Shqipëria (Albania)"));
            countries.Add(new Country("5", "الجزائر (Algeria)"));
            countries.Add(new Country("6", "Angola"));
            countries.Add(new Country("7", "Anguilla"));
            countries.Add(new Country("8", "d’Andorra (Andorra)"));
            countries.Add(new Country("9", "Antarctica"));
            countries.Add(new Country("10", "Antigua and Barbuda"));
            countries.Add(new Country("11", "Antilles"));
            countries.Add(new Country("12", "Argentina"));
            countries.Add(new Country("13", "Հայաստան (Armenia)"));
            countries.Add(new Country("14", "Арулько"));
            countries.Add(new Country("15", "Afğānistān (Afghanistan)"));
            countries.Add(new Country("16", "Ashmore and Cartier Islands"));
            countries.Add(new Country("17", "Bahamas"));
            countries.Add(new Country("18", "বাংলাদেশ (Bangladesh)"));
            countries.Add(new Country("19", "Barbados"));
            countries.Add(new Country("20", "Bassas da India"));
            countries.Add(new Country("21", "مملكة البحرين‎ (Bahrain)"));
            countries.Add(new Country("23", "Belize"));
            countries.Add(new Country("24", "België (Belgium)"));
            countries.Add(new Country("25", "Bénin (Benin)"));
            countries.Add(new Country("26", "Bermuda"));
            countries.Add(new Country("27", "България (Bulgaria)"));
            countries.Add(new Country("28", "Bolivia"));
            countries.Add(new Country("29", "Bosna i Hercegovina (Bosnia and Herzegovina)"));
            countries.Add(new Country("30", "Botswana"));
            countries.Add(new Country("31", "Brasil"));
            countries.Add(new Country("32", "British Oceania"));
            countries.Add(new Country("33", "British Virgin Islands"));
            countries.Add(new Country("34", "Brunei Darussalam"));
            countries.Add(new Country("35", "Burkina Faso"));
            countries.Add(new Country("36", "Burundi"));
            countries.Add(new Country("37", "Druk Yul (Butane)"));
            countries.Add(new Country("38", "Wallis et Futuna"));
            countries.Add(new Country("39", "Vanuatu"));
            countries.Add(new Country("40", "UK (United Kindom)"));
            countries.Add(new Country("41", "Magyarország (Hungary)"));
            countries.Add(new Country("42", "Venezuela"));
            countries.Add(new Country("43", "Timor Leste"));
            countries.Add(new Country("44", "Việt Nam (Vietnam)"));
            countries.Add(new Country("45", "Gabon"));
            countries.Add(new Country("46", "Haiti"));
            countries.Add(new Country("47", "Guyana"));
            countries.Add(new Country("48", "Gambia"));
            countries.Add(new Country("49", "Ghana"));
            countries.Add(new Country("50", "Guadeloupe"));
            countries.Add(new Country("51", "Guatemala"));
            countries.Add(new Country("52", "Guinée"));
            countries.Add(new Country("53", "Guiné-Bissau (Guinea Bissau)"));
            countries.Add(new Country("54", "Deutschland (Germany)"));
            countries.Add(new Country("55", "Guernsey Islands"));
            countries.Add(new Country("56", "Gibraltar"));
            countries.Add(new Country("57", "Îles Glorieuses (Glorioso Islands)"));
            countries.Add(new Country("58", "Honduras"));
            countries.Add(new Country("59", "香港 (Hong Kong)"));
            countries.Add(new Country("60", "Grenada"));
            countries.Add(new Country("61", "Grønland (Greenland)"));
            countries.Add(new Country("62", "Ελλάδα (Greece)"));
            countries.Add(new Country("63", "საქართველო (Georgia)"));
            countries.Add(new Country("64", "Congo"));
            countries.Add(new Country("65", "Danmark (Denmark)"));
            countries.Add(new Country("66", "Jersey Islands"));
            countries.Add(new Country("67", "جيبوتي (Djibouti)"));
            countries.Add(new Country("68", "Jan Mayen Island"));
            countries.Add(new Country("69", "Dominica"));
            countries.Add(new Country("70", "Dominicana"));
            countries.Add(new Country("71", "Europa"));
            countries.Add(new Country("72", "مصر (Egypt)"));
            countries.Add(new Country("74", "Zambia"));
            countries.Add(new Country("75", "Sahara Occidental"));
            countries.Add(new Country("76", "Zimbabwe"));
            countries.Add(new Country("77", "מדינת ישראל‎ (Israel)"));
            countries.Add(new Country("78", "भारत गणराज्य (India)"));
            countries.Add(new Country("79", "Indonesia"));
            countries.Add(new Country("80", "الأردن (Jordan)"));
            countries.Add(new Country("81", "عراق‎ (Iraq)"));
            countries.Add(new Country("82", "جمهوری اسلامی ایران‎ (Iran)"));
            countries.Add(new Country("83", "Éire (Ireland)"));
            countries.Add(new Country("84", "Ísland (Iceland)"));
            countries.Add(new Country("85", "España (Spain)"));
            countries.Add(new Country("86", "Italia"));
            countries.Add(new Country("87", "اليمن‎ (Yemen)"));
            countries.Add(new Country("88", "Cabo Verde"));
            countries.Add(new Country("89", "Қазақстан (Kazakhstan)"));
            countries.Add(new Country("90", "Cayman Islands"));
            countries.Add(new Country("91", "Kâmpŭchea (Cambodia)"));
            countries.Add(new Country("92", "Cameroon"));
            countries.Add(new Country("93", "Canada"));
            countries.Add(new Country("94", "قطر‎ (Qatar)"));
            countries.Add(new Country("95", "Kenya"));
            countries.Add(new Country("96", "Κύπρος (Cyprus)"));
            countries.Add(new Country("97", "Kiribati"));
            countries.Add(new Country("98", "中国 (China)"));
            countries.Add(new Country("99", "Clipperton Island"));
            countries.Add(new Country("100", "Cocos (Keeling) Islands"));
            countries.Add(new Country("101", "Colombia"));
            countries.Add(new Country("102", "Komori"));
            countries.Add(new Country("103", "Kongó (Brazzaville)"));
            countries.Add(new Country("104", "Kongó (Kinshasa)"));
            countries.Add(new Country("105", "Coral Sea Islands Territory"));
            countries.Add(new Country("106", "Costa Rica"));
            countries.Add(new Country("107", "Côte d'Ivoire (Cote de Voir)"));
            countries.Add(new Country("108", "Cuba"));
            countries.Add(new Country("109", "كويت‌‎ (Kuwait)"));
            countries.Add(new Country("110", "Kūki 'Āirani (Cook Islands Maori)"));
            countries.Add(new Country("111", "Кыргызстан (Kyrgyzstan)"));
            countries.Add(new Country("112", "Lao"));
            countries.Add(new Country("113", "Latvija (Latvia)"));
            countries.Add(new Country("114", "Lesotho"));
            countries.Add(new Country("115", "Liberia"));
            countries.Add(new Country("116", "لبنان‌‎ (Lebanon)"));
            countries.Add(new Country("117", "ليبيا (Libya)"));
            countries.Add(new Country("118", "Lietuva (Lithuania)"));
            countries.Add(new Country("119", "Liechtenstein"));
            countries.Add(new Country("120", "Lëtzebuerg (Luxembourg)"));
            countries.Add(new Country("121", "Mauritius"));
            countries.Add(new Country("122", "موريتانيا (Mauritania)"));
            countries.Add(new Country("123", "Madagasikara (Madagascar)"));
            countries.Add(new Country("124", "Mayotte"));
            countries.Add(new Country("125", "Macau"));
            countries.Add(new Country("126", "Македонија (Macedonia)"));
            countries.Add(new Country("127", "Malaŵi (Malawi)"));
            countries.Add(new Country("128", "Malaysia"));
            countries.Add(new Country("129", "Mali"));
            countries.Add(new Country("130", "ދިވެހިރާއްޖޭގެ ޖުމުހޫރިއްޔާ (Maldives)"));
            countries.Add(new Country("131", "Malta"));
            countries.Add(new Country("132", "Martinique"));
            countries.Add(new Country("133", "México (Mexico)"));
            countries.Add(new Country("134", "Moçambique (Mozambique)"));
            countries.Add(new Country("135", "Moldova"));
            countries.Add(new Country("136", "Monaco"));
            countries.Add(new Country("137", "Монгол Улс (Mongolia)"));
            countries.Add(new Country("138", "Montserrat"));
            countries.Add(new Country("139", "المغرب‎ (Morocco)"));
            countries.Add(new Country("140", "Myăma (Burma)"));
            countries.Add(new Country("141", "Man Island"));
            countries.Add(new Country("142", "Namibia"));
            countries.Add(new Country("143", "Nauru"));
            countries.Add(new Country("144", "नेपाल (Nepal)"));
            countries.Add(new Country("145", "Niger"));
            countries.Add(new Country("146", "Nigeria"));
            countries.Add(new Country("147", "Nederland (Netherlands)"));
            countries.Add(new Country("148", "Nicaragua"));
            countries.Add(new Country("149", "New Zealand"));
            countries.Add(new Country("150", "Nouvelle-Calédonie (New Caledonia)"));
            countries.Add(new Country("151", "Norge (Norway)"));
            countries.Add(new Country("152", "Norfolk Island"));
            countries.Add(new Country("154", "الإمارات العربية المتحدة (United Arab Emirates)"));
            countries.Add(new Country("155", "عُمان‎ (Oman)"));
            countries.Add(new Country("156", "پاکِستان (Pakistan)"));
            countries.Add(new Country("157", "Panamá (Panama)"));
            countries.Add(new Country("158", "Papua New Guinea"));
            countries.Add(new Country("159", "Paraguay"));
            countries.Add(new Country("161", "Perú (Peru)"));
            countries.Add(new Country("162", "Pitcairn Islands"));
            countries.Add(new Country("163", "Polska (Poland)"));
            countries.Add(new Country("164", "Portugal"));
            countries.Add(new Country("165", "Puerto Rico"));
            countries.Add(new Country("166", "Réunion"));
            countries.Add(new Country("169", "Rwanda"));
            countries.Add(new Country("170", "România (Romania)"));
            countries.Add(new Country("171", "El Salvador (Salvador)"));
            countries.Add(new Country("172", "Samoa"));
            countries.Add(new Country("173", "San Marino"));
            countries.Add(new Country("174", "São Tomé e Príncipe (San-Tome and Principe)"));
            countries.Add(new Country("175", "السعودية‎ (Saudi Arabia)"));
            countries.Add(new Country("176", "eSwatini (Swaziland)"));
            countries.Add(new Country("179", "Santa Lucia"));
            countries.Add(new Country("180", "Saint Helena Island"));
            countries.Add(new Country("181", "朝鮮民主主義人民共和國 (North Korea)"));
            countries.Add(new Country("182", "Seychelles"));
            countries.Add(new Country("183", "قطاع غزّة (Gaza Strip)"));
            countries.Add(new Country("184", "Saint-Pierre-et-Miquelon (Guernsey)"));
            countries.Add(new Country("185", "Sénégal (Senegal)"));
            countries.Add(new Country("186", "Saint Kitts and Nevis"));
            countries.Add(new Country("187", "Saint Vincent and the Grenadines"));
            countries.Add(new Country("188", "Србија (Serbia)"));
            countries.Add(new Country("189", "Singapore"));
            countries.Add(new Country("190", "سوريا (Syria)"));
            countries.Add(new Country("191", "Slovensko (Slovakia)"));
            countries.Add(new Country("192", "Slovenija (Slovenia)"));
            countries.Add(new Country("193", "Solomon Islands"));
            countries.Add(new Country("194", "الصومال‌‎ (Somalia)"));
            countries.Add(new Country("196", "السودان‌‎ (Sudan)"));
            countries.Add(new Country("197", "Suriname"));
            countries.Add(new Country("198", "USA (United States)"));
            countries.Add(new Country("199", "Sierra Leone"));
            countries.Add(new Country("200", "Тоҷикистон (Tadzhikistan)"));
            countries.Add(new Country("201", "ประเทศไทย (Thailand)"));
            countries.Add(new Country("202", "台湾 (Taiwan)"));
            countries.Add(new Country("203", "Tanzania"));
            countries.Add(new Country("204", "Togolaise"));
            countries.Add(new Country("205", "Tokelau"));
            countries.Add(new Country("206", "Tonga"));
            countries.Add(new Country("207", "Trinidad and Tobago"));
            countries.Add(new Country("209", "Tuvalu"));
            countries.Add(new Country("210", "تونس‌‎ (Tunisia)"));
            countries.Add(new Country("211", "Türkmenistan (Turkmenistan)"));
            countries.Add(new Country("212", "Turks and Caicos Islands"));
            countries.Add(new Country("213", "Türkiye (Turkey)"));
            countries.Add(new Country("214", "Uganda"));
            countries.Add(new Country("215", "Ўзбекистон (Uzbekistan)"));
            countries.Add(new Country("217", "Uruguay"));
            countries.Add(new Country("219", "Føroyar (Faroe Islands)"));
            countries.Add(new Country("220", "Fiji Islands"));
            countries.Add(new Country("221", "Philippines"));
            countries.Add(new Country("222", "Suomi (Finland)"));
            countries.Add(new Country("223", "France"));
            countries.Add(new Country("224", "Guinée (Guinea)"));
            countries.Add(new Country("225", "Polynésie française (French Polynesia)"));
            countries.Add(new Country("228", "Hrvatska (Croatia)"));
            countries.Add(new Country("229", "Centrafricaine"));
            countries.Add(new Country("230", "Tchad"));
            countries.Add(new Country("231", "Česko (Czech)"));
            countries.Add(new Country("232", "Chile"));
            countries.Add(new Country("233", "Schweiz (Switzerland)"));
            countries.Add(new Country("234", "Sverige (Sweden)"));
            countries.Add(new Country("235", "श्री लङ्का (Sri Lanka)"));
            countries.Add(new Country("236", "Ecuador"));
            countries.Add(new Country("237", "Guinea Ecuatorial"));
            countries.Add(new Country("238", "اريتريا (Eritrea)"));
            countries.Add(new Country("239", "Eesti (Estonia)"));
            countries.Add(new Country("240", "ኢትዮጵያ (Ethiopia)"));
            countries.Add(new Country("241", "Republic of South Africa"));
            countries.Add(new Country("242", "大韓 民國 (South Korea)"));
            countries.Add(new Country("244", "Jamaica"));
            countries.Add(new Country("246", "日本 (Japan)"));
            countries.Add(new Country("251", "Аҧсны (Abkhazia)"));

            return countries;
        }
        public List<City> GetCities(string city_id)
        {
            HttpWebRequest rew = (HttpWebRequest)WebRequest.Create("http://darudar.org/ajax/cities_get/?id=" + city_id);
            HttpWebResponse resp = (HttpWebResponse)rew.GetResponse();

            // Получить поток
            Stream cityStream = resp.GetResponseStream();
            StreamReader cityStreamReader = new StreamReader(cityStream);
            string cityString = cityStreamReader.ReadToEnd();

            return parseCities(cityString);
        }

        public List<Region> GetRegions(string country_id)
        {
            HttpWebRequest rew = (HttpWebRequest)WebRequest.Create("http://darudar.org/ajax/regions_get/?id=" + country_id);
            HttpWebResponse resp = (HttpWebResponse)rew.GetResponse();

            // Получить поток
            Stream regionStream = resp.GetResponseStream();
            StreamReader regionStreamReader = new StreamReader(regionStream);
            string regionString = regionStreamReader.ReadToEnd();
            return ParseRegions(regionString);
        }
        private List<City> parseCities(string cities)
        {
            string parsed = parseStream(cities);

            string[] separateParsed = parsed.Split('\n');

            List<City> parsedCitiesList = new List<City>();

            for (int i = 2; i < separateParsed.Length - 1; i++)
            {
                string[] separatedRow = separateParsed[i].Split(',');
                parsedCitiesList.Add(new City(separatedRow[0], separatedRow[1]));
            }
            return parsedCitiesList;
        }
        private List<Region> ParseRegions (string regions)
        {
            string parsed = parseStream(regions);
             
            string[] separateParsed = parsed.Split('\n');

            List<Region> parsedRegionsList = new List<Region>();

            for (int i = 2; i < separateParsed.Length -1; i++)
            {
                string[] separatedRow = separateParsed[i].Split(',');
                parsedRegionsList.Add(new Region(separatedRow[0], separatedRow[1]));
            }
            return parsedRegionsList;
        }
        private string parseStream(string stream)
        {
            string parsed = Regex.Replace(stream, @"\\n", ", \n");
            parsed = Regex.Replace(parsed, @"<option value=\\", "");
            parsed = Regex.Replace(parsed, @"\\", "");
            parsed = Regex.Replace(parsed, @"<\/option>", "");
            parsed = Regex.Replace(parsed, @">", "");
            parsed = Regex.Replace(parsed, @"""", ",");
            parsed = Regex.Replace(parsed, @", \n,", "\n");
            return parsed;
        }
    }
}
