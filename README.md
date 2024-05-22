<img src="/public/Preview.png" alt="Application Preview" />

# TryBets

![Static Badge](https://img.shields.io/badge/Csharp-purple)
![Static Badge](https://img.shields.io/badge/.Net-4.2.0-green)
![Static Badge](https://img.shields.io/badge/ASP.NetMvc-6.0-blue)
![Static Badge](https://img.shields.io/badge/JwtBearer-6.0-white)
![Static Badge](https://img.shields.io/badge/EFCore-7.0.4-yellow)
![Static Badge](https://img.shields.io/badge/Swashbuckle-6.2.3-purple)
![Static Badge](https://img.shields.io/badge/SqlServer-7.0.4-blue)


## Descrição:
A Trybets é uma RestAPI, desenvolvida em ASP.NET com o objetivo de gerenciar uma casa de apostas, através de um CRUD em microsserviços, e armazenar todas informações em um banco de dados com segurança e validações necessarias. Durante o desenvolvimento foi utilizado as seguintes tecnologias: .NET Core, ASP.NET Core Mvc, JWT Authentication, C#, Entity Framework Core, xUnit, Microsoft SQL Server e Docker.
  
## Funcionalidades:
- Endpoints que serão conectados ao banco de dados seguindo princípios REST.
- Gerenciamento de times e partidas.
- Controle de usuários através de validação JWT.
- Gerenciamento de apostas, individuais por usuário.
- Controle dinâmico das chances de vitória (odd).

## Como acessar com Docker
  **:warning: Docker Compose `1.29`, Entity Framewok CLI `8.0.3` ou versões superiores**

  - Abra o terminal e faça um clone do repositório.
  ```bash
    git clone git@github.com:hiagoisoppo/trybets.git
  ```
 - Acesse a pasta clonada do repositório.
  ```bash
    cd trybets
  ```
  - Execute o conjunto de microsserviços.
  ```bash
    docker-compose up -d --build
  ```
  - Acesse a pasta do app em monolito e execute as migrações e seeder.
  ```bash
    cd src/TryBets
    dotnet ef migrations add Master
    dotnet ef database update
    dotnet ef migrations add Seeder
    dotnet ef database update
  ```
  - Agora vá em “Usando solicitações HTTP para testar a API”.

## Usando solicitações HTTP para testar a API
   - Acesse uma plataforma de sua preferência para fazer solicitações HTTP, como [ThunderClient](https://www.thunderclient.com/) ou [Insomnia](https://insomnia.rest/).
   - Importe o arquivo de solicitação HTTP válido para sua plataforma da pasta `requestCollection`.
   - Agora você pode testar esta API.

## ASP.NET RestAPI - Endpoints
### `GET` /team
Rota utilizada para obter a lista de times.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
    </tr>
    <tr>
        <td>(em branco)</td>
        <td>Não</td>
        <td>200</td>
        <td>
            <pre lang="json">
[
    {
        "teamId": 1,
        "teamName": "Sharks"
    }, /*...*/
]
            </pre>
        </td>
    </tr>
</table>

### `GET` /match/{finished}
Rota utilizada para obter a lista de partidas. Parâmetro {finished} varia entre `true` e `false` para listar partidas finalizadas ou não.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
    </tr>
    <tr>
        <td>(em branco)</td>
        <td>Não</td>
        <td>200</td>
        <td>
            <pre lang="json">
    [
        {
            "matchId": 1,
            "matchDate": "2023-07-23T15:00:00",
            "matchTeamAId": 1,
            "matchTeamBId": 8,
            "teamAName": "Sharks",
            "teamBName": "Bulls",
            "matchTeamAOdds": "3,33",
            "matchTeamBOdds": "1,43",
            "matchFinished": true,
            "matchWinnerId": 1
        }, /*...*/
    ]
            </pre>
        </td>
    </tr>
</table>


### `POST` /user/signup
Rota utilizada para cadastrar uma nova pessoa usuária. Ao cadastrar com sucesso, retorna um token. Não permitido adicionar duas pessoas usuárias com o mesmo e-mail.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
        <th>Observações</th>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "Name": "Isabel Santos",
   "Email": "isabel.santos@trybets.com",
   "Password": "123456"
}
            </pre>
        </td>
        <td>Não</td>
        <td>201</td>
        <td>
            <pre lang="json">
{
   "token": "eyJhbG..."
}
            </pre>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "Name": "Isabel Santos",
   "Email": "isabel.santos@trybets.com",
   "Password": "123456"
}
            </pre>
        </td>
        <td>Não</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
   "message": "E-mail already used"
}
            </pre>
        </td>
        <td>Caso o e-mail da pessoa usuária já tenha sido cadastrado no banco de dados.</td>
    </tr>
</table>

### `POST` /user/login
Rota utilizada para realizar o login de uma pessoa usuária.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
        <th>Observações</th>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "Email": "isabel.santos@trybets.com",
   "Password": "123456"
}
            </pre>
        </td>
        <td>Não</td>
        <td>200</td>
        <td>
            <pre lang="json">
{
   "token": "eyJhbG..."
}
            </pre>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "Email": "isabel.santos@trybets.com",
   "Password": "1234567"
}
            </pre>
        </td>
        <td>Não</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
   "message": "Authentication failed"
}
            </pre>
        </td>
        <td>Caso a pessoa usuária não tenha os dados autenticados ou não informe algum dos parâmetros corretamente.</td>
    </tr>
</table>

### `POST` /bet
Rota utilizada para realizar uma nova aposta
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
        <th>Observações</th>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 5,
   "TeamId":  2,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Sim</td>
        <td>201</td>
        <td>
            <pre lang="json">
{
   "betId": 1,
   "matchId": 5,
   "teamId": 2,
   "betValue": 550.65,
   "matchDate": "2024-03-15T14:00:00",
   "teamName": "Eagles",
   "email": "isabel.santos@trybets.com"
}
            </pre>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 5,
   "TeamId":  2,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Não</td>
        <td>401</td>
        <td>
        </td>
        <td>Caso o token não tenha sido informado ou esteja errado</td>
    </tr>
    <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 5,
   "TeamId":  6,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
    "message": "Team is not in this match"
}
            </pre>
        </td>
        <td>Caso o time não esteja na partida correta</td>
    </tr>
     <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 5,
   "TeamId":  60,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
    "message": "Team not founded"
}
            </pre>
        </td>
        <td>Caso o time não exista</td>
    </tr>
     <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 50,
   "TeamId":  6,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
    "message": "Match not founded"
}
            </pre>
        </td>
        <td>Caso a partida não exista</td>
    </tr>
     <tr>
        <td>
            <pre lang="json">
{
   "MatchId": 1,
   "TeamId":  6,
   "BetValue": 550.65
}
            </pre>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
            <pre lang="json">
{
    "message": "Match finished"
}
            </pre>
        </td>
        <td>Caso a partida já tenha sido finalizada</td>
    </tr>
