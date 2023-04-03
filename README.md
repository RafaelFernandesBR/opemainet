# opemainet
## Sobre
Aplicação de bot para telegram, que usa a api da opem AI para responder perguntas.
## Instalação e uso
Para executar essa aplicação tem duas formas. via docker, e localmente.
## via docker
Para executar via docker, basta clonar esse repositório, e executar os seguintes comandos.
            `docker build --build-arg tokem=[Seu tokem do telegram aqui] --build-arg opemAItokem=[Seu tokem opem AI aqui] -t tbotnetimg .`
`            docker run --name tbotnet -d tbotnetimg`
