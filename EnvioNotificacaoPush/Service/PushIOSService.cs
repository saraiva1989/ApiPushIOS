using EnvioNotificacaoPush.Helper;
using EnvioNotificacaoPush.Model;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EnvioNotificacaoPush.Service
{

    public class PushIOSService
    {
        public bool EnviarPush(PushIOSModel pushIOSModel)
        {
            bool enviado = true;

            try
            {
                string prk = pushIOSModel.ChaveArquivoPOito;
                //gera a KEY para enviar a apple
                ECDsaCng key = new ECDsaCng(CngKey.Import(Convert.FromBase64String(prk), CngKeyBlobFormat.Pkcs8PrivateBlob));

                //Gera o token JWT para envio
                string token = CreateTokenJWT(key, pushIOSModel.POitoKeyId, pushIOSModel.TeamId);

                string url = string.Format(VariavelDeAmbiente.URLAPIAPPLE, pushIOSModel.DeviceToken);
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                httpRequestMessage.Headers.TryAddWithoutValidation("apns-push-type", "alert"); // or background
                httpRequestMessage.Headers.TryAddWithoutValidation("apns-id", Guid.NewGuid().ToString("D"));
                httpRequestMessage.Headers.TryAddWithoutValidation("apns-expiration", Convert.ToString(0));
                //Send imediately
                httpRequestMessage.Headers.TryAddWithoutValidation("apns-priority", Convert.ToString(10));
                //App Bundle
                httpRequestMessage.Headers.TryAddWithoutValidation("apns-topic", pushIOSModel.AppPackage);
                //Category
                httpRequestMessage.Headers.TryAddWithoutValidation("apns-collapse-id", "Condominio");

                var body = pushIOSModel.PayloadPush;

                using (var stringContent = new StringContent(body, Encoding.UTF8, "application/json"))
                {
                    //Set Body
                    httpRequestMessage.Content = stringContent;

                    Http2CustomHandler handler = new Http2CustomHandler();

                    handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls;


                    using (HttpClient client = new HttpClient(handler))
                    {
                        HttpResponseMessage resp = client.SendAsync(httpRequestMessage).Result;

                        if (resp != null)
                        {
                            string apnsResponseString = resp.Content.ReadAsStringAsync().Result;
                            handler.Dispose();
                        }

                        handler.Dispose();
                    }
                }
                enviado = true;
            }
            catch (Exception ex)
            {
                enviado = false;
            }
            return enviado;
        }

        public static string CreateTokenJWT(ECDsa key, string keyID, string teamID)
        {
            var securityKey = new ECDsaSecurityKey(key) { KeyId = keyID };
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.Now,
                Issuer = teamID,
                SigningCredentials = credentials
            };

            var handler = new JwtSecurityTokenHandler();
            var encodedToken = handler.CreateEncodedJwt(descriptor);
            return encodedToken;
        }
    }
}
