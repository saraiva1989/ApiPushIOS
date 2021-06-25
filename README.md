## PUSH IOS

Abaixo irei explicar como configurar o envio de push para IOS, explicando as etapas desde a loja até a chamada da API de envio.

## CONFIGURAÇÃO LOJA APPLE

O primeiro passo para enviar push é gerar uma KEY na loja da apple

![](https://lh5.googleusercontent.com/s9uBSF-vRlsJDB8xG7OSy34EoAj3PRQC2mzPAlOkLi8skC475jqoF3VTfyXN5r49VHmjmj6BmSO89KnNaaEFS3X6tFxeTsx4QJRuc8vOrBG-nxW0jT1zTfMDCsi1A3y1-noqp8C3)

Ao clicar em adicionar a key, preencha o nome da key, escolha a opção de push e clique em continuar. E na próxima tela clique em registrar.

![](https://lh6.googleusercontent.com/yEevGl9sGU_7o1rHHjfY10l56-zVyXvIk7ZJ_uzSMtnOv7CQ4Y3JdeVMM4rwfZIuQXbZu5EtwbuJz3V_fpaVg55QTk25vt5UsQw8Sw530MdRGObYshdoRZr21CbbSwg1hscHyQ0G)

Após a geração da key irá para essa tela, onde você terá a KEY ID e o botão download habilitado.

Observação: A Apple só permite fazer o download do certificado P8 uma única vez, então baixe e guarde em um lugar seguro. Se perder o certificado terá que gerar outro e reconfigurar.

![](https://lh3.googleusercontent.com/eop4xYUDSCfVSvJArz6dLvF497MgRe_BPovCLSoa-LWiFZczkv8qzw6yFHOGG0G5YNH7Zlej51Cv9XdkIawXk7650u1P-yiyYshOINuhlkwcSap8m7jciFt3o4-qleQ1yM80PCY2)

Para os próximos passos vou levar em consideração que já conheço a Apple Developer, sendo assim você já sabe o que é um Identifiers (ou package) e TEAMID.

Levarei em consideração também que o aplicativo já esteja pronto e gerando o TOKEN do APP.

## ENVIAR PUSH TESTE VIA API

Essa api foi feita em net core para possibilitar o envio de push para apple, já que se faz necessário usar o padrão HTTP2. Para testar o envio de push é muito simples. Abra o postman e faça os seguintes passos:

1.  Acesse o endpoint /api/PushIOS/enviar

2.  Informe o Token no Header (de acordo como fez em sua api)

3.  Informe no body o seguinte JSON

1.  TeamId - O id do seu time na apple developer

2.  AppPackage - O identifier (ou package) do seu aplicativo

3.  POitoKeyId - A key ID do sua key que gerou na etapa da loja

4.  DeviceToken - O token que o IPHONE gera ao iniciar o aplicativo

5.  ChaveArquivoPOito - O base64 do arquivo P8 que foi gerado

6.  PayloadPush - Os dados que deseja enviar via push, deve ser um json

Veja abaixo as imagens:

![](https://lh3.googleusercontent.com/PRUVRu_V6kkMqWiZF4nKJtPhZmR6bPACqi49vjU0iY5r7W3fobDqVeGPbzBol_vN04iaCM_SVZKEkXb_Lldcf6ROPMNmt9ppKBO83-ri5w04JRL9IIEZI7Jpx7cW9M9owrd3To6i)

![](https://lh6.googleusercontent.com/UikNcgFcbnaNabX5WhNKoqAGOoU463ZXBpqKftdhsjJEh9dRp9mqPTdFWlN2bpeDzFKseyf8thkp_Pg8gP7094QiUquaI3j-xjgP2iDPcvghJnkCk8JFrt77yCfE1BIVB2ELDl4Z)

Os valores foram ocultados pois é de um cliente real. Caso o push seja enviado com sucesso irá receber no dispositivo e a API retornará true.

Observação: Para pegar o base64 do certificado p8 é bem simples, basta editar no notepad. pegue a informação que está entre begin/end private key. Coloque tudo em uma mesma linha e use ele.

![](https://lh5.googleusercontent.com/Giq_850Gp-Z1n10SFCmlTXWBnHPVTvVR4gpNqCt2MFkoh3zzhO0dMQl5F_noHU89sP1xqW8nRnf-Zb63u-59ik7CQ303I0m8XBgSt_Zie6g7RNz2V0LhR8L2VyEK8rXMQS9mASDK)
