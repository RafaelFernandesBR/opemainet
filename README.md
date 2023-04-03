# opemainet
## Sobre
Aplicação de bot para telegram, que usa a api da opem AI para responder perguntas.
## Instalação e uso
Para executar essa aplicação tem duas formas. via docker, e localmente.
## via docker
Para executar via docker, basta clonar esse repositório, e executar os seguintes comandos.
            `docker build --build-arg tokem=[Seu tokem do telegram aqui] --build-arg opemAItokem=[Seu tokem opem AI aqui] -t tbotnetimg .`
`            docker run --name tbotnet -d tbotnetimg`
## localmente
Para executar a aplicação na sua máquina local, instale o visual stúdio e crie duas variáveis de ambientes no windows.
uma com o nome de opemAItokem com o tokem da opem AI, e outra com nome de tokem com o seu tokem do telegram.
Agora basta rodar a aplicação que vai executar normalmente.
