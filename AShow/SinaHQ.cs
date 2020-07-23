using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace AShow
{
    /// <summary>
    /// 方法1：HTTP://HQ.SINAJS.CN/LIST=[股票代码]
    /// 返回结果：JSON实时数据，以逗号隔开相关数据，
    /// 数据依次是“股票名称、今日开盘价、昨日收盘价、当前价格、今日最高价、今日最低价、竞买价、竞卖价、成交股数、
    /// 成交金额、买1手、买1报价、买2手、买2报价、…、买5报价、…、卖5报价、日期、时间”。
    /// 获取当前的股票行情，如http://hq.sinajs.cn/list=sh601006，注意新浪区分沪深是以sh和sz区分。
    /// </summary>
    public class SinaHQ
    {
        private protected static string URL = "http://hq.sinajs.cn/list=";//sh601006
        public static SinaHQData GetRealTimeData(string code)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(URL + code).Result.Content.ReadAsStringAsync().Result;
            var regex = new Regex("\".*?\"", RegexOptions.Multiline | RegexOptions.Singleline);
            var data = regex.Match(response).Value.Replace("\"", "");
            if (data.Contains(","))
            {
                try
                {
                    var sinaHQDataArr = data.Split(',');
                    var sinaHQData = new SinaHQData();
                    sinaHQData.Name = sinaHQDataArr[0];
                    sinaHQData.ToDayOpeningPrice = sinaHQDataArr[1];
                    sinaHQData.YesterdayClosingPrice = sinaHQDataArr[2];
                    sinaHQData.CurrentPrice = sinaHQDataArr[3];
                    sinaHQData.TodayTopProce = sinaHQDataArr[4];
                    sinaHQData.TodayLowProce = sinaHQDataArr[5];
                    sinaHQData.BidPrice = sinaHQDataArr[6];
                    sinaHQData.AuctionPrice = sinaHQDataArr[7];
                    sinaHQData.NumberOfSharesTraded = sinaHQDataArr[8];
                    sinaHQData.TransactionAmount = sinaHQDataArr[9];
                    sinaHQData.BuySalePrice = new List<BuySalePrice> {
                    new BuySalePrice {
                        Buy = sinaHQDataArr[11],
                        Sale = sinaHQDataArr[13]
                    },
                    new BuySalePrice {
                        Buy = sinaHQDataArr[15],
                        Sale = sinaHQDataArr[17]
                    },
                    new BuySalePrice {
                        Buy = sinaHQDataArr[19],
                        Sale = sinaHQDataArr[21]
                    },
                    new BuySalePrice {
                        Buy = sinaHQDataArr[23],
                        Sale = sinaHQDataArr[25]
                    },
                    new BuySalePrice {
                        Buy = sinaHQDataArr[27],
                        Sale = sinaHQDataArr[29]
                    },
                };
                    sinaHQData.Date = sinaHQDataArr[30];
                    sinaHQData.Time = sinaHQDataArr[31];
                    return sinaHQData;
                }
                catch
                {

                }
            }
            return null;
        }
    }
    public class SinaHQData
    {
        /// <summary>
        /// 股票名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 今日开盘价
        /// </summary>
        public string ToDayOpeningPrice { get; set; }
        /// <summary>
        /// 昨日收盘价
        /// </summary>
        public string YesterdayClosingPrice { get; set; }
        /// <summary>
        /// 当前价格
        /// </summary>
        public string CurrentPrice { get; set; }
        /// <summary>
        /// 今日最高价
        /// </summary>
        public string TodayTopProce { get; set; }
        /// <summary>
        /// 今日最低价
        /// </summary>
        public string TodayLowProce { get; set; }
        /// <summary>
        /// 竞买价
        /// </summary>
        public string BidPrice { get; set; }

        /// <summary>
        /// 竞卖价
        /// </summary>
        public string AuctionPrice { get; set; }

        /// <summary>
        /// 成交股数
        /// </summary>
        public string NumberOfSharesTraded { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        public string TransactionAmount { get; set; }

        /// <summary>
        /// 买卖价格
        /// </summary>
        public List<BuySalePrice> BuySalePrice { get; set; } = new List<BuySalePrice>();

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }
    public class BuySalePrice
    {
        public string Buy { get; set; }
        public string Sale { get; set; }
    }
}
