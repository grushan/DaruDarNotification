using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DaruDarNotification.Helper.DaruDarProvider;

namespace DaruDarNotification.Helper
{
    class StorageGiftsHandler
    {
        string directoryName = @"GiftsList";
        string fileName = @"gift.txt";
        public void SaveGifts (List<Gift> gifts, string category)
        {
            fileName = category + ".txt";
            if (Directory.Exists(directoryName) == false)
            {
                Directory.CreateDirectory(directoryName);
            }
            if (File.Exists(Path.Combine(directoryName, fileName)) == false)
            {
                File.Create(Path.Combine(directoryName, fileName));
            }
            else { 
            StreamWriter f = new StreamWriter(Path.Combine(directoryName, fileName));

            int limiter = 10;
            if (gifts.Count < limiter)
            {
                limiter = gifts.Count;
            }
            for (int i = 0; i< limiter; i++)
            {
                f.WriteLine(gifts[i].link);
            }
            f.Close();
                }
        }
    
        public void CompareGifts (List<Gift> gifts, string category)
        {
            fileName = category + ".txt";
            if (Directory.Exists(directoryName) == false)
            {
                Directory.CreateDirectory(directoryName);
            }
            if (File.Exists(Path.Combine(directoryName, fileName)) == false)
            {
                File.Create(Path.Combine(directoryName, fileName));
                
            }
            else
            {
                StreamReader streamReader = new StreamReader(Path.Combine(directoryName, fileName));
                string line;
                int limiter = 10;
                if (gifts.Count < limiter)
                {
                    limiter = gifts.Count;
                }
                for (int i = 0; i < limiter; i++)
                {
                    line = streamReader.ReadLine();
                    bool giftExist = true;
                    for (int j = 0; j < limiter; j++)
                    {

                        if (gifts[j].link == line)
                        {
                            giftExist = false;
                            //Console.Write("Найден новый дар!");
                        }
                    }
                    if (giftExist == true)
                    {
                        FindGift newGift = new FindGift(gifts[i].title, gifts[i].link);
                        newGift.Show();

                    }

                }
                streamReader.Close();
            }
            
        }
    }
}
