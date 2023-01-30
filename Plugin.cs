using MG.WeCode;
using MG.WeCode.WeClients;
using Newtonsoft.Json;
using Plugin;
using System.Text.RegularExpressions;

namespace DiceGamePlugin
{
    internal class Plugin : IPlugin
    {
        public string OriginId { get; set; }

        public string Name => "骰子插件";

        public string Version => "V0.0.1";

        public string Author => "Byboy";

        public string Description => "骰子插件";
        private Config config = new();

        public void Initialize()
        {
            if (File.Exists("Plugins/骰子插件.inf")) {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Plugins/骰子插件.inf"));
            } else {
                config = new();
                File.WriteAllText("Plugins/骰子插件.inf",JsonConvert.SerializeObject(config));
            }
            //订阅收到消息事件
            Events.GetReceiveMsg += Events_GetReceiveMsg;
        }
        private async Task Events_GetReceiveMsg(SuperWx.TLS_BFClent sender,List<WeChat.Pb.Entites.AddMsg> e)
        {
            var orid = sender.WX.UserLogin.OriginalId;
            foreach (var item in e) {
                if (!config.StartGroupUserName.Contains(item.FromUserName.String_t)) {
                    continue;
                }
                var regex = Regex.Match(item.Content.String_t,@"^(?<username>[\d\w-@]+):\n(?<content>.*?)$",RegexOptions.Singleline | RegexOptions.IgnoreCase);
                if (regex.Success) {
                    if (item.MsgType == 1) {
                        var txt = regex.Groups["content"].Value ?? "";
                        switch (txt) {
                            case "剪刀":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"514914788fc461e7205bf0b6ba496c49",10420,"<gameext type=\"1\" content=\"1\" ></gameext>");
                                break;
                            case "石头":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"f790e342a02e0f99d34b316547f9aeab",9253,"<gameext type=\"1\" content=\"2\" ></gameext>");
                                break;
                            case "布":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"091577322c40c05aa3dd701da29d6423",11765,"<gameext type=\"1\" content=\"3\" ></gameext>");
                                break;
                            case "骰子1":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"da1c289d4e363f3ce1ff36538903b92f",8521,"<gameext type=\"2\" content=\"4\" ></gameext>");
                                break;
                            case "骰子2":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"9e3f303561566dc9342a3ea41e6552a6",8178,"<gameext type=\"2\" content=\"5\" ></gameext>");
                                break;
                            case "骰子3":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"dbcc51db2765c1d0106290bae6326fc4",8123,"<gameext type=\"2\" content=\"6\" ></gameext>");
                                break;
                            case "骰子4":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"9a21c57defc4974ab5b7c842e3232671",8429,"<gameext type=\"2\" content=\"7\" ></gameext>");
                                break;
                            case "骰子5":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"3a8e16d650f7e66ba5516b2780512830",8413,"<gameext type=\"2\" content=\"8\" ></gameext>");
                                break;
                            case "骰子6":
                                _ = await WeClient.Messages.SendEmojiMsg(orid,item.FromUserName.String_t,"5ba8e9694b853df10b9f2a77b312cc09",8636,"<gameext type=\"2\" content=\"9\" ></gameext>");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public void Setting()
        {
        }

        public void Terminate()
        {
            //释放插件订阅的一切事件
            Events.GetReceiveMsg -= Events_GetReceiveMsg;
        }
    }
}
