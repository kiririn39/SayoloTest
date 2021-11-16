using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Project.Source.Data;
using Project.Source.Networking;
using UnityEngine;
using UnityEngine.Video;

namespace Project.Source
{
    public class AdvertisementVideoDisplayer : MonoBehaviour
    {
        [SerializeField] private string apiUrl;
        [SerializeField] private VideoPlayer videoPlayer;
        private CancellationTokenSource _cancellationTokenSource;


        private void Awake() => ResetCancelationToken();

        private async void Start()
        {
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = await LoadVideoAd();
            videoPlayer.Play();
        }

        private async Task<string> LoadVideoAd()
        {
            ResetCancelationToken();
            var vastResponse = await HttpClient.GetXmlAsAsync<Vast>(new Uri(apiUrl), _cancellationTokenSource.Token);
            var uri = new Uri(vastResponse.Ad.InLine.Creatives.First().Linear.MediaFiles.First());
            var filename = Path.GetFileName(uri.ToString());
            var filePath = new VideoCacheFactory().GetVideoAdPathForFile(filename);

            await HttpClient.GetVideoAsync(uri, filePath, _cancellationTokenSource.Token);

            return filePath;
        }

        private void ResetCancelationToken()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}