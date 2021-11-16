using System.IO;
using UnityEngine;

namespace Project.Source
{
    public class VideoCacheFactory
    {
        public string GetVideoAdPathForFile(string fileName) =>
            Path.Combine(Application.persistentDataPath, fileName);
    }
}