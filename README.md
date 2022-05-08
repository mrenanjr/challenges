# Desafio Stefanini!!!

Aplicação com os requisitos do desafio, web API .NET Core 6, Swagger funcionando e frontend em Angular.

Foi realizado a utilização de nginx pra poder rodar a aplicação juntamente com todas as suas depêndencias.

Executar o comando abaixo:

```
> docker-compose build --force-rm --no-cache && docker-compose up
```

Após subir todos os containers, acessar:

  * FrontEnd(Angular): http://localhost:80;
  * Swagger: http://localhost:5000/documentation;
