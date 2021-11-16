using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project.Source.Data;
using Project.Source.Extensions;
using Project.Source.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Source
{
    public class ItemPurchaser : MonoBehaviour
    {
        [SerializeField] private string itemApiUri;
        [SerializeField] private string orderApiUri;

        [Header("Item")]
        [SerializeField] private RawImage itemImage;
        [SerializeField] private TMP_Text itemTitle;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemPrice;

        [Header("Receipt")]
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField cardInput;
        [SerializeField] private TMP_InputField expirationInput;
        [SerializeField] private TMP_Text purchaseResponse;

        private CancellationTokenSource _cancellationTokenSource;


        private async void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await GetItem();
        }

        private async Task GetItem()
        {
            var content = JsonUtility.ToJson(new object());

            var item = await HttpClient.PostJsonAsAsync<Item>(new Uri(itemApiUri), content,
                _cancellationTokenSource.Token);
            var imageData = await HttpClient.DownloadImage(new Uri(item.ItemImage), _cancellationTokenSource.Token);
            var itemTexture = new Texture2D(0, 0);
            itemTexture.LoadImage(imageData);

            itemImage.texture = itemTexture;
            itemImage.SizeToParent();
            itemTitle.text = item.Title;
            itemName.text = item.ItemName;
            itemPrice.text = $"{item.Price}{item.CurrencySign} {item.Currency}";
        }

        public async void ExecuteOrderClick()
        {
            var details = new Order
            {
                Email = emailInput.text,
                CardNumber = cardInput.text,
                ExpirationDate = expirationInput.text
            };

            var content = JsonConvert.SerializeObject(details);
            var response = await HttpClient.PostJsonAsAsync<OrderResponse>(new Uri(orderApiUri), content,
                _cancellationTokenSource.Token);

            purchaseResponse.text = response.Message;
        }
    }
}