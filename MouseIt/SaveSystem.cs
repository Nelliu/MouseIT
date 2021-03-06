﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MouseIt
{
    public static class SaveSystem
    {
        
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

     
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        


        public static void Save(List<Profile> prof)
        {
            using (StreamWriter file = File.CreateText("profiles.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, prof);
            }
        }
        
        public static int GetuID()
        {
            string pathy = "profile.txt";
            if (!File.Exists(pathy))
            {
                Random rand = new Random();
                int token = rand.Next(0, 1000000);
                File.WriteAllText(pathy, token.ToString());
                return token;
            }
            else
            {
                string token = File.ReadAllText(pathy);
                return int.Parse(token);
            }



        }

    }
}
