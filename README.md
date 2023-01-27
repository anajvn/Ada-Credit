# AdaCredit
Solução do projeto final do módulo 1 - Code@cs

Você foi contratado para construção de um sistema de controle para uma pequena cooperativa digital de crédito local, a Ada Credit (Código Bancário 777).

Para garantir a segurança das informações, o sistema deve exigir login e senha para que seja possível a sua operação.
O usuário e senha "iniciais" (na primeira execução do programa) devem ser *"user"* e *"pass". *Essa senha padrão deve ser trocada no primeiro login**.

Quando o login for bem sucedido, o sistema exibe um menu com opções para:

### Clientes
- Cadastrar Novo Cliente
- Consultar os Dados de um Cliente existente
- Alterar o Cadastro de um Cliente existente
- Desativar Cadastro de um Cliente existente
### Funcionários
- Cadastrar Novo Funcionário
- Alterar Senha de um Funcionário existente
- Desativar Cadastro de um Funcionário existente
### Transações
- Processar Transações (Reconciliação Bancária)
### Relatórios
- Exibir Todos os Clientes Ativos com seus Respectivos Saldos
- Exibir Todos os Clientes Inativos
- Exibir Todos os Funcionários Ativos e sua Última Data e Hora de Login
- Exibir Transações com Erro (Detalhes da transação e do Erro)

Ao ser cadastrado, o cliente recebe um número de conta de 5 dígitos e um dígito verificador, ambos aleatórios, formando o padrão XXXXX-X.
Por ser uma cooperativa digital, todos os clientes possuem o mesmo número de agência, que é 0001.

As senhas devem ser armazenadas de forma segura. Para isso, nosso cliente solicitou a utilização do mecanismos de segurança BCRYPT com salto (veja Anexo C) para criptografia da senha (Veja este exemplo. Entretanto, use o salto em vez do WorkFactor).

A Ada Credit recebe, diariamente, do "Sistema Nacional de Pagamentos Integrado" múltiplos arquivos representando transações bancárias que envolvam seus clientes. Essas transações podem ser de Entrada (Crédito) ou Saída (Débito). Os arquivos possuem o padrão de nomenclatura "nome-do-banco-parceiro-aaaammdd.csv", em que aaaa, mm e dd representam, respectivamente, o ano com quatro dígitos, o mês com dois dígitos e o dia com dois dígitos da data em que o arquivo foi gerado.

Quando o usuário selecionar a opção "Processar Transações" no menu principal, o sistema buscará pelos arquivos de transação que ficam na pasta "Desktop/Transactions" (ou seu equivalente "~/home/Transactions/Pending" em sistemas *nix) e os processará, respeitando a tabela de tarifas em vigor. Verifique os detalhes sobre o layout do arquivo de transações no Anexo A ao final do enunciado, bem como As Tabelas de Tarifas no Anexo B.

É importante mantermos o registro das transações que não puderam ser processadas que falharam. Essas falhas podem acontecer, por exemplo, por insuficiência de saldo, número da conta inválido ou inexistente, tipo de transação incompatível (no caso de TEFs), etc. Nesses casos, o registro da transação deve ser movido para um arquivo cujo padrão de nomenclatura é "nome-do-banco-parceiro-aaaammdd-failed.csv" que deve ser armazenado na pasta "~/home/Transactions/Failed".

Caso a transação tenha sido processada com sucesso, o registro da transação deve ser movido para um arquivo cujo padrão de nomenclatura é "nome-do-banco-parceiro-aaaammdd-completed.csv" e que deve ser armazenado na pasta "~/home/Transactions/Completed". É importante que o saldo do cliente tenha sido atualizado de forma correta, inclusive com as cobranças das devidas taxas.

## **ANEXO A - Layoute do Arquivo de Transações**

Cada linha no arquivo de transações é composta pelas seguintes informações

**AAA,BBBB,CCCCCC,DDD,EEEE,FFFFFF,GGG,H,I**

Sendo que:

**AAA** Número com 3 dígitos representando o Código do Banco de Origem

**BBBB** Número com 4 dígitos representando a Agência do Banco de Origem

**CCCCCC** Número com 6 dígitos representando o número da conta do Banco de Origem


**DDD** Número com 3 dígitos representando o Código do Banco de Destino

**EEEE** Número com 4 dígitos representando a Agência do Banco de Destino

**FFFFFF** Número com 6 dígitos representando o número da conta do Banco de Destino


**GGG** Tipo da Transação (DOC, TED, TEF).


**H** Número representando o sentido da transação (0 - Débito/Saída, 1 - Crédito/Entrada)


**I** número real com duas casas decimais, separadas por um . e sem separador de milhar


*Obs: TEFs só podem ser realizadas entre clientes do mesmo banco.*

## **ANEXO B - Tabelas de Tarifas**

Transações a Crédito

Todas isentas de Tarifas



Transações a Débito realizadas/recebidas até 30/11/2022

Todas isentas de Tarifas



Transações a Débito realizadas/recebidas a partir de 01/12/2022

TED - Tarifa Única de R$5,00

DOC - Tarifa de R$1,00 + (1% da Transação limitado a R$5,00)

TEF - Isenta


## **ANEXO C - Salt (Salto)**

Um salt é, basicamente, uma cadeia de caracteres aleatória que é concatenada ao começo ou ao final da senha fornecida pelo usuário antes de aplicarmos a função de Criptografia/Hash. O uso do salt permite que o hash gerado seja completamente diferente, mesmo que duas ou mais senhas sejam idênticas. Uma vez que cada uma delas tem seu próprio salto, os hashs serão diferentes.


Para que esse mecanismo funcione, além de armazenar o hash da senha do usuário, precisamos também armazenar o Salt, para que, no momento do login, possamos fazer a concatenação do hash daquele usuário específico com a senha fornecida no login a fim de comparar o resultado com o hash armazenado no "banco de dados".
