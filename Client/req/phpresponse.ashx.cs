using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bussiness.Managers;
using Bussiness;
using SqlDataProvider.Data;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace Tank.Request
{
    /// <summary>
    /// Summary description for phpresponse
    /// </summary>
    public class phpresponse : IHttpHandler
    {
        private string m_iv
        {
            get
            {
                return ConfigurationManager.AppSettings["m_iv"];
            }
        }
        private string m_key
        {
            get
            {
                return ConfigurationManager.AppSettings["m_key"];
            }
        }
        public void ProcessRequest(HttpContext context)
        {            
            string cipherstr = context.Request["phpkey"];
            try
            {
                if (string.IsNullOrEmpty(cipherstr))
                {
                    context.Response.Write("phpkey");
                }
                else
                {
                    string result = DecryptRJ256(m_key, m_iv, cipherstr);
                    string[] strArr = result.Split(';');
                    if (strArr.Length < 3)
                    {
                        context.Response.Write(5);
                        return;
                    }
                    string type = strArr[0];
                    string userinfo = strArr[1];
                    string[] list = strArr[2].Split('|');
                    if (string.IsNullOrEmpty(type))
                    {
                        context.Response.Write(3);
                    }
                    else if (string.IsNullOrEmpty(userinfo))
                    {
                        context.Response.Write(6);
                    }
                    else if (list.Length == 0)
                    {
                        context.Response.Write(5);
                    }
                    else if (list.Length > 50)
                    {
                        context.Response.Write(4);
                    }
                    else
                    {
                        switch (type)
                        {
                            case "senditembyusername":
                            case "senditembyid":
                            case "senditembynickname":
                                {
                                    using (PlayerBussiness pb = new PlayerBussiness())
                                    {
                                        PlayerInfo player = null;// pb.GetUserSingleByUserName(userinfo);
                                        switch (type)
                                        {
                                            case "senditembyusername":
                                                player = pb.GetUserSingleByUserName(userinfo);
                                                break;
                                            case "senditembyid":
                                                player = pb.GetUserSingleByUserID(int.Parse(userinfo));
                                                break;
                                            case "senditembynickname":
                                                player = pb.GetUserSingleByNickName(userinfo);
                                                break;
                                        }
                                        if (player == null)
                                        {
                                            switch (type)
                                            {                                                
                                                case "senditembyusername":
                                                    context.Response.Write(7);
                                                    break;
                                                case "senditembynickname":
                                                case "senditembyid":
                                                    context.Response.Write(6);
                                                    break;                                                
                                            }                                            
                                        }
                                        else
                                        {
                                            int error = 0;
                                            List<ItemInfo> temlistsend = new List<ItemInfo>();
                                            foreach (string itemStr in list)
                                            {
                                                string[] value = itemStr.Split(',');
                                                if (value.Length < 8)
                                                {
                                                    error++;
                                                    continue;
                                                }
                                                ItemTemplateInfo template = ItemMgr.FindItemTemplate(int.Parse(value[0]));
                                                if (template == null)
                                                {
                                                    error++;
                                                    continue;
                                                }
                                                ItemInfo item = ItemInfo.CreateFromTemplate(template, 1, 102);
                                                item.Count = int.Parse(value[1]);
                                                item.StrengthenLevel = int.Parse(value[2]);
                                                item.AttackCompose = int.Parse(value[3]);
                                                item.AgilityCompose = int.Parse(value[4]);
                                                item.LuckCompose = int.Parse(value[5]);
                                                item.DefendCompose = int.Parse(value[6]);
                                                item.ValidDate = int.Parse(value[7]);
                                                item.IsBinds = true;
                                                temlistsend.Add(item);
                                            }
                                            if (error > 0)
                                            {
                                                context.Response.Write(2);
                                            }
                                            else
                                            {
                                                bool sendcomplete = WorldEventMgr.SendItemsToMail(temlistsend, player.ID, player.NickName, "Thư từ hệ thống webshop");
                                                if (sendcomplete)
                                                {
                                                    context.Response.Write(0);
                                                }
                                                else
                                                {
                                                    context.Response.Write(1);
                                                }
                                            }

                                        }
                                    }
                                }
                                break;
                            default:
                                context.Response.Write(3);
                                break;
                        }

                    }
                    //context.Response.Write(list[1]);
                }
                
            }
            catch( Exception e)
            {
                context.Response.Write(e.Message);
                //context.Response.Write("</br>");
                //context.Response.Write(e.StackTrace);
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string DecryptRJ256(string prm_key, string prm_iv, string prm_text_to_decrypt)
        {
            var sEncryptedString = prm_text_to_decrypt;
            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };
            var key = Encoding.ASCII.GetBytes(prm_key);
            var IV = Encoding.ASCII.GetBytes(prm_iv);
            var decryptor = myRijndael.CreateDecryptor(key, IV);
            var sEncrypted = Convert.FromBase64String(sEncryptedString);
            var fromEncrypt = new byte[sEncrypted.Length];
            var msDecrypt = new MemoryStream(sEncrypted);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
            return (Encoding.ASCII.GetString(fromEncrypt));
        }

        public static string EncryptRJ256(string prm_key, string prm_iv, string prm_text_to_encrypt)
        {
            var sToEncrypt = prm_text_to_encrypt;
            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };
            var key = Encoding.ASCII.GetBytes(prm_key);
            var IV = Encoding.ASCII.GetBytes(prm_iv);
            var encryptor = myRijndael.CreateEncryptor(key, IV);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();
            var encrypted = msEncrypt.ToArray();
            return (Convert.ToBase64String(encrypted));
        }
    }
}