</table>

### `GET` /bet/{BetId}
Rota utilizada para visualizar uma aposta criada. Uma aposta só pode ser visualizada pela pessoa que a criou.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
        <th>Observações</th>
    </tr>
    <tr>
        <td>
        </td>
        <td>Sim</td>
        <td>200</td>
        <td>
            <pre lang="json">
{
   "betId": 1,
   "matchId": 5,
   "teamId": 2,
   "betValue": 550.65,
   "matchDate": "2024-03-15T14:00:00",
   "teamName": "Eagles",
   "email": "isabel.santos@trybets.com"
}
            </pre>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
            (Indiferente)
        </td>
        <td>Caso a aposta não pertencer à pessoa usuária do token.</td>
    </tr>
    <tr>
        <td>
        </td>
        <td>Sim</td>
        <td>400</td>
        <td>
             <pre lang="json">
{
   "message": "Bet not founded"
}
            </pre>
        </td>
        <td>Caso a aposta não exista.</td>
    </tr>
    <tr>
        <td>
        </td>
        <td>Não</td>
        <td>401</td>
        <td>
        </td>
        <td>Caso não seja informado um token.</td>
    </tr>
</table>

### `PATCH` /odds/{matchId}/{teamId}/{BetValue}
Rota utilizada para atualizar o valor apostado em um time e em uma partida. O retorno da requisição seguirá a model `Match` sem necessitar realizar nenhuma operação de tratamento de dados, já que este microsserviço só será acessado pelo microsserviço `TryBets.Bet`.
<table>
    <tr>
        <th>Request</th>
        <th>Token?</th>
        <th>Status</th>
        <th>Response</th>
        <th>Observações</th>
    </tr>
    <tr>
        <td>(em branco)</td>
        <td>Não</td>
        <td>200</td>
        <td>
            <pre lang="json">
{
	"matchId": 1,
	"matchDate": "2024-03-17T14:00:00",
	"matchTeamAId": 5,
	"matchTeamBId": 6,
	"matchTeamAValue": 300.00,
	"matchTeamBValue": 1501.50,
	"matchFinished": false,
	"matchWinnerId": null,
	"matchTeamA": null,
	"matchTeamB": null,
	"bets": null
}
            </pre>
        </td>
        <td></td>
    </tr>
     <tr>
        <td>(em branco)</td>
        <td>Não</td>
        <td>400</td>
        <td>
            (indiferente)
        </td>
        <td>Qualquer tipo de erro que impeça a atualização</td>
    </tr>
</table>