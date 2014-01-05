﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SelfControl
{
    public class Helpers
    {
        public static ObservableCollection<InventoryItem> lstInventory;
        public static ObservableCollection<InventoryItem> lstShopping;
        public static InventoryItem lastSelection = null;

        public static async void get(string uri, Action<string> action) {
            WebClient wc = new WebClient();
            string result = await Helpers.DownloadAsync(wc, uri);
            action(result);
        }

        public static Task<string> DownloadAsync(WebClient wc, string url) {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            DownloadStringCompletedEventHandler completed = null;

            completed = (s, e) => {
                try {
                    tcs.SetResult(e.Result);
                }
                catch (Exception ex) {
                    tcs.SetException(ex.InnerException);
                }
            };

            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; de; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12";
            wc.DownloadStringCompleted += completed;
            wc.DownloadStringAsync(new Uri(url));
            return tcs.Task;
        }

    }
}
